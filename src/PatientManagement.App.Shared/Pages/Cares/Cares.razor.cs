using Microsoft.AspNetCore.Components;
using MudBlazor;
using PatientManagement.App.Domain.Dtos;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Shared.Pages.Cares;

public partial class Cares
{
    [Inject] ICareApiClient CareApiClient { get; set; } = default!;
    [Inject] IPatientApiClient PatientApiClient { get; set; } = default!;
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    private bool dense = false;
    private bool hover = true;
    private bool striped = false;
    private bool bordered = false;
    private string _searchString = string.Empty;
    private bool _isEdit = false;

    private CareDto _editCare = new();
    private CareDto? selectedCare = null;
    private List<CareDto> _cares = new();
    private List<PatientDto> _patients = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadCaresAsync();
        await LoadPatientsAsync();
    }

    private async Task LoadCaresAsync()
    {
        var result = await CareApiClient.SearchAsync(); // O search de Care não tem filtro no backend ainda
        if (result.Data != null)
        {
            _cares = result.Data.ToList();
        }
        else
        {
            Snackbar.Add(result.Error ?? "Erro ao carregar atendimentos.", Severity.Error);
        }
    }
    private async Task LoadPatientsAsync()
    {
        var result = await PatientApiClient.SearchAsync();
        if (result.Data != null)
        {
            _patients = result.Data.ToList();
        }
        else
        {
            Snackbar.Add(result.Error ?? "Erro ao carregar atendimentos.", Severity.Error);
        }
    }

    private bool FilterFunc1(CareDto care) => FilterFunc(care, _searchString);

    private bool FilterFunc(CareDto care, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;

        var patientName = GetPatientName(care.PatientId);
        if (patientName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (care.Status.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private string GetPatientName(string patientId) => _patients.FirstOrDefault(p => p.Id == patientId)?.Name ?? "N/A";
    private async Task OpenDialogAsync()
    {
        _editCare = new CareDto();
        _isEdit = false;
        var parameters = new DialogParameters<CareDetails>
        {
            { p => p.Model, _editCare },
            { p => p.IsEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<CareDetails>("Novo Atendimento", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var newCare = (CareDto)result.Data;
            newCare.Id = Guid.NewGuid().ToString().Substring(0, 8);
            var createResult = await CareApiClient.CreateAsync(newCare);
            if (createResult.Success)
            {
                await LoadCaresAsync();
                StateHasChanged();
            }
        }
    }

    private async Task OpenEditDialogAsync(CareDto care)
    {
        _editCare = care;
        _isEdit = true;

        var parameters = new DialogParameters<CareDetails>
        {
            { p => p.Model, _editCare },
            { p => p.IsEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<CareDetails>($"Editar Atendimento", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var updatedCare = (CareDto)result.Data;
            await CareApiClient.UpdateAsync(updatedCare);
            await LoadCaresAsync();
            StateHasChanged();
        }
    }

    private async Task DeleteCareAsync(CareDto care)
    {
        var parameters = new DialogParameters<DialogDefault>
        {
            { p => p.Title, "Excluir Atendimento" },
            { p => p.Message, $"Tem certeza que deseja excluir o atendimento {care.SequenceNumber} do paciente {GetPatientName(care.PatientId)}?" }
        };

        var dialog = await DialogService.ShowAsync<DialogDefault>("Confirmar Exclusão", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await CareApiClient.DeleteAsync(care.Id);
            await LoadCaresAsync();
            StateHasChanged();
        }
    }
}
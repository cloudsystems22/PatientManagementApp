using Microsoft.AspNetCore.Components;
using MudBlazor;
using PatientManagement.App.Domain.Dtos;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Shared.Pages.Triages;

public partial class Triages
{
    [Inject] ITriageApiClient TriageApiClient { get; set; } = default!;
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    private bool dense = false;
    private bool hover = true;
    private bool striped = false;
    private bool bordered = false;
    private string _searchString = string.Empty;
    private bool _isEdit = false;

    private TriageDto _editTriage = new();
    private TriageDto? selectedTriage = null;
    private List<TriageDto> triages = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadTriagesAsync();
    }

    private async Task LoadTriagesAsync()
    {
        var result = await TriageApiClient.SearchAsync(_searchString);
        if (string.IsNullOrWhiteSpace(result.Error))
        {
            triages = result.Data!.ToList();
        }
        else
        {
            Snackbar.Add(result.Error ?? "Erro ao carregar triagens.", Severity.Error);
        }
    }

    private bool FilterFunc1(TriageDto triage) => FilterFunc(triage, _searchString);

    private bool FilterFunc(TriageDto triage, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (triage.CareId.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (triage.Symptoms.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private async Task OpenDialogAsync()
    {
        _editTriage = new TriageDto();
        _isEdit = false;
        var parameters = new DialogParameters<TriageDetails>
        {
            { p => p.Model, _editTriage },
            { p => p.IsEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<TriageDetails>("Nova Triagem", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var newTriage = (TriageDto)result.Data;
            newTriage.Id = Guid.NewGuid().ToString().Substring(0, 8);
            var createResult = await TriageApiClient.CreateAsync(newTriage);
            if (createResult.Success)
            {
                await LoadTriagesAsync();
            }
        }
    }

    private async Task OpenEditDialogAsync(TriageDto triage)
    {
        _editTriage = triage;
        _isEdit = true;

        var parameters = new DialogParameters<TriageDetails>
        {
            { p => p.Model, _editTriage },
            { p => p.IsEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<TriageDetails>($"Editar Triagem", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var updatedTriage = (TriageDto)result.Data;
            await TriageApiClient.UpdateAsync(updatedTriage);
            await LoadTriagesAsync();
        }
    }

    private async Task DeleteTriageAsync(TriageDto triage)
    {
        var parameters = new DialogParameters<DialogDefault>
        {
            { p => p.Title, "Excluir Triagem" },
            { p => p.Message, $"Tem certeza que deseja excluir a triagem do atendimento {triage.CareId}?" }
        };

        var dialog = await DialogService.ShowAsync<DialogDefault>("Confirmar Exclusão", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await TriageApiClient.DeleteAsync(triage.Id);
            await LoadTriagesAsync();
        }
    }
}
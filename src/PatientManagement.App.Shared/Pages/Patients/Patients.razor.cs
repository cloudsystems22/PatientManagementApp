using Microsoft.AspNetCore.Components;
using MudBlazor;
using PatientManagement.App.Domain.Dtos;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Shared.Pages.Patients;

public partial class Patients
{
    [Inject] IPatientApiClient PatientApiClient { get; set; } = default!;
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    private bool dense = false;
    private bool hover = true;
    private bool striped = false;
    private bool bordered = false;
    private string _searchString = string.Empty;
    private bool _isEdit = false;

    private PatientDto _editPatient = new();
    private PatientDto? selectedPatient = null;
    private List<PatientDto> patients = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadPatientsAsync();
    }

    private async Task LoadPatientsAsync()
    {
        var result = await PatientApiClient.SearchAsync(_searchString);
        if (string.IsNullOrEmpty(result.Error))
        {
            patients = result.Data!.ToList();
        }
        else
        {
            Snackbar.Add(result.Error ?? "Erro ao carregar pacientes.", Severity.Error);
        }
    }

    private bool FilterFunc1(PatientDto patient) => FilterFunc(patient, _searchString);

    private bool FilterFunc(PatientDto patient, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (patient.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (patient.EmailAddress.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (patient.Phone.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private async Task OpenDialogAsync()
    {
        _editPatient = new PatientDto();
        _isEdit = false;
        var parameters = new DialogParameters<PatientDetails>
        {
            { p => p.Model, _editPatient },
            { p => p.IsEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<PatientDetails>("Novo Paciente", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var newPatient = (PatientDto)result.Data;
            newPatient.Id = Guid.NewGuid().ToString().Substring(0, 8);
            var createResult = await PatientApiClient.CreateAsync(newPatient);
            if (createResult.Success)
            {
                await LoadPatientsAsync();
            }
        }
    }

    private async Task OpenEditDialogAsync(PatientDto patient)
    {
        _editPatient = patient;
        _isEdit = true;

        var parameters = new DialogParameters<PatientDetails>
        {
            { p => p.Model, _editPatient },
            { p => p.IsEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<PatientDetails>($"Editar {patient.Name}", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var updatedPatient = (PatientDto)result.Data;
            await PatientApiClient.UpdateAsync(updatedPatient);
            await LoadPatientsAsync();
        }
    }

    private async Task DeletePatientAsync(PatientDto patient)
    {
        var parameters = new DialogParameters<DialogDefault>
        {
            { p => p.Title, "Excluir Paciente" },
            { p => p.Message, $"Tem certeza que deseja excluir {patient.Name}?" }
        };

        var dialog = await DialogService.ShowAsync<DialogDefault>("Confirmar Exclusão", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await PatientApiClient.DeleteAsync(patient.Id);
            await LoadPatientsAsync();
        }
    }
}
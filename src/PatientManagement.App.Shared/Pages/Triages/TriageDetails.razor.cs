using Microsoft.AspNetCore.Components;
using MudBlazor;
using PatientManagement.App.Domain.Dtos;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Shared.Pages.Triages;

public partial class TriageDetails
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    [Inject] ICareApiClient CareApiClient { get; set; } = default!;
    [Inject] IPatientApiClient PatientApiClient { get; set; } = default!;
    [Inject] ISpecialityApiClient SpecialityApiClient { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    [Parameter] public TriageDto Model { get; set; } = new();
    [Parameter] public bool IsEdit { get; set; }

    private IEnumerable<CareDto> _cares = new List<CareDto>();
    private IEnumerable<PatientDto> _patients = new List<PatientDto>();
    private IEnumerable<SpecialityDto> _specialities = new List<SpecialityDto>();

    private bool success;
    private MudForm? form;

    protected override async Task OnInitializedAsync()
    {
        await LoadCaresAsync();
        await LoadPatientsAsync();
        await LoadSpecialitiesAsync();
    }

    private async Task LoadCaresAsync()
    {
        var result = await CareApiClient.SearchAsync();
        if (string.IsNullOrEmpty(result.Error)) _cares = result.Data ?? new List<CareDto>();
    }

    private async Task LoadPatientsAsync()
    {
        var result = await PatientApiClient.SearchAsync();
        if (string.IsNullOrEmpty(result.Error)) _patients = result.Data ?? new List<PatientDto>();
    }

    private async Task LoadSpecialitiesAsync()
    {
        var result = await SpecialityApiClient.SearchAsync();
        if (string.IsNullOrEmpty(result.Error)) _specialities = result.Data ?? new List<SpecialityDto>();
    }

    private string GetPatientName(string patientId) => _patients.FirstOrDefault(p => p.Id == patientId)?.Name ?? "Paciente não encontrado";

    private void Submit()
    {
        string successMsg = IsEdit ? "editada com sucesso!" : "adicionada com sucesso!";
        Snackbar.Add($"Triagem {successMsg}", Severity.Success);
        MudDialog.Close(DialogResult.Ok(Model));
    }

    private void Cancel() => MudDialog.Cancel();

    private void OnImcDataChanged()
    {
        StateHasChanged();
    }
}

using Microsoft.AspNetCore.Components;
using MudBlazor;
using PatientManagement.App.Domain.Dtos;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Shared.Pages.Cares;

public partial class CareDetails
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;
    [Inject] IPatientApiClient PatientApiClient { get; set; } = default!;

    [Parameter] public CareDto Model { get; set; } = new();
    [Parameter] public bool IsEdit { get; set; }

    private bool success;
    private MudForm? form;
    private PatientDto? _selectedPatient;
    private List<PatientDto> _patients = new();

    public PatientDto? SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            _selectedPatient = value;
            if (value != null)
            {
                Model.PatientId = value.Id;
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await PatientApiClient.SearchAsync();
        if (result.Data != null)
        {
            _patients = result.Data.ToList();
        }

        if (IsEdit && !string.IsNullOrEmpty(Model.PatientId))
        {
            SelectedPatient = _patients.FirstOrDefault(p => p.Id == Model.PatientId);
        }

        if (!IsEdit)
        {
            Model.SequenceNumber = GenerateSequenceNumber(4);
        }
    }

    private async Task<IEnumerable<PatientDto>> SearchPatient(string value, CancellationToken token)
    {
        // A pesquisa agora é feita em memória para uma melhor experiência do usuário.
        await Task.Delay(1, token); // Permite que o cancelamento seja observado
        if (string.IsNullOrEmpty(value))
            return _patients;
        return _patients.Where(p => p.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private void Submit()
    {
        string successMsg = IsEdit ? "editado com sucesso!" : "adicionado com sucesso!";
        Snackbar.Add($"Atendimento {successMsg}", Severity.Success);
        MudDialog.Close(DialogResult.Ok(Model));
    }

    private void Cancel() => MudDialog.Cancel();

    private string GenerateSequenceNumber(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
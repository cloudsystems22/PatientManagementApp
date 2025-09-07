using Microsoft.AspNetCore.Components;
using MudBlazor;
using PatientManagement.App.Domain.Dtos;

namespace PatientManagement.App.Shared.Pages.Patients;

public partial class PatientDetails
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    [Parameter] public PatientDto Model { get; set; } = new();
    [Parameter] public bool IsEdit { get; set; }

    private bool success;
    private MudForm? form;

    private void Submit()
    {
        string successMsg = IsEdit ? "editado com sucesso!" : "adicionado com sucesso!";
        Snackbar.Add($"{Model.Name} {successMsg}", Severity.Success);
        MudDialog.Close(DialogResult.Ok(Model));
    }

    private void Cancel() => MudDialog.Cancel();
}
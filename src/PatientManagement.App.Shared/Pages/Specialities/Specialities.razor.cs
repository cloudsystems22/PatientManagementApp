using Microsoft.AspNetCore.Components;
using PatientManagement.App.Domain.Interfaces;
using PatientManagement.App.Domain.Dtos;
using MudBlazor;


namespace PatientManagement.App.Shared.Pages.Specialities;

public partial class Specialities
{
    [Inject] ISpecialityApiClient SpecialityApiClient { get; set; }
    [Inject] IDialogService DialogService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    private bool dense = false;
    private bool hover = true;
    private bool striped = false;
    private bool bordered = false;
    private string _searchString = string.Empty;
    private bool _isEdit = false;

    private SpecialityDto _editSpeciality = new();
    private SpecialityDto? selectedSpeciality = null;
    private List<SpecialityDto> specialities = new();

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(500);
        var result = await SpecialityApiClient.SearchAsync(_searchString);
        specialities = result.Data!.ToList();
    }

    private bool FilterFunc1(SpecialityDto speciality) => FilterFunc(speciality, _searchString);
    private bool FilterFunc(SpecialityDto speciality, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (speciality.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    }

    private async Task OpenDialogAsync()
    {
        _editSpeciality = new SpecialityDto(); // novo produto
        _isEdit = false;
        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
        var parameters = new DialogParameters<SpecialityDetails>
        {
           { p => p.Model, _editSpeciality },
           { p => p._isEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<SpecialityDetails>("Nova Especialidade", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var newSpeciality = (SpecialityDto)result.Data;
            newSpeciality.Id = Guid.NewGuid().ToString().Substring(0, 6);
            var resultSpeciality = await SpecialityApiClient.CreateAsync(newSpeciality);
            var resultSp = await SpecialityApiClient.SearchAsync(_searchString);
            specialities = resultSp.Data!.ToList();
        }
        StateHasChanged();
    }
    private async Task OpenEditDialogAsync(SpecialityDto speciality)
    {
        _editSpeciality = new SpecialityDto
        {
            Id = speciality.Id,
            Name = speciality.Name
        };

        _isEdit = true;

        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true };
        var parameters = new DialogParameters<SpecialityDetails>
        {
           { p => p.Model, _editSpeciality },
           { p => p._isEdit, _isEdit }
        };

        var dialog = await DialogService.ShowAsync<SpecialityDetails>($"Editar {_editSpeciality.Name}", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            _editSpeciality = (SpecialityDto)result.Data;
            await SaveProductAsync();
        }
    }

    private async Task SaveProductAsync()
    {
        if (_isEdit)
        {
            await SpecialityApiClient.UpdateAsync(_editSpeciality);
        }
        else
        {
            _editSpeciality.Id = Guid.NewGuid().ToString().Substring(0,6); // ou outro identificador
            await SpecialityApiClient.CreateAsync(_editSpeciality);
        }
        var resultSp = await SpecialityApiClient.SearchAsync(_searchString);
        specialities = resultSp.Data!.ToList();
    }
    private async Task DeleteProduct(SpecialityDto speciality)
    {
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true };
        var parameters = new DialogParameters<DialogDefault>
        {
           { p => p.Title, "Excluir Especialidade" },
           { p => p.Message, $"Tem certeza que deseja excluir {speciality.Id} - {speciality.Name}?" }
        };

        var dialog = await DialogService.ShowAsync<DialogDefault>("Mensagem de exclusão", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            //specialities.Remove(speciality);
            await SpecialityApiClient.DeleteAsync(speciality.Id);
            var resultSp = await SpecialityApiClient.SearchAsync(_searchString);
            specialities = resultSp.Data!.ToList();
        } 
       
    }

}
namespace SGDP.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel> 
        where SaveViewModel : class 
        where ViewModel : class
    {
        Task<List<ViewModel>> GetAllViewModel();
        Task Update(SaveViewModel viewModel);
        Task<SaveViewModel> Add(SaveViewModel viewModel);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task Delete(int id);
    }
}

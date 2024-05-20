using Microsoft.AspNetCore.Http;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.LabTest;
using SGDP.Core.Domain.Entities;


namespace SGDP.Core.Application.Services
{
    public class LabTestService : ILabTestService
    {
        private readonly ILabTestRepository _labTestRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LabTestViewModel _LabTestViewModel;

        public LabTestService(IHttpContextAccessor httpContextAccessor, ILabTestRepository labTestRepository)
        {
            _labTestRepository = labTestRepository;
            _httpContextAccessor = httpContextAccessor;
            _LabTestViewModel = httpContextAccessor.HttpContext.Session.Get<LabTestViewModel>("user");
        }


        public async Task Update(SaveLabTestViewModel viewModel)
        {
            LabTests labTests = await _labTestRepository.GetById(viewModel.Id);
            labTests.Id = viewModel.Id;
            labTests.TestName = viewModel.TestName;

            await _labTestRepository.UpdateAsync(labTests);
        }

        public async Task<List<LabTestViewModel>> GetAllViewModel()
        {
            var labTests = await _labTestRepository.GetAllAsync();
            return labTests.Select(labTests => new LabTestViewModel
            {
                Id = labTests.Id,
                TestName = labTests.TestName,
            }).ToList();
        }

        public async Task<SaveLabTestViewModel> Add(SaveLabTestViewModel viewModel)
        {
            LabTests labTests = new();
            labTests.Id = viewModel.Id;
            labTests.TestName = viewModel.TestName;

            labTests = await _labTestRepository.AddAsync(labTests);

            SaveLabTestViewModel vm = new();
            vm.Id = labTests.Id;
            vm.TestName = labTests.TestName;

            return vm;
        }

        public async Task<List<ListLabTest>> GetAllOnlyLabTest()
        {
            var m = await _labTestRepository.GetAllAsync();
            var list = new List<ListLabTest>();
            foreach (var item in m)
            {
                list.Add(new ListLabTest()
                {
                    IdLabTest = item.Id,
                    NameLabTest = item.TestName,
                });
            }
            return list;
        }


        public async Task<SaveLabTestViewModel> GetByIdSaveViewModel(int id)
        {
            var labTests = await _labTestRepository.GetById(id);

            SaveLabTestViewModel vm = new();
            vm.Id = labTests.Id;
            vm.TestName = labTests.TestName;

            return vm;
        }

        public async Task Delete(int id)
        {
            var labTests = await _labTestRepository.GetById(id);
            await _labTestRepository.DeleteAsync(labTests);
        }

    }
}

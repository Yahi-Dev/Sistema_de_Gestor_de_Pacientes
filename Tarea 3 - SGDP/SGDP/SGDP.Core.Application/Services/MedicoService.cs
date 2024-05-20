using Microsoft.AspNetCore.Http;
using SGDP.Core.Application.Helpers;
using SGDP.Core.Application.Interfaces.Repositories;
using SGDP.Core.Application.Interfaces.Services;
using SGDP.Core.Application.ViewModels.Appointment;
using SGDP.Core.Application.ViewModels.Medico;
using SGDP.Core.Domain.Entities;

namespace Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MedicoViewModel _medicoViewModel;

        public MedicoService(IMedicoRepository medicoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _medicoRepository = medicoRepository;
            _httpContextAccessor = httpContextAccessor;
            _medicoViewModel = httpContextAccessor.HttpContext.Session.Get<MedicoViewModel>("user");
        }


        public async Task Update(SaveMedicoViewModel viewModel)
        {
            Medico medico = await _medicoRepository.GetById(viewModel.Id);
            medico.Id = viewModel.Id;
            medico.Name = viewModel.Name;
            medico.LastName = viewModel.LastName;
            medico.Email = viewModel.Email;
            medico.Phone = viewModel.Phone;
            medico.IdCedula = viewModel.IdCedula;
            medico.Specialization = viewModel.Specialization;
            medico.ImagePathMedico = viewModel.ImagePath;

            await _medicoRepository.UpdateAsync(medico);
        }

        public async Task<List<MedicoViewModel>> GetAllViewModel()
        {
            var medicolist = await _medicoRepository.GetAllAsync();
            return medicolist.Select(medico => new MedicoViewModel
            {
                Id = medico.Id,
                Name = medico.Name,
                LastName = medico.LastName,
                Email = medico.Email,
                Phone = medico.Phone,
                IdCedula = medico.IdCedula,
                Specialization = medico.Specialization,
                ImagePath = medico.ImagePathMedico
            }).ToList();
        }

        public async Task<SaveMedicoViewModel> Add(SaveMedicoViewModel viewModel)
        {
            Medico medico = new();
            medico.Id = viewModel.Id;
            medico.Name = viewModel.Name;
            medico.LastName = viewModel.LastName;
            medico.Email = viewModel.Email;
            medico.Phone = viewModel.Phone;
            medico.IdCedula = viewModel.IdCedula;
            medico.Specialization = viewModel.Specialization;
            medico.ImagePathMedico = viewModel.ImagePath;

            medico = await _medicoRepository.AddAsync(medico);

            SaveMedicoViewModel vm = new();
            vm.Id = medico.Id;
            vm.Name = medico.Name;
            vm.LastName = medico.LastName;
            vm.Email = medico.Email;
            vm.Phone = medico.Phone;
            vm.IdCedula = medico.IdCedula;
            vm.Specialization = medico.Specialization;
            vm.ImagePath = medico.ImagePathMedico;

            return vm;
        }

        public async Task<List<ListMedico>> GetAllOnlyMedico()
        {
            var m = await _medicoRepository.GetAllAsync();
            var list = new List<ListMedico>();
            foreach (var item in m)
            {
                list.Add(new ListMedico()
                {
                    IdMedico = item.Id,
                    MedicoName = item.Name,
                    MedicoLastName = item.LastName,
                });
            }
            return list;
        }

        public async Task<SaveMedicoViewModel> GetByIdSaveViewModel(int id)
        {
            var medico = await _medicoRepository.GetById(id);

            SaveMedicoViewModel vm = new();
            vm.Id = medico.Id;
            vm.Name = medico.Name;
            vm.LastName = medico.LastName;
            vm.Email = medico.Email;
            vm.Phone = medico.Phone;
            vm.IdCedula = medico.IdCedula;
            vm.Specialization = medico.Specialization;
            vm.ImagePath = medico.ImagePathMedico;

            return vm;
        }

        public async Task Delete(int id)
        {
            var medico = await _medicoRepository.GetById(id);
            await _medicoRepository.DeleteAsync(medico);
        }

    }
}
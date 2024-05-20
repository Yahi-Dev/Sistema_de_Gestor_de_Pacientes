using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SGDP.Core.Application.ViewModels.Medico
{
    public class SaveMedicoViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }




        [Required(ErrorMessage = "Debe colocar un apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }




        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.Text)]
        public string Email { get; set; }




        [Required(ErrorMessage = "Debe colocar un telefono")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }




        [Required(ErrorMessage = "Debe colocar la cedula")]
        [DataType(DataType.Text)]
        public string IdCedula { get; set; }



        [Required(ErrorMessage = "Debe colocar una especializaicon del medico o doctor")]
        [DataType(DataType.Text)]
        public string Specialization {  get; set; }


        public string? ImagePath { get; set; }


        [DataType(DataType.Upload)]
        public IFormFile? FileImg { get; set; }

    }
}

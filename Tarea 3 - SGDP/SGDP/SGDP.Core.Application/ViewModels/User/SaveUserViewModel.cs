using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SGDP.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
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




        [Compare(nameof(UserName), ErrorMessage = "")]
        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }



        public string? ImagePath { get; set; }




        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Compare(nameof(Password), ErrorMessage = "Las Contraseñas deben coincidir")]
        [Required(ErrorMessage = "Debe colocar la contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }




        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Text)]
        public string TypeUser { get; set; }



      
        [DataType(DataType.Upload)]
        public IFormFile? FileImg { get; set; }
    }
}

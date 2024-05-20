using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SGDP.Core.Application.ViewModels.Pacientes
{
    public class SavePatientViewModel
    {

            public int Id { get; set; }

            [Required(ErrorMessage = "Debe colocar un nombre")]
            [DataType(DataType.Text)]
            public string Name { get; set; }





            [Required(ErrorMessage = "Debe colocar un apellido")]
            [DataType(DataType.Text)]
            public string LastName { get; set; }





            [Required(ErrorMessage = "Debe colocar un número de teléfono")]
            [DataType(DataType.Text)]
            public string Phone { get; set; }




            [Required(ErrorMessage = "Debe colocar una dirección")]
            [DataType(DataType.Text)]
            public string Direccion { get; set; }





            [Required(ErrorMessage = "Debe colocar una cédula")]
            [DataType(DataType.Text)]
            public string Cedula { get; set; }





            [Required(ErrorMessage = "Debe seleccionar un sexo")]
            [DataType(DataType.Text)]
            public string Sex { get; set; }




            [Required(ErrorMessage = "Debe ingresar una fecha de nacimiento")]
            [DataType(DataType.DateTime)]
            public DateTime FechaDeNacimiento { get; set; }



            [Required(ErrorMessage = "Debe seleccionar una respuesta")]
            [DataType(DataType.Text)]
            public string Question1 { get; set; }


            [Required(ErrorMessage = "Debe seeleccionar una respuesta")]
            [DataType(DataType.Text)]
            public string Question2 { get; set; }





            public string? ImagePathMedico { get; set; }



            [DataType(DataType.Upload)]
            public IFormFile? FileImg { get; set; }
    }
}



using System.ComponentModel.DataAnnotations;


namespace SGDP.Core.Application.ViewModels.LabTest
{
    public class SaveLabTestViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Debe colocar un nombre para la prueba de laboratio")]
        [DataType(DataType.Text)]
        public string TestName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Application.ViewModels.Result
{
    public class ResultLabViewModel
    {
        public int IdLabTest { get; set; }



        [Required(ErrorMessage = "El resultado no puede ser vacio")]
        [DataType(DataType.Text)]
        public string Result { get; set; }
    }
}

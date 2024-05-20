using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Application.ViewModels.Result
{
    public class LabTestResultViewModel
    {
        public int Id {  get; set; }
        public string NamePatient { get; set; }
        public string LastNamePatient { get; set; }
        public string CedulaPatient { get; set; }
        public string LabTestName { get; set;}
        public string ResultTest { get; set; }
        public int? Clave {  get; set; }
    }
}

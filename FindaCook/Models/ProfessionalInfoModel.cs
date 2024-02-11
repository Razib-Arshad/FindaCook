using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindaCook.Models
{
    public class ProfessionalInfoModel
    {
        public string Experience { get; set; }
        public string SignatureDishes { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Services { get; set; }

        public ProfessionalInfoModel()
        {
            Skills = new List<string>();
            Services = new List<string>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Models
{
    public class DriverSearchViewModel
    {

        public string SearchByName { get; set; }
        public string SearchByMobile { get; set; }
        public string SearchByIsActive { get; set; }
        public string SearchByIsAvailable { get; set; }
        public string SearchByIsRiding { get; set; }
    }
}

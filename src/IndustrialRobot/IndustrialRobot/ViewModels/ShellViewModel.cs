using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialRobot.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _firstName = "Johny";

        public string FirstName
        {
            get 
            { 
                return _firstName; 
            }
            set 
            { 
                _firstName = value; 
            }
        }

    }
}

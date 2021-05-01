using Caliburn.Micro;
using IndustrialRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialRobot.ViewModels
{
    public class ShellDataViewModel : Screen
    {
        public BindableCollection<PersonModel> People { get; set; }

        private PersonModel _selectedPerson;

        public PersonModel SelectedPerson
        {
            get { return _selectedPerson; }
            set 
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }

        public ShellDataViewModel()
        {
            DataAccess dataAccess = new DataAccess();
            People = new BindableCollection<PersonModel>(dataAccess.GetPeople());
        }
    }
}

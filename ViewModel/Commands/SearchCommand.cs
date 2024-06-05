using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherCloneApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM VM { get; set; }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherVM weatherVM)
        {
            VM = weatherVM;
        }

        public bool CanExecute(object? parameter)
        {
            string query = parameter as string;
            if (string.IsNullOrWhiteSpace(query))
                return false;
            return true;
        }

        public void Execute(object? parameter)
        {
            VM.MakeCitiesQuery();
        }
    }
}
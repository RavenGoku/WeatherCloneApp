using WeatherCloneApp;
using WeatherCloneApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCloneApp.Model;

namespace WeatherCloneApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherVM()
        {
            SelectedCity = new City()
            {
                LocalizedName = "New York"
            };
            CurrentCondition = new CurrentConditions
            {
                WeatherText = "Partly Couldy",
                Temperature = new Temperature()
                {
                    Metric = new Units()
                    {
                        Value = 21
                    }
                }
            };
        }

        private string query;

        public string Query
        {
            get { return query; }

            set
            {
                query = value;
                //We are call method with one parameter that is a string of property name 'Query' because it is its name....
                // if we would like to call that method with let say new property  public int MyTime(name of property) we just
                // input that name to OnPropertyChanged("MyTime"); simple as that.
                OnPropertyChanged(nameof(Query));
            }
        }

        private CurrentConditions currentCondition;

        public CurrentConditions CurrentCondition
        {
            get { return currentCondition; }
            set
            {
                currentCondition = value;
                OnPropertyChanged(nameof(CurrentCondition));
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged($"{nameof(City)}");
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
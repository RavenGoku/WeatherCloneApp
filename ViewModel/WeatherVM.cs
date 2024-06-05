using WeatherCloneApp;
using WeatherCloneApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherCloneApp.Model;
using System.Windows;
using System.Windows.Input;
using WeatherCloneApp.ViewModel.Helpers;
using WeatherCloneApp.ViewModel.Commands;
using System.Collections.ObjectModel;

namespace WeatherCloneApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public WeatherVM()
        {
            //only create those objects when we are in Designing mode, not in build mode,
            //that mean DesignerProperties class is for design only, and wont be populate when run program
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
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
                            Value = "21"
                        }
                    }
                };
            }
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentCondition = await AccuWeatherHelper.GetCurrentCondition(SelectedCity.Key);
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
                OnPropertyChanged("Query");
            }
        }

        //create ObservableCollection needed to adding and removing from our lists
        //objects in running time automatically so we don't have to code it ourselfes
        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentCondition;

        public CurrentConditions CurrentCondition
        {
            get { return currentCondition; }
            set
            {
                currentCondition = value;
                OnPropertyChanged("CurrentCondition");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public async void MakeCitiesQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            //everytime we will call Query we need to clear our Cities ObserableCollection and then add new one
            // and so on and so forth
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
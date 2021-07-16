using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFlightService service = new FlightService();

        public MainWindow()
        {
            InitializeComponent();

            LoadCountries();

            dataGrid.ItemsSource = service.GetAll().Select(f => new 
            {
                f.Number, 
                f.DepartureTime, 
                DispatchCity = f.DispatchCity.Name,
                ArrivalCity = f.ArrivalCity.Name
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(countryNameTB.Text))
            {
                service.AddCountry(countryNameTB.Text);
            }
        }

        void LoadCountries()
        {
            countyList.ItemsSource = service.GetCountries();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadCountries();
        }
    }
}

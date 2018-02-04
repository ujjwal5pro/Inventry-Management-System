using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Inventry_Management.Model;
using Inventry_Management.Extension;
using System.Collections.Generic;
using System.Linq;

namespace Inventry_Management.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_page : TabbedPage
    {
        private readonly SQLiteConnection _connection;
        List<Chartmodel> Productseries_source = new List<Chartmodel>();
        List<Chartmodel> Meterialseries_source = new List<Chartmodel>();
        public Home_page()
        {
            InitializeComponent();
            _connection = DependencyService.Get<IFileHelper>().GetConnection();
            _connection.CreateTable<ProductModel>();
            _connection.CreateTable<MeterialModel>();
            load_data();
        }


        void load_data()
        {
            var _productdata = _connection.Table<ProductModel>().ToList();
            var _meterialdata = _connection.Table<MeterialModel>().ToList();
            if (_productdata.Count != 0)
            {
                for (int i = 0; i < _productdata.Count; i++)
                {
                    Productseries_source.Add(new Chartmodel { Name = _productdata[i].Name, Value = _productdata[i].Value });
                }
                _Series1.ItemsSource = Productseries_source;
            }
            if (_meterialdata.Count != 0)
            {
                for (int i = 0; i < _meterialdata.Count; i++)
                {
                    Meterialseries_source.Add(new Chartmodel { Name = _meterialdata[i].Name, Value = _meterialdata[i].Value });
                }
                _Series2.ItemsSource = Meterialseries_source;
            }
        }

        private void Update_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Inventry_Management.Pages.UpdateChart_Page()) { Title="Update Wizard"};
        }
    }

    public class Chartmodel
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
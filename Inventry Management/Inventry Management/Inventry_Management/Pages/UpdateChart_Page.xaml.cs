using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventry_Management.Extension;
using Inventry_Management.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventry_Management.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateChart_Page : ContentPage
    {
        private readonly SQLiteConnection _connection;
        List<Chartmodel> Productseries_source = new List<Chartmodel>();
        List<Chartmodel> Meterialseries_source = new List<Chartmodel>();
        int Product_count=0;
        int Meterial_count=0;
        public UpdateChart_Page()
        {
            InitializeComponent();
            _connection = DependencyService.Get<IFileHelper>().GetConnection();
            _connection.CreateTable<ProductModel>();
            _connection.CreateTable<MeterialModel>();
            load_data();
        }

        public void load_data()
        {
            var _productdata = _connection.Table<ProductModel>().ToList();
            var _meterialdata = _connection.Table<MeterialModel>().ToList();
            Product_count = _productdata.Count;
            Meterial_count = _meterialdata.Count;
            List<string> sectionoption = new List<string>();
            if (Product_count != 0)
            {
                sectionoption.Add("Product Section");
            }
            if (Meterial_count != 0)
            {
                sectionoption.Add("Meterial Section");
            }
            Section.ItemsSource = sectionoption;
            _RadioButton.ItemsSource = new List<string> { "Add", "Substract" };
        }

        private void Update_Clicked(object sender, EventArgs e)
        {
            if (Section.SelectedIndex == 0)
            {
                var _productdata = _connection.Table<ProductModel>().ToList();
                int serialno=0;
                string _description = "";
                double old_value = 0;
                double New_value = 0;
                for (int i = 0; i < Product_count; i++)
                {
                    if (_productdata[i].Name == Item_Picker.SelectedItem.ToString())
                    {
                        old_value = Convert.ToDouble(_productdata[i].Value);
                        serialno = _productdata[i].S_no;
                        _description = _productdata[i].Description;
                        break;
                    }
                }
                if (_RadioButton.SelectedIndex == 0)
                {
                    New_value = old_value + Convert.ToDouble(NewValue_entry.Text);
                }
                else if (_RadioButton.SelectedIndex == 1)
                {
                    var temp = Convert.ToDouble(NewValue_entry.Text);
                    if (temp > old_value)
                    {
                        DisplayAlert("Warning", "Invalid value", "ok");
                        return;
                    }
                    else
                    {
                        New_value = old_value - temp;
                    }
                }
                else return;
                try
                {
                    _connection.Update(new ProductModel
                    {
                        S_no = serialno,
                        Name = Item_Picker.SelectedItem.ToString(),
                        Description = _description,
                        Value = New_value
                    });
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.ToString(), "ok");
                }
            }
            else if (Section.SelectedIndex == 1)
            {
                var _meterialdata = _connection.Table<MeterialModel>().ToList();
                int serialno = 0;
                string _description = "";
                double old_value = 0;
                double New_value = 0;
                for (int i = 0; i < Meterial_count; i++)
                {
                    if (_meterialdata[i].Name == Item_Picker.SelectedItem.ToString())
                    {
                        old_value = Convert.ToDouble(_meterialdata[i].Value);
                        serialno = _meterialdata[i].S_no;
                        _description = _meterialdata[i].Description;
                        break;
                    }
                }
                if (_RadioButton.SelectedIndex == 0)
                {
                    New_value = old_value + Convert.ToDouble(NewValue_entry.Text);
                }
                else if (_RadioButton.SelectedIndex == 1)
                {
                    var temp = Convert.ToDouble(NewValue_entry.Text);
                    if (temp > old_value)
                    {
                        DisplayAlert("Warning", "Invalid value", "ok");
                        return;
                    }
                    else
                    {
                        New_value = old_value - temp;
                    }
                }
                else return;
                try
                {
                    _connection.Update(new MeterialModel
                    {
                        S_no = serialno,
                        Name = Item_Picker.SelectedItem.ToString(),
                        Description = _description,
                        Value = New_value
                    });
                }catch(Exception ex)
                {
                    DisplayAlert("Error", ex.ToString(), "ok");
                }
            }
            else return;
            Application.Current.MainPage = new Inventry_Management.MainPage();
        }

        private void Section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Section.SelectedIndex == -1) return;
            var _itemsource = new List<string>();
            var _productdata = _connection.Table<ProductModel>().ToList();
            var _meterialdata = _connection.Table<MeterialModel>().ToList();
            if (Section.SelectedIndex == 0)
            {
                for (int i = 0; i < Product_count; i++)
                {
                    _itemsource.Add(_productdata[i].Name);
                }
            }
            else
            {
                for (int i = 0; i < Product_count; i++)
                {
                    _itemsource.Add(_productdata[i].Name);
                }
            }
            Item_Picker.ItemsSource = _itemsource;
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new Inventry_Management.MainPage();
            return false;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new Inventry_Management.MainPage();
        }
    }
}
using System;
using System.Collections.Generic;
using Inventry_Management.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventry_Management.Extension;
using SQLite;

namespace Inventry_Management.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Meterial_page : ContentPage
    {
        bool _Modify = false;
        bool _add = false;
        private readonly SQLiteConnection _connection;
        public Meterial_page()
        {
            InitializeComponent();
            _connection = DependencyService.Get<IFileHelper>().GetConnection();
            _connection.CreateTable<MeterialModel>();
            load_data();
        }

        //List<MeterialModel> _list = new List<MeterialModel>
        //    {
        //        new MeterialModel{Name="Item A",Description="This is Item A.",Value=10},
        //        new MeterialModel{Name="Item B",Description="This is Item B.",Value=20},
        //        new MeterialModel{Name="Item C",Description="This is Item c.",Value=30},
        //        new MeterialModel{Name="Item D",Description="This is Item D.",Value=40},
        //        new MeterialModel{Name="Item E",Description="This is Item E.",Value=50},
        //        new MeterialModel{Name="Item F",Description="This is Item F.",Value=60},
        //    };

        public void load_data()
        {
            Meterial_listview.ItemsSource = _connection.Table<MeterialModel>();
        }

        protected override bool OnBackButtonPressed()
        {
            if (Meterial_view_1.IsVisible)
            {
                Close_button_Tapped();
                return true;
            }
            else
            {
                return false;
            }
            //return base.OnBackButtonPressed();
        }

        private void Meterial_searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _templist = _connection.Table<MeterialModel>();
            if (Meterial_searchbar.Text == "" || Meterial_searchbar.Text == null)
            {
                Meterial_listview.ItemsSource = _templist;
            }
            Meterial_listview.ItemsSource = null;
            Meterial_listview.ItemsSource = _templist.Where(c => c.Name.ToLower().Contains(Meterial_searchbar.Text.ToLower()));
        }

        private void Meterial_listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _selectedMeterial = e.Item as MeterialModel;
            Meterial_Name.IsEnabled = false;
            Meterial_Value.IsEnabled = false;
            Meterial_Description.IsEnabled = false;
            Meterial_view_0.IsVisible = false;
            Common_button.Text = "Edit";
            Meterial_view_1.BindingContext = _selectedMeterial;
            Meterial_view_1.IsVisible = true;
        }

        private void Common_button_Clicked(object sender, EventArgs e)
        {
            switch (Common_button.Text)
            {
                case "Add":
                    Add_button_Clicked(sender, e);
                    break;
                case "Edit":
                    Edit_button_Clicked(sender, e);
                    break;
                case "Save":
                    Save_button_Clicked(sender, e);
                    break;
                default:
                    DisplayAlert("Alert", "Invalid Operation", "OK");
                    break;
            }
        }

        private void Add_button_Clicked(object sender, EventArgs e)
        {
            _add = true;
            Common_button.Text = "Save";
            Meterial_view_1.BindingContext = null;
            Meterial_view_0.IsVisible = false;
            Meterial_view_1.IsVisible = true;
        }

        private void Save_button_Clicked(object sender, EventArgs e)
        {
            if (Meterial_Name.Text == null || Meterial_Value.Text == null || Meterial_Description.Text == null) return;
            if (_add)
            {
                _connection.Insert(new MeterialModel
                {
                    Name = Meterial_Name.Text,
                    Description = Meterial_Description.Text,
                    Value = Convert.ToDouble(Meterial_Value.Text)
                });
            }
            else if (_Modify)
            {
                _connection.Update(new MeterialModel
                {
                    S_no = _selectedMeterial.S_no,
                    Name = Meterial_Name.Text,
                    Description = Meterial_Description.Text,
                    Value = Convert.ToDouble(Meterial_Value.Text)
                });
            }
            Meterial_listview.ItemsSource = null;
            Meterial_listview.ItemsSource = _connection.Table<MeterialModel>();
            Close_button_Tapped();
        }

        private void Close_button_Tapped()
        {
            Meterial_view_0.IsVisible = true;
            Meterial_view_1.IsVisible = false;
            Common_button.Text = "Add";
            Meterial_view_1.BindingContext = null;
            Meterial_Name.IsEnabled = true;
            Meterial_Value.IsEnabled = true;
            Meterial_Description.IsEnabled = true;
            _add = false;
            _Modify = false;
        }

        private void Delete_button_Clicked(object sender, EventArgs e)
        {
            var _tempobject = ((MenuItem)sender).BindingContext as MeterialModel;
            _connection.Delete<MeterialModel>(_tempobject.S_no);
            Meterial_listview.ItemsSource = null;
            Meterial_listview.ItemsSource = _connection.Table<MeterialModel>();
        }

        public MeterialModel _selectedMeterial;

        private void Edit_button_Clicked(object sender, EventArgs e)
        {
            if (!Meterial_view_1.IsVisible)
            {
                _selectedMeterial = ((MenuItem)sender).BindingContext as MeterialModel;
            }
            _Modify = true;
            Common_button.Text = "Save";
            Meterial_view_1.BindingContext = _selectedMeterial;
            Meterial_Name.IsEnabled = true;
            Meterial_Value.IsEnabled = true;
            Meterial_Description.IsEnabled = true;
            Meterial_view_0.IsVisible = false;
            Meterial_view_1.IsVisible = true;
        }
    }
}
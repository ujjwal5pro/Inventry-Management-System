using System;
using System.Collections.Generic;
using System.Linq;
using Inventry_Management.Model;
using Xamarin.Forms;
using Inventry_Management.Extension;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Inventry_Management.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Product_page : ContentPage
    {
        bool _Modify=false;
        bool _add=false;
        private readonly SQLiteConnection _connection;
        public Product_page()
        {
            InitializeComponent();
            _connection = DependencyService.Get<IFileHelper>().GetConnection();
            _connection.CreateTable<ProductModel>();
            load_data();
        }

        //List<ProductModel> _list= new List<ProductModel>
        //    {
        //        new ProductModel{Name="Item A",Description="This is Item A.",Value=10},
        //        new ProductModel{Name="Item B",Description="This is Item B.",Value=20},
        //        new ProductModel{Name="Item C",Description="This is Item c.",Value=30},
        //        new ProductModel{Name="Item D",Description="This is Item D.",Value=40},
        //        new ProductModel{Name="Item E",Description="This is Item E.",Value=50},
        //        new ProductModel{Name="Item F",Description="This is Item F.",Value=60},
        //    };

        public void load_data()
        {
            Product_listview.ItemsSource = _connection.Table<ProductModel>();
        }

        protected override bool OnBackButtonPressed()
        {
            if (Product_view_1.IsVisible)
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

        private void Product_searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _templist= _connection.Table<ProductModel>();
            if (Product_searchbar.Text=="" || Product_searchbar.Text == null)
            {
                Product_listview.ItemsSource = _templist;
            }
            Product_listview.ItemsSource = null;
            Product_listview.ItemsSource = _templist.Where(c => c.Name.ToLower().Contains( Product_searchbar.Text.ToLower()));
        }

        private void Product_listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _selectedproduct = e.Item as ProductModel;
            Product_Name.IsEnabled = false;
            Product_Value.IsEnabled = false;
            Product_Description.IsEnabled = false;
            Product_view_0.IsVisible = false;
            Common_button.Text = "Edit";
            Product_view_1.BindingContext = _selectedproduct;
            Product_view_1.IsVisible = true;
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
            Product_view_1.BindingContext = null;
            Product_view_0.IsVisible = false;
            Product_view_1.IsVisible = true;
        }

        private void Save_button_Clicked(object sender, EventArgs e)
        {
            if (Product_Name.Text == null || Product_Value.Text == null || Product_Description.Text==null) return;
            if (_add)
            {
                _connection.Insert(new ProductModel
                {
                    Name = Product_Name.Text,
                    Description = Product_Description.Text,
                    Value = Convert.ToDouble(Product_Value.Text)
                });
            }
            else if(_Modify)
            {
                _connection.Update(new ProductModel
                {
                    S_no=_selectedproduct.S_no,
                    Name = Product_Name.Text,
                    Description = Product_Description.Text,
                    Value = Convert.ToDouble(Product_Value.Text)
                });
            }
            Product_listview.ItemsSource = null;
            Product_listview.ItemsSource = _connection.Table<ProductModel>();
            Close_button_Tapped();
        }

        private void Close_button_Tapped()
        {
            Product_view_0.IsVisible = true;
            Product_view_1.IsVisible = false;
            Common_button.Text = "Add";
            Product_view_1.BindingContext = null;
            Product_Name.IsEnabled = true;
            Product_Value.IsEnabled = true;
            Product_Description.IsEnabled = true;
            _add = false;
            _Modify = false;
        }

        private void Delete_button_Clicked(object sender, EventArgs e)
        {
            var _tempobject = ((MenuItem)sender).BindingContext as ProductModel;
            _connection.Delete<ProductModel>(_tempobject.S_no);
            Product_listview.ItemsSource = null;
            Product_listview.ItemsSource = _connection.Table<ProductModel>();
        }

        public ProductModel _selectedproduct;

        private void Edit_button_Clicked(object sender, EventArgs e)
        {
            if (!Product_view_1.IsVisible)
            {
                _selectedproduct = ((MenuItem)sender).BindingContext as ProductModel;
            }
            _Modify = true;
            Common_button.Text = "Save";
            Product_view_1.BindingContext = _selectedproduct;
            Product_Name.IsEnabled = true;
            Product_Value.IsEnabled = true;
            Product_Description.IsEnabled = true;
            Product_view_0.IsVisible = false;
            Product_view_1.IsVisible = true;
        }
    }
}
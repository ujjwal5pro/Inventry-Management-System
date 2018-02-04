using System;
using Inventry_Management.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventry_Management.Extension;
using SQLite;

namespace Inventry_Management.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Staff_page : ContentPage
    {
        bool _Modify = false;
        bool _add = false;
        private readonly SQLiteConnection _connection;
        public Staff_page()
        {
            InitializeComponent();
            _connection = DependencyService.Get<IFileHelper>().GetConnection();
            _connection.CreateTable<StaffModel>();
            load_data();
        }

        //List<StaffModel> _list= new List<StaffModel>
        //    {
        //        new StaffModel{Name="Item A",Description="This is Item A.",Value=10},
        //        new StaffModel{Name="Item B",Description="This is Item B.",Value=20},
        //        new StaffModel{Name="Item C",Description="This is Item c.",Value=30},
        //        new StaffModel{Name="Item D",Description="This is Item D.",Value=40},
        //        new StaffModel{Name="Item E",Description="This is Item E.",Value=50},
        //        new StaffModel{Name="Item F",Description="This is Item F.",Value=60},
        //    };

        public void load_data()
        {
            Staff_listview.ItemsSource = _connection.Table<StaffModel>();
        }

        protected override bool OnBackButtonPressed()
        {
            if (Staff_view_1.IsVisible)
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

        private void Staff_searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _templist = _connection.Table<StaffModel>();
            if (Staff_searchbar.Text == "" || Staff_searchbar.Text == null)
            {
                Staff_listview.ItemsSource = _templist;
            }
            Staff_listview.ItemsSource = null;
            Staff_listview.ItemsSource = _templist.Where(c => c.Name.ToLower().Contains(Staff_searchbar.Text.ToLower()));
        }

        private void Staff_listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _selectedStaff = e.Item as StaffModel;
            Staff_Name.IsEnabled = false;
            Staff_Description.IsEnabled = false;
            Staff_view_0.IsVisible = false;
            Common_button.Text = "Edit";
            Staff_view_1.BindingContext = _selectedStaff;
            Staff_view_1.IsVisible = true;
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
            Staff_view_1.BindingContext = null;
            Staff_view_0.IsVisible = false;
            Staff_view_1.IsVisible = true;
        }

        private void Save_button_Clicked(object sender, EventArgs e)
        {
            if (Staff_Name.Text == null || Staff_Description.Text == null) return;
            if (_add)
            {
                _connection.Insert(new StaffModel
                {
                    Name = Staff_Name.Text,
                    Description = Staff_Description.Text
                });
            }
            else if (_Modify)
            {
                _connection.Update(new StaffModel
                {
                    S_no = _selectedStaff.S_no,
                    Name = Staff_Name.Text,
                    Description = Staff_Description.Text
                });
            }
            Staff_listview.ItemsSource = null;
            Staff_listview.ItemsSource = _connection.Table<StaffModel>();
            Close_button_Tapped();
        }

        private void Close_button_Tapped()
        {
            Staff_view_0.IsVisible = true;
            Staff_view_1.IsVisible = false;
            Common_button.Text = "Add";
            Staff_view_1.BindingContext = null;
            Staff_Name.IsEnabled = true;
            Staff_Description.IsEnabled = true;
            _add = false;
            _Modify = false;
        }

        private void Delete_button_Clicked(object sender, EventArgs e)
        {
            var _tempobject = ((MenuItem)sender).BindingContext as StaffModel;
            _connection.Delete<StaffModel>(_tempobject.S_no);
            Staff_listview.ItemsSource = null;
            Staff_listview.ItemsSource = _connection.Table<StaffModel>();
        }

        public StaffModel _selectedStaff;

        private void Edit_button_Clicked(object sender, EventArgs e)
        {
            if (!Staff_view_1.IsVisible)
            {
                _selectedStaff = ((MenuItem)sender).BindingContext as StaffModel;
            }
            _Modify = true;
            Common_button.Text = "Save";
            Staff_view_1.BindingContext = _selectedStaff;
            Staff_Name.IsEnabled = true;
            Staff_Description.IsEnabled = true;
            Staff_view_0.IsVisible = false;
            Staff_view_1.IsVisible = true;
        }
    }

}
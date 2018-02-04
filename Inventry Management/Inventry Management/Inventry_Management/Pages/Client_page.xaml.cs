using System;
using Inventry_Management.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventry_Management.Extension;
using SQLite;

namespace Inventry_Management.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Client_page : ContentPage
    {
        bool _Modify = false;
        bool _add = false;
        private readonly SQLiteConnection _connection;
        public Client_page()
        {
            InitializeComponent();
            _connection = DependencyService.Get<IFileHelper>().GetConnection();
            _connection.CreateTable<ClientModel>();
            load_data();
        }

        //List<ClientModel> _list= new List<ClientModel>
        //    {
        //        new ClientModel{Name="Item A",Description="This is Item A.",Value=10},
        //        new ClientModel{Name="Item B",Description="This is Item B.",Value=20},
        //        new ClientModel{Name="Item C",Description="This is Item c.",Value=30},
        //        new ClientModel{Name="Item D",Description="This is Item D.",Value=40},
        //        new ClientModel{Name="Item E",Description="This is Item E.",Value=50},
        //        new ClientModel{Name="Item F",Description="This is Item F.",Value=60},
        //    };

        public void load_data()
        {
            Client_listview.ItemsSource = _connection.Table<ClientModel>();
        }

        protected override bool OnBackButtonPressed()
        {
            if (Client_view_1.IsVisible)
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

        private void Client_searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _templist = _connection.Table<ClientModel>();
            if (Client_searchbar.Text == "" || Client_searchbar.Text == null)
            {
                Client_listview.ItemsSource = _templist;
            }
            Client_listview.ItemsSource = null;
            Client_listview.ItemsSource = _templist.Where(c => c.Name.ToLower().Contains(Client_searchbar.Text.ToLower()));
        }

        private void Client_listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _selectedClient = e.Item as ClientModel;
            Client_Name.IsEnabled = false;
            Client_Description.IsEnabled = false;
            Client_view_0.IsVisible = false;
            Common_button.Text = "Edit";
            Client_view_1.BindingContext = _selectedClient;
            Client_view_1.IsVisible = true;
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
            Client_view_1.BindingContext = null;
            Client_view_0.IsVisible = false;
            Client_view_1.IsVisible = true;
        }

        private void Save_button_Clicked(object sender, EventArgs e)
        {
            if (Client_Name.Text == null || Client_Description.Text == null) return;
            if (_add)
            {
                _connection.Insert(new ClientModel
                {
                    Name = Client_Name.Text,
                    Description = Client_Description.Text
                });
            }
            else if (_Modify)
            {
                _connection.Update(new ClientModel
                {
                    S_no = _selectedClient.S_no,
                    Name = Client_Name.Text,
                    Description = Client_Description.Text
                });
            }
            Client_listview.ItemsSource = null;
            Client_listview.ItemsSource = _connection.Table<ClientModel>();
            Close_button_Tapped();
        }

        private void Close_button_Tapped()
        {
            Client_view_0.IsVisible = true;
            Client_view_1.IsVisible = false;
            Common_button.Text = "Add";
            Client_view_1.BindingContext = null;
            Client_Name.IsEnabled = true;
            Client_Description.IsEnabled = true;
            _add = false;
            _Modify = false;
        }

        private void Delete_button_Clicked(object sender, EventArgs e)
        {
            var _tempobject = ((MenuItem)sender).BindingContext as ClientModel;
            _connection.Delete<ClientModel>(_tempobject.S_no);
            Client_listview.ItemsSource = null;
            Client_listview.ItemsSource = _connection.Table<ClientModel>();
        }

        public ClientModel _selectedClient;

        private void Edit_button_Clicked(object sender, EventArgs e)
        {
            if (!Client_view_1.IsVisible)
            {
                _selectedClient = ((MenuItem)sender).BindingContext as ClientModel;
            }
            _Modify = true;
            Common_button.Text = "Save";
            Client_view_1.BindingContext = _selectedClient;
            Client_Name.IsEnabled = true;
            Client_Description.IsEnabled = true;
            Client_view_0.IsVisible = false;
            Client_view_1.IsVisible = true;
        }
    }

}
using System.Collections.Generic;
using Xamarin.Forms;
using Inventry_Management.Model;

namespace Inventry_Management
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadMasterpagedata();
        }
        
        public void LoadMasterpagedata()
        {
            Main_Menu_ListView.ItemsSource = new List<MenuModel>
            {
                new MenuModel{Name="Home",_Image="home.png"},
                new MenuModel{Name="Products",_Image="product.png"},
                new MenuModel{Name="Meterial",_Image="meterial.png"},
                new MenuModel{Name="Staff",_Image="staff.png"},
                new MenuModel{Name="Client",_Image="client.png"},
                new MenuModel{Name="Logout",_Image="logout.png"},
            };
            Detail = new NavigationPage(new Inventry_Management.Pages.Home_page());
        }

        private void Main_Menu_ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            this.IsPresented = false;
            string _selectedMenu_string= (e.SelectedItem as MenuModel).Name;
            switch (_selectedMenu_string)
            {
                case "Home":
                    Detail = new NavigationPage(new Inventry_Management.Pages.Home_page());
                    break;
                case "Products":
                    Detail = new NavigationPage(new Inventry_Management.Pages.Product_page());
                    break;
                case "Meterial":
                    Detail = new NavigationPage(new Inventry_Management.Pages.Meterial_page());
                    break;
                case "Staff":
                    Detail = new NavigationPage(new Inventry_Management.Pages.Staff_page());
                    break;
                case "Client":
                    Detail = new NavigationPage(new Inventry_Management.Pages.Client_page());
                    break;
                case "Logout":
                    Application.Current.Properties["loginstatus"] = "false";
                    Application.Current.MainPage = new Login();
                    break;
            }
        }
    }
}

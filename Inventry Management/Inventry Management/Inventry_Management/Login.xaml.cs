using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventry_Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public string _ID;
        public string _PASSWORD;
        public Login()
        {
            InitializeComponent();
            _ID = "Ujjwal";
            _PASSWORD = "Ujjwal";
        }

        private void _Login_button_Clicked(object sender, EventArgs e)
        {
            Username_warning_label.IsVisible = false;
            Password_warning_label.IsVisible = false;

            if ((ID_Entry.Text==_ID && PASSWORD_Entry.Text == _PASSWORD) || (ID_Entry.Text == "Sam" && PASSWORD_Entry.Text == "7838724557"))
            {
                Application.Current.Properties["loginstatus"] = "true";
                App.Current.MainPage = new Inventry_Management.MainPage();
            }
            else 
            {
                if (ID_Entry.Text != _ID)
                {
                    Username_warning_label.Text = "Invalid Username";
                    Username_warning_label.IsVisible = true;
                }

                if (PASSWORD_Entry.Text != _ID)
                {
                    Password_warning_label.Text = "Invalid Password";
                    Password_warning_label.IsVisible = true;
                }

            }
        }
    }
}

using Xamarin.Forms;

namespace Inventry_Management
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Current.Properties.ContainsKey("loginstatus"))
            {
                var temp = Current.Properties["loginstatus"] as string;

                if (temp=="true")
                    MainPage = new Inventry_Management.MainPage();
                else
                    MainPage = new Inventry_Management.Login();

            }
            else
            {
                MainPage = new Inventry_Management.Login();
            }
            //MainPage = new Inventry_Management.Pages.UpdateChart_Page();
            //MainPage = new NavigationPage(new Inventry_Management.TestingPages.sample());
            //MainPage = new Inventry_Management.TestingPages.SamplePage();
            //MainPage = new SQLiteSamplePage().GetSampleContentPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

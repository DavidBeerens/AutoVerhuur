using System.Configuration;
using System.Data;
using System.Windows;

namespace AutoVerhuurProject.Presentatie.GebruikersGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e) {
            // Create the startup window
            LoginWindow wnd = new LoginWindow();

            // Do stuff here
            wnd.Title = "Klant inloggen";

            // Show the window
            wnd.Show();
        }
    }

}

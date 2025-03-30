using System.Configuration;
using System.Data;
using System.Windows;

namespace AutoVerhuurProject.Presentatie.GegevensGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e) {
            // Create the startup window
            GegevensApp wnd = new GegevensApp();

            // Do stuff here
            wnd.Title = "Applicatie gegevens invoeren";

            // Show the window
            wnd.Show();
        }
    }

}

using AutoVerhuurProject.Presentatie.GegevensApp;

namespace AutoVerhuurProject.StartUpGegevensApp;

internal class StartUp
{
    static void Main(string[] args)
    {
        GegevensApp app = new GegevensApp();
        app.Run();
    }
}

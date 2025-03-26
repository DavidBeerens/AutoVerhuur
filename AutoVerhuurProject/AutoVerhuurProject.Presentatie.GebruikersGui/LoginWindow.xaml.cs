using AutoVerhuurProject.Persistentie;
using System.Windows;
using System.Windows.Controls;

namespace AutoVerhuurProject.Presentatie.GebruikersGui;

/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow() {
        InitializeComponent();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
        // Lijst van klanten tonen

        KlantRepository klantRepo = new KlantRepository();
        foreach (var klant in klantRepo.GetByNaam(TxtBoxVoornaam.Text, TxtAchternaam.Text)) {
            TxtLijst.Text += klant.email;
        }
    }

    private void BtnInloggen_Click(object sender, RoutedEventArgs e) {
        // Inloggen
    }
}
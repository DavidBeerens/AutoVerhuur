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
        UpdateKlantenLijst();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
        // Lijst van klanten tonen
        UpdateKlantenLijst();
    }

    private void BtnInloggen_Click(object sender, RoutedEventArgs e) {
        // Inloggen
    }


    private void UpdateKlantenLijst() {
        TxtLijst.Text = "";

        KlantRepository klantRepo = new KlantRepository();
        foreach (var klant in klantRepo.GetByNaam(TxtBoxVoornaam.Text, TxtBoxAchternaam.Text)) {
            TxtLijst.Text +=
                klant.voornaam + " " + klant.achternaam + "\n" +
                klant.email + "\n" +
                klant.straat + "\n" +
                klant.postcode + " " + klant.woonplaats + "\n" +
                klant.land + "\n\n";
        }
    }
}
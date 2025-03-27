using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Domein.Models;
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

    private void LstKlanten_DoubleClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        //Inloggen
        ReservatieAanmakenWindow reservatieWnd = new ReservatieAanmakenWindow();
        reservatieWnd.Show();
    }



    private void UpdateKlantenLijst() {
        LstKlanten.Items.Clear();

        KlantRepository klantRepo = new KlantRepository();
        List<KlantDto> klanten = new List<KlantDto>();
        klanten = klantRepo.GetByNaam(TxtBoxVoornaam.Text, TxtBoxAchternaam.Text);

        foreach (var klant in klanten)
            LstKlanten.Items.Add(klant);
    }
}
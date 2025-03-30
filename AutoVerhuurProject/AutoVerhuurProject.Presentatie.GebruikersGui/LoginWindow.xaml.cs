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
    public KlantDto klant;

    public LoginWindow() {
        InitializeComponent();
        UpdateKlantenLijst();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
        // Lijst van klanten tonen
        UpdateKlantenLijst();
    }

    private void LstKlanten_DoubleClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        //controleren of er op een item werd geklikt
        var listBox = sender as ListBox;
        var positieGeklikt = e.GetPosition(listBox);
        var element = listBox.InputHitTest(positieGeklikt) as FrameworkElement;
        if (element != null) {
            var listBoxItem = ItemsControl.ContainerFromElement(listBox, element) as ListBoxItem;

            if (listBoxItem != null) {

                //Inloggen
                ReservatieAanmakenWindow reservatieWnd = new ReservatieAanmakenWindow((KlantDto)LstKlanten.SelectedItem);
                reservatieWnd.Show();
            }
        }
    }


    private void UpdateKlantenLijst() {
        LstKlanten.Items.Clear();

        KlantRepository klantRepo = new KlantRepository();
        List<KlantDto> klanten = new List<KlantDto>();
        klanten = klantRepo.GetByNaam(TxtBoxVoornaam.Text, TxtBoxAchternaam.Text);

        foreach (var klant in klanten)
            LstKlanten.Items.Add(klant);
    }

    private void BtnAutoOverzicht_Click(object sender, RoutedEventArgs e) {
        //Venster met auto overzichten openen
        OverzichtWindow autoWnd = new OverzichtWindow();
        autoWnd.Show();
    }

    private void BtnReservatieOverzicht_Click(object sender, RoutedEventArgs e) {
        //Venster met reservatie overzichten openen
        ReservatieOverzichtWindow resWnd = new ReservatieOverzichtWindow();
        resWnd.Show();
    }
}
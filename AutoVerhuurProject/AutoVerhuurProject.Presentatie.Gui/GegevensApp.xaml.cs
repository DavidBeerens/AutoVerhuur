using AutoVerhuurProject.Domein.CsvReaders;
using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Domein.Interfaces;
using AutoVerhuurProject.Domein.Models;
using AutoVerhuurProject.Persistentie;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace AutoVerhuurProject.Presentatie.GegevensGui;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class GegevensApp : Window
{
    public GegevensApp() {
        InitializeComponent();
    }


    private AutoReader autoReader = new AutoReader();
    private KlantReader klantReader = new KlantReader();
    private VestigingReader vestigingReader = new VestigingReader();

    private string geselecteerdBestand;

    private void BtnBladeren_Click(object sender, RoutedEventArgs e) {
        OpenFileDialog openFileDialog = new OpenFileDialog {
            Filter = "CSV files (*.csv)|*.csv",
            Title = "Selecter een CSV bestand"
        };
        if (openFileDialog.ShowDialog() == true)
            TextBoxPad.Text = openFileDialog.FileName;
    }

    private void RadioAutos_Checked(object sender, RoutedEventArgs e) {
        BtnInvoeren.IsEnabled = true;
    }

    private void RadioKlanten_Checked(object sender, RoutedEventArgs e) {
        BtnInvoeren.IsEnabled = true;
    }

    private void RadioVestigingen_Checked(object sender, RoutedEventArgs e) {
        BtnInvoeren.IsEnabled = true;
    }

    private void BtnInvoeren_Click(object sender, RoutedEventArgs e) {
        geselecteerdBestand = TextBoxPad.Text;

        if (geselecteerdBestand.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)) {
            try {
                if (RadioAutos.IsChecked == true)
                    AutosInvoeren();
                else if (RadioKlanten.IsChecked == true)
                    KlantenInvoeren();
                else if (RadioVestigingen.IsChecked == true)
                    VestigingenInvoeren();
            } catch {
                MessageBox.Show("Onverwachte fout.");
            }
        } else {
            MessageBox.Show("Geen CSV bestand!");
        }
    }


    private void AutosInvoeren() {
        var vestigingRepo = new VestigingRepository();
        List<string> luchthavens = vestigingRepo.GetLuchthavens();

        if (luchthavens.Count() > 0) {
            List<(Auto, int)> autos = autoReader.ReadCsv(geselecteerdBestand, luchthavens);


            List<(AutoDto, int)> autosDtos = autos.ConvertAll(autoTuple => (
                new AutoDto(
                    autoTuple.Item1.Nummerplaat,
                    autoTuple.Item1.Model,
                    autoTuple.Item1.Zitplaatsen,
                    autoTuple.Item1.Motortype,
                    autoTuple.Item1.Luchthaven
                ),
                autoTuple.Item2 // lijn nummer
            ));


            var autoRepo = new AutoRepository();
            autoRepo.VoegAutosToe(autosDtos);
            
            MessageBox.Show($"Aantal auto's toegevoegd: {autosDtos.Count()}", "Bevestiging", MessageBoxButton.OK, MessageBoxImage.Information);

            if (autoReader.errors.Count() > 0 || autoRepo.errors.Count() > 0)
                MaakErrorFile(geselecteerdBestand, autoReader.errors, autoRepo.errors);
            //errors en autos verwijderen om ze bij een volgende invoer van een bestand niet meer te hebben
            autoReader.errors.Clear();
            autoRepo.errors.Clear();
            autoReader.autos.Clear();
        } else {
            MessageBox.Show("Nog geen vestigingen.\nVoer eerst de vestigingen in!");
        }
    }

    private void KlantenInvoeren() {
        List<(Klant, int)> klanten = klantReader.ReadCsv(geselecteerdBestand);

        List<(KlantDto, int)> klantenDtos = klanten.ConvertAll(klantTuple => (
                new KlantDto(
                    klantTuple.Item1.Email,
                    klantTuple.Item1.Voornaam,
                    klantTuple.Item1.Achternaam,
                    klantTuple.Item1.Straat,
                    klantTuple.Item1.Postcode,
                    klantTuple.Item1.Woonplaats,
                    klantTuple.Item1.Land
                ),
                klantTuple.Item2 // lijn nummer
        ));

        var klantRepo = new KlantRepository();
        klantRepo.VoegKlantenToe(klantenDtos);

        MessageBox.Show($"Aantal klanten toegevoegd: {klantenDtos.Count()}", "Bevestiging", MessageBoxButton.OK, MessageBoxImage.Information);

        if (klantReader.errors.Count() > 0 || klantRepo.errors.Count() > 0)
            MaakErrorFile(geselecteerdBestand, klantReader.errors, klantRepo.errors);
        //errors en klanten verwijderen om ze bij een volgende invoer van een bestand niet meer te hebben
        klantReader.errors.Clear();
        klantRepo.errors.Clear();
        klantReader.klanten.Clear();
    }


    private void VestigingenInvoeren() {
        List<(Vestiging, int)> vestigingen = vestigingReader.ReadCsv(geselecteerdBestand);

        List<(VestigingDto, int)> vestigingenDtos = vestigingen.ConvertAll(vestigingTuple => (
                new VestigingDto(
                    vestigingTuple.Item1.Luchthaven,
                    vestigingTuple.Item1.Straat,
                    vestigingTuple.Item1.Postcode,
                    vestigingTuple.Item1.Plaats,
                    vestigingTuple.Item1.Land
                ),
                vestigingTuple.Item2 // lijn nummer
        ));

        var vestigingRepo = new VestigingRepository();
        vestigingRepo.VoegVestigingenToe(vestigingenDtos);

        MessageBox.Show($"Aantal vestigingen toegevoegd: {vestigingenDtos.Count()}", "Bevestiging", MessageBoxButton.OK, MessageBoxImage.Information);

        if (vestigingReader.errors.Count() > 0 || vestigingRepo.errors.Count() > 0)
            MaakErrorFile(geselecteerdBestand, vestigingReader.errors, vestigingRepo.errors);
        //errors en vestigingen verwijderen om ze bij een volgende invoer van een bestand niet meer te hebben
        vestigingReader.errors.Clear();
        vestigingRepo.errors.Clear();
        vestigingReader.vestigingen.Clear();
    }


    private void MaakErrorFile(string pad, List<string> errors1, List<string> errors2) {
        List<string> errors = new List<string>(errors1);
        errors.AddRange(errors2);
        errors = errors.OrderBy(error => {
            var match = System.Text.RegularExpressions.Regex.Match(error, @"Lijn (\d+):");
            return match.Success ? int.Parse(match.Groups[1].Value) : int.MaxValue;
        }).ToList();

        using (StreamWriter writer = new StreamWriter("errors.csv", true)) {
            writer.WriteLine($"Errors in bestand {pad}");

            foreach (string error in errors)
                writer.WriteLine(error);

            writer.WriteLine();
        }


        if (MessageBox.Show("Er zijn errors gevonden.\nWil je het error bestand openen?", "Error bestand aangemaakt",
    MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes) {
            // Bestand openen met default programma
            Process.Start(new ProcessStartInfo {
                FileName = "errors.csv",
                UseShellExecute = true // Needed for .NET Core / .NET 5+
            });
        }
    }
}
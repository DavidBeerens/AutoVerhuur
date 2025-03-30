using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Persistentie;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoVerhuurProject.Presentatie.GebruikersGui
{
    /// <summary>
    /// Interaction logic for OverzichtWindow.xaml
    /// </summary>
    public partial class OverzichtWindow : Window
    {
        public OverzichtWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 24; ++i)
                Tijd.Items.Add(i.ToString("00") + ":00");

            VestigingRepository vestigingRepo = new VestigingRepository();
            List<string> luchthavens = vestigingRepo.GetLuchthavens();
            foreach (string v in luchthavens)
                ComboBoxLuchthavens.Items.Add(v);
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (Datum.SelectedDate.HasValue && Tijd.SelectedIndex != -1 && ComboBoxLuchthavens.SelectedIndex != -1) {
                LstBeschikbareAutos.Items.Clear();

                AutoRepository autoRepo = new AutoRepository();
                List<AutoDto> autos = autoRepo.GetBeschikbareAutos(ComboBoxLuchthavens.SelectedItem.ToString(), Datum.SelectedDate.Value.AddHours(Tijd.SelectedIndex));
                foreach (var auto in autos)
                    LstBeschikbareAutos.Items.Add(auto);
            }
        }

        private void BtnMaakAscii_Click(object sender, RoutedEventArgs e) {
            if (LstBeschikbareAutos.HasItems) {
                using (StreamWriter writer = new StreamWriter("overzicht.asciidoc", false)) {
                    writer.WriteLine("= Overzicht auto's\n");



                    writer.WriteLine($"Vestiging: {ComboBoxLuchthavens.SelectedItem}\n");
                    writer.WriteLine($"Datum: {(Datum.SelectedDate.Value).ToLongDateString()} - {Tijd.SelectedIndex.ToString("00") + ":00"}\n");
                    foreach (var v in LstBeschikbareAutos.Items)
                        if (v is AutoDto auto) {
                            ReserveringRepository reservatieRepo = new ReserveringRepository();
                            bool heeftVorige, heeftVolgende;
                            (ReservatieDto, KlantDto) resVorige = (null, null), resVolgende = (null, null);

                            try {
                                resVorige = reservatieRepo.GetLaatsteReservatie(auto);
                                heeftVorige = true;
                                } catch {
                                    heeftVorige = false;
                                }
                            try {
                                resVolgende = reservatieRepo.GetVolgendeReservatie(auto);
                                heeftVolgende = true;
                            } catch {
                                heeftVolgende = false;
                            }



                            writer.WriteLine($".{auto.nummerplaat} - {auto.model}");
                            writer.WriteLine("[%autowidth]");
                            writer.WriteLine("|===");

                            writer.WriteLine("|vorige reservatie |volgende reservatie\n");

                            if (heeftVorige) {
                                writer.WriteLine($"|Klant: {resVorige.Item2.voornaam} {resVorige.Item2.achternaam} +");
                                writer.WriteLine($"{resVorige.Item2.straat} +");
                                writer.WriteLine($"{resVorige.Item2.postcode} {resVorige.Item2.woonplaats} +");
                                writer.WriteLine($"{resVorige.Item2.land}\n");
                            }else {
                                writer.WriteLine("|Geen vorige reservatie\n");
                            }

                            if (heeftVolgende) {
                                writer.WriteLine($"|Klant: {resVolgende.Item2.voornaam} {resVolgende.Item2.achternaam} +");
                                writer.WriteLine($"{resVolgende.Item2.straat} +");
                                writer.WriteLine($"{resVolgende.Item2.postcode} {resVolgende.Item2.woonplaats} +");
                                writer.WriteLine($"{resVolgende.Item2.land}\n");
                            } else {
                                writer.WriteLine("|Geen vorige reservatie\n");
                            }


                            if (heeftVorige) {
                                writer.WriteLine($"|Starttijd: {resVorige.Item1.startTijdstip} +");
                                writer.WriteLine($"Eindtijd: {resVorige.Item1.eindTijdstip}");
                            } else {
                                writer.WriteLine("|\n");
                            }

                            if (heeftVolgende) {
                                writer.WriteLine($"|Starttijd: {resVolgende.Item1.startTijdstip} +");
                                writer.WriteLine($"Eindtijd: {resVolgende.Item1.eindTijdstip}");
                            }
                            
                            writer.WriteLine("|===\n\n");
                        }
                }
                if (MessageBox.Show("Asciidoc van overzicht aangemaakt.\nWile je het bestand openen?", "Overzicht aangemaakt",
                    MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes) {
                    // Bestand openen met default programma
                    Process.Start(new ProcessStartInfo {
                        FileName = "overzicht.asciidoc",
                        UseShellExecute = true // Needed for .NET Core / .NET 5+
                    });
                }
            }
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}

using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Domein.Models;
using AutoVerhuurProject.Persistentie;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ReservatieAanmakenWindow.xaml
    /// </summary>
    public partial class ReservatieAanmakenWindow : Window
    {
        private TimeOnly beginTijd = new TimeOnly(0, 0, 0);
        private TimeOnly eindTijd = new TimeOnly(0, 0, 0);
        private DateTime beginDatum = new DateTime();
        private DateTime eindDatum = new DateTime();

        private KlantDto klant;

        public ReservatieAanmakenWindow(KlantDto ingelogdeKlant) {
            InitializeComponent();

            klant = ingelogdeKlant;

            VestigingRepository vestigingRepo = new VestigingRepository();
            List<string> luchthavens = vestigingRepo.GetLuchthavens();
            foreach (string v in luchthavens) {
                ComboBoxLuchthavens.Items.Add(v);
                ComboBoxRetour.Items.Add(v);
            }

            ToonAutoLijst("%", 0);

            //ComboboxItems van tijdstippen invullen
            for (int i = 0; i < 24; ++i) {
                BeginTijd.Items.Add(i.ToString("00") + ":00 - " + (i + 1).ToString("00") + ":00");
                EindTijd.Items.Add(i.ToString("00") + ":00 -" + (i + 1).ToString("00") + ":00");
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            AantalZitplaatsen.Text = SliderZitplaatsen.Value switch {
                0 => "alle",
                1 => "2",
                2 => "3",
                3 => "4",
                4 => "5",
                5 => "6",
                6 => "7"
            };

            ToonAutoLijst((ComboBoxLuchthavens.SelectedValue == null ? "%" : ComboBoxLuchthavens.SelectedValue.ToString()), (int)SliderZitplaatsen.Value);
        }

        private void StartDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            UpdateDatums();
        }

        private void EindDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            UpdateDatums();
        }

        private void ComboBoxLuchthavens_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ToonAutoLijst((ComboBoxLuchthavens.SelectedIndex == -1 ? "%" : ComboBoxLuchthavens.SelectedValue.ToString()), (int)SliderZitplaatsen.Value);
        }



        private void Button_Click(object sender, RoutedEventArgs e) {
            ComboBoxLuchthavens.SelectedIndex = -1;
            SliderZitplaatsen.Value = 0;
            StartDatum.SelectedDate = DateTime.Today;
            EindDatum.SelectedDate = new DateTime();
            BeginTijd.SelectedIndex = -1;
            EindTijd.SelectedIndex = -1;
        }

        private void BeginTijd_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            beginTijd = new TimeOnly((BeginTijd.SelectedIndex == -1) ? 0 : BeginTijd.SelectedIndex, 0, 0);
            UpdateDatums();
        }
        private void EindTijd_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (EindTijd.SelectedIndex != 23)
                eindTijd = new TimeOnly((EindTijd.SelectedIndex == -1) ? 0 : EindTijd.SelectedIndex + 1, 0, 0);
            else //zet de tijd op 00:00:0 er zal een dag aan de einddatum worden toegevoegd in UpdateDatums
                eindTijd = new TimeOnly(0, 0, 0);

            UpdateDatums();
        }


        private void ToonAutoLijst(string luchthaven, int zitplaatsen) {
            LstAutos.Items.Clear();

            AutoRepository autoRepo = new AutoRepository();
            List<AutoDto> autos = new List<AutoDto>();
            autos = autoRepo.GetBeschikbareAutos(luchthaven, zitplaatsen + 1, beginDatum, eindDatum);


            if (autos.Count() == 0)
                GeenAutosGevonden.Text = "Geen auto's gevonden!";
            else {
                GeenAutosGevonden.Text = "";

                foreach (var auto in autos)
                    LstAutos.Items.Add(auto);
            }
        }



        private void UpdateDatums() {
            //Datums inclusief tijden instellen
            beginDatum = StartDatum.SelectedDate.Value.Date + beginTijd.ToTimeSpan();
            if (EindDatum.SelectedDate.HasValue)
                eindDatum = EindDatum.SelectedDate.Value.Date + eindTijd.ToTimeSpan();

            if (eindTijd == new TimeOnly(0, 0, 0)) //dag toevoegen aan eindtijd aangezien er wordt teruggebracht tussen 23:00 en 00:00
                eindDatum = eindDatum.AddDays(1);


            //begingrens van einddatum instellen
            if (StartDatum.SelectedDate.HasValue) {
                DateTime nextDay = StartDatum.SelectedDate.Value.AddDays(1);
                EindDatum.DisplayDateStart = nextDay;

                //reset einddatum als begindatum verandert naar een verdere datum
                if (EindDatum.SelectedDate < nextDay) {
                    EindDatum.SelectedDate = null;
                }
            }
                

            //attributen enablen/disablen
            if (StartDatum.SelectedDate.HasValue) {
                BeginTijd.IsEnabled = true;
                EindDatum.IsEnabled = true;
            } else {
                BeginTijd.IsEnabled = false;
                EindDatum.IsEnabled = false;
            }
            if (EindDatum.SelectedDate.HasValue) {
                EindTijd.IsEnabled = true;
            } else {
                EindTijd.IsEnabled = false;
            }


            ToonAutoLijst((ComboBoxLuchthavens.SelectedValue == null ? "%" : ComboBoxLuchthavens.SelectedValue.ToString()), (int)SliderZitplaatsen.Value);
        }

        private void LstAutos_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            //controleren of er op een item werd geklikt
            var listBox = sender as ListBox;
            var positieGeklikt = e.GetPosition(listBox);
            var element = listBox.InputHitTest(positieGeklikt) as FrameworkElement;
            if (element != null) {
                var listBoxItem = ItemsControl.ContainerFromElement(listBox, element) as ListBoxItem;

                if (listBoxItem != null) {





                    //Reservatie aanmaken
                    if (BeginTijd.SelectedIndex == -1 || EindTijd.SelectedIndex == -1)
                        MessageBox.Show("Selecteer een begin- en eindtijdstip.");
                    else if ((eindDatum - beginDatum).TotalHours < 24)
                        MessageBox.Show("Reservatie moet minstens 24 uur zijn.");
                    else if (ComboBoxRetour.SelectedIndex == -1)
                        MessageBox.Show("Selecteer een retourluchthaven.");
                    else {
                        //reservatie toevoegen

                        var reservatieRepo = new ReserveringRepository();
                        if (reservatieRepo.VoegReservatieToe(klant, (AutoDto)LstAutos.SelectedItem, ComboBoxRetour.SelectedItem.ToString(), beginDatum, eindDatum))
                            LstAutos.Items.Remove(LstAutos.SelectedItem);
                    }
                }
            }
        }
    }
}

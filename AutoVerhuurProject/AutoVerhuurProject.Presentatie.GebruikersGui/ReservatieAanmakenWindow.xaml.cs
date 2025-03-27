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

        public ReservatieAanmakenWindow() {
            InitializeComponent();
            VestigingRepository vestigingRepo = new VestigingRepository();
            List<string> luchthavens = vestigingRepo.GetLuchthavens();
            foreach (string v in luchthavens)
                ComboBoxLuchthavens.Items.Add(v);

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
        }

        private void BeginTijd_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            beginTijd = new TimeOnly(BeginTijd.SelectedIndex, 0, 0);
            UpdateDatums();
        }
        private void EindTijd_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            eindTijd = new TimeOnly(EindTijd.SelectedIndex + 1, 0, 0);
            UpdateDatums();
        }


        private void ToonAutoLijst(string luchthaven, int zitplaatsen) {
            LstAutos.Items.Clear();

            AutoRepository autoRepo = new AutoRepository();
            List<AutoDto> autos = new List<AutoDto>();
            autos = autoRepo.GetBeschikbareAutos(luchthaven, zitplaatsen + 1);


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



        }

        private void LstAutos_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if ((eindDatum - beginDatum).TotalHours < 24) {
                MessageBox.Show("Reservatie moet minstens 24 uur zijn.");
            } else {

            }
        }
    }
}

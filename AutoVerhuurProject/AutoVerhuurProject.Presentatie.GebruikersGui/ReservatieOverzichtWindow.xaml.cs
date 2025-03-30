using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Domein.Models;
using AutoVerhuurProject.Persistentie;
using Microsoft.Identity.Client;
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
    /// Interaction logic for ReservatieOverzichtWindow.xaml
    /// </summary>
    public partial class ReservatieOverzichtWindow : Window
    {
        public ReservatieOverzichtWindow() {
            InitializeComponent();
            for (int i = 0; i < 24; ++i)
                ComboTijd.Items.Add(i.ToString("00") + ":00");

            VestigingRepository vestigingRepo = new VestigingRepository();
            List<string> luchthavens = vestigingRepo.GetLuchthavens();
            foreach (string v in luchthavens)
                ComboLuchthavens.Items.Add(v);

            UpdateLst();
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e) {
            if (LstReserveringen.SelectedItem is ReservatieViewModel selectedRes) {
                ReserveringRepository resRepo = new ReserveringRepository();
                resRepo.DeleteById(selectedRes.Reservatie.reservatieId);

                LstReserveringen.Items.Remove(selectedRes);
            }
        }

        private void UpdateTxt(object sender, TextChangedEventArgs e) {
            UpdateLst();
        }

        private void UpdateCombo(object sender, SelectionChangedEventArgs e) {
            UpdateLst();
        }

        
        private void UpdateLst() {
            DateTime? datum = null;

            if (DatePickerDatum.SelectedDate.HasValue) {
                ComboTijd.IsEnabled = true;
                datum = DatePickerDatum.SelectedDate.Value.AddHours((ComboTijd.SelectedIndex == -1) ? 0 : ComboTijd.SelectedIndex);
            } else
                ComboTijd.IsEnabled = false;

            LstReserveringen.Items.Clear();

            ReserveringRepository resRepo = new ReserveringRepository();
            List<(ReservatieDto, AutoDto, KlantDto)> lijst = resRepo.GetResevaties(TxtVoornaam.Text, TxtAchternaam.Text,
                 datum, (ComboLuchthavens.SelectedIndex == -1) ? "%" : ComboLuchthavens.SelectedItem.ToString());


            foreach (var v in lijst)
                LstReserveringen.Items.Add(new ReservatieViewModel(v.Item1, v.Item2, v.Item3));
        }

        private void LstReserveringen_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            BtnAnnuleren.IsEnabled = (LstReserveringen.SelectedIndex != -1);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e) {
            TxtVoornaam.Text = "";
            TxtAchternaam.Text = "";
            ComboLuchthavens.SelectedIndex = -1;
            DatePickerDatum.SelectedDate = null;
            ComboTijd.SelectedIndex = -1;
        }
    }
}

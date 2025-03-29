using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Persistentie;
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
    }
}

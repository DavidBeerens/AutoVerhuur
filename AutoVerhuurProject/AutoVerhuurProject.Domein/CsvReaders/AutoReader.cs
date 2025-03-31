using AutoVerhuurProject.Domein.Models;
using System.Windows.Markup;

namespace AutoVerhuurProject.Domein.CsvReaders;

internal class AutoReader
{
    public List<(Auto, int)> autos = new List<(Auto, int)>();
    public List<string> errors = new List<string>();

    public List<(Auto, int)> ReadCsv(string bestandpad, List<string> luchthavens) {
        List<string[]> data = new List<string[]>();

        try {
            using (StreamReader reader = new StreamReader(bestandpad)) {

                string header = reader.ReadLine();
                if (header == "Nummerplaat;Model;Zitplaatsen;Motortype") {

                    for (int i = 2; !reader.EndOfStream; ++i) {
                        string lijn = reader.ReadLine();

                        if (!string.IsNullOrWhiteSpace(lijn)) {
                            string[] waarden = lijn.Split(';');

                            if (waarden.Length == 4) {
                                string nummerplaat = waarden[0];
                                string model = waarden[1];
                                if (!int.TryParse(waarden[2], out int zitplaatsen)) {
                                    AddError(i, "Zitplaatsen is geen nummer.");
                                    continue;
                                }
                                if (!Enum.TryParse(waarden[3], out MotorTypes motortype)) {
                                    AddError(i, "Motortype is geen geldige waarde.");
                                }


                                int randomLuchthaven = (new Random()).Next(luchthavens.Count());
                                string luchthaven = luchthavens[randomLuchthaven];

                                try {
                                    autos.Add((new Auto(nummerplaat, model, zitplaatsen, motortype, luchthaven), i));
                                } catch (Exception ex) {
                                    AddError(i, ex.Message);
                                }
                            } else {
                                AddError(i, "Geen juist aantal gegevens.");
                            }
                        }
                    }
                } else {
                    errors.Add("Incorrect bestand.");
                }
            }
        } catch (Exception ex) {
            errors.Add("Fout bij inlezen van bestand. " +  ex.Message);
        }


        return autos;
    }

    private void AddError(int lijn, string message) {
        errors.Add($"Lijn {lijn}: {message}");
    }
}
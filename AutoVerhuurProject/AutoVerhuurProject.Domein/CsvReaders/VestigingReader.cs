using AutoVerhuurProject.Domein.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutoVerhuurProject.Domein.CsvReaders;

internal class VestigingReader
{
    public List<(Vestiging, int)> vestigingen = new List<(Vestiging, int)>();
    public List<string> errors = new List<string>();

    public List<(Vestiging, int)> ReadCsv(string bestandpad) {
        if (!File.Exists(bestandpad))
            throw new FileNotFoundException(bestandpad, "niet gevonden");


        List<string[]> data = new List<string[]>();

        try {
            using (StreamReader reader = new StreamReader(bestandpad)) {

                string header = reader.ReadLine();
                if (header == "Luchthaven;Straat;Postcode;Plaats;Land") {

                    for (int i = 2; !reader.EndOfStream; ++i) {
                        string lijn = reader.ReadLine();

                        if (!string.IsNullOrWhiteSpace(lijn)) {
                            string[] waarden = lijn.Split(';');

                            if (waarden.Length == 5) {
                                string luchthaven = waarden[0];
                                string straat = waarden[1];
                                string postcode = waarden[2];
                                string plaats = waarden[3];
                                string land = waarden[4];

                                try {
                                    vestigingen.Add((new Vestiging(luchthaven, straat, postcode, plaats, land), i));
                                } catch (Exception ex) {
                                    AddError(i, ex.Message);
                                }
                            } else {
                                AddError(i, "Geen juist aantal gegevens");
                            }
                        }
                    }
                } else {
                    errors.Add("Incorrect bestand.");
                }
            }
        } catch (Exception ex) {
            errors.Add($"Fout bij inlezen van bestand. {ex.Message}");
        }


        return vestigingen;
    }
    private void AddError(int lijn, string message) {
        errors.Add($"Lijn {lijn}: {message}");
    }
}

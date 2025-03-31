using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.CsvReaders;

internal class KlantReader
{
    public List<(Klant, int)> klanten = new List<(Klant, int)>();
    public List<string> errors = new List<string>();

    public List<(Klant, int)> ReadCsv(string bestandpad) {
        List<string[]> data = new List<string[]>();

        try {
            using (StreamReader reader = new StreamReader(bestandpad)) {

                string header = reader.ReadLine();
                if (header == "Voornaam;Achternaam;Email;Straat;Postcode;Woonplaats;Land") {

                    for (int i = 2; !reader.EndOfStream; ++i) {
                        string lijn = reader.ReadLine();

                        if (!string.IsNullOrWhiteSpace(lijn)) {
                            string[] waarden = lijn.Split(';');

                            if (waarden.Length == 7) {
                                string voornaam = waarden[0];
                                string achternaam = waarden[1];
                                string email = waarden[2];
                                string straat = waarden[3];
                                string postcode = waarden[4];
                                string woonplaats = waarden[5];
                                string land = waarden[6];

                                try {
                                    klanten.Add((new Klant(email, voornaam, achternaam, straat, postcode, woonplaats, land), i));
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
            errors.Add($"Fout bij inlezen van bestand. {ex.Message}");
        }



        return klanten;
    }
    private void AddError(int lijn, string message) {
        errors.Add($"Lijn {lijn}: {message}");
    }
}

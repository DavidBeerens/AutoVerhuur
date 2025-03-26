using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.Factories;

internal class KlantFactory
{
    internal static Klant CreateNewKlant(string email, string voornaam, string achternaam, string straat, string postcode, string woonplaats, string land) {
        return new Klant(email, voornaam, achternaam, straat, postcode, woonplaats, land);
    }
}

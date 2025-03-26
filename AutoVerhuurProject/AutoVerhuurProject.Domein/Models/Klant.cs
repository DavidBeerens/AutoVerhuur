namespace AutoVerhuurProject.Domein.Models;

internal class Klant
{
    public string Email { get; }
    public string Voornaam { get; }
    public string Achternaam { get; }
    public string Straat { get; }
    public string Postcode { get; }
    public string Woonplaats { get; }
    public string Land { get; }


    //om te controleren of een email adres nog niet bestaat
    private static HashSet<string> geregistreerdeEmails = new();

    public Klant(string email, string voornaam, string achternaam, string straat, string postcode, string woonplaats, string land) {
        if (String.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email moet ingevuld zijn.");
        else if (geregistreerdeEmails.Contains(email))
            throw new ArgumentException("Email is al geregistreerd.");
        else
            Email = email;

        Voornaam =
        !String.IsNullOrWhiteSpace(voornaam)
        ? voornaam
        : throw new ArgumentException("Voornaam moet ingevuld zijn.");

        Achternaam =
        !String.IsNullOrWhiteSpace(achternaam)
        ? achternaam
        : throw new ArgumentException("Achternaam moet ingevuld zijn.");

        Straat = straat;
        Postcode = postcode;
        Woonplaats = woonplaats;
        Land = land;


        geregistreerdeEmails.Add(email);
    }
}

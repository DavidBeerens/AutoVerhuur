namespace AutoVerhuurProject.Domein.Models; 
internal class Auto
{
    public string Nummerplaat { get; }

    public string Model { get; }

    public int Zitplaatsen { get; }

    public MotorTypes Motortype { get; }

    public string Luchthaven { get; }


    //om te controleren of een nummerplaat al bestaat
    private static HashSet<string> geregistreerdeNummerPlaten = new();



    public Auto(string nummerplaat, string model, int zitplaatsen, MotorTypes motortype, string luchthaven) {
        Model =
        !String.IsNullOrWhiteSpace(model)
        ? model
        : throw new ArgumentException("Model moet ingevuld zijn.");

        Zitplaatsen =
        zitplaatsen >= 2
        ? zitplaatsen
        : throw new ArgumentException("Zitplaatsen moet minstens 2 zijn.");

        Nummerplaat = 
        String.IsNullOrWhiteSpace(nummerplaat)
        ? throw new ArgumentException("Nummerplaat moet ingevuld zijn.")
        : !geregistreerdeNummerPlaten.Contains(nummerplaat)
        ? nummerplaat
        : throw new ArgumentException("Nummerplaat bestaat al.");
        Motortype = motortype;

        Luchthaven = luchthaven;



        geregistreerdeNummerPlaten.Add(nummerplaat);
    }


}

    public enum MotorTypes
    {
        Benzine,
        Diesel,
        Hybride,
        Elektrisch
    }
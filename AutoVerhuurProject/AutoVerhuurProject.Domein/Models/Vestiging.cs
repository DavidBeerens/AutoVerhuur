using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace AutoVerhuurProject.Domein.Models;

internal class Vestiging {
    public string Luchthaven { get; }

    public string Straat { get; }

    public string Postcode { get; }

    public string Plaats { get; }

    public string Land { get; }

    private static HashSet<string> geregistreerdeLuchthavens = new();



    public Vestiging(string luchthaven, string straat, string postcode, string plaats, string land) {
        Luchthaven =
        !IsIngevuld(luchthaven)
        ? throw new ArgumentException("Luchthaven moet ingevuld zijn.")
        : geregistreerdeLuchthavens.Contains(luchthaven)
        ? throw new ArgumentException("Luchthaven bestaat al.")
        : luchthaven;

        Straat =
        IsIngevuld(straat)
        ? straat
        : throw new ArgumentException("Straat moet ingevuld zijn.");

        Postcode =
        IsIngevuld(postcode)
        ? postcode
        : throw new ArgumentException("Postcode moet ingevuld zijn.");

        Plaats =
        IsIngevuld(plaats)
        ? plaats
        : throw new ArgumentException("Plaats moet ingevuld zijn.");

        Land =
        IsIngevuld(land)
        ? land
        : throw new ArgumentException("Land moet ingevuld zijn.");



        geregistreerdeLuchthavens.Add(luchthaven);
    }

    private bool IsIngevuld(string text) {
        return !String.IsNullOrWhiteSpace(text);
    }
}
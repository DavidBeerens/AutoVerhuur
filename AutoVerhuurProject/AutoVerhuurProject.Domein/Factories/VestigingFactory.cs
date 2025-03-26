using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.Factories;

internal class VestigingFactory
{
    internal static Vestiging CreateNewVestiging(string luchthaven, string straat, string postcode, string plaats, string land) {
        return new Vestiging(luchthaven, straat, postcode, plaats, land);
    }
}

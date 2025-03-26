using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.Factories;

internal class ReservatieFactory
{
    internal static Reservatie CreateNewReservatie(string klantEmail, string autoNummerplaat, string retourLuchthaven, DateTime startTijdStip, DateTime eindTijstip) {
        return new Reservatie(Guid.NewGuid(), klantEmail, autoNummerplaat, retourLuchthaven, startTijdStip, eindTijstip);
    }
}

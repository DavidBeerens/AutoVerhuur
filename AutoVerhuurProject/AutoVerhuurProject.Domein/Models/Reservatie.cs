namespace AutoVerhuurProject.Domein.Models;

internal class Reservatie (Guid reservatieId, string klantEmail, string autoNummerplaat, string retourLuchthaven, DateTime startTijdStip, DateTime eindTijstip)
{
    public Guid ReservatieId { get; } = reservatieId;

    public string KlantEmail { get; } =
    !String.IsNullOrWhiteSpace(klantEmail)
    ? klantEmail
    : throw new ArgumentException("Email moet ingevuld zijn.");

    public string AutoNummerplaat { get; } =
    !String.IsNullOrWhiteSpace(autoNummerplaat)
    ? autoNummerplaat
    : throw new ArgumentException("Nummerplaat moet ingevuld zijn.");

    public string RetourLuchthaven { get; } =
    !String.IsNullOrWhiteSpace(retourLuchthaven)
    ? retourLuchthaven
    : throw new ArgumentException("VestigingsID moet ingevuld zijn.");

    public DateTime StartTijdStip { get; } =
    startTijdStip > DateTime.Now
    ? startTijdStip
    : throw new ArgumentException("Huurperiode moet starten in de toekomst.");

    public DateTime EindTijdstip { get; } =
    startTijdStip >= eindTijstip
    ? throw new ArgumentException("Eindtijdstip moet na begintijdstip liggen.")
    : (eindTijstip - startTijdStip).TotalDays < 1
    ? throw new ArgumentException("Verhuurperiode moet minstens 1 dag zijn.")
    : eindTijstip;
}

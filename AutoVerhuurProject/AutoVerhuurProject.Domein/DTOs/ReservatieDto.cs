namespace AutoVerhuurProject.Domein.DTOs;

public record ReservatieDto(Guid reservatieId, string klantEmail, string autoNummerplaat, string retourLuchthaven, DateTime startTijdStip, DateTime eindTijstip);
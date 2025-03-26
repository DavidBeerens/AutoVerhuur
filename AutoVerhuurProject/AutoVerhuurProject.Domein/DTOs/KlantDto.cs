namespace AutoVerhuurProject.Domein.DTOs;

public record KlantDto(string email, string voornaam, string achternaam, string straat, string postcode, string woonplaats, string land);

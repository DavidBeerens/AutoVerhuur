using AutoVerhuurProject.Domein.DTOs;

namespace AutoVerhuurProject.Presentatie.GebruikersGui;

public class ReservatieViewModel
{
    public ReservatieViewModel(ReservatieDto item1, AutoDto item2, KlantDto item3) {
        Reservatie = item1;
        Auto = item2;
        Klant = item3;
    }

    public ReservatieDto Reservatie { get; set; }
    public AutoDto Auto { get; set; }
    public KlantDto Klant { get; set; }
}

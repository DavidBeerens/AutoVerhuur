using AutoVerhuurProject.Domein.DTOs;

namespace AutoVerhuurProject.Domein.Interfaces;

public interface IAutorepositoryRead
{
    AutoDto? GetByNummerplaat(string nummerplaat);
    //IEnumerable<AutoDto> GetAll();
}

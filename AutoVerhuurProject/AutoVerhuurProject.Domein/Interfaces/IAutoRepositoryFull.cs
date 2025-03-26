using AutoVerhuurProject.Domein.DTOs;

namespace AutoVerhuurProject.Domein.Interfaces;

public interface IAutoRepositoryFull : IAutorepositoryRead
{
    void Add(AutoDto auto);
}

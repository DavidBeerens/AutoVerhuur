using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.Factories;

internal static class AutoFactory
{
    internal static AutoDto ConvertToAutoDto(Auto auto) {
        return new AutoDto(auto.Nummerplaat, auto.Model, auto.Zitplaatsen, auto.Motortype, auto.Luchthaven);
    }
}

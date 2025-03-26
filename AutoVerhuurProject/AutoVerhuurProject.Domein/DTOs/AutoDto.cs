using AutoVerhuurProject.Domein.Models;

namespace AutoVerhuurProject.Domein.DTOs;

public record AutoDto(string nummerplaat, string model, int zitplaatsen, MotorTypes motortype, string luchthaven);
﻿using AutoVerhuurProject.Domein.DTOs;
using Microsoft.Data.SqlClient;

namespace AutoVerhuurProject.Persistentie;

public class KlantRepository
{
    private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoVerhuurDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
    public List<string> errors = new List<string>();

    public List<string> VoegKlantenToe(List<(KlantDto auto, int lijnNummer)> klantDtos) {
        using (SqlConnection connection = new(connectionString)) {
            connection.Open();

            string query = "IF NOT EXISTS (SELECT 1 FROM Klanten WHERE Email = @Email) " +
                "INSERT INTO Klanten (Email, Voornaam, Achternaam, Straat, Postcode, Woonplaats, Land) " +
                "VALUES (@Email, @Voornaam, @Achternaam, @Straat, @Postcode, @Woonplaats, @Land);";


            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Voornaam", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Achternaam", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Straat", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Postcode", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Woonplaats", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Land", System.Data.SqlDbType.VarChar);

                
                foreach (var (klant, lijnNummer) in klantDtos) {
                    command.Parameters["@Email"].Value = klant.email;
                    command.Parameters["@Voornaam"].Value = klant.voornaam;
                    command.Parameters["@Achternaam"].Value = klant.achternaam;
                    command.Parameters["@Straat"].Value = klant.straat;
                    command.Parameters["@Postcode"].Value = klant.postcode;
                    command.Parameters["@Woonplaats"].Value = klant.woonplaats;
                    command.Parameters["@Land"].Value = klant.land;

                    if (command.ExecuteNonQuery() == -1)
                        errors.Add($"Lijn {lijnNummer}: Email is al geregistreerd.");
                }
            }
        }
        return errors;
    }


    public List<KlantDto> GetByNaam(string voornaam, string achternaam) {
        List<KlantDto> results = new List<KlantDto>();

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        const string query = "SELECT * FROM Klanten " +
            "WHERE Voornaam LIKE CAST(@Voornaam AS NVARCHAR) AND Achternaam LIKE CAST(@Achternaam AS NVARCHAR);";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Voornaam", voornaam + "%");
        command.Parameters.AddWithValue("@Achternaam", achternaam + "%");

        using var reader = command.ExecuteReader();


        while (reader.Read()) {
                var Klant = new KlantDto(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6)
                );

                results.Add(Klant);

        }
        return results;
    }
}

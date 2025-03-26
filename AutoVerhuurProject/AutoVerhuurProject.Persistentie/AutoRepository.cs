using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Domein.Interfaces;
using Microsoft.Data.SqlClient;

namespace AutoVerhuurProject.Persistentie;

internal class AutoRepository : IAutoRepositoryFull
{
    private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoVerhuurDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
    public List<string> errors = new List<string>();

    public List<string> VoegAutosToe(List<(AutoDto auto, int lijnNummer)> autoDtos) {
        using (SqlConnection connection = new(connectionString)) {
            connection.Open();

            string query = "IF NOT EXISTS (SELECT 1 FROM Autos WHERE Nummerplaat = @Nummerplaat) " +
                "INSERT INTO Autos (Nummerplaat, Model, Zitplaatsen, Motortype, Luchthaven) " +
                "VALUES (@Nummerplaat, @Model, @Zitplaatsen, @Motortype, @Luchthaven);";


            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.Add("@Nummerplaat", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Model", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Zitplaatsen", System.Data.SqlDbType.TinyInt);
                command.Parameters.Add("@Motortype", System.Data.SqlDbType.TinyInt);
                command.Parameters.Add("@Luchthaven", System.Data.SqlDbType.VarChar);


                foreach (var (auto, lijnNummer) in autoDtos) {
                    command.Parameters["@Nummerplaat"].Value = auto.nummerplaat;
                    command.Parameters["@Model"].Value = auto.model;
                    command.Parameters["@Zitplaatsen"].Value = auto.zitplaatsen;
                    command.Parameters["@Motortype"].Value = auto.motortype.ToString() switch {
                        "Benzine" => 1,
                        "Diesel" => 2,
                        "Hybride" => 3,
                        "Elektrisch" => 4
                    };
                    command.Parameters["@Luchthaven"].Value = auto.luchthaven;

                    if (command.ExecuteNonQuery() == -1)
                        errors.Add($"Lijn {lijnNummer}: Nummerplaat bestaat al.");
                }
            }
        }
        return errors;
    }




    public void Add(AutoDto auto) {
        throw new NotImplementedException();
    }

    public AutoDto? GetByNummerplaat(string nummerplaat) {
        throw new NotImplementedException();
    }
}

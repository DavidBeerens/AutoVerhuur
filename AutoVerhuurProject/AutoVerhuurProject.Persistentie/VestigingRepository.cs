using AutoVerhuurProject.Domein.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Markup;

namespace AutoVerhuurProject.Persistentie;

public class VestigingRepository
{
    private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoVerhuurDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
    public List<string> errors = new List<string>();

    public void VoegVestigingenToe(List<(VestigingDto vestiging, int lijnNummer)> vestigingDtos) {
        using (SqlConnection connection = new(connectionString)) {
            connection.Open();

            string query = "IF NOT EXISTS (SELECT 1 FROM Vestigingen WHERE Luchthaven = @Luchthaven) " +
                "INSERT INTO Vestigingen (Luchthaven, Straat, Postcode, Plaats, Land) " +
                "VALUES (@Luchthaven, @Straat, @Postcode, @Plaats, @Land);";


            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.Add("@Luchthaven", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Straat", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Postcode", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Plaats", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@Land", System.Data.SqlDbType.VarChar);


                foreach (var (vestiging, lijnNummer) in vestigingDtos) {
                    command.Parameters["@Luchthaven"].Value = vestiging.luchthaven;
                    command.Parameters["@Straat"].Value = vestiging.straat;
                    command.Parameters["@Postcode"].Value = vestiging.postcode;
                    command.Parameters["@Plaats"].Value = vestiging.plaats;
                    command.Parameters["@Land"].Value = vestiging.land;

                    if (command.ExecuteNonQuery() == -1)
                        errors.Add($"Lijn {lijnNummer}: Luchthaven bestaat al.");
                }
            }
        }
    }

    public List<string> GetLuchthavens() {
        List<string> results = new List<string>();

        using var connection = new SqlConnection(connectionString);
        connection.Open();

        const string query = "SELECT Luchthaven FROM Vestigingen;";

        using var command = new SqlCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read()){
            results.Add(reader[0].ToString());
        }

        return results;
    }
}

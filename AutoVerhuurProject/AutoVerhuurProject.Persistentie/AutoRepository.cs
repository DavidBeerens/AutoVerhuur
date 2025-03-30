using AutoVerhuurProject.Domein.DTOs;
using AutoVerhuurProject.Domein.Interfaces;
using AutoVerhuurProject.Domein.Models;
using Microsoft.Data.SqlClient;

namespace AutoVerhuurProject.Persistentie;

public class AutoRepository : IAutoRepositoryFull
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

    //om te controleren welke auto's beschikbaar zijn op 1 bepaald moment voor het overzicht
    public List<AutoDto> GetBeschikbareAutos(string luchthaven, DateTime tijdstip) {
        List<AutoDto> results = new List<AutoDto>();

        using var connection = new SqlConnection(connectionString);
        connection.Open();


        string query = "SELECT * FROM Autos a " +
            "WHERE a.Luchthaven = @Luchthaven AND " +
            "a.Nummerplaat NOT IN ( " +
            "SELECT r.AutoNummerplaat " +
            "FROM Reservaties r " +
            "WHERE @Tijdstip BETWEEN r.StartTijdStip AND r.EindTijdStip);";


        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Luchthaven", luchthaven);
        command.Parameters.AddWithValue("@Tijdstip", tijdstip);

        
        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var auto = new AutoDto(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetByte(2),
                reader.GetByte(3) switch {
                    1 => MotorTypes.Benzine,
                    2 => MotorTypes.Diesel,
                    3 => MotorTypes.Hybride,
                    4 => MotorTypes.Elektrisch
                },
                reader.GetString(4)
            );
            results.Add(auto);
        };


        return results;
    }



    public List<AutoDto> GetBeschikbareAutos(string luchthaven, int zitplaatsen, DateTime start, DateTime einde) {
        List<AutoDto> results = new List<AutoDto>();

        using var connection = new SqlConnection(connectionString);
        connection.Open();


        string query = (zitplaatsen == 1)
        ? "SELECT a.* FROM Autos a " +
            "WHERE a.Luchthaven LIKE @Luchthaven " +
            "AND a.Nummerplaat NOT IN (" +
                "SELECT r.AutoNummerplaat " +
                "FROM Reservaties r " +
                "WHERE r.StartTijdStip < @EindTijd AND r.EindTijdStip > @StartTijd " +
            ");"
        : "SELECT a.* FROM Autos a " +
            "WHERE a.Luchthaven LIKE @Luchthaven AND a.Zitplaatsen = @Zitplaatsen " +
            "AND a.Nummerplaat NOT IN (" +
                "SELECT r.AutoNummerplaat " +
                "FROM Reservaties r " +
                "WHERE r.StartTijdStip < @EindTijd AND r.EindTijdStip > @StartTijd " +
            ");";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Luchthaven", luchthaven);
        command.Parameters.AddWithValue("@Zitplaatsen", zitplaatsen);

        //minimum en maximum datum meegeven als er niks is ingevuld
        command.Parameters.AddWithValue("@StartTijd",(start <= new DateTime(1753, 1, 1) ? new DateTime(9999, 12, 31, 11, 59, 58) : start));
        command.Parameters.AddWithValue("@EindTijd", (einde <= new DateTime(1753, 1, 1) ? new DateTime(9999, 12, 31, 11, 59, 59) : einde));

        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            var auto = new AutoDto(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetByte(2),
                reader.GetByte(3) switch {
                    1 => MotorTypes.Benzine,
                    2 => MotorTypes.Diesel,
                    3 => MotorTypes.Hybride,
                    4 => MotorTypes.Elektrisch
                },
                reader.GetString(4)
            );
            results.Add(auto);
        };


        return results;
    }

    public void VeranderLuchthaven() {
        using var connection = new SqlConnection(connectionString);
        connection.Open();


        string query = "UPDATE a " +
            "SET a.Luchthaven = r.RetourLuchthaven " +
            "FROM Autos a " +
            "JOIN ( " +
                "SELECT r1.AutoNummerplaat, r1.RetourLuchthaven " +
                "FROM Reservaties r1 " +
                "WHERE r1.EindTijdStip = ( " +
                    "SELECT MAX(r2.EindTijdStip) " +
                    "FROM Reservaties r2 " +
                    "WHERE r2.AutoNummerplaat = r1.AutoNummerplaat " +
                    "AND r2.EindTijdStip < CURRENT_TIMESTAMP " +
                ") " +
            ") AS r ON a.Nummerplaat = r.AutoNummerplaat " +
            "WHERE a.Luchthaven <> r.RetourLuchthaven;";


        using var command = new SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }


    public void Add(AutoDto auto) {
        throw new NotImplementedException();
    }

    public AutoDto? GetByNummerplaat(string nummerplaat) {
        throw new NotImplementedException();
    }
}

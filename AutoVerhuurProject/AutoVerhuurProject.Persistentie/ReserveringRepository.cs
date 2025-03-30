using AutoVerhuurProject.Domein.DTOs;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace AutoVerhuurProject.Persistentie;

public class ReserveringRepository
{
    private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoVerhuurDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

    public bool VoegReservatieToe(KlantDto klant, AutoDto auto, string retourLuchthaven, DateTime start, DateTime einde) {
        using (SqlConnection connection = new(connectionString)) {
            connection.Open();

            string query = "IF NOT EXISTS (SELECT 1 FROM Reservaties WHERE ReserveringsId = @ReserveringsId) " +
                "INSERT INTO Reservaties " +
                "VALUES (@ReserveringsId, @KlantEmail, @AutoNummerplaat, @RetourLuchthaven, @StartTijdStip, @EindTijdStip);";


            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.Add("@ReserveringsId", System.Data.SqlDbType.Binary);
                command.Parameters.Add("@KlantEmail", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@AutoNummerplaat", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@RetourLuchthaven", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@StartTijdStip", System.Data.SqlDbType.DateTime);
                command.Parameters.Add("@EindTijdStip", System.Data.SqlDbType.DateTime);


                command.Parameters["@ReserveringsId"].Value = Guid.NewGuid().ToByteArray();
                command.Parameters["@KlantEmail"].Value = klant.email;
                command.Parameters["@AutoNummerplaat"].Value = auto.nummerplaat;
                command.Parameters["@RetourLuchthaven"].Value = retourLuchthaven;
                command.Parameters["@StartTijdStip"].Value = start;
                command.Parameters["@EindTijdStip"].Value = einde;

                
                try {
                    command.ExecuteNonQuery();
                    return true;
                } catch {
                    return false;
                }
            }
        }
    }

    public (ReservatieDto, KlantDto) GetLaatsteReservatie(AutoDto auto) {
        using var connection = new SqlConnection(connectionString);
        connection.Open();


        string query = "SELECT TOP 1 * " +
            "FROM Reservaties r " +
            "JOIN Klanten k ON r.KlantEmail = k.Email " +
            "WHERE r.AutoNummerplaat = @Nummerplaat " +
            "AND r.EindTijdStip < CURRENT_TIMESTAMP " +
            "ORDER BY r.EindTijdStip DESC;";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Nummerplaat", auto.nummerplaat);

        using var reader = command.ExecuteReader();
        reader.Read();
        var reservatie = new ReservatieDto(
            new Guid((byte[])reader[0]),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetDateTime(4),
            reader.GetDateTime(5));
        var klant = new KlantDto(
            reader.GetString(6),
            reader.GetString(7),
            reader.GetString(8),
            reader.GetString(9),
            reader.GetString(10),
            reader.GetString(11),
            reader.GetString(12));


        return (reservatie, klant);
    }


    public (ReservatieDto, KlantDto) GetVolgendeReservatie(AutoDto auto) {
        using var connection = new SqlConnection(connectionString);
        connection.Open();


        string query = "SELECT TOP 1 * " +
            "FROM Reservaties r " +
            "JOIN Klanten k ON r.KlantEmail = k.Email " +
            "WHERE r.AutoNummerplaat = @Nummerplaat " +
            "AND r.StartTijdStip > CURRENT_TIMESTAMP " +
            "ORDER BY r.StartTijdStip ASC;";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Nummerplaat", auto.nummerplaat);

        using var reader = command.ExecuteReader();
        reader.Read();
        var reservatie = new ReservatieDto(
            new Guid((byte[])reader[0]),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetDateTime(4),
            reader.GetDateTime(5));
        var klant = new KlantDto(
            reader.GetString(6),
            reader.GetString(7),
            reader.GetString(8),
            reader.GetString(9),
            reader.GetString(10),
            reader.GetString(11),
            reader.GetString(12));



        return (reservatie, klant);
    }
}
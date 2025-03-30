using Microsoft.Data.SqlClient;

namespace AutoVerhuurProject.Persistentie;

public class StartupRepository
{
    private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AutoVerhuurDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";


    public void EmptyDB() {
        using (SqlConnection connection = new(connectionString)) {
            connection.Open();

            string query = "DELETE FROM Reservaties;" +
                "DELETE FROM Autos;" +
                "DELETE FROM Klanten;" +
                "DELETE FROM Vestigingen;";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.ExecuteNonQuery();
            }
        }
    }
}

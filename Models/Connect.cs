using Microsoft.Data.SqlClient;

namespace hopital.Models;
public class Connect
{
    public SqlConnection connectDB()
    {

        var datasource = @".\sqlexpress";
        var database = "hopital1";

        //your connection string
        string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True; Trusted_Connection=True; TrustServerCertificate=True";

        //create instanace of database connection
        SqlConnection conn = new SqlConnection(connString);

        try{
            Console.WriteLine("Openning Connection ...");

            //open connection
            conn.Open();

            Console.WriteLine("Connection successful!");
        }
        catch (Exception e){
            throw e;
        }
        return conn;
    }
}
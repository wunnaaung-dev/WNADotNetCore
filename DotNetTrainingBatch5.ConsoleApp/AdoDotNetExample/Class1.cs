using System.Data.SqlClient;

namespace AdoDotNetExample;

public class AdoDotNet
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";
    public void Read()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        Console.WriteLine("Connection opening");
        connection.Open();
        Console.WriteLine("Connect opened");

        string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog]";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader["BlogId"]} {reader["BlogTitle"]} {reader["BlogAuthor"]} {reader["BlogContent"]}");
        }
        connection.Close();
    }

    public void Create()
    {
        Console.WriteLine("Blog Title: ");
        string title = Console.ReadLine();

        Console.WriteLine("Blog Author: ");
        string author = Console.ReadLine();

        Console.WriteLine("Blog Content: ");
        string content = Console.ReadLine();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);

        int result = cmd.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
    }

}

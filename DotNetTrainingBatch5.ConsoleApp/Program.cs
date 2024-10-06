// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, Myanmar Pyi Ka Lu Twy!");

string connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";
Console.WriteLine("Connection string", connectionString);
SqlConnection connection = new SqlConnection(connectionString);

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
// SqlDataAdapter adapter = new SqlDataAdapter(cmd);
// DataTable dt = new DataTable();
// adapter.Fill(dt);
SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader["BlogId"]} {reader["BlogTitle"]} {reader["BlogAuthor"]} {reader["BlogContent"]}");
}


Console.WriteLine("Connection closing");
connection.Close();
Console.WriteLine("Connection closed");
Console.ReadKey();
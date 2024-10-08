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
connection.Close();

Console.WriteLine("Blog Title: ");
string title = Console.ReadLine();

Console.WriteLine("Blog Author: ");
string author = Console.ReadLine();

Console.WriteLine("Blog Content: ");
string content = Console.ReadLine();

connection.Open();
string queryInsert = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

SqlCommand cmd2 = new SqlCommand(queryInsert, connection);
cmd2.Parameters.AddWithValue("@BlogTitle", title);
cmd2.Parameters.AddWithValue("@BlogAuthor", author);
cmd2.Parameters.AddWithValue("@BlogContent", content);
// SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
// DataTable dt = new DataTable();
// adapter.Fill(dt);

int result = cmd2.ExecuteNonQuery();
connection.Close();
Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
Console.ReadKey();
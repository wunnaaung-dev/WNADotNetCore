using System.Data;
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

    public void Edit()
    {
        Console.WriteLine("Blog Id: ");
        string id = Console.ReadLine();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        connection.Close();
        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No data found");
            return;
        }
        DataRow dr = dt.Rows[0];
        Console.WriteLine(dr["BlogId"]);
        Console.WriteLine(dr["BlogTitle"]);
        Console.WriteLine(dr["BlogAuthor"]);
        Console.WriteLine(dr["BlogContent"]);
    }

    public void Update()
    {
        Console.WriteLine("Blog Id: ");
        string id = Console.ReadLine();

        Console.WriteLine("Blog Title: ");
        string title = Console.ReadLine();

        Console.WriteLine("Blog Author: ");
        string author = Console.ReadLine();

        Console.WriteLine("Blog Content: ");
        string content = Console.ReadLine();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);

        int result = cmd.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");
    }

    public void Delete() {
        Console.WriteLine("Enter the BlogId you want to delete: ");
        string id = Console.ReadLine();
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string query = $@"UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);

        int result = cmd.ExecuteNonQuery();
        connection.Close();
        Console.WriteLine(result == 1 ? "Deleting Successful" : "Deleting Failed");
    }


}

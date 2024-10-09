using System.Data;
using System.Data.SqlClient;
using Dapper;
using DotNetTrainingBatch5.ConsoleApp.Models;

namespace DotNetTrainingBatch5.ConsoleApp.DapperExample;

public class DapperExample
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";

    public void Read()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0;";
            var lst = db.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }
    }

    public void Create(string title, string author, string content)
    {
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

        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            int result = db.Execute(query, new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Insert Successful" : "Insert Failed");
        }
    }

    public void Edit(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId AND DeleteFlag = 0;";
            var item = db.Query<BlogDataModel>(query, new BlogDataModel
            {
                BlogId = id
            }).FirstOrDefault();

            if(item is null) {
                Console.WriteLine("No data found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
    }

}

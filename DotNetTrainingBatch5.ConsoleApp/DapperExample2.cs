using DotNetTrainingBatch5.ConsoleApp.Models;
using DotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";
        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Edit(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId AND DeleteFlag = 0;";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = id
            });

            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
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

          
                int result = _dapperService.Execute(query, new BlogDapperDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Insert Successful" : "Insert Failed");
            
        }

        public void Update(int id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] = @BlogTitle
                ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
                ,[DeleteFlag] = 0
                WHERE BlogId = @BlogId";

            var item = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(item == 1 ? "Update Successful" : "Update Failed");
        }

        public void Delete(int id)
        {
            string query = $@"UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";

            var result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = id
            });
            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
        }
    }
}


namespace DotNetTrainingBatch5.MinimalAPI.Endpoints.Blog
{
    public static class BlogEndpoint
    {
        //public static string Test(this int i)
        //{
        //    return i.ToString();
        //}

        public static void MapBlogEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found");
                }
                return Results.Ok(item);
            })
.WithName("Get Blog with id")
.WithOpenApi();

            app.MapGet("/blogs", () =>
            {
                AppDbContext db = new AppDbContext();
                var model = db.TblBlogs.AsNoTracking().ToList();
                return Results.Ok(model);
            })
            .WithName("Get Blogs")
            .WithOpenApi();

            app.MapPost("/blogs", (TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                db.TblBlogs.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("Create Blog")
            .WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found");
                }
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("Update Blogs")
            .WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found");
                }
                db.Entry(item).State = EntityState.Deleted;
                db.SaveChanges();
                return Results.Ok();
            })
            .WithName("Delete Blogs")
            .WithOpenApi(); ;

        }
    }
}

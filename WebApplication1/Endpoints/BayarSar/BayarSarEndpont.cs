
namespace DotNetTrainingBatch5.JSON_MinimalAPI.Endpoints.BayarSarEndpoint
{
    public static class BayarSarEndpont
    {
        public static void MapBayarSarEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/bayarsar", () =>
            {
                var result = BayarSarDataHelper.LoadBayarSarData();
                return Results.Ok(result.Tbl_BayarSar);
            })
            .WithName("Get BayarSar")
            .WithOpenApi();

            app.MapGet("/api/bayarsar/{id}", (int id) =>
            {
                var result = BayarSarDataHelper.LoadBayarSarData();
                var item = result.Tbl_BayarSar.FirstOrDefault(x => x.groupId == id);
                if (item is null) return Results.BadRequest("No data found");
                return Results.Ok(item);
            })
            .WithName("Get BayarSar with id")
            .WithOpenApi();

            app.MapPost("/api/bayarsar", (Tbl_Bayarsar newData) =>
            {
                var result = BayarSarDataHelper.LoadBayarSarData();
                var dataList = result.Tbl_BayarSar.ToList();

                
                var newGroupId = dataList.Count > 0 ? dataList.Max(x => x.groupId) + 1 : 1;
                newData.groupId = newGroupId;

                dataList.Add(newData);
                result.Tbl_BayarSar = dataList.ToArray();
                BayarSarDataHelper.SaveBayarSarData(result);

                return Results.Ok(result);
            })
            .WithName("Create New BayarSar")
            .WithOpenApi();

            app.MapPut("/api/bayarsar/{id}", (int id, Tbl_Bayarsar updatedData) =>
            {
                var result = BayarSarDataHelper.LoadBayarSarData();
                var dataList = result.Tbl_BayarSar.ToList();

               
                var existingItem = dataList.FirstOrDefault(x => x.groupId == id);
                if (existingItem is null) return Results.BadRequest("No data found");

              
                existingItem.title = updatedData.title;
                existingItem.data = updatedData.data;

                result.Tbl_BayarSar = dataList.ToArray();
                BayarSarDataHelper.SaveBayarSarData(result);

                return Results.Ok(existingItem);
            })
           .WithName("Update BayarSar")
           .WithOpenApi();

            app.MapDelete("/api/bayarsar/{id}", (int id) =>
            {
                var result = BayarSarDataHelper.LoadBayarSarData();
                var dataList = result.Tbl_BayarSar.ToList();

                var itemToRemove = dataList.FirstOrDefault(x => x.groupId == id);
                if (itemToRemove is null) return Results.BadRequest("No data found");

                dataList.Remove(itemToRemove);
                result.Tbl_BayarSar = dataList.ToArray();
                BayarSarDataHelper.SaveBayarSarData(result);

                return Results.Ok("Deleted successfully");
            })
            .WithName("Delete BayarSar")
            .WithOpenApi();
        }


    }
}

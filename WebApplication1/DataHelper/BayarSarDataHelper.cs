
namespace DotNetTrainingBatch5.JSON_MinimalAPI.DataHelper
{
    public static class BayarSarDataHelper
    {
        private static readonly string _folderPath = "Data/BayarSar.json";

        public static BayarSarResponseModel LoadBayarSarData()
        {
            if (!File.Exists(_folderPath))
                return new BayarSarResponseModel { Tbl_BayarSar = new Tbl_Bayarsar[0] };

            var jsonStr = File.ReadAllText(_folderPath);
            return JsonConvert.DeserializeObject<BayarSarResponseModel>(jsonStr)!;
        }

        public static void SaveBayarSarData(BayarSarResponseModel data)
        {
            var jsonStr = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_folderPath, jsonStr);
        }




        public class BayarSarResponseModel
        {
            public Tbl_Bayarsar[] Tbl_BayarSar { get; set; }
        }

        public class Tbl_Bayarsar
        {
            public int groupId { get; set; }
            public string title { get; set; }
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public int id { get; set; }
            public int groupId { get; set; }
            public string title { get; set; }
        }


    }
}

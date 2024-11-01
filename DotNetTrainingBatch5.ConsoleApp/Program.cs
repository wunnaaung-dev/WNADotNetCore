// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using AdoDotNetExample;
using DotNetTrainingBatch5.ConsoleApp;
using DotNetTrainingBatch5.ConsoleApp.DapperExample;

Console.WriteLine("Hello, Myanmar Pyi Ka Lu Twy!");

AdoDotNet ado = new AdoDotNet();
// ado.Create();
// ado.Read();
// ado.Edit();
// ado.Update();
// ado.Delete();

//DapperExample dapperExample = new DapperExample();

// dapperExample.Create("ကိုယ်ကျင့်တရားကောင်းဖို့", "ဆရာတော်ဘုရား", "ကိုယ်လဲ ပဲ ကြိုးပမ်းအားထုတ်ရမယ်");
// dapperExample.Create("တရားဓမ္မနဲ့မွေ့လျော်နေ", "ဆရာတော်ဘုရား", "ဆရာတော်ဘုရားက အမှာတော်ကြား");

// dapperExample.Update(3, "တရားဒေသနာ", "ဆရာတော်ဘုရား", "ဆုံးမတော်မူပေထ");
// dapperExample.Delete(1);
//EFCoreExample efCoreExample = new EFCoreExample();
//efCoreExample.Read();

//DapperConnection dapperConnection = new DapperConnection();
//dapperConnection.Read();
//dapperConnection.Create("ကိုယ်ကျင့်တရားကောင်းဖို့", "ဆရာတော်ဘုရား", "ကိုယ်လဲ ပဲ ကြိုးပမ်းအားထုတ်ရမယ်");
//dapperConnection.Edit(2);
//dapperConnection.Update(1, "Hee Hee", "Lil Z", "ae kg hee hee har har lote");
//dapperConnection.Delete(2);

EFCoreExample efCoreExample = new EFCoreExample();
efCoreExample.Create("Morning", "Oppa Wunna Aung", "Saw Saw Hta Pee Sar Lote");
Console.ReadKey();
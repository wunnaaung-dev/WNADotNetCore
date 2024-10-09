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

DapperExample dapperExample = new DapperExample();

// dapperExample.Create("ကိုယ်ကျင့်တရားကောင်းဖို့", "ဆရာတော်ဘုရား", "ကိုယ်လဲ ပဲ ကြိုးပမ်းအားထုတ်ရမယ်");
// dapperExample.Create("တရားဓမ္မနဲ့မွေ့လျော်နေ", "ဆရာတော်ဘုရား", "ဆရာတော်ဘုရားက အမှာတော်ကြား");

// dapperExample.Update(3, "တရားဒေသနာ", "ဆရာတော်ဘုရား", "ဆုံးမတော်မူပေထ");
// dapperExample.Delete(1);
EFCoreExample efCoreExample = new EFCoreExample();
efCoreExample.Read();

Console.ReadKey();
// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using AdoDotNetExample;
using DotNetTrainingBatch5.ConsoleApp.DapperExample;

Console.WriteLine("Hello, Myanmar Pyi Ka Lu Twy!");

AdoDotNet ado = new AdoDotNet();
// ado.Create();
// ado.Read();
// ado.Edit();
// ado.Update();
// ado.Delete();

DapperExample dapperExample = new DapperExample();

dapperExample.Create("Oppa Wunna", "Wunna", "Wunna is on fire");
Console.ReadKey();
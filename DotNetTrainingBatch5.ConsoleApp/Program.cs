// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using AdoDotNetExample;

Console.WriteLine("Hello, Myanmar Pyi Ka Lu Twy!");

AdoDotNet ado = new AdoDotNet();
// ado.Create();
ado.Read();
Console.ReadKey();
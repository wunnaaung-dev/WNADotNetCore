// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleApp3;
using DotNetTrainingBatch5.ConsoleApp3.AdoDotNetExample;
using DotNetTrainingBatch5.ConsoleApp3.DapperExample;
using DotNetTrainingBatch5.ConsoleApp3.EFCoreExample;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection()
    .AddSingleton<AdoDotNetExample>()
    .AddSingleton<DapperExample>()
    .AddScoped<AppDbContext>()
    .AddSingleton<EFCoreExample>()
    .BuildServiceProvider();

//var adoDotNetExample = services.GetRequiredService<AdoDotNetExample>();
//adoDotNetExample.Read();

//var dapperExample = services.GetRequiredService<DapperExample>();
//dapperExample.Read();
//dapperExample.Create("Ngr Nha Mhaw Dl", "SGL", "song");

var efcoreExample = services.GetRequiredService<EFCoreExample>();

efcoreExample!.Edit(6);

Console.ReadKey();
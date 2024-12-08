using Microsoft.EntityFrameworkCore;
using WNADotNetCore.MiniKpay.Database.Models;
using WNADotNetCore.MiniKpay.Domain.Features.Deposit;
using WNADotNetCore.MiniKpay.Domain.Features.Transaction;
using WNADotNetCore.MiniKpay.Domain.Features.Transfer;
using WNADotNetCore.MiniKpay.Domain.Features.User;
using WNADotNetCore.MiniKpay.Domain.Features.Withdraw;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// register db context for dependency injection
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")) ;
}, ServiceLifetime.Transient, ServiceLifetime.Transient);

// register services
builder.Services.AddScoped<DepositService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<WithdrawService>();
builder.Services.AddScoped<TransferService>();
builder.Services.AddScoped<TransactionService>();
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

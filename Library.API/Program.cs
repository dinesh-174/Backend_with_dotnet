using Library.Application.Interfaces;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Library.Application.Services;
using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


// STEP 1: Register database services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// STEP 2: Register API services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
// STEP 3: Build the application
var app = builder.Build();

// STEP 4: Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// STEP 5: Test database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    try
    {
        var canConnect =
            await dbContext.Database.CanConnectAsync();

        Console.WriteLine(
            canConnect
                ? "SQL Server connection successful!"
                : "SQL Server connection failed!"
        );
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database error: " + ex.Message);
    }
}

// STEP 6: Start application
app.Run();

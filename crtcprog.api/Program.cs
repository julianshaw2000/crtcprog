using crtcprog.api.Models;
using crtcprog.api.Services;
using ctrcprog.api.Data; 

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;



builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();


var connectionString = configuration.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));
//options.UseSqlite("DataSource=app.db"));




builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<DataContext>()
    .AddApiEndpoints();




// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program).Assembly);
  
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));


// builder.Services.AddScoped<AuthService>();


//builder.Services.AddScoped(_ =>
//{
//    var connectString = configuration.GetConnectionString("AzureBlobStorageConnectionString");
//    return new BlobServiceClient(connectString);
//});




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

// Use CORS middleware
app.UseCors("AllowAllOrigins");




app.UseAuthorization();

app.MapControllers();



app.MapGroup("/api/identity").MapIdentityApi<ApplicationUser>();

// Define a Minimal API endpoint
app.MapGet("/api/hello", () => "Welcome to Booker API!");


//app.MapGet("/", (ClaimsPrincipal user) => new { data = 34 })
//    .RequireAuthorization();



app.Run();

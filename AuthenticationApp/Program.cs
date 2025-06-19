using AuthenticationApp.Business.User;
using AuthenticationApp.DependencyIngectionContainer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthenticationApp.Helpers; // <- This is required


var builder = WebApplication.CreateBuilder(args);

//this is an cors allow origin to make the connection with the backend now this only allow the https://localhost:4200
builder.Services.AddCors(option =>
{
  option.AddPolicy("AllowLocalhost", builder => builder.WithOrigins("http://localhost:4200")
  .AllowAnyMethod()
  .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();
DependencyIngectionContainer.Injector(builder.Services);

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register JWT BEFORE Build()
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
      };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
builder.Services.AddScoped<DataBaseHelper>();


app.UseCors("AllowLocalhost"); // ✅ Add this line to apply the CORS policy

// ✅ Middleware order: Auth before Controllers
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

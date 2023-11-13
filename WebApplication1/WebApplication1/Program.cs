using IncomeBridgeAPI.BasicAuth;
using IncomeBridgeAPI.Extension;
using IncomeBridgeAPI.Service.Implementation.Costomer.Implement;
using IncomeBridgeAPI.Service.Implementation.Costomer.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApplication1.HelperClass.Implementation;
using WebApplication1.HelperClass.Interface;
using WebApplication1.HelperClass.PrepareRequestHeader.Implementation;
using WebApplication1.HelperClass.PrepareRequestHeader.Interface;
using WebApplication1.Service.Implementation.Costomer.Implement;
using WebApplication1.Service.Implementation.Costomer.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGetAccountIncome, GetAccountIncome>();
builder.Services.AddScoped<ISendRequest, SendRequest>();
builder.Services.AddScoped<IPrepareRequestHeader, PrepareRequestHeader>();
builder.Services.AddScoped<ICreateUser, CreateUser>();
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthentication>("BasicAuthentication", null);

// Use your extension method to register DbContext
builder.Services.AddCustomDbContext(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "IncomeBridgeAppi",
        Description = "API for retrieving customer income"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
    s.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                 {
                       new OpenApiSecurityScheme
                        {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id ="bearer"
                                }
                        },
                        new List<string>()
                  }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/V1/swagger.json", "Income Bridge API V");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

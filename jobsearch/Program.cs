using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// https://github.com/davidfowl/TodoApi/blob/main/TodoApi/Program.cs

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Place ONLY your JWT Bearer token in the text box below.",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowedOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddDbContext<JobSearchContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.RegisterServices();

var secretKey = Settings.GenerateSecretByte();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user", policy => policy.RequireRole("user"));
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
});

var app = builder.Build();

app.UseCors("allowedOrigins");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.RegisterJobSearchEndpoints();
app.RegisterApplicationEndpoints();
app.RegisterContactEndpoints();
app.RegisterUsersEndpoints();

app.Run();
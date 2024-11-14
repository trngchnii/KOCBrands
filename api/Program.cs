using api.Data;
using api.Models;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<User>("Users");
        modelBuilder.EntitySet<Brand>("Brands");
        modelBuilder.EntitySet<Influencer>("Influencers");
        modelBuilder.EntitySet<SocialMedia>("SocialMedias");
        modelBuilder.EntitySet<Category>("Categories");
        modelBuilder.EntitySet<Favourite>("Favourites");
        modelBuilder.EntitySet<Proposal>("Proposals");
        modelBuilder.EntitySet<Campaign>("Campaigns");
        modelBuilder.EntityType<Influencer>()
            .HasRequired(p => p.User);
        modelBuilder.EntityType<Influencer>()
            .HasMany(p => p.Categories);
        modelBuilder.EntityType<Influencer>()
            .HasMany(p => p.Proposals);


        // Add services to the container.
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Enable reference handling to prevent cycles
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<IInfluencerRepository, InfluencerRepository>();
        builder.Services.AddScoped<ISearchKOLRepository, SearchKOLRepository>();
        builder.Services.AddScoped<ICampainRepository, CampainRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
        builder.Services.AddTransient<IFileService, FileService>();
        builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                    ;
                });
        });


        builder.Services.AddControllers();
        builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    })
    .AddOData(
    o => o.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents("odata", modelBuilder.GetEdmModel())
    );

        IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true).Build();
        // Setup JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                };
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
                }
    });
        });

        // Authorization
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Role", "admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireClaim("Role", "user"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseODataBatching();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseStaticFiles(); // Để phục vụ file từ wwwroot

        // Cấu hình thư mục Uploads bên ngoài wwwroot
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
            RequestPath = "/Uploads" // Đường dẫn sẽ được truy cập qua /Uploads
        });
        /*app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath,"Uploads")),
            RequestPath = "/Resources"
        });*/
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        //app.MapControllers();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Bản đồ các controller OData
        });
        app.Run();
    }
}
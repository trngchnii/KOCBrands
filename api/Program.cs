using api.Data;
using api.Models;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
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
        modelBuilder.EntityType<Campaign>().HasRequired(p => p.Brand);
        modelBuilder.EntityType<Campaign>().HasMany(p => p.Categories);

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
        builder.Services.AddScoped<IBrandRepository,BrandRepository>();
        builder.Services.AddScoped<IInfluencerRepository,InfluencerRepository>();
        builder.Services.AddScoped<ISearchKOLRepository,SearchKOLRepository>();
        builder.Services.AddScoped<ICampainRepository,CampainRepository>();
        builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
        builder.Services.AddScoped<IProposalRepository,ProposalRepository>();
        builder.Services.AddTransient<IFileService,FileService>();
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
    o => o.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents("odata",modelBuilder.GetEdmModel())
    );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
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
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Uploads")),
            RequestPath = "/Uploads" // Đường dẫn sẽ được truy cập qua /Uploads
        });
        /*app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath,"Uploads")),
            RequestPath = "/Resources"
        });*/
        app.UseCors();

        //app.MapControllers();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Bản đồ các controller OData
        });
        app.Run();
    }
}
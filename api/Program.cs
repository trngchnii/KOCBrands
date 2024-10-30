using api.Data;
using api.Models;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OData.ModelBuilder;

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
        builder.Services.AddControllers().AddOData(
                        o => o.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents("odata",modelBuilder.GetEdmModel())
                        );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath,"Uploads")),
            RequestPath = "/Resources"
        });
        app.UseCors();

        app.MapControllers();
        app.Run();
    }
}
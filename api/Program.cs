using api.Data;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

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
builder.Services.AddScoped<IProposalRepository,ProposalRepository>();

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

//app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


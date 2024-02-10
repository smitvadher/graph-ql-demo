using GraphQlProductsDemo.Data;
using GraphQlProductsDemo.GraphQl.Mutations;
using GraphQlProductsDemo.GraphQl.Queries;
using GraphQlProductsDemo.GraphQl.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
{
    if (!string.IsNullOrEmpty(dbConnectionString))
    {
        options.UseSqlServer(dbConnectionString)
#if DEBUG
            .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
#endif
            ;
    }
    else
    {
        options.UseInMemoryDatabase("GraphQlProductsDemoDB")
#if DEBUG
            .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
#endif
            ;
    }
});

builder.Services.AddGraphQLServer()
    .RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
    .AddQueryType(d => d.Name(Constants.QueryType))
        .AddTypeExtension<ProductQueries>()
        .AddTypeExtension<CategoryQueries>()
    .AddMutationType(d => d.Name(Constants.MutationType))
        .AddTypeExtension<ProductMutations>()
        .AddTypeExtension<CategoryMutations>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

var app = builder.Build();

if (!string.IsNullOrEmpty(dbConnectionString))
    MigrateDatabase(app.Services);
await SeedDatabase(app.Services);

app.UseRouting();
app.MapGraphQL();
app.UseGraphQLVoyager();

app.Run();
return;

void MigrateDatabase(IServiceProvider services)
{
    var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();

    using var scope = scopeFactory.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>()
        .CreateDbContext();
    context.Database.Migrate();
}

async Task SeedDatabase(IServiceProvider services)
{
    var scopeFactory = services.GetRequiredService<IServiceScopeFactory>();

    using var scope = scopeFactory.CreateScope();
    await using var context = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext();
    await new DataSeeder(context).SeedAsync();
}
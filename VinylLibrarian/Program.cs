using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VinylLibrarian.Services.Interfaces;
using VinylLibrarian.Services.Classes;
using VinylLibrarian.Services.OAuth;
using Microsoft.Extensions.Options;
using WebIdentity.Data;
using DomainModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var databasePath = Path.Combine(Directory.GetCurrentDirectory(), "collection.db");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

Console.WriteLine($"Using database: {connectionString}");
Console.WriteLine($"Using database path: {databasePath}");

// Register your DbContexts
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Add Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add exception filters and Razor Pages
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();

// Register services for DI
builder.Services.AddScoped<IDataContext, DataContext>();
builder.Services.AddScoped<IArtistServices, ArtistService>();
builder.Services.AddScoped<IRecordServices, RecordService>();
builder.Services.AddHttpClient<IDiscogServices, DiscogServices>();
builder.Services.Configure<DiscogsOAuthKeys>(builder.Configuration.GetSection("Discogs"));

builder.Services.AddSingleton<DiscogsOAuthKeys>(sp =>
{
    var options = sp.GetRequiredService<IOptions<DiscogsOAuthKeys>>();
    return new DiscogsOAuthKeys(options, sp.GetRequiredService<HttpClient>());
});

builder.Services.AddHttpClient();


var app = builder.Build();

builder.Logging.AddConsole(); // For logging to the console
builder.Logging.AddDebug();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Brain}/{action=Collection}");

    endpoints.MapRazorPages();

    /* endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/Brain/Collection");
    });
    */
});

app.MapRazorPages();
app.Run();

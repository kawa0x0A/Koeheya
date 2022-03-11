using Koeheya.Areas.Identity;
using Koeheya.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["DefaultConnection"];

switch (ApplicationDbContext.ConnectionAddressString)
{
    case ApplicationDbContext.ConnectDatabaseStaging:
    case ApplicationDbContext.ConnectDatabaseProcution:
        connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        break;
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString!)
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddUserStore<ApplicationUserStore>()
    .AddRoleStore<ApplicationRoleStore>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddAuthentication().AddCookie().AddTwitter(twitterOptions =>
{
    twitterOptions.ConsumerKey = builder.Configuration[Koeheya.Twitter.ConsumerKeyName];
    twitterOptions.ConsumerSecret = builder.Configuration[Koeheya.Twitter.ConsumerKeySecretName];
    twitterOptions.SignInScheme = IdentityConstants.ExternalScheme;
    twitterOptions.SaveTokens = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

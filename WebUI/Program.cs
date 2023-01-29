var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "home",
    pattern: "/",
    defaults: new { controller = "Product", action = "Index" }
    );
app.MapControllerRoute(
    name: "product",
    pattern: "/product/{categoryId}",
    defaults: new { controller = "Product", action = "GetAllProductByCategory" }
    );

app.Run();

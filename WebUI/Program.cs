using Application;

using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebUI();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "ViewTopic", pattern: "Topic/{id}", new { controller = "Topic", action = "Display" });
    endpoints.MapControllerRoute(name: "EditTopic", pattern: "Edit/{id}", new { controller = "Topic", action = "Edit" });
    endpoints.MapControllerRoute(name: "CreateTopic", pattern: "Create", new { controller = "Topic", action = "Create" });
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Topic}/{action=Index}/{id?}");
});

app.Run();

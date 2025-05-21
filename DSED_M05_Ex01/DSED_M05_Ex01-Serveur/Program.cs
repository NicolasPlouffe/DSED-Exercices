using DSED_M05_Model;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IOperationsService, OperationsService>();
builder.Services.AddSoapCore();

var app = builder.Build();

app.UseSoapEndpoint<IOperationsService>("/OperationService.svc", new SoapEncoderOptions());

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
    endpoints.UseSoapEndpoint<IOperationsService>(opt =>
    {
        opt.Path = "/OperationService.asmx ";
        opt.SoapSerializer = SoapSerializer.DataContractSerializer;
    });

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();

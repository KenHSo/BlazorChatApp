var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Enable CORS (Allow Blazor WASM frontend to call API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy => policy
            .WithOrigins(
                "https://localhost:7183", // Local Client HTTPS
                "https://localhost:7256", // Local API HTTPS
                "http://localhost:5120",  // Local Client HTTP
                "http://localhost:5054",  // Local API HTTP
                "https://blazor-chat-api-enh3c5gsd8b7becz.westeurope-01.azurewebsites.net", // Deployed API
                "https://ambitious-ocean-0fb1e3b03.6.azurestaticapps.net" // Deployed Blazor WASM
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Apply middleware
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowBlazorClient"); // Apply CORS policy
app.UseAuthorization();

app.MapControllers();

// Explicitly handle OPTIONS (preflight) requests
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireCors("AllowBlazorClient");
});

app.Run();

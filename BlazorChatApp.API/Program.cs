var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Enable CORS (Allow Blazor WASM frontend to call API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        policy => policy
            .WithOrigins("https://blazor-chat-app.azurestaticapps.net") // Replace with your actual WASM URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Apply middleware
app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm"); // Apply CORS policy
app.UseAuthorization();

app.MapControllers();

app.Run();

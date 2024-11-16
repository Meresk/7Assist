using LiveKit.API.Interfaces;
using LiveKit.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddSingleton<ILiveKitService, LiveKitService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:5221")  // Разрешаем запросы только с этого домена
               .AllowAnyHeader()  // Разрешаем любые заголовки
               .AllowAnyMethod()  // Разрешаем любые методы
               .AllowCredentials();  // Разрешаем использование cookies в запросах
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Use CORS and routing
app.UseCors("_myAllowSpecificOrigins");

app.MapControllers();

app.Run();
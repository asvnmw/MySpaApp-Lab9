using Microsoft.EntityFrameworkCore;
using MySpaApp.Components;
using MySpaApp.Services;
using MySpaApp.Hubs; // Если ваш Hub лежит в папке Hubs

var builder = WebApplication.CreateBuilder(args);

// 1. НАСТРОЙКА БАЗЫ ДАННЫХ
// Берем строку подключения из переменной окружения (для Docker),
// если её нет, используем локальную (для отладки).
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? "Host=localhost;Port=5432;Database=appdb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. РЕГИСТРАЦИЯ СЕРВИСОВ BLAZOR
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ITrainerService, TrainerService>();

// 3. РЕГИСТРАЦИЯ SIGNALR (из Лабораторной 10)
builder.Services.AddSignalR();

var app = builder.Build();

// 4. АВТОМАТИЧЕСКОЕ СОЗДАНИЕ БАЗЫ ДАННЫХ
// При запуске проверяем, создана ли база, если нет - создаем (для упрощения лабы)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// 5. ЭНДПОИНТ ДЛЯ ПРОВЕРКИ БАЗЫ ДАННЫХ (из методички Лабораторной 11)
app.MapGet("/items", async (AppDbContext db) => await db.Items.ToListAsync());

// 6. МАРШРУТ ДЛЯ SIGNALR (из Лабораторной 10)
app.MapHub<TrainerHub>("/trainerhub");

app.Run();

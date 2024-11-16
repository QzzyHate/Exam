using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(); //Добавляем контекст в качестве сервиса приложения. Подключение настроено в appsettings.json
builder.Services.AddRazorPages(); //Добавляем Razor Pages

var app = builder.Build();

app.MapRazorPages();
app.Run();

public class Order
{
    public int Id { get; set; } // Первичный ключ

    public int number { get; set; } //Номер заявки
    public DateOnly dateAdded { get; set; } //Дата добавления
    public string? device { get; set; } //Неисправное оборудование
    public string? problemType { get; set; } //Тип проблемы
    public string? description { get; set; } //Описание
    public string? client { get; set; } //Клиент
    public string? master { get; set; } = "Не назначен"; //Мастер
    public string? status { get; set; } //Статус
}

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } //Создаём таблицу с названием Orders

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //Настраивает подключение с бд
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}
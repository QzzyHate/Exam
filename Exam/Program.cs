using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(); //��������� �������� � �������� ������� ����������. ����������� ��������� � appsettings.json
builder.Services.AddRazorPages(); //��������� Razor Pages

var app = builder.Build();

app.MapRazorPages();
app.Run();

public class Order
{
    public int Id { get; set; } // ��������� ����

    public int number { get; set; } //����� ������
    public DateOnly dateAdded { get; set; } //���� ����������
    public string? device { get; set; } //����������� ������������
    public string? problemType { get; set; } //��� ��������
    public string? description { get; set; } //��������
    public string? client { get; set; } //������
    public string? master { get; set; } = "�� ��������"; //������
    public string? status { get; set; } //������
}

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } //������ ������� � ��������� Orders

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //����������� ����������� � ��
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}
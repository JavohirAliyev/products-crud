using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ma'lumotlar bazasi ulanishini sozlash
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Xizmatlarni ro'yxatdan o'tkazish
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();

// Kontrollerlarni qo'shish
builder.Services.AddControllers();

// Swagger qo'shish
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger sozlamalari
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware sozlamalari
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
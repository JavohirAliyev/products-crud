using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Kontrollerlarni qo'shish
builder.Services.AddControllers();

// Swagger qo'shish
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ma'lumotlar bazasi ulanishini sozlash
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Xizmatlarni ro'yxatdan o'tkazish
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();


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
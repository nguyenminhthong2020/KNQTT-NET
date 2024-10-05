using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen(); // dòng này là code mặc định
// Nếu mình muốn custom lại Swagger
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

// Dòng này là mình tự thêm (cùng với folder wwwroot)
// Chứ tạo project Web API bằng VisualStudio thì nó không có sẵn, chừng nào MVC mới có sẵn
app.UseStaticFiles();


// Configure the HTTP request pipeline.
// --> Nếu không comment lại --> chỉ môi trường Development mới thấy được Swagger
//if (app.Environment.IsDevelopment())  
{
    app.UseSwagger();
    //app.UseSwaggerUI();  // Dòng này là code mặc định. Mình muốn custom thêm nên comment nó lại
    app.UseSwaggerUI(options =>
    {
        //options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //options.RoutePrefix = string.Empty;

        options.InjectStylesheet("/assets/css/swagger-style.css"); // thêm css cho đẹp thôi
    });
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

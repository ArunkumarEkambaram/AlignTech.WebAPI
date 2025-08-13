using DemoFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(options =>
{
    //Applying Filters Globally
    options.Filters.Add<MyAuthorizationFilter>();
    options.Filters.Add(typeof(MyResourceFilter));
    options.Filters.Add<MyResultFilter>();
    //options.Filters.Add<MyActionFilter>();
    options.Filters.Add<MyExceptionFilter>();
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

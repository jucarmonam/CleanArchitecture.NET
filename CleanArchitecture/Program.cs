using Application;
using Infrastructure;
using WebUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding a cors policy to allow connections from web apps
builder.Services.AddCors(options =>
{
    /*
    options.AddPolicy("AllowMyApp",
        policy => policy
            .WithOrigins("https://example.com"))
    */
    options.AddPolicy("AllowMyApp",
        policy => policy
            .AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    //HTTP Strict Transport Security(HSTS)
    //Is a header returned by the server (this API in this case) and instructs web browsers to prevent users from connecting over HTTP
    //However trying from the browser it redirects automatically from the http port to the https port
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Returns a 300 status code with a redirect response pointing them at the https port
app.UseHttpsRedirection();
app.UseCors("AllowMyApp");

app.MapControllers();

app.Run();

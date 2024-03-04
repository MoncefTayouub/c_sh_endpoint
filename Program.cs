
using Newtonsoft.Json ;
using solutionAPI;
using solutionAPI.Properties;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(option =>
    {
        option.AddPolicy("reactApp", policyB =>
        {
            policyB.WithOrigins("http://localhost:3000");
            policyB.AllowAnyHeader();
            policyB.AllowAnyMethod();
            policyB.AllowCredentials();
        });
    }
);
var app = builder.Build();

app.MapMethods("/", new[] { "GET", "POST" }, (HttpContext context) =>
{
    if (context.Request.Method == "GET")
    {
        return context.Response.WriteAsync("Hello endos!");
    }
    else if (context.Request.Method == "POST")
    {
        Obj bj = new Obj("hello", "word");
        
        string jsonString = JsonConvert.SerializeObject(bj);
        
        return context.Response.WriteAsync(jsonString);
    }

    return Task.CompletedTask;
});
app.UseCors("reactApp");
app.Run();
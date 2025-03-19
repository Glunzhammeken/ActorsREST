using ActorReposLib;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin().
                                  WithMethods("GET","PUT", "DELETE").
                                  AllowAnyHeader().
                                  SetPreflightMaxAge(TimeSpan.FromSeconds(14440));
                              });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ActorReposList>(new ActorReposList());

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

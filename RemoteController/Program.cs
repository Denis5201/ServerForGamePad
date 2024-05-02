using RemoteController.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(option => option.AllowAnyOrigin());

// Configure the HTTP request pipeline.
app.MapGrpcService<InputService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

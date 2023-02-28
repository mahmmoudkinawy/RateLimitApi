using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache(); //To keep tracking if the blocked ips + it works per server.

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

//builder.Services.Configure<IpRateLimitOptions>(options =>
//{
//    options.EnableEndpointRateLimiting = false;
//    options.StackBlockedRequests = false;
//    options.HttpStatusCode = StatusCodes.Status429TooManyRequests;
//    options.RealIpHeader = "X-Real-IP";
//    options.GeneralRules = new List<RateLimitRule>
//    {
//        new RateLimitRule
//        {
//            Endpoint = "*",
//            Period = "10s",
//            Limit = 2
//        }
//    };
//});

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

app.UseIpRateLimiting();

app.Run();

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Alerts.Application.CommandServices;
using Flowbit.Qullqa.Platform.Alerts.Application.QueryServices;
using Flowbit.Qullqa.Platform.Alerts.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Alerts.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;
using Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Dashboard.Application.CommandServices;
using Flowbit.Qullqa.Platform.Dashboard.Application.QueryServices;
using Flowbit.Qullqa.Platform.Dashboard.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Dashboard.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;
using Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Delivery.Application.CommandServices;
using Flowbit.Qullqa.Platform.Delivery.Application.QueryServices;
using Flowbit.Qullqa.Platform.Delivery.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Delivery.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Delivery.Domain.Repositories;
using Flowbit.Qullqa.Platform.Delivery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Iam.Application.CommandServices;
using Flowbit.Qullqa.Platform.Iam.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Iam.Application.Internal.OutboundServices;
using Flowbit.Qullqa.Platform.Iam.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using Flowbit.Qullqa.Platform.Product.Application.CommandServices;
using Flowbit.Qullqa.Platform.Product.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Product.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Product.Application.QueryServices;
using Flowbit.Qullqa.Platform.Product.Domain.Repositories;
using Flowbit.Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Sales.Application.CommandServices;
using Flowbit.Qullqa.Platform.Sales.Application.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Sales.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;
using Flowbit.Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Flowbit.Qullqa.Platform.Suppliers.Application.CommandServices;
using Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Suppliers.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// EF Core interceptor
builder.Services.AddSingleton<AuditableEntityInterceptor>();

// MySQL / EF Core
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<AuditableEntityInterceptor>();
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySQL(connectionString!)
           .AddInterceptors(interceptor);
});

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IBusinessCommandService, BusinessCommandService>();
builder.Services.AddScoped<IBusinessQueryService, BusinessQueryService>();
builder.Services.AddScoped<IRoleCommandService, RoleCommandService>();
builder.Services.AddScoped<IRoleQueryService, RoleQueryService>();

// Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<IWarehouseStockRepository, WarehouseStockRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();

// Sales
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();
builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();
builder.Services.AddScoped<ICustomerCommandService, CustomerCommandService>();
builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();

// Suppliers
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<ISupplierCommandService, SupplierCommandService>();
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();
builder.Services.AddScoped<IPurchaseOrderCommandService, PurchaseOrderCommandService>();
builder.Services.AddScoped<IPurchaseOrderQueryService, PurchaseOrderQueryService>();

// Delivery
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryCommandService, DeliveryCommandService>();
builder.Services.AddScoped<IDeliveryQueryService, DeliveryQueryService>();

// Alerts
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();

// Dashboard
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IMetricsSnapshotRepository, MetricsSnapshotRepository>();
builder.Services.AddScoped<IDashboardCommandService, DashboardCommandService>();
builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();

// Cortex Mediator logging behavior
builder.Services.AddScoped(typeof(LoggingCommandBehavior<>));

// Controllers with kebab-case naming and SCREAMING_SNAKE_CASE enum serialization
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
}).AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Enter your JWT token"
    });
    options.AddSecurityRequirement(_ => new Microsoft.OpenApi.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.OpenApiSecuritySchemeReference("Bearer"),
            new List<string>()
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Run EF migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseGlobalExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

// Custom JWT middleware
app.UseMiddleware<RequestAuthorizationMiddleware>();

app.MapControllers();

app.Run();

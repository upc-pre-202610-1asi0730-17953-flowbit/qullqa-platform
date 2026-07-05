using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;
using Flowbit.Qullqa.Platform.Iam.Application.CommandServices;
using Flowbit.Qullqa.Platform.Iam.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Iam.Application.Internal.OutboundServices;
using Flowbit.Qullqa.Platform.Iam.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using Flowbit.Qullqa.Platform.Iam.Resources;
using Flowbit.Qullqa.Platform.Products.Application.Acl;
using Flowbit.Qullqa.Platform.Products.Application.CommandServices;
using Flowbit.Qullqa.Platform.Products.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Products.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Products.Application.QueryServices;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;
using Flowbit.Qullqa.Platform.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Products.Interfaces.Acl;
using Flowbit.Qullqa.Platform.Products.Resources;
using Flowbit.Qullqa.Platform.Subscription.Application.Acl;
using Flowbit.Qullqa.Platform.Subscription.Application.CommandServices;
using Flowbit.Qullqa.Platform.Subscription.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Subscription.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Subscription.Application.QueryServices;
using Flowbit.Qullqa.Platform.Subscription.Domain.Repositories;
using Flowbit.Qullqa.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Subscription.Interfaces.Acl;
using Flowbit.Qullqa.Platform.Subscription.Resources;
using Flowbit.Qullqa.Platform.Dashboard.Application.CommandServices;
using Flowbit.Qullqa.Platform.Dashboard.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Dashboard.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Dashboard.Application.QueryServices;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;
using Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Dashboard.Resources;
using Flowbit.Qullqa.Platform.Alerts.Application.CommandServices;
using Flowbit.Qullqa.Platform.Alerts.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Alerts.Application.QueryServices;
using Flowbit.Qullqa.Platform.Alerts.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;
using Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Alerts.Infrastructure.Pipeline.BackgroundServices;
using Flowbit.Qullqa.Platform.Alerts.Resources;
using Flowbit.Qullqa.Platform.Deliveries.Application.CommandServices;
using Flowbit.Qullqa.Platform.Deliveries.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Deliveries.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Deliveries.Application.QueryServices;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Repositories;
using Flowbit.Qullqa.Platform.Deliveries.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Deliveries.Resources;
using Flowbit.Qullqa.Platform.Sales.Application.CommandServices;
using Flowbit.Qullqa.Platform.Sales.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Sales.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Application.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;
using Flowbit.Qullqa.Platform.Sales.Application.Acl;
using Flowbit.Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Sales.Interfaces.Acl;
using Flowbit.Qullqa.Platform.Sales.Resources;
using Flowbit.Qullqa.Platform.Shared.Application;
using Flowbit.Qullqa.Platform.Suppliers.Application.CommandServices;
using Flowbit.Qullqa.Platform.Suppliers.Application.Internal.CommandServices;
using Flowbit.Qullqa.Platform.Suppliers.Application.Internal.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Application.Acl;
using Flowbit.Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Acl;
using Flowbit.Qullqa.Platform.Suppliers.Resources;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Security;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Flowbit.Qullqa.Platform.Shared.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

// CORS — allows the Vue frontend (a different origin) to call this API.
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("QullqaFrontend",
        policy => policy.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Database — MySQL via EF Core. Connection string may contain %VAR% placeholders
// (see appsettings.json), expanded from environment variables at startup.
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Localization — no ResourcesPath override: each bounded context keeps its
// own Resources/ folder mirroring its namespace (e.g. Iam/Resources/IamMessages.resx),
// not a single root-level Resources/ folder, so the default convention
// (resx path = type's namespace, relative to the project root) is what we want.
builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>();
builder.Services.AddSingleton<IStringLocalizer<ProductMessages>, StringLocalizer<ProductMessages>>();
builder.Services.AddSingleton<IStringLocalizer<SalesMessages>, StringLocalizer<SalesMessages>>();
builder.Services.AddSingleton<IStringLocalizer<SuppliersMessages>, StringLocalizer<SuppliersMessages>>();
builder.Services.AddSingleton<IStringLocalizer<DeliveryMessages>, StringLocalizer<DeliveryMessages>>();
builder.Services.AddSingleton<IStringLocalizer<AlertsMessages>, StringLocalizer<AlertsMessages>>();
builder.Services.AddSingleton<IStringLocalizer<DashboardMessages>, StringLocalizer<DashboardMessages>>();
builder.Services.AddSingleton<IStringLocalizer<SubscriptionMessages>, StringLocalizer<SubscriptionMessages>>();

builder.Services.AddSingleton<ProblemDetailsFactory>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Qullqa Platform API",
        Version = "v1",
        Description = "Backend real de Qullqa — SaaS de gestión de inventario, ventas y proveedores para bodegas y farmacias independientes."
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });
    options.EnableAnnotations();
});

// Mediator — used for publishing domain events across bounded contexts
// (e.g. Product's StockLevelChangedEvent, consumed by Alerts in a future phase).
builder.Services.AddCortexMediator([typeof(Program)]);

// Shared infrastructure
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Subscription (Plan Management) Bounded Context — registered before IAM
// since Business.PlanId references Plans.

builder.Services.AddScoped<IPlanRepository, PlanRepository>();

builder.Services.AddScoped<IPlanCommandService, PlanCommandService>();
builder.Services.AddScoped<IPlanQueryService, PlanQueryService>();

builder.Services.AddScoped<ISubscriptionContextFacade, SubscriptionContextFacade>();

// IAM Bounded Context

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IBusinessCommandService, BusinessCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IBusinessQueryService, BusinessQueryService>();
builder.Services.AddScoped<IRoleQueryService, RoleQueryService>();

// Product & Inventory Management Bounded Context

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
builder.Services.AddScoped<IBatchRepository, BatchRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();

builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IWarehouseCommandService, WarehouseCommandService>();
builder.Services.AddScoped<IInventoryCommandService, InventoryCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<IWarehouseQueryService, WarehouseQueryService>();
builder.Services.AddScoped<IInventoryQueryService, InventoryQueryService>();
builder.Services.AddScoped<IBatchQueryService, BatchQueryService>();
builder.Services.AddScoped<IStockMovementQueryService, StockMovementQueryService>();

builder.Services.AddScoped<IProductContextFacade, ProductContextFacade>();

// Sales & POS Management Bounded Context

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();
builder.Services.AddScoped<ICustomerCommandService, CustomerCommandService>();
builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();
builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();

builder.Services.AddScoped<ISalesContextFacade, SalesContextFacade>();

// Supplier & Replenishment Management Bounded Context

builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

builder.Services.AddScoped<ISupplierCommandService, SupplierCommandService>();
builder.Services.AddScoped<IPurchaseOrderCommandService, PurchaseOrderCommandService>();
builder.Services.AddScoped<ISupplierQueryService, SupplierQueryService>();
builder.Services.AddScoped<IPurchaseOrderQueryService, PurchaseOrderQueryService>();

builder.Services.AddScoped<ISupplierContextFacade, SupplierContextFacade>();

// Delivery Tracking Bounded Context

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();

builder.Services.AddScoped<IDeliveryCommandService, DeliveryCommandService>();
builder.Services.AddScoped<IDeliveryQueryService, DeliveryQueryService>();

// Alerts & Operational Monitoring Bounded Context

builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertRuleRepository, AlertRuleRepository>();

builder.Services.AddScoped<IAlertCommandService, AlertCommandService>();
builder.Services.AddScoped<IAlertRuleCommandService, AlertRuleCommandService>();
builder.Services.AddScoped<IAlertQueryService, AlertQueryService>();
builder.Services.AddScoped<IAlertRuleQueryService, AlertRuleQueryService>();

// Event handlers are auto-discovered by AddCortexMediator's assembly scan
// (StockLevelChangedEventHandler, BatchRegisteredEventHandler) — no
// explicit registration needed here.
builder.Services.AddHostedService<AlertExpirationSweepJob>();

// Dashboard & Analytics Bounded Context — no aggregates of its own, composes
// Product/Sales via their ACL facades (already registered above).

builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

var app = builder.Build();

// Apply pending migrations on startup (safe to call even when schema is up to date).
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("QullqaFrontend");

app.UseRouting();

app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

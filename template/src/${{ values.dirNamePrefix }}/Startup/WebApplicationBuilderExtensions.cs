namespace ${{ values.namespacePrefix }}.Startup;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Host.AddFundaDefaults("${{ values.applicationName }}");

        // Add services to the container.

        builder.Services.AddHealthChecks();

        {%- if values.enableControllers %}
        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        {%- endif %}

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        {%- if values.enableEndpoints %}
        builder.Services.AddEndpointsApiExplorer();
        {%- endif %}

        builder.Services.AddSwaggerGen();

        builder.Services.AddFundaMetrics(
            "${{ values.applicationName }}",
            config => config.AddDogstatsd(
                hostname: builder.Configuration["Statsd:Hostname"],
                port: int.Parse(builder.Configuration["Statsd:Port"]),
                prefix: builder.Configuration["Statsd:Prefix"],
                environmentName: builder.Configuration["Statsd:EnvironmentName"]));

        {%- if values.enableControllers %}
		
        // MVC Controllers
        builder.Services.AddControllers();
        {%- endif %}

        {%- if values.enableFundaMessaging %}
		
        // Funda.Messaging
        builder.AddFundaMessaging();
		{%- endif %}
		
        // Fluent Validation
		builder.Services.AddFluentValidation(fv => 
            fv.RegisterValidatorsFromAssemblyContaining<CreateMeasurementCommandValidator>());
		
		{%- if values.enableCosmosDb %}
        
		// Add Cosmos DB
        builder.Services.AddCosmosDbInfrastructure(options =>
            builder.Configuration.GetSection("CosmosDb").Bind(options)
        );
        {%- endif %}

        {%- if values.enableEntityFramework %}
		
        // Add repository based on Entity Framework
        builder.Services.AddScoped<IMeasurementRepository, EntityFrameworkMeasurementRepository>();
		{%- endif %}

        {%- if values.enableInMemoryRepository %}

        // Add repository based on in memory storage
        builder.Services.AddSingleton<IMeasurementRepository, InMemoryMeasurementRepository>();
        {%- endif %}


        {%- if values.enableEntityFrameworkSqlServer %}

		// Configure EntityFramework to use SQL Server
        builder.Services
            .AddDbContext<IDbContext, SqlServerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        {%- elif values.enableEntityFrameworkCosmos %}

		// Configure EntityFramework to use CosmosDb
        builder.Services.AddDbContext<IDbContext, CosmosDbContext>(options =>
            options.UseCosmos(builder.Configuration["CosmosDb:Endpoint"],
                builder.Configuration["CosmosDb:PrimaryKey"],
                builder.Configuration["CosmosDb:DatabaseId"]));
		{%- endif %}

        builder.Services.AddMediatR(typeof(IMeasurementRepository).Assembly);

        return builder;
    }
}

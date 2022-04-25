using ${{ values.namespacePrefix }}.Startup;

var builder = WebApplication.CreateBuilder(args).ConfigureBuilder();

var app = builder.Build().ConfigureApp();

app.Run();

// Make Program class visible to component tests
public partial class Program { }

using ${{ values.namespacePrefix }}.Startup;

var builder = WebApplication.CreateBuilder(args).ConfigureBuilder();

var app = builder.Build().Configure();

app.Run();

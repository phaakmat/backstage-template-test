using ${{ values.namespacePrefix }};

var builder = WebApplication.CreateBuilder(args).Configure();

var app = builder.Build().Configure();

app.Run();

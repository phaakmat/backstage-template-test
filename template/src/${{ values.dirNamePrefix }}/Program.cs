using ${{ values.namespacePrefix }};
using ${{ values.namespacePrefix }}.Extensions;

var builder = WebApplication.CreateBuilder(args).Configure();

var app = builder.Build().Configure();

app.Run();

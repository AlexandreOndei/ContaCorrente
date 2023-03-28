using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace ContaCorrente;

public class ContaCorrenteWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<ContaCorrenteWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}

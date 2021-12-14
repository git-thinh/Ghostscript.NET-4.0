using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FileView
{
    public class GSService : BackgroundService
    {
        readonly string _serviceName = nameof(GSService);
        readonly ILogger _logger;
        readonly IConfiguration _configuration;
        readonly IWebHostEnvironment _environment;
        public GSService(ILoggerFactory loggerFactory,
            IWebHostEnvironment env,
            IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger(GetType());
            _configuration = configuration;
            _environment = env;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceInit();
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{_serviceName} is starting.");
            stoppingToken.Register(() => _logger.LogInformation($"{_serviceName} background task is stopping."));

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    if (_isRunning == false)
            //    {
            //        _isRunning = true;

            //        _isRunning = false;
            //    }
            //    await Task.Delay(500, stoppingToken);
            //}

            await Task.Delay(Timeout.Infinite, stoppingToken);
            _logger.LogDebug($"{_serviceName} is stopping.");
        }

        void _serviceInit()
        { 
        }
    }
}

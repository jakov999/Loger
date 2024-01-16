using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NCrontab;


namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Logging a message at {0}", DateTime.Now);

                string crontime= _configuration.GetValue<string>("LoggingInterval");
                var cronSchedule = CrontabSchedule.Parse(crontime);
                var nextRun = cronSchedule.GetNextOccurrence(DateTime.Now);
                var delay = nextRun - DateTime.Now;

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}

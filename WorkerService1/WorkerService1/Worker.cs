using NLog;
using NLog.Extensions.Logging;



namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Logging a message at {0}", DateTime.Now);
                await Task.Delay(TimeSpan.FromSeconds(600), stoppingToken);
            }
        }
    }
}

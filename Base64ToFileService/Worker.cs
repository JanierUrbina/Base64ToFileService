using Base64ToFileService.Archivos;

namespace Base64ToFileService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string _ConnectionString;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _ConnectionString = configuration.GetConnectionString("MyConnection"); // Obtenemos la cadena de conexión
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                Proceso();
                await Task.Delay(1 * 60 * 1_000, stoppingToken);
            }
        }

        private void Proceso()
        {
            Console.WriteLine("Iniciando el servicio.");
            Consultas.Obtener(_ConnectionString);
            Console.WriteLine("Servicio Finalizado.");
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker starts");
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stops");
            await base.StopAsync(cancellationToken);
        }
        
    }
}

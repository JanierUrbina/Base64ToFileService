using Base64ToFileService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    }).UseWindowsService()  //Método que permite ejecutar el servicio en Windows. 
    .Build();

await host.RunAsync();

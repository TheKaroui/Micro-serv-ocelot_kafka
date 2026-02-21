using Confluent.Kafka;
namespace catalog.Kafka;
public class OrderEventsConsumer : BackgroundService
{
    private readonly IConfiguration _cfg;
    private readonly ILogger<OrderEventsConsumer> _logger;
    public OrderEventsConsumer(IConfiguration cfg, ILogger<OrderEventsConsumer> logger){ _cfg = cfg; _logger = logger; }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bootstrap = _cfg["Kafka:BootstrapServers"] ?? Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP") ?? "kafka:9092";
        var conf = new ConsumerConfig
        {
            GroupId = "catalog-consumers",
            BootstrapServers = bootstrap,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using var consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
        consumer.Subscribe("orders-events");
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var cr = consumer.Consume(stoppingToken);
                _logger.LogInformation("Consumed event: {evt}", cr.Message.Value);
                // Simple processing placeholder
            }
        }
        catch (OperationCanceledException) { }
        finally { consumer.Close(); }
    }
}

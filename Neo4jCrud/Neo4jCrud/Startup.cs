using Neo4jClient;

namespace Neo4jCrud
{
    public class Startup
    {
        public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        ///     The application configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Hosting environment
        /// </summary>
        public IWebHostEnvironment Environment { get; set; }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var neo4JClient = new BoltGraphClient("bolt://localhost:7687", "neo4j", "wissem");
            neo4JClient.ConnectAsync();
            services.AddSingleton<IGraphClient>(neo4JClient);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddNeo4jAnnotations();
        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

    }
}

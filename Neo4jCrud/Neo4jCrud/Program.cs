namespace Neo4jCrud
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = Startup.InitializeApp(args);
            app.Run();
        }

    }
}
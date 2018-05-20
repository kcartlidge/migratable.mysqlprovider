using System;
using Migratable.Providers;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Testing MySqlProvider using Migratable");
            Console.WriteLine();
            Console.WriteLine("This assumes a local 'migratable_test' database exists.");
            Console.WriteLine("The user is 'test', as is the password. Port is 3306.");
            Console.WriteLine();
            var provider = new MySqlProvider(
                "server=localhost;" +
                "port=3306;" +
                "user=test;" +
                "password=test;" +
                "database=migratable_test;" +
                "SslMode=none");

            var migrator = new Migratable.Migrator(provider);
            migrator.LoadMigrations("./migrations");

            Console.WriteLine("Getting version");
            var version = provider.GetVersion();

            Console.WriteLine("Moving to version 1");
            migrator.SetVersion(1);
            Console.WriteLine("Version {0}", provider.GetVersion());

            Console.WriteLine("Moving to version 2");
            migrator.SetVersion(2);
            Console.WriteLine("Version {0}", provider.GetVersion());

            Console.WriteLine("Rolling back to version 1");
            migrator.RollBackward(1);
            Console.WriteLine("Version {0}", provider.GetVersion());
        }
    }
}

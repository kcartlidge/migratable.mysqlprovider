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
            Console.WriteLine("Getting version");
            var version = provider.GetVersion();
            Console.WriteLine("Current database version: {0}", version);
            Console.WriteLine("Incrementing the version number");
            var sql = "insert into `version` (`version_number`) values ({0})";
            provider.Execute(string.Format(sql, version + 1));
            version = provider.GetVersion();
            Console.WriteLine("Current database version: {0}", version);
            Console.WriteLine();
        }
    }
}

using System;
using Migratable.Interfaces;

namespace Migratable.MySqlProvider
{
    public class MySqlProvider : IProvider
    {
        private readonly string connectionString;

        public MySqlProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Execute(string instructions)
        {
            throw new NotImplementedException();
        }

        public long GetVersion()
        {
            throw new NotImplementedException();
        }

        public void SetVersion(long versionNumber)
        {
            throw new NotImplementedException();
        }
    }
}

using Migratable.Interfaces;
using MySql.Data.MySqlClient;

namespace Migratable.Providers
{
    /// <summary>Database provider for use with Migratable.</summary>
    public class MySqlProvider : IProvider
    {
        private readonly string connectionString;

        /// <summary>Create a database provider for use with Migratable.</summary>
        public MySqlProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// User-friendly description of this connection.
        /// No connection string credentials are included.
        /// </summary>
        public string Describe()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                return $"MySQL/MariaDB against {conn.DataSource} {conn.Database}";
            }
        }

        /// <summary>
        /// Run a given SQL statement (in a transaction).
        /// If there is an error the transaction is rolled back.
        /// </summary>
        public void Execute(string instructions)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                var cmd = new MySqlCommand(instructions, conn);
                cmd.Transaction = transaction;
                try
                {
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                conn.Close();
            }
        }

        /// <summary>Gets the migration version number.</summary>
        public int GetVersion()
        {
            var v = ExecuteScalar(
                "create table if not exists `migratable_version` ( " +
                "  `id` int(11) NOT NULL auto_increment," +
                "  `version_number` INT(11) NOT NULL, " +
                "  `actioned` TIMESTAMP NOT NULL " +
                "    DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, " +
                "  PRIMARY KEY  (`id`)" +
                ")"
            );
            v = ExecuteScalar("select version_number from migratable_version order by id desc limit 1");
            if (v == null)
            {
                SetVersion(0);
                return 0;
            }
            return (int)v;
        }

        /// <summary>Sets the migration version number.</summary>
        public void SetVersion(int versionNumber)
        {
            var sql = "insert into `migratable_version` (`version_number`) values ({0})";
            Execute(string.Format(sql, versionNumber));
        }

        private object ExecuteScalar(string instructions)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(instructions, conn);
                var result = cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
        }
    }
}

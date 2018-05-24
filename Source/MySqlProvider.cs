using System;
using Migratable.Interfaces;
using MySql.Data.MySqlClient;

namespace Migratable.Providers
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
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(instructions, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public int GetVersion()
        {
            var v = ExecuteScalar(
                "create table if not exists `migratable_version` ( " +
                "  `id` int(11) NOT NULL auto_increment," +
                "  `version_number` INT(11) NOT NULL, " +
                "  `actioned` TIMESTAMP NOT NULL, " +
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

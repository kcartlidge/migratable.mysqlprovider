﻿using Migratable.Interfaces;
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

        public string Describe()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                return $"MySQL/MariaDB against {conn.DataSource} {conn.Database}";
            }
        }

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

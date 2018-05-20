# Migratable.MySqlProvider

Database provider for [https://github.com/kcartlidge/migratable](Migratable) adding support for *MySql*/*MariaDB*.

## Usage

(See [https://github.com/kcartlidge/migratable](Migratable) for more details)

``` cs
var provider = new Migratable.MySqlProvider("your-connection-string");
var migrator = new Migratable.Migrator(provider);
migrator.LoadMigrations("./migrations");
migrator.RollForward(5);
```

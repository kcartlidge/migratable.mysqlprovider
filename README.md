# Migratable.MySqlProvider

Database provider for https://github.com/kcartlidge/migratable adding support for *MySql*/*MariaDB*.

## Installation

Both this provider and the main Migratable are on Nuget.

## Usage

See https://github.com/kcartlidge/migratable for more details, including nuget.

``` cs
var provider = new Migratable.Providers.MySqlProvider("connection-string");
var migrator = new Migratable.Migrator(provider);
migrator.LoadMigrations("./migrations");
migrator.SetVersion(5);
```

## Examples

There are two examples in the ```Examples``` subfolder.
Each is a complete .Net Core solution, with a database dependency.
You will need a local database named *migratable_test* (it may be empty).
There should be a user *test* with a password *test* and suitable access.

``` sql
create database `migratable_test`;
grant ALL PRIVILEGES on *.* to 'test'@'localhost' identified by 'test';
```

* **Standalone** - this hits the provider directly
* **Migratable** - this uses [Migratable](https://github.com/kcartlidge/migratable) via *nuget*

---

## MIT Licence

Copyright (c) 2018 K Cartlidge

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

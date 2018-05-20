create table if not exists `accounts` (
  `id` int(11) NOT NULL auto_increment,
  `username` VARCHAR(50) NOT NULL,
  `created` TIMESTAMP NOT NULL,
  PRIMARY KEY  (`id`)
);

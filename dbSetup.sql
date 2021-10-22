-- CREATE TABLE IF NOT EXISTS accounts(
--   id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
--   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
--   updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
--   name varchar(255) COMMENT 'User Name',
--   email varchar(255) COMMENT 'User Email',
--   picture varchar(255) COMMENT 'User Picture'
-- ) default charset utf8 COMMENT '';



SELECT * FROM recipes;
SELECT * FROM spices;
SELECT * FROM accounts;

-- CREATE TABLE IF NOT EXISTS accounts(
--   id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
--   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
--   updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
--   name varchar(255) COMMENT 'User Name',
--   email varchar(255) COMMENT 'User Email',
--   picture varchar(255) COMMENT 'User Picture'
-- ) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS spices(
id int NOT NULL AUTO_INCREMENT primary key,
recipesId int,
name VARCHAR(255) NOT NULL,
family VARCHAR(255) NOT NULL,
FOREIGN KEY(recipesId) REFERENCES recipes(id)
)default charset utf8;

CREATE TABLE IF NOT EXISTS recipes(  
    id int NOT NULL primary key AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;

SELECT 
s.*,
r.*
FROM spices s 
LEFT JOIN recipes r on r.id = s.recipeId 
WHERE s.id = 2;

auth off on mine - nooooooooo =(
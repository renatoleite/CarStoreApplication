IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SQL_CAR_STORE')
BEGIN
	CREATE DATABASE	[SQL_CAR_STORE]
END;
GO
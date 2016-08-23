USE master
GO

IF EXISTS(select * from sys.databases where name = 'LoggerApp')
    DROP DATABASE [LoggerApp]
GO

CREATE DATABASE [LoggerApp]
GO
USE [LoggerApp]
GO

IF OBJECT_ID(N'application', N'U') IS NOT NULL
    DROP TABLE [application]
GO

IF OBJECT_ID(N'log', N'U') IS NOT NULL
    DROP TABLE [log]
GO

IF OBJECT_ID(N'token', N'U') IS NOT NULL
    DROP TABLE [token]
GO

IF OBJECT_ID(N'config', N'U') IS NOT NULL
    DROP TABLE [config]
GO

CREATE TABLE [application](
    application_id varchar(32) PRIMARY KEY NOT NULL,
    display_name   varchar(25) NOT NULL,
    secret         varchar(25) NOT NULL)
GO

CREATE TABLE [log](
    log_id         int           NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    logger         varchar(256)  NOT NULL,
    level          varchar(256)  NOT NULL,
    message        varchar(2048) NOT NULL,
    application_id varchar(32)   REFERENCES [application](application_id)
    ON UPDATE CASCADE ON DELETE CASCADE)
GO

CREATE TABLE [token](
    token_id		uniqueidentifier PRIMARY KEY NOT NULL,
    application_id  varchar(32)   REFERENCES [application](application_id),
    access_token	uniqueidentifier NOT NULL,
	issued_on		datetime	NOT NULL,
	expires_on		datetime	NOT NULL)
GO

CREATE TABLE config(
    id		INT PRIMARY KEY NOT NULL,
	session_lifetime		int	NOT NULL)
GO

INSERT INTO config VALUES(1, 900);
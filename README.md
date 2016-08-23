# logger-api
Log application that allows clients register their applications to a Rest API and then log messages.

# Rate-limit Implementation
Limit the API calls to 60 calls per minute.

# Token Based Authentication
Use token based auth to authenticate users.

# Run application

Initialize the databases

- Create and initialize the database (and tables)  by running the sql script located in /SqlServer/create_database.sql


Configure Web.Config

- Change the Connection string in the hibernate.cfg.xml file of the LoggerApi project with your SQL Server DB info.


Run the application

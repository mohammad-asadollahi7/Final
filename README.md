###It is an online store mini-project that has buyer and seller admin roles, each role can perform special operations.

####**Follow the steps below to run the project:**
####1- Create an empty database named 'final' on your sql server.
####2- Right-click on it and select the tasks and restore database.
####3- In the opened window, in the source tab, select device and finally select the database backup file located in the 'Infra.Db.EF' project, 'Database' folder.
####4- Finally, in the options tab, please check the "overwrite the existing database [WITH REPLACE]", Uncheck "Take tail-log backup before restoring" and check "Close existing connections to destination database".
####5- After the successful restore db, open the project and set the startup project on the API and MVC projects and run that.
####6- You can test the functionality of the customer (username: customer, pass: 123456), seller (username: seller, pass: 123456) and admin (username: admin, pass:123456).

# Michael-Sloushch-12-07-2019-FullStack
 simple, responsive, web app that shows the weather of some city.

#Additional features

- Added search among favorite cities as well
- If the last saved weather forecast date is too old, the app would update it

#Necessary settings and actions for correct operation

- run the SQL script from file “CreateDbScript.sql” in order to create database (please, check that path to save database file itself is exist)

- set license key to access the AccuWeather API in Web.Config file under section appSettings=>WeaherServiceApiKey

- set connection string to the database in Web.Config file under section connectionStrings=>ConnectionString

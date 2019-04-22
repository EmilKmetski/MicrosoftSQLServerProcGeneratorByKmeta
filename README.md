# MicrosoftSQLServerProcGeneratorByKmeta

This is application which generates CRUD MSSQL Procedures
Types of procedures:
Insert 
Update 
Delete
GET takes all columns from the table without any filters.
GET takes all columns from the table filtered by FK.
GET takes all columns from the table filtered by PK.
GET takes all columns from the table filtered by any column with and combination between supplied columns.
Creates non clustered indexes for FK columns
Creates non clustered covering indexes for FK columns
Creates non clustered covering indexes for All FK columns
Creates unique indexes for All FK columns
It reads the meta data from the SQL Server and generate the procedures
There is option for the Insert to choose the result either result set with all data or PK as output 
 
There is an option to set specific mappings for columns in the Stored procedures and Parameters
They can be configured from this file Configurations-> CustomColumsMappings.xlsx 
 Column Name – Colum name from database
 Value 		- the value that you want to be returned  for example if its audit date column you can set GETDATE() or SYSUTCDATETIME()
 INSERT PARAMETER – Enables or disables the column as parameter in the SP
 INSERT COLUMNS – Enables or disables the column in the Insert SP
The rest follow the same principle 
There is an option to have different template files for the different Operations (INSERT,UPDATE,GETBYID……..)
There are settings files in the Configurations directory.
This configuration is saved in this file: Configurations -> TemplateConfigurations.xlsx
In the file there are file name templates and the operation for which is the template.
Other options format SQL Code for free 
Search Stored procedures by different criteria for the chosen database.
 
SQL Connection Settings in the App.config file
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="ServerAddress" value="localhost "/>
    <add key="ServerUser" value="test"/>
    <add key="ServerUserPassword" value="123456"/> 
 </appSettings>


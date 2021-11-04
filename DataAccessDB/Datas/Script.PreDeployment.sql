/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
if(EXISTS(Select * from sys.objects WHERE Type='U' and Name='InfosPerso'))
DELETE FROM InfosPerso;
if(EXISTS(Select * from sys.objects WHERE Type='U' and Name='Users'))
DELETE FROM Users;
if(EXISTS(Select * from sys.objects WHERE Type='U' and Name='Parametres'))
DELETE FROM Parametres;
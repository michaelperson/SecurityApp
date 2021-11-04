/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT Users ON 
INSERT INTO Users([ID],[Login],[Password],[Email], [Role]) VALUES (1,'Admin', 'admin','admin@test.be', 'Admin')
INSERT INTO Users([ID],[Login],[Password],[Email], [Role]) VALUES (2,'Mike','Test1234','Mike@test.be', 'User')
SET IDENTITY_INSERT Users OFF
 
SET IDENTITY_INSERT InfosPerso ON
GO
INSERT INTO InfosPerso  ([ID],[IsMarried],[CompteEnBanque],[NbEnfants],[Rue],[Ville],[IdUser]) VALUES(1,0,'BE56-0000000000-97',5,'Allée des coucou,23','Charleroi',1);

INSERT INTO InfosPerso  ([ID],[IsMarried],[CompteEnBanque],[NbEnfants],[Rue],[Ville],[IdUser]) VALUES(2,0,'BE86-0000000097-97',2,'Allée des cerisiers,2','Bruxelles',2);
GO
SET IDENTITY_INSERT InfosPerso OFF
GO
SET IDENTITY_INSERT Parametres ON
GO
INSERT INTO Parametres  ([ID],[Name], [Value]) VALUES(1,'Taxes', 'MAX');
INSERT INTO Parametres  ([ID],[Name], [Value]) VALUES(2,'Key', '15236E8745Abcf'); 
SET IDENTITY_INSERT Parametres OFF
GO
	 

CREATE TABLE [dbo].[InfosPerso]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
    IsMarried bit,
	CompteEnBanque nvarchar(28),
	NbEnfants tinyint,
	Rue nvarchar(50),
	Ville nvarchar(250),
	IdUser int, 
    CONSTRAINT [FK_InfosPerso_ToUser] FOREIGN KEY (IdUser) REFERENCES [Users]([ID])
)

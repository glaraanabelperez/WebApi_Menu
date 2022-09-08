 use master 
 alter database Menu_Practica set single_user with rollback immediate


IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Menu_Practica')
DROP DATABASE Menu_Practica
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Product_Categorie]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Product] DROP CONSTRAINT FK_Product_Categorie
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Categorie_User]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Categorie] DROP CONSTRAINT FK_Categorie_user
GO

CREATE DATABASE [Menu_Practica]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

----------------------------------------------------Init

use [Menu_Practica]
GO
DECLARE @Error int
BEGIN TRAN

CREATE TABLE [dbo].[Users] (
	Id_User [int] IDENTITY (1, 1) NOT NULL ,
	Business_Name [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	Slogan [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL ,
	user_email [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Password] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	Phone [int] NOT NULL ,
	Direction varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	Ig [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	Facebook [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	Logo [nvarchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL ,
	Orders_Whatsapp tinyint  NOT NULL ,
) ON [PRIMARY]
GO

CREATE TABLE [dbo].Products (
	Id_Product [int] IDENTITY (1, 1) NOT NULL ,
	Id_Categorie_FK [int] NOT NULL ,
	Id_User_FK [int] NOT NULL ,
	[State] tinyint  NOT NULL ,
	Title varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	Subtitle varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Description] varchar (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	Name_Image varchar (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	CreatedOn DateTime not null,
	Price Money  NOT NULL ,
	Featured tinyint  NOT NULL,
	Promotion [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL ,
) ON [PRIMARY] 
GO

CREATE TABLE [dbo].Categories (
	Id_Category [int] IDENTITY (1, 1) NOT NULL ,
	[Description] varchar (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	Id_user_FK [int] NOT NULL 
) ON [PRIMARY] 
GO


--PK

ALTER TABLE [dbo].Categories WITH NOCHECK ADD 
	CONSTRAINT [PK_Category] PRIMARY KEY   
	( Id_Category )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].Users WITH NOCHECK ADD 
	CONSTRAINT [PK_User] PRIMARY KEY   
	( Id_User )  ON [PRIMARY] 
GO

ALTER TABLE [dbo].Products WITH NOCHECK ADD 
	CONSTRAINT [PK_Product] PRIMARY KEY   
	( Id_Product)  ON [PRIMARY] 
GO

--FK
ALTER TABLE [dbo].Products ADD 
	CONSTRAINT [FK_Product_Category] FOREIGN KEY 
	(
		Id_Categorie_FK
	) REFERENCES [dbo].[Categories] (
		Id_Category
	)
GO

ALTER TABLE [dbo].Categories ADD 
	CONSTRAINT [FK_Category_User] FOREIGN KEY 
	(
		Id_User_FK
	) REFERENCES [dbo].[Users] (
		Id_User
	)
GO


--Check
ALTER TABLE Users
ADD CONSTRAINT 
   CHK_email CHECK (user_email like '%_@__%.__%')

ALTER TABLE [dbo].Products WITH NOCHECK ADD 
	CONSTRAINT Price_check DEFAULT (0) FOR Price
GO
--SET @Error=@@ERROR

COMMIT TRAN

If @@Error<>0 
	BEGIN
	PRINT 'Ha ecorrido un error. Abortamos la transacción'
	--Se lo comunicamos al usuario y deshacemos la transacción
	--todo volverá a estar como si nada hubiera ocurrido
	ROLLBACK TRAN
	END

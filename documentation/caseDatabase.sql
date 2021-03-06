USE [master]
GO
/****** Object:  Database [AutomatCase]    Script Date: 30.08.2021 19:40:28 ******/
CREATE DATABASE [AutomatCase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AutomatCase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AutomatCase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AutomatCase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\AutomatCase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AutomatCase] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutomatCase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AutomatCase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AutomatCase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AutomatCase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AutomatCase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AutomatCase] SET ARITHABORT OFF 
GO
ALTER DATABASE [AutomatCase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AutomatCase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AutomatCase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AutomatCase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AutomatCase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AutomatCase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AutomatCase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AutomatCase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AutomatCase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AutomatCase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AutomatCase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AutomatCase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AutomatCase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AutomatCase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AutomatCase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AutomatCase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AutomatCase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AutomatCase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AutomatCase] SET  MULTI_USER 
GO
ALTER DATABASE [AutomatCase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AutomatCase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AutomatCase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AutomatCase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AutomatCase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AutomatCase] SET QUERY_STORE = OFF
GO
USE [AutomatCase]
GO
/****** Object:  Table [dbo].[AutomatSlots]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutomatSlots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SlotNumber] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AutomatSlots] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ParentId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](350) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AutomatSlotProducts]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutomatSlotProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AutomatSlotId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AutomatSlotProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_AutomatProducts]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_AutomatProducts]
AS
SELECT dbo.AutomatSlotProducts.Id, dbo.AutomatSlotProducts.AutomatSlotId, dbo.AutomatSlots.SlotNumber, dbo.AutomatSlotProducts.ProductId, dbo.Products.Name, dbo.Products.CategoryId, dbo.Categories.Name AS Expr1
FROM   dbo.AutomatSlotProducts INNER JOIN
             dbo.Products ON dbo.AutomatSlotProducts.ProductId = dbo.Products.Id INNER JOIN
             dbo.Categories ON dbo.Products.CategoryId = dbo.Categories.Id INNER JOIN
             dbo.AutomatSlots ON dbo.AutomatSlotProducts.AutomatSlotId = dbo.AutomatSlots.Id
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryFeatureOptions]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryFeatureOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryFeatureId] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[IsSelectQuantity] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CategoryFeatureOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryFeatures]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryFeatures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_CategoryFeatures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProductFeatureOptions]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProductFeatureOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDetailId] [int] NOT NULL,
	[CategoryFeatureOptionId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderProductFeatureOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [uniqueidentifier] NOT NULL,
	[ProcessId] [uniqueidentifier] NOT NULL,
	[OrderStatus] [tinyint] NOT NULL,
	[PaymentTypeOptionId] [int] NOT NULL,
	[AutomatSlotId] [int] NOT NULL,
	[PaymentTotal] [decimal](18, 2) NOT NULL,
	[RefundAmount] [decimal](18, 2) NULL,
	[OrderDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTypeOptions]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTypeOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[PaymentTypeId] [int] NOT NULL,
	[RefundPaymentStatus] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PaymentTypeOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTypes]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCarts]    Script Date: 30.08.2021 19:40:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCarts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProcessId] [uniqueidentifier] NOT NULL,
	[AutomatSlotId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[CategoryFeatureOptionId] [int] NULL,
	[FeatureOptionQuantity] [int] NULL,
	[PaymentTypeOptionId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210827200920_mig', N'5.0.9')
GO
SET IDENTITY_INSERT [dbo].[AutomatSlotProducts] ON 

INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (7, 1, 1, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (8, 2, 2, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (9, 3, 3, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (10, 4, 7, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (11, 5, 9, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (12, 6, 10, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (13, 7, 12, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (15, 8, 15, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (16, 9, 16, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (17, 10, 51, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (18, 11, 52, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (19, 12, 53, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (20, 13, 54, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (21, 14, 55, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (22, 15, 56, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (23, 16, 57, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (24, 17, 58, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (25, 18, 59, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (26, 19, 60, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (27, 20, 61, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (28, 21, 62, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (29, 22, 63, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (30, 23, 64, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (31, 24, 65, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (32, 25, 66, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (33, 26, 67, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (34, 27, 68, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (35, 28, 69, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (36, 29, 13, NULL, NULL)
INSERT [dbo].[AutomatSlotProducts] ([Id], [AutomatSlotId], [ProductId], [CreatedDate], [ModifiedDate]) VALUES (37, 30, 14, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AutomatSlotProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[AutomatSlots] ON 

INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (1, 1, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (2, 2, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (3, 3, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (4, 4, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (5, 5, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (6, 6, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (7, 7, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (8, 8, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (9, 9, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (10, 10, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (11, 11, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (12, 12, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (13, 13, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (14, 14, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (15, 15, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (16, 16, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (17, 17, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (18, 18, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (19, 19, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (20, 20, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (21, 21, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (22, 22, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (23, 23, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (24, 24, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (25, 25, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (26, 26, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (27, 27, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (28, 28, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (29, 29, NULL, NULL)
INSERT [dbo].[AutomatSlots] ([Id], [SlotNumber], [CreatedDate], [ModifiedDate]) VALUES (30, 30, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AutomatSlots] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (1, N'İçecek', 0, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (2, N'Soğuk İçecek', 1, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (3, N'Sıcak İçecek', 1, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (4, N'Yiyecek', 0, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (5, N'Cips', 4, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (6, N'Kraker', 4, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (7, N'Kek', 4, 0, CAST(N'2021-08-28T22:53:13.057' AS DateTime), CAST(N'2021-08-28T23:11:10.393' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (8, N'Gofret', 4, 0, CAST(N'2021-08-28T22:54:22.867' AS DateTime), NULL)
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (12, N'Kuruyemiş', 4, 0, CAST(N'2021-08-29T19:34:43.630' AS DateTime), CAST(N'2021-08-30T15:43:23.210' AS DateTime))
INSERT [dbo].[Categories] ([Id], [Name], [ParentId], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (30, N'Çikolata', 4, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[CategoryFeatureOptions] ON 

INSERT [dbo].[CategoryFeatureOptions] ([Id], [CategoryFeatureId], [Name], [IsSelectQuantity], [CreatedDate], [ModifiedDate]) VALUES (1, 1, N'Şekerli', 1, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[CategoryFeatureOptions] ([Id], [CategoryFeatureId], [Name], [IsSelectQuantity], [CreatedDate], [ModifiedDate]) VALUES (2, 1, N'Şekersiz', 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[CategoryFeatureOptions] ([Id], [CategoryFeatureId], [Name], [IsSelectQuantity], [CreatedDate], [ModifiedDate]) VALUES (3, 2, N'Buzlu', 1, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[CategoryFeatureOptions] ([Id], [CategoryFeatureId], [Name], [IsSelectQuantity], [CreatedDate], [ModifiedDate]) VALUES (4, 2, N'Buzsuz', 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[CategoryFeatureOptions] OFF
GO
SET IDENTITY_INSERT [dbo].[CategoryFeatures] ON 

INSERT [dbo].[CategoryFeatures] ([Id], [CategoryId], [Name], [CreatedDate], [ModifiedDate]) VALUES (1, 3, N'Şeker', CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[CategoryFeatures] ([Id], [CategoryId], [Name], [CreatedDate], [ModifiedDate]) VALUES (2, 2, N'Buz', CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[CategoryFeatures] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice], [CreatedDate], [ModifiedDate]) VALUES (5, 6, 2, 3, CAST(14.50 AS Decimal(18, 2)), CAST(N'2021-08-28T00:48:28.310' AS DateTime), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice], [CreatedDate], [ModifiedDate]) VALUES (6, 7, 1, 2, CAST(3.00 AS Decimal(18, 2)), CAST(N'2021-08-29T19:51:54.140' AS DateTime), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice], [CreatedDate], [ModifiedDate]) VALUES (7, 8, 12, 3, CAST(12.75 AS Decimal(18, 2)), CAST(N'2021-08-30T14:39:26.677' AS DateTime), NULL)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [UnitPrice], [CreatedDate], [ModifiedDate]) VALUES (8, 9, 52, 3, CAST(7.50 AS Decimal(18, 2)), CAST(N'2021-08-30T18:59:50.420' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderProductFeatureOptions] ON 

INSERT [dbo].[OrderProductFeatureOptions] ([Id], [OrderDetailId], [CategoryFeatureOptionId], [Quantity], [CreatedDate], [ModifiedDate]) VALUES (1, 5, 1, 3, CAST(N'2021-08-28T00:48:46.590' AS DateTime), NULL)
INSERT [dbo].[OrderProductFeatureOptions] ([Id], [OrderDetailId], [CategoryFeatureOptionId], [Quantity], [CreatedDate], [ModifiedDate]) VALUES (2, 6, 1, 2, CAST(N'2021-08-29T19:51:54.193' AS DateTime), NULL)
INSERT [dbo].[OrderProductFeatureOptions] ([Id], [OrderDetailId], [CategoryFeatureOptionId], [Quantity], [CreatedDate], [ModifiedDate]) VALUES (3, 8, 3, 3, CAST(N'2021-08-30T18:59:50.477' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[OrderProductFeatureOptions] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderCode], [ProcessId], [OrderStatus], [PaymentTypeOptionId], [AutomatSlotId], [PaymentTotal], [RefundAmount], [OrderDate], [CreatedDate], [ModifiedDate]) VALUES (6, N'ad37bed7-0d3c-47e2-80ed-7e66be3f8d32', N'e8ce1cb4-611b-4f91-9a74-52c2c7133548', 1, 4, 2, CAST(43.50 AS Decimal(18, 2)), CAST(6.50 AS Decimal(18, 2)), CAST(N'2021-08-28T00:48:10.057' AS DateTime), CAST(N'2021-08-28T00:48:10.060' AS DateTime), NULL)
INSERT [dbo].[Orders] ([Id], [OrderCode], [ProcessId], [OrderStatus], [PaymentTypeOptionId], [AutomatSlotId], [PaymentTotal], [RefundAmount], [OrderDate], [CreatedDate], [ModifiedDate]) VALUES (7, N'818c06f4-88bb-46c9-a8fc-7e24c66c6205', N'93a32142-b301-496f-b828-baff0d3761b3', 1, 3, 1, CAST(6.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(N'2021-08-29T19:51:53.923' AS DateTime), CAST(N'2021-08-29T19:51:53.923' AS DateTime), NULL)
INSERT [dbo].[Orders] ([Id], [OrderCode], [ProcessId], [OrderStatus], [PaymentTypeOptionId], [AutomatSlotId], [PaymentTotal], [RefundAmount], [OrderDate], [CreatedDate], [ModifiedDate]) VALUES (8, N'a8cd5b75-7a50-4b1c-ade0-4699024660ec', N'b4b39949-0ca3-46dd-8954-485c19fa3984', 1, 2, 7, CAST(38.25 AS Decimal(18, 2)), NULL, CAST(N'2021-08-30T14:39:26.503' AS DateTime), CAST(N'2021-08-30T14:39:26.503' AS DateTime), NULL)
INSERT [dbo].[Orders] ([Id], [OrderCode], [ProcessId], [OrderStatus], [PaymentTypeOptionId], [AutomatSlotId], [PaymentTotal], [RefundAmount], [OrderDate], [CreatedDate], [ModifiedDate]) VALUES (9, N'1d82c4b3-9fcb-45eb-a6f8-d0a00fdc882f', N'3292e2db-d012-4207-9913-b02cbdde387f', 1, 4, 11, CAST(22.50 AS Decimal(18, 2)), CAST(27.50 AS Decimal(18, 2)), CAST(N'2021-08-30T18:59:50.217' AS DateTime), CAST(N'2021-08-30T18:59:50.220' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentTypeOptions] ON 

INSERT [dbo].[PaymentTypeOptions] ([Id], [Name], [PaymentTypeId], [RefundPaymentStatus], [CreatedDate], [ModifiedDate]) VALUES (1, N'Temaslı', 1, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[PaymentTypeOptions] ([Id], [Name], [PaymentTypeId], [RefundPaymentStatus], [CreatedDate], [ModifiedDate]) VALUES (2, N'Temassız', 1, 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[PaymentTypeOptions] ([Id], [Name], [PaymentTypeId], [RefundPaymentStatus], [CreatedDate], [ModifiedDate]) VALUES (3, N'Bozuk Para', 2, 1, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[PaymentTypeOptions] ([Id], [Name], [PaymentTypeId], [RefundPaymentStatus], [CreatedDate], [ModifiedDate]) VALUES (4, N'Kağıt Para', 2, 1, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[PaymentTypeOptions] ([Id], [Name], [PaymentTypeId], [RefundPaymentStatus], [CreatedDate], [ModifiedDate]) VALUES (5, N'Ethereum', 3, 0, CAST(N'2021-08-28T23:32:42.380' AS DateTime), NULL)
INSERT [dbo].[PaymentTypeOptions] ([Id], [Name], [PaymentTypeId], [RefundPaymentStatus], [CreatedDate], [ModifiedDate]) VALUES (7, N'Bilezik', 4, 0, CAST(N'2021-08-29T19:36:59.460' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[PaymentTypeOptions] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentTypes] ON 

INSERT [dbo].[PaymentTypes] ([Id], [Name], [CreatedDate], [ModifiedDate]) VALUES (1, N'Kredi Kartı', CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[PaymentTypes] ([Id], [Name], [CreatedDate], [ModifiedDate]) VALUES (2, N'Nakit', CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[PaymentTypes] ([Id], [Name], [CreatedDate], [ModifiedDate]) VALUES (3, N'Kripto Para', CAST(N'2021-08-28T23:30:13.893' AS DateTime), NULL)
INSERT [dbo].[PaymentTypes] ([Id], [Name], [CreatedDate], [ModifiedDate]) VALUES (4, N'Takı', CAST(N'2021-08-29T19:33:19.940' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[PaymentTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (1, N'Çay', 3, CAST(3.00 AS Decimal(18, 2)), 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (2, N'Latte', 3, CAST(14.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (3, N'Espresso', 3, CAST(17.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (7, N'Doritos Nacho', 5, CAST(6.75 AS Decimal(18, 2)), 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (9, N'Red Bull', 2, CAST(8.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (10, N'Pepsi', 2, CAST(5.00 AS Decimal(18, 2)), 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (12, N'Milkshake', 2, CAST(12.75 AS Decimal(18, 2)), 0, CAST(N'2021-08-27T23:26:32.760' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (13, N'Eti Pop Kek', 7, CAST(5.30 AS Decimal(18, 2)), 0, CAST(N'2021-08-28T23:20:53.833' AS DateTime), CAST(N'2021-08-28T23:24:18.803' AS DateTime))
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (14, N'Ülker Çikolatalı Gofret', 8, CAST(2.75 AS Decimal(18, 2)), 0, CAST(N'2021-08-28T23:22:58.753' AS DateTime), CAST(N'2021-08-28T23:26:14.547' AS DateTime))
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (15, N'Coca Cola', 2, CAST(2.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-29T17:52:49.373' AS DateTime), CAST(N'2021-08-29T17:53:59.080' AS DateTime))
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (16, N'Sade Maden Suyu', 2, CAST(2.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-29T17:52:50.650' AS DateTime), CAST(N'2021-08-30T15:43:23.490' AS DateTime))
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (51, N'Sprite', 2, CAST(3.00 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:18:30.420' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (52, N'Schweppes Mandalina', 2, CAST(7.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:19:14.003' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (53, N'Ülker Sütlü', 30, CAST(5.75 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:27:37.580' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (54, N'Ülker Fındıklı ve Üzümlü', 30, CAST(6.60 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:28:09.907' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (55, N'Ülker Golden Karamelli', 30, CAST(7.15 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:28:33.503' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (56, N'Ülker Antep Fıstıklı', 30, CAST(8.25 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:28:55.427' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (57, N'Nestlé Damak Antep Fıstıklı', 30, CAST(9.10 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:29:22.257' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (58, N'Nestlé Damak İnci', 30, CAST(9.10 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:29:39.357' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (59, N'Milka Oreo', 30, CAST(11.95 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:30:45.770' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (60, N'Eti Antep Fıstıklı', 30, CAST(7.25 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:31:10.420' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (61, N'Lay''s Klasik', 5, CAST(10.19 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:32:09.833' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (62, N'Ruffles Originals', 5, CAST(8.90 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:32:38.973' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (63, N'Ruffles Peynir & Soğan', 5, CAST(6.40 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:33:09.113' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (64, N'Doritos Acı Biberli', 5, CAST(6.40 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:33:23.390' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (65, N'Dankek Kakaolu Lokmalık', 7, CAST(5.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:34:22.957' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (66, N'O''lala Gurme Kırmızı Kadife', 7, CAST(5.00 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:34:48.100' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (67, N'Ülker Susamlı', 6, CAST(1.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:35:41.827' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (68, N'Krispi Baharatlı', 6, CAST(3.75 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:35:57.453' AS DateTime), NULL)
INSERT [dbo].[Products] ([Id], [Name], [CategoryId], [Price], [IsDeleted], [CreatedDate], [ModifiedDate]) VALUES (69, N'Eti Crax Acı Baharatlı', 6, CAST(2.50 AS Decimal(18, 2)), 0, CAST(N'2021-08-30T15:36:16.767' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
/****** Object:  Index [IX_AutomatSlotProducts_AutomatSlotId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_AutomatSlotProducts_AutomatSlotId] ON [dbo].[AutomatSlotProducts]
(
	[AutomatSlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AutomatSlotProducts_ProductId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_AutomatSlotProducts_ProductId] ON [dbo].[AutomatSlotProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryFeatureOptions_CategoryFeatureId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_CategoryFeatureOptions_CategoryFeatureId] ON [dbo].[CategoryFeatureOptions]
(
	[CategoryFeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryFeatures_CategoryId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_CategoryFeatures_CategoryId] ON [dbo].[CategoryFeatures]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_OrderId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_OrderId] ON [dbo].[OrderDetails]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_ProductId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_ProductId] ON [dbo].[OrderDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderProductFeatureOptions_CategoryFeatureOptionId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_OrderProductFeatureOptions_CategoryFeatureOptionId] ON [dbo].[OrderProductFeatureOptions]
(
	[CategoryFeatureOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderProductFeatureOptions_OrderDetailId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_OrderProductFeatureOptions_OrderDetailId] ON [dbo].[OrderProductFeatureOptions]
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_AutomatSlotId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_AutomatSlotId] ON [dbo].[Orders]
(
	[AutomatSlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_PaymentTypeOptionId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_PaymentTypeOptionId] ON [dbo].[Orders]
(
	[PaymentTypeOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaymentTypeOptions_PaymentTypeId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_PaymentTypeOptions_PaymentTypeId] ON [dbo].[PaymentTypeOptions]
(
	[PaymentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCarts_AutomatSlotId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCarts_AutomatSlotId] ON [dbo].[ShoppingCarts]
(
	[AutomatSlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCarts_CategoryFeatureOptionId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCarts_CategoryFeatureOptionId] ON [dbo].[ShoppingCarts]
(
	[CategoryFeatureOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCarts_PaymentTypeOptionId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCarts_PaymentTypeOptionId] ON [dbo].[ShoppingCarts]
(
	[PaymentTypeOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShoppingCarts_ProductId]    Script Date: 30.08.2021 19:40:28 ******/
CREATE NONCLUSTERED INDEX [IX_ShoppingCarts_ProductId] ON [dbo].[ShoppingCarts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AutomatSlotProducts]  WITH CHECK ADD  CONSTRAINT [FK_AutomatSlotProducts_AutomatSlots_AutomatSlotId] FOREIGN KEY([AutomatSlotId])
REFERENCES [dbo].[AutomatSlots] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AutomatSlotProducts] CHECK CONSTRAINT [FK_AutomatSlotProducts_AutomatSlots_AutomatSlotId]
GO
ALTER TABLE [dbo].[AutomatSlotProducts]  WITH CHECK ADD  CONSTRAINT [FK_AutomatSlotProducts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AutomatSlotProducts] CHECK CONSTRAINT [FK_AutomatSlotProducts_Products_ProductId]
GO
ALTER TABLE [dbo].[CategoryFeatureOptions]  WITH CHECK ADD  CONSTRAINT [FK_CategoryFeatureOptions_CategoryFeatures_CategoryFeatureId] FOREIGN KEY([CategoryFeatureId])
REFERENCES [dbo].[CategoryFeatures] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryFeatureOptions] CHECK CONSTRAINT [FK_CategoryFeatureOptions_CategoryFeatures_CategoryFeatureId]
GO
ALTER TABLE [dbo].[CategoryFeatures]  WITH CHECK ADD  CONSTRAINT [FK_CategoryFeatures_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryFeatures] CHECK CONSTRAINT [FK_CategoryFeatures_Categories_CategoryId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[OrderProductFeatureOptions]  WITH CHECK ADD  CONSTRAINT [FK_OrderProductFeatureOptions_CategoryFeatureOptions_CategoryFeatureOptionId] FOREIGN KEY([CategoryFeatureOptionId])
REFERENCES [dbo].[CategoryFeatureOptions] ([Id])
GO
ALTER TABLE [dbo].[OrderProductFeatureOptions] CHECK CONSTRAINT [FK_OrderProductFeatureOptions_CategoryFeatureOptions_CategoryFeatureOptionId]
GO
ALTER TABLE [dbo].[OrderProductFeatureOptions]  WITH CHECK ADD  CONSTRAINT [FK_OrderProductFeatureOptions_OrderDetails_OrderDetailId] FOREIGN KEY([OrderDetailId])
REFERENCES [dbo].[OrderDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderProductFeatureOptions] CHECK CONSTRAINT [FK_OrderProductFeatureOptions_OrderDetails_OrderDetailId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_AutomatSlots_AutomatSlotId] FOREIGN KEY([AutomatSlotId])
REFERENCES [dbo].[AutomatSlots] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_AutomatSlots_AutomatSlotId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_PaymentTypeOptions_PaymentTypeOptionId] FOREIGN KEY([PaymentTypeOptionId])
REFERENCES [dbo].[PaymentTypeOptions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_PaymentTypeOptions_PaymentTypeOptionId]
GO
ALTER TABLE [dbo].[PaymentTypeOptions]  WITH CHECK ADD  CONSTRAINT [FK_PaymentTypeOptions_PaymentTypes_PaymentTypeId] FOREIGN KEY([PaymentTypeId])
REFERENCES [dbo].[PaymentTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentTypeOptions] CHECK CONSTRAINT [FK_PaymentTypeOptions_PaymentTypes_PaymentTypeId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ShoppingCarts]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCarts_AutomatSlots_AutomatSlotId] FOREIGN KEY([AutomatSlotId])
REFERENCES [dbo].[AutomatSlots] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShoppingCarts] CHECK CONSTRAINT [FK_ShoppingCarts_AutomatSlots_AutomatSlotId]
GO
ALTER TABLE [dbo].[ShoppingCarts]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCarts_CategoryFeatureOptions_CategoryFeatureOptionId] FOREIGN KEY([CategoryFeatureOptionId])
REFERENCES [dbo].[CategoryFeatureOptions] ([Id])
GO
ALTER TABLE [dbo].[ShoppingCarts] CHECK CONSTRAINT [FK_ShoppingCarts_CategoryFeatureOptions_CategoryFeatureOptionId]
GO
ALTER TABLE [dbo].[ShoppingCarts]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCarts_PaymentTypeOptions_PaymentTypeOptionId] FOREIGN KEY([PaymentTypeOptionId])
REFERENCES [dbo].[PaymentTypeOptions] ([Id])
GO
ALTER TABLE [dbo].[ShoppingCarts] CHECK CONSTRAINT [FK_ShoppingCarts_PaymentTypeOptions_PaymentTypeOptionId]
GO
ALTER TABLE [dbo].[ShoppingCarts]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCarts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShoppingCarts] CHECK CONSTRAINT [FK_ShoppingCarts_Products_ProductId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[18] 2[2] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AutomatSlotProducts"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Products"
            Begin Extent = 
               Top = 9
               Left = 336
               Bottom = 206
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Categories"
            Begin Extent = 
               Top = 9
               Left = 615
               Bottom = 206
               Right = 837
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AutomatSlots"
            Begin Extent = 
               Top = 9
               Left = 894
               Bottom = 206
               Right = 1116
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1000
         Width = 1170
         Width = 1000
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_AutomatProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_AutomatProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_AutomatProducts'
GO
USE [master]
GO
ALTER DATABASE [AutomatCase] SET  READ_WRITE 
GO

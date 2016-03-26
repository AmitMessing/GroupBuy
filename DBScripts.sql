-- ////////////////////////////////////////////////////////
-- 				Create Tables
-- ////////////////////////////////////////////////////////
USE [GroupBuy]
GO

/****** Object:  Table [dbo].[t_users]    Script Date: 26/03/2016 7:25:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_users](
	[id] [int] NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_t_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [GroupBuy]
GO

/****** Object:  Table [dbo].[t_categories]    Script Date: 26/03/2016 7:27:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_categories](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[desciption] [nvarchar](max) NULL,
 CONSTRAINT [PK_t_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


USE [GroupBuy]
GO

/****** Object:  Table [dbo].[t_products]    Script Date: 26/03/2016 7:28:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_products](
	[id] [int] NOT NULL,
	[seller_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[descriptaion] [nvarchar](max) NOT NULL,
	[basic_price] [numeric](18, 0) NOT NULL,
	[publish_date] [date] NOT NULL,
	[end_date] [date] NOT NULL,
	[picture] [image] NOT NULL,
 CONSTRAINT [PK_t_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[t_products]  WITH CHECK ADD  CONSTRAINT [FK_t_products_t_users] FOREIGN KEY([seller_id])
REFERENCES [dbo].[t_users] ([id])
GO

ALTER TABLE [dbo].[t_products] CHECK CONSTRAINT [FK_t_products_t_users]
GO


USE [GroupBuy]
GO

/****** Object:  Table [dbo].[t_products_discount]    Script Date: 26/03/2016 7:28:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_products_discount](
	[product_id] [int] NOT NULL,
	[users_amount] [int] NOT NULL,
	[present] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_t_products_discount] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[users_amount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[t_products_discount]  WITH CHECK ADD  CONSTRAINT [FK_t_products_discount_t_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[t_products] ([id])
GO

ALTER TABLE [dbo].[t_products_discount] CHECK CONSTRAINT [FK_t_products_discount_t_products]
GO



USE [GroupBuy]
GO

/****** Object:  Table [dbo].[rel_product_buyer]    Script Date: 26/03/2016 7:28:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rel_product_buyer](
	[product_id] [int] NOT NULL,
	[buyer_id] [int] NOT NULL,
 CONSTRAINT [PK_rel_product_buyer] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[buyer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[rel_product_buyer]  WITH CHECK ADD  CONSTRAINT [FK_rel_product_buyer_t_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[t_products] ([id])
GO

ALTER TABLE [dbo].[rel_product_buyer] CHECK CONSTRAINT [FK_rel_product_buyer_t_products]
GO

ALTER TABLE [dbo].[rel_product_buyer]  WITH CHECK ADD  CONSTRAINT [FK_rel_product_buyer_t_users] FOREIGN KEY([buyer_id])
REFERENCES [dbo].[t_users] ([id])
GO

ALTER TABLE [dbo].[rel_product_buyer] CHECK CONSTRAINT [FK_rel_product_buyer_t_users]
GO


USE [GroupBuy]
GO

/****** Object:  Table [dbo].[rel_product_category]    Script Date: 26/03/2016 7:28:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rel_product_category](
	[product_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
 CONSTRAINT [PK_rel_product_category] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[rel_product_category]  WITH CHECK ADD  CONSTRAINT [FK_rel_product_category_t_categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[t_categories] ([id])
GO

ALTER TABLE [dbo].[rel_product_category] CHECK CONSTRAINT [FK_rel_product_category_t_categories]
GO

ALTER TABLE [dbo].[rel_product_category]  WITH CHECK ADD  CONSTRAINT [FK_rel_product_category_t_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[t_products] ([id])
GO

ALTER TABLE [dbo].[rel_product_category] CHECK CONSTRAINT [FK_rel_product_category_t_products]
GO

-- ////////////////////////////////////////////////////////
-- 				Insert to Tables
-- ////////////////////////////////////////////////////////

INSERT INTO [dbo].[t_users]
           ([id]
           ,[first_name]
           ,[last_name]
           ,[password]
           ,[email])
     VALUES
           (1
           ,'Noam'
           ,'Horovitz'
           ,'Aa123456'
           ,'ho.noam4@gmail.com')

INSERT INTO [dbo].[t_users]
           ([id]
           ,[first_name]
           ,[last_name]
           ,[password]
           ,[email])
     VALUES
           (2
           ,'Amit'
           ,'Teverovsky'
           ,'Aa123456'
           ,'Amitmessing16@gmail.com')

INSERT INTO [dbo].[t_users]
           ([id]
           ,[first_name]
           ,[last_name]
           ,[password]
           ,[email])
     VALUES
           (3
           ,'Yulia'
           ,'Teverovsky'
           ,'Aa123456'
           ,'ytev91@gmail.com')

INSERT INTO [dbo].[t_users]
           ([id]
           ,[first_name]
           ,[last_name]
           ,[password]
           ,[email])
     VALUES
           (4
           ,'Yoni'
           ,'Abitbol'
           ,'Aa123456'
           ,'yoni14504@gmail.com')

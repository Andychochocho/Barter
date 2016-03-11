USE [barter]
GO
/****** Object:  Table [dbo].[barter_users]    Script Date: 3/11/2016 8:33:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[barter_users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](255) NULL,
	[pic] [varchar](255) NULL,
	[user_password] [varchar](255) NULL,
	[user_location] [varchar](255) NULL,
	[about_me] [varchar](500) NULL,
	[auth_token] [varchar](500) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[emails]    Script Date: 3/11/2016 8:33:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[emails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[barter_user_id] [int] NULL,
	[email] [varchar](500) NULL,
	[time_stamp] [datetime] NOT NULL,
	[sender_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[posts]    Script Date: 3/11/2016 8:33:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[posts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[barter_user_id] [int] NULL,
	[post] [varchar](500) NULL,
	[time_stamp] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_auth]    Script Date: 3/11/2016 8:33:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_auth](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[auth] [varchar](255) NULL,
	[user_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

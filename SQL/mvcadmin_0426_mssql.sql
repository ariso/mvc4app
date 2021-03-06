USE [MVCAdmin]
GO
/****** Object:  Table [dbo].[sys_modulecategory]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_modulecategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](20) NOT NULL,
	[des] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_sys_modulecategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[sys_modulecategory] ON
INSERT [dbo].[sys_modulecategory] ([id], [categoryName], [des]) VALUES (1, N'CMS管理', N'新闻文章类管理')
INSERT [dbo].[sys_modulecategory] ([id], [categoryName], [des]) VALUES (2, N'图片浏览器', N'用于浏览图片和上传图片')
INSERT [dbo].[sys_modulecategory] ([id], [categoryName], [des]) VALUES (3, N'图表管理', N'图表管理统计')
SET IDENTITY_INSERT [dbo].[sys_modulecategory] OFF
/****** Object:  Table [dbo].[sys_module]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_module](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[moduleName] [nvarchar](50) NOT NULL,
	[commandParameter] [varchar](50) NOT NULL,
	[categoryId] [int] NOT NULL,
 CONSTRAINT [PK_sys_module] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[sys_module] ON
INSERT [dbo].[sys_module] ([id], [moduleName], [commandParameter], [categoryId]) VALUES (1, N'文章管理', N'/Admin/CMS/Index', 1)
INSERT [dbo].[sys_module] ([id], [moduleName], [commandParameter], [categoryId]) VALUES (2, N'相册管理', N'/Admin/Photo/Index', 2)
INSERT [dbo].[sys_module] ([id], [moduleName], [commandParameter], [categoryId]) VALUES (3, N'简单统计', N'/Admin/Chart/Index', 3)
INSERT [dbo].[sys_module] ([id], [moduleName], [commandParameter], [categoryId]) VALUES (4, N'栏目管理', N'/Admin/CMS/Column', 1)
INSERT [dbo].[sys_module] ([id], [moduleName], [commandParameter], [categoryId]) VALUES (5, N'友情链接', N'/Admin/CMS/LinkList', 1)
INSERT [dbo].[sys_module] ([id], [moduleName], [commandParameter], [categoryId]) VALUES (6, N'自定义区域', N'/Admin/CMS/baseContentList', 1)
SET IDENTITY_INSERT [dbo].[sys_module] OFF
/****** Object:  Table [dbo].[sys_accountgrouptomodule]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_accountgrouptomodule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rAccountGroup] [int] NOT NULL,
	[rModule] [int] NOT NULL,
	[isEnable] [bit] NOT NULL,
 CONSTRAINT [PK_sys_accountgrouptomodule] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[sys_accountgrouptomodule] ON
INSERT [dbo].[sys_accountgrouptomodule] ([id], [rAccountGroup], [rModule], [isEnable]) VALUES (1, 1, 1, 1)
INSERT [dbo].[sys_accountgrouptomodule] ([id], [rAccountGroup], [rModule], [isEnable]) VALUES (2, 1, 4, 1)
INSERT [dbo].[sys_accountgrouptomodule] ([id], [rAccountGroup], [rModule], [isEnable]) VALUES (3, 1, 5, 1)
INSERT [dbo].[sys_accountgrouptomodule] ([id], [rAccountGroup], [rModule], [isEnable]) VALUES (4, 1, 6, 1)
INSERT [dbo].[sys_accountgrouptomodule] ([id], [rAccountGroup], [rModule], [isEnable]) VALUES (5, 1, 2, 1)
INSERT [dbo].[sys_accountgrouptomodule] ([id], [rAccountGroup], [rModule], [isEnable]) VALUES (6, 1, 3, 1)
SET IDENTITY_INSERT [dbo].[sys_accountgrouptomodule] OFF
/****** Object:  Table [dbo].[sys_accountgroup]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_accountgroup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[groupName] [varchar](50) NOT NULL,
	[des] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_sys_accountgroup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[sys_accountgroup] ON
INSERT [dbo].[sys_accountgroup] ([id], [groupName], [des]) VALUES (1, N'日常管理', N'日常管理')
SET IDENTITY_INSERT [dbo].[sys_accountgroup] OFF
/****** Object:  Table [dbo].[sys_account]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountName] [varchar](20) NOT NULL,
	[accountPwd] [varchar](50) NOT NULL,
	[rAccountGroup] [int] NOT NULL,
 CONSTRAINT [PK_sys_account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[sys_account] ON
INSERT [dbo].[sys_account] ([id], [accountName], [accountPwd], [rAccountGroup]) VALUES (1, N'lww001', N'e10adc3949ba59abbe56e057f20f883e', 1)
SET IDENTITY_INSERT [dbo].[sys_account] OFF
/****** Object:  Table [dbo].[cms_link]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_link](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[linkName] [nvarchar](10) NOT NULL,
	[linkUrl] [varchar](255) NOT NULL,
	[linkImg] [varchar](255) NULL,
 CONSTRAINT [PK_cms_link] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cms_link] ON
INSERT [dbo].[cms_link] ([id], [linkName], [linkUrl], [linkImg]) VALUES (1, N'lww', N'lww', NULL)
INSERT [dbo].[cms_link] ([id], [linkName], [linkUrl], [linkImg]) VALUES (2, N'lww01', N'lww', NULL)
INSERT [dbo].[cms_link] ([id], [linkName], [linkUrl], [linkImg]) VALUES (3, N'lww02', N'lww', NULL)
SET IDENTITY_INSERT [dbo].[cms_link] OFF
/****** Object:  Table [dbo].[cms_column]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_column](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[isNode] [bit] NOT NULL,
	[parentId] [int] NOT NULL,
	[link] [varchar](255) NULL,
 CONSTRAINT [PK_cms_column] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cms_column] ON
INSERT [dbo].[cms_column] ([id], [name], [isNode], [parentId], [link]) VALUES (1, N'测试栏目', 1, 0, NULL)
INSERT [dbo].[cms_column] ([id], [name], [isNode], [parentId], [link]) VALUES (2, N'白度', 0, 0, N'https://www.baidu.com/?tn=56060048_4_pg')
SET IDENTITY_INSERT [dbo].[cms_column] OFF
/****** Object:  Table [dbo].[cms_basecontent]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_basecontent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
	[des] [nvarchar](50) NOT NULL,
	[content] [ntext] NOT NULL,
 CONSTRAINT [PK_cms_basecontent] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cms_basecontent] ON
INSERT [dbo].[cms_basecontent] ([id], [name], [des], [content]) VALUES (1, N'测试', N'测试45', N'<strong>测试</strong>')
SET IDENTITY_INSERT [dbo].[cms_basecontent] OFF
/****** Object:  Table [dbo].[cms_article]    Script Date: 04/26/2015 09:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_article](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NOT NULL,
	[content] [ntext] NOT NULL,
	[addTime] [datetime] NOT NULL,
	[editTime] [datetime] NOT NULL,
	[isTop] [bit] NOT NULL,
	[isShow] [bit] NOT NULL,
	[isRecommend] [bit] NOT NULL,
	[des] [nvarchar](200) NULL,
	[keyStr] [nvarchar](100) NULL,
	[author] [nvarchar](20) NULL,
	[source] [nvarchar](30) NULL,
	[rColumn] [int] NOT NULL,
 CONSTRAINT [PK_cms_article] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cms_article] ON
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (1, N'自定义内容区', N'自定义内容区', CAST(0x0000A48500E8EDB0 AS DateTime), CAST(0x0000A48500E8EDB0 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (2, N'自定义内容区01', N'自定义内容区', CAST(0x0000A48500E902C8 AS DateTime), CAST(0x0000A48500E902C8 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (3, N'自定义内容区02', N'自定义内容区01', CAST(0x0000A48500E90FAC AS DateTime), CAST(0x0000A48500E90FAC AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (4, N'自定义内容区03', N'自定义内容区01', CAST(0x0000A48500E91C90 AS DateTime), CAST(0x0000A48500E91C90 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (5, N'自定义内容区04', N'自定义内容区01', CAST(0x0000A48500E92848 AS DateTime), CAST(0x0000A48500E92848 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (6, N'自定义内容区05', N'自定义内容区01', CAST(0x0000A48500E93400 AS DateTime), CAST(0x0000A48500E93400 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (7, N'自定义内容区06', N'自定义内容区01', CAST(0x0000A48500E93D60 AS DateTime), CAST(0x0000A48500E93D60 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (8, N'自定义内容区07', N'自定义内容区01', CAST(0x0000A48500E94918 AS DateTime), CAST(0x0000A48500E94918 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (9, N'自定义内容区08', N'自定义内容区01', CAST(0x0000A48500E955FC AS DateTime), CAST(0x0000A48500E955FC AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (10, N'自定义内容区09', N'<span style="font-family:Verdana, sans-serif;line-height:16.7999992370605px;background-color:#F9F9F9;">自定义内容区08</span>', CAST(0x0000A48500EB7940 AS DateTime), CAST(0x0000A48500EB9434 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[cms_article] ([id], [title], [content], [addTime], [editTime], [isTop], [isShow], [isRecommend], [des], [keyStr], [author], [source], [rColumn]) VALUES (11, N'自定义内容区10', N'<span style="font-family:Verdana, sans-serif;line-height:16.7999992370605px;background-color:#F9F9F9;">自定义内容区08</span>', CAST(0x0000A48500EB89A8 AS DateTime), CAST(0x0000A48500EB89A8 AS DateTime), 0, 1, 0, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[cms_article] OFF

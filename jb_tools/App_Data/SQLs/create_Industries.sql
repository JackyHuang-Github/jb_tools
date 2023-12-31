USE [jb_tools]
GO
/****** Object:  Table [dbo].[Industries]    Script Date: 2023/7/19 下午 03:37:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Industries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IndustryNo] [varchar](50) NOT NULL,
	[IndustryName] [nvarchar](250) NOT NULL,
	[Remark] [nvarchar](250) NULL,
 CONSTRAINT [PK_Industries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Industries] ON 

INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (1, N'001', N'光電業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (2, N'002', N'其他電子業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (3, N'003', N'其他業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (4, N'004', N'化學工業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (5, N'005', N'半導體業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (6, N'006', N'塑膠工業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (7, N'007', N'建材營造業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (8, N'008', N'文化創意業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (9, N'009', N'橡膠工業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (10, N'010', N'水泥工業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (11, N'011', N'汽車工業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (12, N'012', N'油電燃氣業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (13, N'013', N'玻璃陶瓷', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (14, N'014', N'生技醫療業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (15, N'015', N'紡織纖維', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (16, N'016', N'航運業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (17, N'017', N'觀光事業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (18, N'018', N'貿易百貨業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (19, N'019', N'資訊服務業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (20, N'020', N'農業科技業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (21, N'021', N'通信網路業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (22, N'022', N'造紙工業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (23, N'023', N'金融保險業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (24, N'024', N'鋼鐵工業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (25, N'025', N'電子零組件業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (26, N'026', N'電機機械', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (27, N'027', N'電子通路業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (28, N'028', N'電子商務', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (29, N'029', N'電器電纜', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (30, N'030', N'電腦及週邊設備業', N'')
INSERT [dbo].[Industries] ([Id], [IndustryNo], [IndustryName], [Remark]) VALUES (31, N'031', N'食品工業', N'')
SET IDENTITY_INSERT [dbo].[Industries] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Industries_1]    Script Date: 2023/7/19 下午 03:37:27 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Industries_1] ON [dbo].[Industries]
(
	[IndustryNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Industries] ADD  CONSTRAINT [DF_Industries_IndustryNo]  DEFAULT ('') FOR [IndustryNo]
GO
ALTER TABLE [dbo].[Industries] ADD  CONSTRAINT [DF_Industries_IndustryName]  DEFAULT ('') FOR [IndustryName]
GO
ALTER TABLE [dbo].[Industries] ADD  CONSTRAINT [DF_Industries_Remark]  DEFAULT ('') FOR [Remark]
GO

GO
/****** Object:  Table [dbo].[tb_consignacao]    Script Date: 16/04/2026 07:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_consignacao](
	[id_consignacao] [int] IDENTITY(1,1) NOT NULL,
	[dt_consignacao] [datetime] NOT NULL,
	[nr_consignacao] [nvarchar](20) NOT NULL,
	[id_fornecedor] [int] NOT NULL,
	[dm_ativo] [bit] NOT NULL,
 CONSTRAINT [PK_tb_consignacao] PRIMARY KEY CLUSTERED 
(
	[id_consignacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_fornecedor]    Script Date: 16/04/2026 07:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_fornecedor](
	[id_fornecedor] [int] IDENTITY(1,1) NOT NULL,
	[nm_fornecedor] [nvarchar](50) NOT NULL,
	[ds_acesso] [varchar](max) NULL,
	[dm_ativo] [bit] NOT NULL,
 CONSTRAINT [PK_tb_fornecedor] PRIMARY KEY CLUSTERED 
(
	[id_fornecedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_movimentacao]    Script Date: 16/04/2026 07:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_movimentacao](
	[id_movimentacao] [int] IDENTITY(1,1) NOT NULL,
	[dm_entrada] [int] NOT NULL,
	[dt_movimentacao] [datetime] NOT NULL,
	[nr_valor] [numeric](10, 2) NOT NULL,
	[nr_quantidade] [int] NOT NULL,
	[nm_comprador] [nvarchar](50) NOT NULL,
	[id_consignado] [int] NOT NULL,
	[id_produto] [int] NOT NULL,
	[id_operacao] [int] NOT NULL,
	[dm_ativo] [bit] NOT NULL,
 CONSTRAINT [PK_tb_movimentacao] PRIMARY KEY CLUSTERED 
(
	[id_movimentacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_operacao]    Script Date: 16/04/2026 07:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_operacao](
	[id_operacao] [int] IDENTITY(1,1) NOT NULL,
	[dt_operacao] [datetime] NOT NULL,
	[nm_operacao] [nchar](10) NOT NULL,
	[dm_ativo] [int] NOT NULL,
 CONSTRAINT [PK_tb_operacao] PRIMARY KEY CLUSTERED 
(
	[id_operacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_produto]    Script Date: 16/04/2026 07:21:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_produto](
	[id_produto] [int] IDENTITY(1,1) NOT NULL,
	[nm_produto] [nvarchar](200) NOT NULL,
	[d_produto] [nvarchar](max) NOT NULL,
	[nr_preco_venda] [numeric](10, 2) NOT NULL,
	[dm_ativo] [int] NOT NULL,
 CONSTRAINT [PK_tb_produto] PRIMARY KEY CLUSTERED 
(
	[id_produto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_consignacao] ADD  CONSTRAINT [DF_tb_consignacao_dt_consignacao]  DEFAULT (getdate()) FOR [dt_consignacao]
GO
ALTER TABLE [dbo].[tb_consignacao] ADD  CONSTRAINT [DF_tb_consignacao_dm_ativo]  DEFAULT ((1)) FOR [dm_ativo]
GO
ALTER TABLE [dbo].[tb_fornecedor] ADD  CONSTRAINT [DF_tb_fornecedor_dm_ativo]  DEFAULT ((1)) FOR [dm_ativo]
GO
ALTER TABLE [dbo].[tb_movimentacao] ADD  CONSTRAINT [DF_tb_movimentacao_dm_entrada]  DEFAULT ((1)) FOR [dm_entrada]
GO
ALTER TABLE [dbo].[tb_movimentacao] ADD  CONSTRAINT [DF_tb_saida_dt_saida]  DEFAULT (getdate()) FOR [dt_movimentacao]
GO
ALTER TABLE [dbo].[tb_movimentacao] ADD  CONSTRAINT [DF_tb_saida_nr_quantidade_saida]  DEFAULT ((0)) FOR [nr_quantidade]
GO
ALTER TABLE [dbo].[tb_movimentacao] ADD  CONSTRAINT [DF_tb_movimentacao_id_motivo_saida]  DEFAULT ((1)) FOR [id_operacao]
GO
ALTER TABLE [dbo].[tb_movimentacao] ADD  CONSTRAINT [DF_tb_movimentacao_dm_ativo]  DEFAULT ((1)) FOR [dm_ativo]
GO
ALTER TABLE [dbo].[tb_operacao] ADD  CONSTRAINT [DF_tb_operacao_dm_ativo]  DEFAULT ((1)) FOR [dm_ativo]
GO
ALTER TABLE [dbo].[tb_consignacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_consignacao_tb_fornecedor] FOREIGN KEY([id_fornecedor])
REFERENCES [dbo].[tb_fornecedor] ([id_fornecedor])
GO
ALTER TABLE [dbo].[tb_consignacao] CHECK CONSTRAINT [FK_tb_consignacao_tb_fornecedor]
GO
ALTER TABLE [dbo].[tb_movimentacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_movimentacao_tb_consignacao] FOREIGN KEY([id_consignado])
REFERENCES [dbo].[tb_consignacao] ([id_consignacao])
GO
ALTER TABLE [dbo].[tb_movimentacao] CHECK CONSTRAINT [FK_tb_movimentacao_tb_consignacao]
GO
ALTER TABLE [dbo].[tb_movimentacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_movimentacao_tb_operacao] FOREIGN KEY([id_operacao])
REFERENCES [dbo].[tb_operacao] ([id_operacao])
GO
ALTER TABLE [dbo].[tb_movimentacao] CHECK CONSTRAINT [FK_tb_movimentacao_tb_operacao]
GO
ALTER TABLE [dbo].[tb_movimentacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_movimentacao_tb_produto] FOREIGN KEY([id_produto])
REFERENCES [dbo].[tb_produto] ([id_produto])
GO
ALTER TABLE [dbo].[tb_movimentacao] CHECK CONSTRAINT [FK_tb_movimentacao_tb_produto]
GO

Dado o script sql:
<script sql>
CREATE TABLE [dbo].[tb_conta](
	[id_conta] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[nm_conta] [nvarchar](100) NOT NULL,
	[nr_saldo] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_tb_conta] PRIMARY KEY CLUSTERED 
(
	[id_conta] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_divida]    Script Date: 12/05/2026 14:22:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_divida](
	[id_divida] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[nm_divida] [nvarchar](100) NOT NULL,
	[dia_vencimento] [int] NOT NULL,
	[dt_primeiro_vencimento] [datetime] NOT NULL,
	[nr_parcelas] [int] NOT NULL,
	[nr_valor] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_tb_divida] PRIMARY KEY CLUSTERED 
(
	[id_divida] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_usuario]    Script Date: 12/05/2026 14:22:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nm_usuario] [nvarchar](100) NOT NULL,
	[ds_emaill] [nvarchar](100) NOT NULL,
	[dm_ativo] [bit] NOT NULL,
	[ds_senha] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_tb_usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
</script sql>

!Faça tudo em C#
!Respeite os Padrões de Projeto
!Model na pasta Models
!Repository na pasta Repositories
!Contexto na pasta Contexts

Crie um conexão com o banco de dados utilizando Entity Framework Core, seguindo os padrões de projeto.
string de conexao:
Server=tcp:aquarela-sql.database.windows.net,1433;Initial Catalog=free-sql-db-5947062;Persist Security Info=False;User ID=aquarela-sql;Password=Azure@99!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

Colque a string de conexao no appsettings.json e utilize-a no contexto do Entity Framework Core.

Crie as Models para as tabelas tb_conta, tb_divida e tb_usuario, seguindo os padrões de projeto.
Crie os Repositories para as Models criadas, seguindo os padrões de projeto.
Crie um Metodo Login para autenticar o usuário utilizando o email e senha, seguindo os padrões de projeto.
Crie um endpoint para o método de Login, seguindo os padrões de projeto. 
Implemente JWT para autenticação e autorização, seguindo os padrões de projeto.
Aqui está um exemplo de como criar as Models, Repositories, Contexto e o método de Login utilizando JWT para autenticação e autorização em C# com Entity Framework Core.	

Crie uma Camada chamada UseCases na pasta UseCases para implementar a lógica de negócio, seguindo os padrões de projeto.
Crie as Controllers para as Models criadas, seguindo os padrões de projeto.

Ficará assim:
  A controller recebe a requisição, chama o UseCase correspondente, que por sua vez chama o Repository para acessar os dados no banco de dados.]
  O Repository utiliza o Contexto para realizar as operações de CRUD no banco de dados.
  O método de Login na Controller chama o UseCase de Login, que autentica o usuário e gera um token JWT para autorização.

  Cada Model deve ter um Repository correspondente para realizar as operações de CRUD no banco de dados. 
  O Contexto do Entity Framework Core deve ser configurado para se conectar ao banco de dados utilizando a string de conexão definida no appsettings.json. O método de Login deve validar as credenciais do usuário e, se forem válidas, gerar um token JWT que será utilizado para autenticação e autorização nas requisições subsequentes.
  As Controllers devem ser responsáveis por receber as requisições HTTP, chamar os UseCases correspondentes e retornar as respostas adequadas.



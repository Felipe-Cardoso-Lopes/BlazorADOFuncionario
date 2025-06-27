# 🚀 Blazor ADO.NET: Gerenciamento de Funcionários e Departamentos

Este projeto é uma aplicação Blazor interativa desenvolvida em .NET 8 que demonstra a integração e persistência de dados utilizando ADO.NET com um banco de dados SQL Server. O objetivo principal é fornecer um sistema CRUD (Create, Read, Update, Delete) completo para gerenciar informações de funcionários e departamentos de forma eficiente.

## ✨ Funcionalidades

* **Gerenciamento de Departamentos:**
    * Listar todos os departamentos.
    * Adicionar novos departamentos.
    * Visualizar detalhes de um departamento específico.
    * Atualizar informações de departamentos existentes.
    * Excluir departamentos.
* **Gerenciamento de Funcionários:**
    * Listar todos os funcionários, incluindo seus respectivos departamentos.
    * Adicionar novos funcionários.
    * Visualizar detalhes de um funcionário específico.
    * Atualizar informações de funcionários existentes.
    * Excluir funcionários.

## 🏛️ Arquitetura

O projeto segue uma arquitetura modular, separando as responsabilidades para facilitar a manutenção e escalabilidade:

* **Camada de Apresentação (UI):** Desenvolvida com Blazor (Blazor Server), responsável pela interface do usuário e interação com os serviços.
* **Camada de Serviço (`Servico`):** Contém a lógica de negócio e as operações de acesso a dados para as entidades `Funcionario` e `Departamento`.
* **Camada de Entidades (`Entidades`):** Define os modelos de dados (`Funcionario` e `Departamento`) que representam as tabelas do banco de dados.
* **Camada de Persistência:** Utiliza **ADO.NET** para a comunicação direta com o banco de dados SQL Server, executando comandos SQL para operações CRUD.

## 📦 Entidades

O projeto é construído em torno das seguintes entidades principais:

### `Departamento`

Representa um departamento da empresa.

* `Id` (int): Chave primária.
* `Nome` (string, max 50 caracteres): Nome do departamento.
* `Sigla` (string, max 50 caracteres): Sigla do departamento.
* `Email` (string, max 50 caracteres): E-mail de contato do departamento.
* `Telefone` (string, max 50 caracteres): Telefone do departamento.
* `Funcionarios` (List<Funcionario>): Coleção de funcionários associados a este departamento.

### `Funcionario`

Representa um funcionário da empresa.

* `Id` (int): Chave primária.
* `DepartamentoId` (int): Chave estrangeira para o departamento ao qual o funcionário pertence.
* `NomeDepartamento` (string, max 200 caracteres): Nome do departamento (para exibição, não persistido diretamente na tabela `tbFuncionario`).
* `Nome` (string, obrigatório, max 200 caracteres): Nome completo do funcionário.
* `NomeMae` (string, max 200 caracteres): Nome da mãe do funcionário.
* `NomePai` (string, max 200 caracteres): Nome do pai do funcionário.
* `DataNascimento` (DateTime): Data de nascimento do funcionário.
* `CPF` (string, obrigatório, max 11 caracteres): CPF do funcionário.
* `RG` (string, max 11 caracteres): RG do funcionário.
* `Email` (string, max 50 caracteres): E-mail do funcionário.
* `Departamento` (Departamento): Objeto de navegação para o departamento associado.

## 💻 Tecnologias Utilizadas

* **ASP.NET Core (Blazor Server)**: Framework para construção de aplicações web interativas com C#.
* **ADO.NET**: Tecnologia para acesso a dados no .NET, utilizada para interagir diretamente com o SQL Server.
* **Microsoft SQL Server**: Sistema de gerenciamento de banco de dados relacional.
* **C#**: Linguagem de programação principal utilizada no desenvolvimento.

## ⚙️ Como Configurar e Executar o Projeto

Siga estas instruções passo a passo para configurar e executar o projeto em sua máquina local:

### Pré-requisitos

Certifique-se de ter os seguintes itens instalados:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (edição Express ou superior)
* [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/pt-br/sql/ssms/download-sql-server-management-studio-ssms) (Opcional, mas recomendado para gerenciar o banco de dados)

### 1. Configuração do Banco de Dados 💾

1.  **Crie o Banco de Dados:**
    Abra o `script.sql` localizado na raiz do projeto. Execute este script em seu SQL Server para criar o banco de dados `bdEmpresa` e as tabelas `tbDepartamento` e `tbFuncionario`.
    *Nota: Verifique e ajuste os caminhos `FILENAME` no script SQL para corresponder à sua instalação do SQL Server, se necessário.*

2.  **Crie o Usuário do Banco de Dados:**
    O `script.sql` também cria um usuário `usuariobd` e o adiciona à função `db_owner`. Este usuário é usado na string de conexão.

### 2. Configuração da String de Conexão 🔗

1.  Abra o arquivo `appsettings.json` localizado em `ExemploBlazorADOFuncionario/ExemploBlazorADOFuncionario/`.
2.  Localize a seção `ConnectionStrings` e atualize o valor de `DefaultConnection` para apontar para sua instância do SQL Server. A string de conexão padrão no arquivo é:

    ```json
    "DefaultConnection": "Server=DESKTOP-E1RR4VV\\SQLEXPRESS;Database=bdEmpresa;User Id=usuarioEmpresa;Password=senha;TrustServerCertificate=True;"
    ```

    * Altere `DESKTOP-E1RR4VV\\SQLEXPRESS` para o nome do seu servidor SQL Server e instância.
    * Mantenha `Database=bdEmpresa;` a menos que você tenha renomeado o banco de dados.
    * Mantenha `User Id=usuarioEmpresa;Password=senha;` se você usou o script SQL fornecido para criar o usuário e senha.
    * `TrustServerCertificate=True;` é importante para conexões locais sem um certificado SSL configurado.

### 3. Executar o Projeto ▶️

Você pode executar o projeto de duas maneiras:

#### Opção 1: Usando o Visual Studio

1.  Abra o arquivo de solução (`.sln`) do projeto no Visual Studio.
2.  Defina `ExemploBlazorADOFuncionario` como o projeto de inicialização.
3.  Pressione `F5` ou clique no botão "IIS Express" (ou "ExemploBlazorADOFuncionario") para iniciar a aplicação.

#### Opção 2: Usando a Linha de Comando

1.  Navegue até o diretório raiz do projeto `ExemploBlazorADOFuncionario/ExemploBlazorADOFuncionario/` onde o arquivo `.csproj` está localizado.
2.  Execute o seguinte comando para restaurar as dependências e compilar o projeto:
    ```bash
    dotnet build
    ```
3.  Após a compilação bem-sucedida, execute a aplicação:
    ```bash
    dotnet run
    ```
4.  O console indicará o(s) endereço(s) onde a aplicação está sendo executada (ex: `http://localhost:5168` ou `https://localhost:7283`). Abra um navegador e acesse um desses endereços.

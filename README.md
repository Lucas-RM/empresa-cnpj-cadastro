# Teste Técnico - Cadastro de Empresas com Consulta via CNPJ

## Descrição do Projeto

Este projeto consiste em uma aplicação fullstack para cadastro de empresas, permitindo a consulta de dados via CNPJ, autenticação de usuários e gerenciamento de empresas. O sistema é dividido em backend (API REST em C#) e frontend (Angular), proporcionando uma interface amigável e segura para o usuário.

---

## Stack Tecnológica

### Backend

- **Linguagem:** C#
- **Framework:** .NET 7+
- **ORM:** Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autenticação:** JWT
- **Ferramentas:** EF Core Migrations, Swagger e Postman

### Frontend

- **Linguagem:** TypeScript
- **Framework:** Angular 20+
- **Gerenciador de Pacotes:** npm

---

## Estrutura do Projeto

```plaintext
empresa-cnpj-cadastro/
│
├── CadastroEmpresasApp/
│   ├── backend/
│   │   ├── CadastroEmpresas.API/         # Projeto principal da API REST
│   │   │   ├── Controllers/              # Controllers da API
│   │   │   ├── DTOs/                     # Data Transfer Objects
│   │   │   ├── Models/                   # Modelos auxiliares
│   │   │   ├── Repositories/             # Repositórios
│   │   │   ├── Services/                 # Serviços de negócio
│   │   │   └── Program.cs                # Configuração principal da API
│   │   ├── CadastroEmpresas.Domain/      # Entidades de domínio e interfaces
│   │   └── CadastroEmpresas.Infrastructure/ # Infraestrutura, contexto e migrations
│   └── frontend/
│       └── cadastro-empresas-ui/     # Projeto Angular
│           ├── src/app/
│           │   ├── components/       # Componentes de UI
│           │   ├── constants/        # Constantes globais
│           │   ├── models/           # Modelos de dados
│           │   └── services/         # Serviços de comunicação
│           └── ...
└── README.md
```

---

## Setup e Configuração

### Pré-requisitos

- **.NET SDK** 7.0+
- **Node.js** 20+
- **Angular CLI** 20+
- **npm** 10+
- **SQL Server**
- **Ferramenta EF Core CLI** (opcional, para comandos de migração)

---

## Passo a Passo para Instalação e Configuração

### Clonar o Repositório

Clone o repositório para o seu ambiente local:

```bash
git clone https://github.com/Lucas-RM/empresa-cnpj-cadastro
```

---

### Backend (C# - API REST)

1. Navegue até a pasta do backend:

   ```bash
   cd CadastroEmpresasApp/backend
   ```

2. Restaure as dependências do projeto:

   ```bash
   dotnet restore
   ```

3. Crie o arquivo `appsettings.json` em `CadastroEmpresasApp/backend/CadastroEmpresas.API/` e utilize o template `appsettings.Development.template.json` para configurar a conexão com o banco de dados e as propriedades de configuração de autenticação JWT:

    - Conexão com banco de dados:

        ```json
        "ConnectionStrings": {
            "SqlServer": "<sua-string-de-conexão-aqui>"
        }
        ```

    - Propriedades de configuração de autenticação JWT:

        ```json
         "JwtSettings": {
            "ChaveSecreta": "INSIRA_SUA_CHAVE_AQUI_COM_32+_CARACTERES",
            "Emissor": "CadastroEmpresas.API",
            "Audiencia": "CadastroEmpresas.UI",
            "ExpiracaoEmMinutos": 60
        },
        ```

4. Em `Program.cs`, configure o banco de dados e a autenticação JWT:

    ```csharp
    // Conexão com o banco de dados
    builder.Services.AddDbContext<DbContexto>(options =>
        options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer")
        ));

    // Configurações de JWT (appsettings.json)
    builder.Services.Configure<ConfiguracoesToken>(builder.Configuration.GetSection("JwtSettings"));

    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<ConfiguracoesToken>();
    ```

    - Verifique se o nome da string de conexão é o mesmo que está em `appsettings.json` ("DefaultConnection" ou "SqlServer"). E também verifique para a Autenticação ("JwtSettings").

5. Configure a URL da Política CORS em `Program.cs` para a URL do frontend:

   ```csharp
   policy.WithOrigins("http://localhost:4200") // URL do frontend
         .AllowAnyHeader()
         .AllowAnyMethod();
   ```

   - Verifique se a URL `http://localhost:4200` é a mesma do frontend.

6. Execute as migrações para criar o banco de dados:

   ```bash
   dotnet ef database update
   ```

7. Inicie o servidor backend (`CadastroEmpresasApp/backend/CadastroEmpresas.API/`):

   ```bash
   cd CadastroEmpresasApp/backend/CadastroEmpresas.API/
   dotnet run
   ```

   - O servidor será iniciado na URL `https://localhost:7113` (verifique se a porta está correta).

---

### Frontend (Angular)

1. Navegue até a pasta do frontend:

   ```bash
   cd CadastroEmpresasApp/frontend/cadastro-empresas-ui/
   ```

2. Instale as dependências do projeto:

   ```bash
   npm install
   ```

3. Verifique no arquivo `src/app/constants/api.constants.ts` se a constante `API_BASE_URL` aponta para a URL do backend, por exemplo:

   ```typescript
   export const API_BASE_URL = 'https://localhost:7113/api';
   ```

   - Certifique-se de que a URL corresponde à do backend.

4. Inicie o servidor de desenvolvimento (`cadastro-empresas-ui/`):

   ```bash
   npm start
   ```

   - O frontend será iniciado na URL padrão `http://localhost:4200` (verifique se a porta está correta).

---

## Documentação dos Endpoints da API REST

### Fazer Login

**Descrição:** Gera um token de autenticação e realiza o login na aplicação.

- **URL:** `api/auth/login`
- **Método:** `POST`
- **Corpo da Requisição (Body > raw (json)):**

   ```json
   {
      "email": "string",
      "senha": "string"
   }
   ```

- **Resposta:**
  - **Sucesso: Código 200 (OK)**

      ```json
      {
         "token": "string",
         "nome": "string",
         "email": "string"
      }
      ```

---

### Cadastrar Usuário

**Descrição:** Gera um token de autenticação e realiza o cadastro do usuário na aplicação.

- **URL:** `api/auth/registrar`
- **Método:** `POST`
- **Corpo da Requisição (Body > raw (json)):**

   ```json
   {
      "nome": "string",
      "email": "string",
      "senha": "string"
   }
   ```

- **Resposta:**
  - **Sucesso: Código 200 (OK)**

      ```json
      {
         "token": "string",
         "nome": "string",
         "email": "string"
      }
      ```

---

### Cadastrar Empresa

**Descrição:** Cadastra uma nova empresa

- **URL:** `api/empresa/cadastrar`
- **Método:** `POST`
- **Headers:**
  - **Accept:** `application/json`
  - **Authorization**: `Bearer <token>`
- **Corpo da Requisição (Body > raw (json)):**

   ```json
   {
      "cnpj": "string"
   }
   ```

- **Resposta:**
  - **Sucesso: Código 200 (OK)**

      ```json
      {
         "mensagem": "string"
      }
      ```

---

### Listar Empresas

**Descrição:** Lista as empresas cadastradas

- **URL:** `api/empresa/listar?pagina=1&tamanho=10`
- **Método:** `GET`
- **Headers:**
  - **Accept:** `application/json`
  - **Authorization**: `Bearer <token>`
- **Resposta:**
  - **Sucesso: Código 200 (OK)**

      ```json
      {
         "total": "int",
         "paginaAtual": "int",
         "paginaTamanho": "int",
         "dados": []
      }
      ```

---

## Arquivos do Postman

> Os arquivos do Postman estão disponíveis em [Postman Collection](https://github.com/Lucas-RM/empresa-cnpj-cadastro/tree/main/Postman%20Collection).

### Conteúdo da Pasta

> A pasta "Postman Collection" contém:

- **Coleções do Postman:** Arquivos `.json` que incluem todas as requisições configuradas para os endpoints da API.

- **Documentação de Requisições:** Parâmetros, corpos de requisição e exemplos de respostas para facilitar o teste e a validação da API.

### Como Usar

1. Baixe os arquivos da pasta "Postman Collection".

2. Importe os arquivos no Postman:

    - Abra o Postman.

    - Clique em "Import" no canto superior esquerdo.

    - Selecione o arquivo `.json` baixado.

3. Utilize as requisições configuradas para testar a API com facilidade.

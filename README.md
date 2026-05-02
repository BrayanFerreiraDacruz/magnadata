# Teste Técnico - Gestão de Tarefas (MagnaData)

Este projeto foi desenvolvido como parte de um teste técnico para uma vaga de estágio. 
Consiste em um aplicativo completo de gestão de tarefas (To-Do List) com Backend em .NET e Frontend em Vue.js.

## Estrutura do Projeto

- `/Magnadata.API`: Backend desenvolvido em .NET 8 utilizando Web API, Entity Framework Core e MySQL.
- `/src`: Frontend desenvolvido em Vue.js 3 com Vite e Axios.
- `database.sql`: Script para criação do banco de dados MySQL.
- `EXPLICACAO.md`: Documento detalhado explicando o código para a apresentação.

## Pré-requisitos

- .NET 8 SDK
- Node.js (v18+)
- MySQL Server

## Como Iniciar

### 1. Banco de Dados
Execute o conteúdo de `database.sql` no seu servidor MySQL.

### 2. Backend (.NET)
1. Navegue até a pasta `Magnadata.API`.
2. Configure a string de conexão no `appsettings.json`.
3. Execute:
   ```bash
   dotnet run
   ```

### 3. Frontend (Vue.js)
1. Na raiz do projeto, instale as dependências:
   ```bash
   npm install
   ```
2. Inicie o servidor de desenvolvimento:
   ```bash
   npm run dev
   ```

---
Desenvolvido por [Seu Nome]

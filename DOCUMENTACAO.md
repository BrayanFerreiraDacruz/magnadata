# Guia de Explicação do Projeto - Teste Técnico MagnaData

Olá! Este documento foi preparado para ajudar você a entender e apresentar o projeto de Gestão de Tarefas. O foco aqui é a clareza e a organização, ideais para um nível de estágio inicial.

---

## 1. Arquitetura do Projeto

O projeto segue o modelo **Client-Server** (Cliente-Servidor), onde temos:
- **Backend (.NET 8 Web API):** O "cérebro" da aplicação, que lida com as regras de negócio e o banco de dados.
- **Frontend (Vue.js 3):** A interface visual com a qual o usuário interage.
- **Banco de Dados (MySQL):** Onde as informações são guardadas de forma permanente.

---

## 2. O Backend (.NET)

O código do backend foi organizado em **camadas**, uma boa prática que facilita a manutenção:

### Camada de Modelos (`Models/`)
- **`TaskItem.cs`**: Define como é uma "Tarefa" no sistema (Id, Título, Descrição, Status, Data de Criação). Usamos um `enum` para os status (Pendente, Em andamento, Concluído).

### Camada de Dados (`Data/`)
- **`AppDbContext.cs`**: É a ponte entre o código C# e o Banco de Dados MySQL usando o Entity Framework Core. Ele diz ao banco como a tabela de tarefas deve ser criada.

### Camada de Acesso a Dados (`Repositories/`)
- **`TaskRepository.cs`**: Contém as funções que realmente tocam no banco de dados (Salvar, Deletar, Buscar). Isso isola a lógica de banco do resto do sistema.

### Camada de Negócio (`Services/`)
- **`TaskService.cs`**: Aqui moram as **Regras de Negócio** pedidas no teste:
  - Uma tarefa não pode ser concluída sem título.
  - Tarefas concluídas não podem ser excluídas.
  - Conversão de dados entre o Banco (Model) e o que o Front vê (DTO).

### Camada de Interface (`Controllers/`)
- **`TasksController.cs`**: Expõe os "Endpoints" (URLs) que o Frontend chama (GET, POST, PUT, DELETE).

### DTOs (`DTOs/`)
- **Data Transfer Objects**: São classes simples usadas para carregar dados entre o front e o back sem expor toda a estrutura interna do banco de dados.

---

## 3. O Frontend (Vue.js)

A interface foi feita para ser **reativa** e **limpa**.

- **`App.vue`**: É o componente principal que gerencia o estado da lista de tarefas e decide qual tela mostrar (Lista ou Formulário).
- **`TaskList.vue`**: Mostra as tarefas em cards bonitos, com cores diferentes para cada status e filtros de busca.
- **`TaskForm.vue`**: O formulário inteligente que serve tanto para criar quanto para editar tarefas.
- **`services/api.js`**: Usa a biblioteca **Axios** para conversar com o Backend .NET.

---

## 4. Regras de Negócio Implementadas

Para brilhar na apresentação, mencione que você garantiu as seguintes regras:
1. **Validação de Título**: No Backend (`TaskService.cs`), há um cheque que impede salvar sem título.
2. **Restrição de Exclusão**: O botão "Excluir" fica desativado no Frontend para tarefas concluídas, e o Backend também bloqueia a operação por segurança.
3. **Filtros Dinâmicos**: É possível buscar por texto ou filtrar por status em tempo real.

---

## 5. Como rodar o projeto (Dicas para você)

### No Banco de Dados:
1. Execute o script `database.sql` no seu MySQL (Workbench ou via linha de comando).

### No Backend:
1. Abra a pasta `Magnadata.API` no Visual Studio ou VS Code.
2. No `appsettings.json`, ajuste a senha do seu MySQL em `DefaultConnection`.
3. Rode o comando `dotnet run`. O Swagger abrirá no navegador mostrando os endpoints.

### No Frontend:
1. Na raiz do projeto, execute `npm install` para garantir todas as dependências (incluindo axios).
2. Execute `npm run dev`.
3. Acesse o endereço que aparecer (geralmente `http://localhost:5173`).

---

## Dicas para a Apresentação:
- Comece mostrando a aplicação funcionando (Crie uma tarefa, mude o status, tente excluir).
- Mostre o código do `TaskService.cs` para provar que você pensou nas regras de negócio.
- Explique que você usou o padrão **Repository** para deixar o código mais profissional e fácil de testar no futuro.

---

## 7. Próximos Passos (Melhorias Futuras)

Caso perguntem o que você faria se tivesse mais tempo, aqui estão algumas sugestões:
- **Paginação**: Essencial para quando a lista de tarefas crescer muito.
- **Testes Unitários**: Criar testes para o `TaskService` para garantir que as regras de negócio nunca quebrem.
- **Docker**: Containerizar a aplicação para que ela rode em qualquer lugar sem precisar instalar nada manualmente.
- **Autenticação**: Adicionar um sistema de login para que cada usuário tenha suas próprias tarefas.


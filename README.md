# UserManagementAPI

API para gerencimento CRUD de usuários, com controle de acesso via login e chave api.

## Tecnologias Utilizadas

- ASP. Net Core 6.0
- Entity Framework Core 5.0.9
- JWT Authentication 5.0.11
- SQL Server 5.0.9
- Swagger 6.2.3

## Funcionalidades

- Cadastro e login de usuários
- Atribuição e gerenciamento de papéis (roles)
- Autenticação com JWT
- Autorização via chave de API personalizada
- Ocultação de credenciais senvíveis (SMTP, JWT e KEY API)
- Documentação via Swagger
- Padrão RESTful


## Endpoints

###  Account

| Método | Rota                   | Descrição                    |
|--------|------------------------|-------------------------------|
| POST   | `/api/accounts/register` | Registra um novo usuário      |
| POST   | `/api/accounts/login`    | Realiza login e retorna token |


###  Role

| Método | Rota                | Descrição              |
|--------|---------------------|-------------------------|
| GET    | `/api/roles`        | Lista todas as roles    |
| GET    | `/api/roles/{id}`   | Busca role por ID       |
| POST   | `/api/roles`        | Cria uma nova role      |
| PUT    | `/api/roles/{id}`   | Atualiza uma role       |
| DELETE | `/api/roles/{id}`   | Remove uma role         |


###  User

| Método | Rota                | Descrição                 |
|--------|---------------------|----------------------------|
| GET    | `/api/users`        | Lista todos os usuários e roles    |
| GET    | `/api/users/{id}`   | Busca usuário por ID       |
| POST   | `/api/users`        | Cria um novo usuário       |
| PUT    | `/api/users/{id}`   | Atualiza um usuário        |
| DELETE | `/api/users/{id}`   | Remove um usuário          |

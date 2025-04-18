CleanArchDotNet
Projeto completo em .NET 8 seguindo os principais princípios e boas práticas de desenvolvimento backend profissional.

✅ Tecnologias e Princípios Utilizados
.NET 8

Entity Framework Core (SQLite)

Autenticação com JWT

Swagger

Clean Architecture

DDD (Domain-Driven Design)

Princípios SOLID

TDD (Test-Driven Development) com xUnit e Moq

Injeção de Dependência (DI)

Middlewares customizados

Docker e Docker Compose

📁 Estrutura do Projeto
bash
Copiar
Editar
CleanArchDotNet/
│
├── src/
│   ├── API/             # Camada de Apresentação (Web API)
│   ├── Application/     # Casos de uso, regras de negócio
│   ├── Core/            # Entidades e contratos (DDD)
│   └── Infrastructure/  # Acesso a dados, repositórios, contexto EF Core
│
├── tests/               # Testes de unidade
└── docker-compose.yml
▶️ Como executar o projeto
Requisitos
.NET 8 SDK

Docker (opcional)

Passos
Restaure as dependências do projeto:

bash
Copiar
Editar
cd src/API
dotnet restore
Atualize o banco de dados com as migrations:

bash
Copiar
Editar
dotnet ef database update
Execute o projeto:

bash
Copiar
Editar
dotnet run
Acesse o Swagger em: https://localhost:5001/swagger

🐳 Rodando com Docker
Caso queira rodar o projeto com Docker, utilize o seguinte comando:

bash
Copiar
Editar
docker-compose up --build
A API será exposta em https://localhost:5001 (ou http://localhost:5000).

🔐 Autenticação JWT
Endpoint de Login
http
Copiar
Editar
POST /auth/login
Body (JSON):

json
Copiar
Editar
{
  "username": "admin",
  "password": "admin123"
}
Uso do Token
Inclua o token no header Authorization com o formato:

http
Copiar
Editar
Bearer {token}
🧪 Executar Testes
Para executar os testes de unidade, execute o comando abaixo:

bash
Copiar
Editar
cd tests
dotnet test
🧱 Conceitos e Padrões Abordados
Responsabilidade única e separação de camadas

Repositórios desacoplados com interfaces

Casos de uso organizados

Middlewares customizados para logging e autenticação

Validações e serviços bem definidos

Autenticação e autorização com JWT

Testes unitários para regras de negócio e controllers

Injeção de dependência (DI) para desacoplamento de serviços

👨‍💻 Autor
Bruno — Projeto de estudo com aplicação prática de Clean Architecture em .NET
LinkedIn: linkedin.com/in/051bruno

📄 Licença
Este projeto está licenciado sob a MIT License.
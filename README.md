
# CleanArchDotNet

Projeto completo em .NET 8 seguindo os principais princípios e boas práticas de desenvolvimento backend profissional.

## ✅ Tecnologias e Princípios Utilizados

- **.NET 8**
- **Entity Framework Core (SQLite)**
- **Autenticação com JWT**
- **Swagger**
- **Clean Architecture**
- **DDD (Domain-Driven Design)**
- **Princípios SOLID**
- **TDD (Test-Driven Development) com xUnit e Moq**
- **Injeção de Dependência (DI)**
- **Middlewares customizados**
- **Docker e Docker Compose**

---

### 🔐 Atenção
Este projeto é **apenas para fins de estudo**. As chaves de autenticação estão visíveis apenas para facilitar testes e aprendizado. Em produção, essas informações devem ser protegidas com `dotnet user-secrets`, variáveis de ambiente, ou outros mecanismos seguros.



## 📁 Estrutura do Projeto

```
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
```



---

## ▶️ Como executar o projeto

### Requisitos
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- Docker (opcional)

### Passos

```bash
cd src/API
dotnet restore
dotnet ef database update
dotnet run
```

Acesse o Swagger em: `https://localhost:5001/swagger`

---

## 🐳 Rodando com Docker

```bash
docker-compose up --build
```

A API será exposta em `https://localhost:5001` (ou `http://localhost:5000`)

---

## 🔐 Autenticação JWT

### Endpoint de Login

```
POST /auth/login
```

```json
{
  "username": "admin",
  "password": "admin123"
}
```

### Uso do Token
Inclua o token no header `Authorization` com o formato:

```
Bearer {token}
```

---

## 🧪 Executar Testes

```bash
cd tests
dotnet test
```

---

## 🧱 Conceitos e padrões abordados

- **Responsabilidade única e separação de camadas**
- **Repositórios desacoplados com interfaces**
- **Casos de uso organizados**
- **Middlewares customizados para logging e autenticação**
- **Validações e serviços bem definidos**
- **Autenticação e autorização com JWT**
- **Testes unitários para regras de negócio e controllers**

---

## 👨‍💻 Autor

Bruno — projeto de estudo com aplicação prática de Clean Architecture em .NET  
LinkedIn: [linkedin.com/in/051bruno](https://linkedin.com/in/051bruno)

---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

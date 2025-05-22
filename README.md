# REST API com ASP.NET Core

- API de gerenciamento simples desenvolvida com ASP.NET Core, C# e .NET 6, demonstrando conceitos de CRUD (Create, Read, Update, Delete) e autenticação JWT.

---

## 🚀 Funcionalidades
- Endpoints RESTful para operações CRUD
- Autenticação JWT (JSON Web Token)
- Validação de modelos
- Documentação via Swagger/OpenAPI
- Entity Framework Core para acesso ao banco de dados
- Evolve para suporte a migrations.
- Front-End em TypeScript + Vite consumindo a api e fornecendo suporte a interface de usuário.

---

## 📦 Tecnologias Utilizadas
- **ASP.NET Core 6**
- **C#**
- **Entity Framework Core**
- **Evolve**
- **Docker**
- **JWT Bearer Authentication**
- **Swagger UI**
- **PostgreSQL**
- **TypeScript**
- Vite + TailwindCSS

---

## 🛠️ Como Executar

1. **Clonar o repositório:**
   
   ```bash
   git clone https://github.com/seu-usuario/rest-api-aspnet.git
   ```
   
3. **Iniciar os contêineres:**
   
   ```bash
   docker-compose up
   ```
- A Aplicação iniciará em **http://localhost:5151**.

## Como Executar o Frontend

```bash
  cd frontend
  cd books-frontend-main
  npm i // instala as dependências
  npm run dev
```
- A Aplicação iniciará em **http://localhost:8080**.
  
## 🤝 Contribuição

- Contribuições são bem-vindas! Para contribuir:

- Faça um fork do projeto.
- Crie uma branch (git checkout -b feature/nova-funcionalidade).
- Commit suas mudanças (git commit -m 'Adicionar nova funcionalidade').
- Push para a branch (git push origin feature/nova-funcionalidade).
- Abra um Pull Request.

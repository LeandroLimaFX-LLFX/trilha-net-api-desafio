Instruções de entrega - Projeto Trilha .NET API

Este repositório contém a solução do desafio (CRUD de Tarefa).

Observações:
- A implementação do `TarefaController` foi completada (ObterPorId, ObterTodos, ObterPorTitulo, ObterPorData, ObterPorStatus, Criar, Atualizar, Deletar).
- `Program.cs` foi adaptado para usar um banco InMemory quando a connection string não estiver configurada, facilitando execução sem SQL Server.

Como executar (PowerShell):

# Build

dotnet build .\TrilhaApiDesafio.csproj

# Rodar a API (usa InMemory se conexão não estiver configurada)

dotnet run --project .\TrilhaApiDesafio.csproj

Abra o Swagger UI no endereço mostrado pelo terminal (ex: https://localhost:5001/swagger)

Se quiser usar SQL Server em vez do InMemory, atualize `appsettings.Development.json` com a sua connection string e gere/aplique migrations:

# Instalar dotnet-ef (se necessário)

dotnet tool install --global dotnet-ef

# Gerar migration e aplicar

dotnet ef migrations add InitialCreate --project .\TrilhaApiDesafio.csproj --startup-project .\TrilhaApiDesafio.csproj

dotnet ef database update --project .\TrilhaApiDesafio.csproj --startup-project .\TrilhaApiDesafio.csproj

Branch com alterações: feat/complete-challenge

Obrigado, qualquer ajuste adicional me avise.

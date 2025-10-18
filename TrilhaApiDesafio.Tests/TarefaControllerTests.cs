using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Controllers;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace TrilhaApiDesafio.Tests
{
    public class TarefaControllerTests : IDisposable
    {
        private readonly OrganizadorContext _context;
        private readonly TarefaController _controller;

        public TarefaControllerTests()
        {
            var options = new DbContextOptionsBuilder<OrganizadorContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new OrganizadorContext(options);
            _context.Database.EnsureCreated();

            _controller = new TarefaController(_context);
        }

        [Fact]
        public void Crud_Tarefa_Workflow()
        {
            var nova = new Tarefa
            {
                Titulo = "Teste",
                Descricao = "Desc",
                Data = DateTime.Now,
                Status = EnumStatusTarefa.Pendente
            };

            var criarResult = _controller.Criar(nova) as CreatedAtActionResult;
            Assert.NotNull(criarResult);
            var created = criarResult.Value as Tarefa;
            Assert.NotNull(created);
            Assert.Equal("Teste", created.Titulo);

            var todosResult = _controller.ObterTodos() as OkObjectResult;
            Assert.NotNull(todosResult);
            var lista = todosResult.Value as System.Collections.IEnumerable;
            Assert.NotNull(lista);

            var obterResult = _controller.ObterPorId(created.Id) as OkObjectResult;
            Assert.NotNull(obterResult);
            var obter = obterResult.Value as Tarefa;
            Assert.NotNull(obter);

            created.Titulo = "NovoTitulo";
            var atualizar = _controller.Atualizar(created.Id, created) as OkObjectResult;
            Assert.NotNull(atualizar);
            var atualizado = atualizar.Value as Tarefa;
            Assert.NotNull(atualizado);
            Assert.Equal("NovoTitulo", atualizado.Titulo);

            var del = _controller.Deletar(atualizado.Id) as NoContentResult;
            Assert.NotNull(del);

            var todosAfter = _controller.ObterTodos() as OkObjectResult;
            Assert.NotNull(todosAfter);
            var listaAfter = todosAfter.Value as System.Collections.IEnumerable;
            Assert.NotNull(listaAfter);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}


using System;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Controllers;
using TrilhaApiDesafio.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace TrilhaApiDesafio.Tests
{
    public class TarefaControllerTests
    {
        private OrganizadorContext CriarContextoEmMemoria(string dbName)
        {
            var options = new DbContextOptionsBuilder<OrganizadorContext>()
                .UseInMemoryDatabase(dbName)
                using System;
                using Microsoft.EntityFrameworkCore;
                using TrilhaApiDesafio.Context;
                using TrilhaApiDesafio.Controllers;
                using TrilhaApiDesafio.Models;
                using Xunit;
                using Microsoft.AspNetCore.Mvc;
                using System.Collections;

                namespace TrilhaApiDesafio.Tests
                {
                    public class TarefaControllerTests
                    {
                        private OrganizadorContext CriarContextoEmMemoria(string dbName)
                        {
                            var options = new DbContextOptionsBuilder<OrganizadorContext>()
                                .UseInMemoryDatabase(dbName)
                                .Options;

                            var context = new OrganizadorContext(options);
                            return context;
                        }

                        [Fact]
                        public void Criar_Obter_Atualizar_Deletar_Tarefa()
                        {
                            var contexto = CriarContextoEmMemoria("TesteDB");
                            var controller = new TarefaController(contexto);

                            var nova = new Tarefa
                            {
                                Titulo = "Teste",
                                Descricao = "Desc",
                                Data = DateTime.Now,
                                Status = EnumStatusTarefa.Pendente
                            };

                            var criarResult = controller.Criar(nova) as CreatedAtActionResult;
                            Assert.NotNull(criarResult);

                            var tarefasTodos = (controller.ObterTodos() as OkObjectResult)?.Value as IEnumerable;
                            Assert.NotNull(tarefasTodos);

                            var obterIdResult = controller.ObterPorId(nova.Id) as OkObjectResult;
                            Assert.NotNull(obterIdResult);

                            var tarefaObtida = obterIdResult.Value as Tarefa;
                            Assert.Equal("Teste", tarefaObtida.Titulo);

                            // Atualizar
                            tarefaObtida.Titulo = "NovoTitulo";
                            var atualizar = controller.Atualizar(tarefaObtida.Id, tarefaObtida) as OkObjectResult;
                            Assert.NotNull(atualizar);
                            var atualizado = atualizar.Value as Tarefa;
                            Assert.Equal("NovoTitulo", atualizado.Titulo);

                            // Deletar
                            var del = controller.Deletar(atualizado.Id) as StatusCodeResult;
                            Assert.Equal(204, del.StatusCode);
                        }
                    }
                }

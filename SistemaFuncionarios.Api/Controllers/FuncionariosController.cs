using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFuncionarios.Api.Models;
using SistemaFuncionarios.Data.Entities;
using SistemaFuncionarios.Data.Repositories;

namespace SistemaFuncionarios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(FuncionariosPostModel model)
        {
            try
            {
                var funcionario = new Funcionario();

                funcionario.Nome = model.Nome;
                funcionario.Cpf = model.Cpf;
                funcionario.Matricula = model.Matricula;
                funcionario.DataAdmissao = model.DataAdmissao;

                var funcionarioRepository = new FuncionarioRepository();
                funcionarioRepository.Inserir(funcionario);

                //HTTP 201 (CREATED)
                return StatusCode(201, new { mensagem = $"Funcionário {funcionario.Nome}, cadastrado com sucesso." });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = $"Falha: {e.Message}" });
            }
        }

        [HttpPut]
        public IActionResult Put(FuncionariosPutModel model)
        {
            try
            {
                var funcionarioRepository = new FuncionarioRepository();
                var obterFuncionario = funcionarioRepository.ObterPorId(model.IdFuncionario);
                if(obterFuncionario != null)
                {
                    var funcionario = new Funcionario();
                    funcionario.Nome = model.Nome;
                    funcionario.Cpf = model.Cpf;
                    funcionario.Matricula = model.Matricula;
                    funcionario.DataAdmissao = model.DataAdmissao;

                    funcionarioRepository.Atualizar(funcionario);

                    return StatusCode(200, new { mensagem = $"Funcionario {funcionario.Nome}, atualizado com sucesso." });
                }
                else
                {
                    return StatusCode(400, new { mensagem = $"Funcionário não encontrado, verifique o ID informado." });
                }
            }catch (Exception e)
            {
                return StatusCode(500, new { mensagem = $"Falha: {e.Message}" });
            }
        }

        [HttpDelete("{idFuncionario}")]
        public IActionResult Delete(Guid idFuncionario)
        {
            try
            {
                var funcionarioRepository = new FuncionarioRepository();
                var obterFuncionario = funcionarioRepository.ObterPorId(idFuncionario);
                if(obterFuncionario != null)
                {
                    funcionarioRepository.Excluir(idFuncionario);

                    return StatusCode(200, new { mensagem = $"Funcionário {obterFuncionario.Nome}, excluído com sucesso!" });
                }
                else
                {
                    return StatusCode(400, new { mensagem = $"Funcionário não encontrado, verifique o ID e tente novamente." });
                }
              
            }catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Falha: {ex.Message}" });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FuncionariosGetModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var lista = new List<FuncionariosGetModel>();

                //acessando o banco de dados e executar a consulta de funcionarios
                var funcionarioRepository = new FuncionarioRepository();
                foreach (var item in funcionarioRepository.Consultar())
                {
                    var model = new FuncionariosGetModel();

                    model.IdFuncionario = item.IdFuncionario;
                    model.Nome = item.Nome;
                    model.Cpf = item.Cpf;
                    model.Matricula = item.Matricula;
                    model.DataAdmissao = item.DataAdmissao;
                    model.DataCadastro = item.DataCadastro;
                    model.DataUltimaAlteracao = item.DataUltimaAlteracao;

                    lista.Add(model);
                }

                //HTTP 200 (OK)
                return StatusCode(200, lista);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { mensagem = $"Falha: {e.Message}" });
            }
        }

        [HttpGet("{idFuncionario}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FuncionariosGetModel))]
        public IActionResult GetById(Guid idFuncionario)
        {
            try
            {
                var funcionarioRepository = new FuncionarioRepository();
                var funcionario = funcionarioRepository.ObterPorId(idFuncionario);
                if(funcionario != null)
                {
                    var model = new FuncionariosGetModel();
                    model.IdFuncionario = funcionario.IdFuncionario;
                    model.Nome = funcionario.Nome;
                    model.Cpf = funcionario.Cpf;
                    model.Matricula = funcionario.Matricula;
                    model.DataAdmissao = funcionario.DataAdmissao;
                    model.DataCadastro = funcionario.DataCadastro;
                    model.DataUltimaAlteracao = funcionario.DataUltimaAlteracao;

                    return StatusCode(200, model);
                }
                else
                {
                    //HTTP 204 (No Content)
                    return StatusCode(204);
                }
            }catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Falha: {ex.Message}" });
            }
        }
    }
}



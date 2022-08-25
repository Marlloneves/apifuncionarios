using Dapper;
using SistemaFuncionarios.Data.Configurations;
using SistemaFuncionarios.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaFuncionarios.Data.Repositories
{
    /// <summary>
    /// Classe para executar as operações de funcionário no banco de dados
    /// </summary>
    public class FuncionarioRepository
    {
        //método para inserir um funcionário no banco de dados
        public void Inserir(Funcionario funcionario)
        {
            //abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(SqlServerConfiguration.GetSqlConnectionString()))
            {
                //executando a procedure
                connection.Execute("SP_INSERIR_FUNCIONARIO",
                    new
                    {
                        @NOME = funcionario.Nome,
                        @CPF = funcionario.Cpf,
                        @MATRICULA = funcionario.Matricula,
                        @DATAADMISSAO = funcionario.DataAdmissao
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        //método para atualizar um funcionário no banco de dados
        public void Atualizar(Funcionario funcionario)
        {
            //abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(SqlServerConfiguration.GetSqlConnectionString()))
            {
                //executando a procedure
                connection.Execute("SP_ALTERAR_FUNCIONARIO",
                    new
                    {
                        @IDFUNCIONARIO = funcionario.IdFuncionario,
                        @NOME = funcionario.Nome,
                        @CPF = funcionario.Cpf,
                        @MATRICULA = funcionario.Matricula,
                        @DATAADMISSAO = funcionario.DataAdmissao
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        //método para excluir um funcionário no banco de dados
        public void Excluir(Guid idFuncionario)
        {
            //abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(SqlServerConfiguration.GetSqlConnectionString()))
            {
                //executando a procedure
                connection.Execute("SP_EXCLUIR_FUNCIONARIO",
                    new
                    {
                        @IDFUNCIONARIO = idFuncionario
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        //método para consultar todos os funcionários no banco de dados
        public List<Funcionario> Consultar()
        {
            //abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(SqlServerConfiguration.GetSqlConnectionString()))
            {
                //executando a procedure
                return connection
                    .Query<Funcionario>("SP_CONSULTAR_FUNCIONARIOS",
                        commandType: CommandType.StoredProcedure)
                    .ToList();
            }
        }
        //método para consultar 1 funcionário através do ID
        public Funcionario? ObterPorId(Guid idFuncionario)
        {
            //abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(SqlServerConfiguration.GetSqlConnectionString()))
            {
                //executando a procedure
                return connection
                    .Query<Funcionario>("SP_OBTER_FUNCIONARIO",
                        new { @IDFUNCIONARIO = idFuncionario },
                        commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }
        }

    }
}

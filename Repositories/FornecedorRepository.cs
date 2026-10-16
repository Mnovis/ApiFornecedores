using ApiFornecedores.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiFornecedores.Repositories
{
    public class FornecedorRepository (string connectionString)
    {
        public void Cadastrar(Fornecedor fornecedor)
        {
            var sql = """
                      INSERT INTO FORNECEDORES(ID, NOME, CNPJ, EMAIL, TELEFONE, DATACADASTRO)
                      VALUES(@Id, @Nome, @Cnpj, @Email, @Telefone, @DataCadastro)
                      """;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, fornecedor);
            }
        }

        public void Editar(Fornecedor fornecedor)
        {
            var sql = """
                      UPDATE FORNECEDORES
                      SET 
                        NOME = @Nome,
                        CNPJ = @Cnpj,
                        EMAIL = @Email,
                        TELEFONE = @Telefone
                      """;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, fornecedor);
            }
        }

        public void Excluir(Guid id)
        {
            var sql = """
                      DELETE FROM FORNECEDORES
                      WHERE ID = @Id
                      """;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, new { @Id = id });
            }
        }

        public List<Fornecedor> ListarTodos()
        {
            var sql = """
                      SELECT * FROM FORNECEDORES
                      ORDER BY NOME
                      """;

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Fornecedor>(sql).ToList();
            }
        }

        public Fornecedor? ObterFornecedor(Guid id)
        {
            var sql = """
                        SELECT * FROM FORNECEDORES
                        WHERE ID = @Id
                      """;

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QueryFirstOrDefault<Fornecedor>(sql, new { @Id = id });
            }
        }
    }
}

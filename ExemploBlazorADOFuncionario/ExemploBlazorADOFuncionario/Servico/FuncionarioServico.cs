using ExemploBlazorADOFuncionario.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExemploBlazorADOFuncionario.Servico
{
    public class FuncionarioServico
    {
        private readonly string _connectionString;

        public FuncionarioServico(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Funcionario>> ObterTodosAsync()
        {
            var Funcionarios = new List<Funcionario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(@"SELECT dbo.tbFuncionario.Id, dbo.tbFuncionario.DepartamentoId, 
                                                dbo.tbDepartamento.Nome AS NomeDepartamento, dbo.tbFuncionario.Nome, 
                                                dbo.tbFuncionario.NomeMae, dbo.tbFuncionario.NomePai, dbo.tbFuncionario.DataNascimento,
                                                dbo.tbFuncionario.CPF, dbo.tbFuncionario.RG, dbo.tbFuncionario.Email
                                               FROM dbo.tbDepartamento INNER JOIN dbo.tbFuncionario ON dbo.tbDepartamento.Id = dbo.tbFuncionario.DepartamentoId", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Funcionarios.Add(new Funcionario
                        {
                            Id = reader.GetInt32("Id"),
                            DepartamentoId = reader.GetInt32("DepartamentoId"),
                            NomeDepartamento = reader.GetString("NomeDepartamento"),
                            Nome = reader.GetString("Nome"),
                            NomeMae = reader.GetString("NomeMae"),
                            NomePai = reader.GetString("NomePai"),
                            DataNascimento = reader.GetDateTime("DataNascimento"),
                            CPF = reader.GetString("CPF"),
                            RG = reader.GetString("RG"),
                            Email = reader.GetString("Email")
                        });
                    }
                }
            }

            return Funcionarios;
        }

        public async Task<Funcionario> ObterPorIdAsync(int id)
        {
            Funcionario funcionario = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(@"SELECT dbo.tbFuncionario.Id, dbo.tbFuncionario.DepartamentoId, 
                                                  dbo.tbDepartamento.Nome AS NomeDepartamento, dbo.tbFuncionario.Nome, 
                                                  dbo.tbFuncionario.NomeMae, dbo.tbFuncionario.NomePai, dbo.tbFuncionario.DataNascimento,
                                                  dbo.tbFuncionario.CPF, dbo.tbFuncionario.RG, dbo.tbFuncionario.Email
                                               FROM dbo.tbDepartamento INNER JOIN dbo.tbFuncionario ON dbo.tbDepartamento.Id = dbo.tbFuncionario.DepartamentoId
                                               WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        funcionario = new Funcionario
                        {
                            Id = reader.GetInt32("Id"),
                            DepartamentoId = reader.GetInt32("DepartamentoId"),
                            NomeDepartamento = reader.GetString("NomeDepartamento"),
                            Nome = reader.GetString("Nome"),
                            NomeMae = reader.GetString("NomeMae"),
                            NomePai = reader.GetString("NomePai"),
                            DataNascimento = reader.GetDateTime("DataNascimento"),
                            CPF = reader.GetString("CPF"),
                            RG = reader.GetString("RG"),
                            Email = reader.GetString("Email")
                            
                        };
                    }
                }
            }

            return funcionario;
        }


        public async Task AdicionarFuncionarioAsync(Funcionario funcionario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO tbFuncionario (DepartamentoId, Nome, NomeMae, NomePai, DataNascimento, CPF, RG, Email) " +
                                             "VALUES (@DepartamentoId, @Nome, @NomeMae, @NomePai, @DataNascimento, @CPF, @RG, @Email)", connection);

                command.Parameters.AddWithValue("@DepartamentoId", funcionario.DepartamentoId);
                command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                command.Parameters.AddWithValue("@NomeMae", funcionario.NomeMae);
                command.Parameters.AddWithValue("@NomePai", funcionario.NomePai);
                // Use SqlDbType.DateTime for explicit date handling
                command.Parameters.Add("@DataNascimento", SqlDbType.DateTime).Value = funcionario.DataNascimento;
                command.Parameters.AddWithValue("@CPF", funcionario.CPF);
                command.Parameters.AddWithValue("@RG", funcionario.RG);
                command.Parameters.AddWithValue("@Email", funcionario.Email);


                await command.ExecuteNonQueryAsync();

            }
        }

        public async Task AtualizarFuncionarioAsync(Funcionario funcionario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE tbFuncionario SET " +
                                                     "DepartamentoId = @DepartamentoId, " +
                                                     "Nome = @Nome, " +
                                                     "NomeMae = @NomeMae, " +
                                                     "NomePai = @NomePai, " +
                                                     "DataNascimento = @DataNascimento, " +
                                                     "CPF = @CPF, " +
                                                     "RG = @RG, " +
                                                     "Email = @Email " +
                                                     "WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", funcionario.Id);
                command.Parameters.AddWithValue("@DepartamentoId", funcionario.DepartamentoId);
                command.Parameters.AddWithValue("@Nome", funcionario.Nome);
                command.Parameters.AddWithValue("@NomeMae", funcionario.NomeMae ?? (object)DBNull.Value); // Permite valores nulos
                command.Parameters.AddWithValue("@NomePai", funcionario.NomePai ?? (object)DBNull.Value); // Permite valores nulos
                command.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);
                command.Parameters.AddWithValue("@CPF", funcionario.CPF);
                command.Parameters.AddWithValue("@RG", funcionario.RG);
                command.Parameters.AddWithValue("@Email", funcionario.Email);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeletarFuncionarioAsync(int funcionarioId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM tbFuncionario WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", funcionarioId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

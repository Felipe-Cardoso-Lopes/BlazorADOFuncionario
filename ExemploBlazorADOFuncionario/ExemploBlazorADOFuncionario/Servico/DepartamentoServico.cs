using ExemploBlazorADOFuncionario.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ExemploBlazorADOFuncionario.Servico
{
    public class DepartamentoServico
    {
        private readonly string _connectionString;

        public DepartamentoServico(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Departamento>> ObterTodosAsync()
        {
            var Departamentos = new List<Departamento>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT Id, Nome, Sigla, Email, Telefone FROM tbDepartamento", connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Departamentos.Add(new Departamento
                        {
                            Id = reader.GetInt32("Id"),
                            Nome = reader.GetString("Nome"),
                            Sigla = reader.GetString("Sigla"),
                            Email = reader.GetString("Email"),
                            Telefone = reader.GetString("Telefone")
                        });
                    }
                }
            }

            return Departamentos;
        }

        public async Task<Departamento> ObterPorIdAsync(int id)
        {
            Departamento departamento = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT Id, Nome, Sigla, Email, Telefone FROM tbDepartamento WHERE Id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        departamento = new Departamento
                        {
                            Id = reader.GetInt32("Id"),
                            Nome = reader.GetString("Nome"),
                            Sigla = reader.GetString("Sigla"),
                            Email = reader.GetString("Email"),
                            Telefone = reader.GetString("Telefone")
                        };
                    }
                }
            }

            return departamento;
        }


        public async Task AdicionarDepartamentoAsync(Departamento departamento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO tbDepartamento (Nome, Sigla, Email, Telefone) VALUES (@Nome, @Sigla, @Email, @Telefone)", connection);

                command.Parameters.AddWithValue("@Nome", departamento.Nome);
                command.Parameters.AddWithValue("@Sigla", departamento.Sigla);
                command.Parameters.AddWithValue("@Email", departamento.Email);
                command.Parameters.AddWithValue("@Telefone", departamento.Telefone);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AtualizarDepartamentoAsync(Departamento departamento)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE tbDepartamento SET " +
                                                     "Nome = @Nome, " +
                                                     "Sigla = @Sigla, " +
                                                     "Email = @Email, " +
                                                     "Telefone = @Telefone " +
                                                     " WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", departamento.Id);
                command.Parameters.AddWithValue("@Nome", departamento.Nome);
                command.Parameters.AddWithValue("@Sigla", departamento.Sigla);
                command.Parameters.AddWithValue("@Email", departamento.Email);
                command.Parameters.AddWithValue("@Telefone", departamento.Telefone);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeletarDepartamentoAsync(int departamentoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM tbDepartamento WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", departamentoId);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}


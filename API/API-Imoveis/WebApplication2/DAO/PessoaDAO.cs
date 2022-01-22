using APICondominio.Model;
using System.Data.SqlClient;

namespace APICondominio.DAO
{
    public class PessoaDAO
    {
        string ConnectionString;

        public PessoaDAO()
        {
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Apcoders\Apcoders_Alanis\DB\CondominioDB.mdf;Integrated Security=True;Connect Timeout=30";
        }

        public void Atualizar(PessoaModel model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "update Pessoa set Nome = @Nome, Idade = @Idade, Telefone = @Telefone, Email = @Email, Genero =  @Genero";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Nome", model.Nome);
                cmd.Parameters.AddWithValue("@Idade", model.Idade);
                cmd.Parameters.AddWithValue("@Telefone", model.Telefone);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Genero", (int)model.Genero);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public int Criar(PessoaModel model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "insert into Pessoa (Nome, Idade, Telefone, Email, Genero) values(@Nome, @Idade, @Telefone, @Email, @Genero)";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Nome", model.Nome);
                cmd.Parameters.AddWithValue("@Idade", model.Idade);
                cmd.Parameters.AddWithValue("@Telefone", model.Telefone);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Genero", (int)model.Genero);
                cmd.CommandType = System.Data.CommandType.Text;
                return cmd.ExecuteNonQuery();
            }
        }

        public PessoaModel BuscarPeloId(int pID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "select * from pessoa where id = @ID";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", pID);

                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var model = new PessoaModel();

                    model.ID = GetSafeInt(reader["ID"].ToString());
                    model.Idade = GetSafeInt(reader["Idade"].ToString());
                    model.Email = (string)reader["Email"];
                    model.Telefone = (string)reader["Telefone"];
                    model.Nome = (string)reader["Nome"];
                    model.Genero = (Genero)((int)reader["Genero"]);
                    return model;
                }

                return null;
            }
        }

        public void Deletar(int pID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "delete from pessoa where id = @ID";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", pID);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public List<PessoaModel> BuscarTodos()
        {
            var lista = new List<PessoaModel>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "select * from pessoa";
                var cmd = new SqlCommand(sql, connection);

                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var model = new PessoaModel();

                    model.ID = GetSafeInt(reader["ID"].ToString());
                    model.Idade = GetSafeInt(reader["Idade"].ToString());
                    model.Email = (string)reader["Email"];
                    model.Telefone = (string)reader["Telefone"];
                    model.Nome = (string)reader["Nome"];
                    model.Genero = (Genero)((int)reader["Genero"]);
                    lista.Add(model);
                }

                return lista;
            }
        }

        private int GetSafeInt(string? value, int defaultValue = -1)
        {
            if (int.TryParse(value, out int idConvertido))
                return idConvertido;

            return defaultValue;
        }
    }
}

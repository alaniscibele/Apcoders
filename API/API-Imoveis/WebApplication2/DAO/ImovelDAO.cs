using APICondominio.Model;
using System.Data.SqlClient;

namespace APICondominio.DAO
{
    public class ImovelDAO
    {
        string ConnectionString;

        public ImovelDAO()
        {
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Apcoders\Apcoders_Alanis\DB\CondominioDB.mdf;Integrated Security=True;Connect Timeout=30";
        }

        public void Atualizar(ImovelModel model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "update Imovel set Proprietario = @Proprietario, Endereco = @Endereco, Numero = @Numero, NomeCondominio = @NomeCondominio";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Proprietario", model.Proprietario);
                cmd.Parameters.AddWithValue("@Endereco", model.Endereco);
                cmd.Parameters.AddWithValue("@Numero", model.Numero);
                cmd.Parameters.AddWithValue("@NomeCondominio", model.NomeCondominio);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public int Criar(ImovelModel model)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "insert into Imovel (Proprietario, Endereco, Numero, NomeCondominio) values(@Proprietario, @Endereco, @Numero, @NomeCondominio)";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Proprietario", model.Proprietario);
                cmd.Parameters.AddWithValue("@Endereco", model.Endereco);
                cmd.Parameters.AddWithValue("@Numero", model.Numero);
                cmd.Parameters.AddWithValue("@NomeCondominio", model.NomeCondominio);
                cmd.CommandType = System.Data.CommandType.Text;
                return cmd.ExecuteNonQuery();
            }
        }

        public ImovelModel BuscarPeloId(int pID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "select * from Imovel where id = @ID";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", pID);

                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    var model = new ImovelModel();

                    model.ID = GetSafeInt(reader["ID"].ToString());
                    model.NomeCondominio = reader["NomeCondominio"].ToString();
                    model.Endereco = reader["Endereco"].ToString();
                    model.Proprietario = reader["Proprietario"].ToString();
                    model.Numero = reader["Numero"].ToString();
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
                string sql = "delete from Imovel where id = @ID";
                var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@ID", pID);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public List<ImovelModel> BuscarTodos()
        {
            var lista = new List<ImovelModel>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "select * from Imovel";
                var cmd = new SqlCommand(sql, connection);

                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var model = new ImovelModel();

                    model.ID = GetSafeInt(reader["ID"].ToString());
                    model.NomeCondominio = reader["NomeCondominio"].ToString();
                    model.Endereco = reader["Endereco"].ToString();
                    model.Proprietario = reader["Proprietario"].ToString();
                    model.Numero = reader["Numero"].ToString();
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

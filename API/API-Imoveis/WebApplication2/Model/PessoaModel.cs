namespace APICondominio.Model
{
    public class PessoaModel
    {
        public int ID { get; set; }
        public int Idade { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Genero Genero { get; set; }
    }

    public enum Genero
    {
        Nenhum = 0,
        Masculino = 1,
        Feminino = 2
    }
}

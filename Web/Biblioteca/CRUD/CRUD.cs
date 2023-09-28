namespace Web.Biblioteca.CRUD
{
    public class CRUD
    {
        public string Titulo { get; set; } = String.Empty;
        public string SubTitulo { get; set; } = String.Empty;
        public string Descricao { get; set; } = String.Empty;
        public Opcoes Operacao { get; set; } = Opcoes.Information;


    }

    public enum Opcoes
    {
        Create = 0,
        Read = 1,
        Update = 2,
        Delete = 3,
        Information = 4

    }
}

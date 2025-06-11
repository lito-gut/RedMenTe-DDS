namespace RedMenTeWeb.Models
{
    public class UsuarioModel
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public int Edad { get; set; }
        public int ProgramaID { get; set; }
    }
}
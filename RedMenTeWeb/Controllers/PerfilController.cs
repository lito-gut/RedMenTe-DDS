using RedMenTeWeb.Models; // Asegúrate de tener este namespace
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

public class PerfilController : Controller
{
    private string cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

    // GET: /Perfil/Editar/1
    public ActionResult Editar(int id)
    {
        UsuarioModel usuario = new UsuarioModel();

        using (SqlConnection conn = new SqlConnection(cadenaConexion))
        {
            conn.Open();
            string query = "SELECT UsuarioID, Nombre, Correo, Rol, Edad, ProgramaID FROM Usuarios WHERE UsuarioID = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                usuario.UsuarioID = (int)reader["UsuarioID"];
                usuario.Nombre = reader["Nombre"].ToString();
                usuario.Correo = reader["Correo"].ToString();
                usuario.Rol = reader["Rol"].ToString();
                usuario.Edad = (int)reader["Edad"];
                usuario.ProgramaID = (int)reader["ProgramaID"];
            }
        }

        return View(usuario);
    }

    [HttpPost]
    public ActionResult Editar(UsuarioModel usuario)
    {
        using (SqlConnection conn = new SqlConnection(cadenaConexion))
        {
            conn.Open();
            string query = @"UPDATE Usuarios 
                             SET Nombre = @Nombre, Correo = @Correo, Rol = @Rol, Edad = @Edad, ProgramaID = @ProgramaID 
                             WHERE UsuarioID = @UsuarioID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
            cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
            cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
            cmd.Parameters.AddWithValue("@ProgramaID", usuario.ProgramaID);
            cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);

            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("PerfilActualizado"); // Puedes crear esta vista de confirmación
    }

    public ActionResult PerfilActualizado()
    {
        return View();
    }

    public ActionResult Index()
    {
        return View("~/Views/Home/Profile.cshtml");
    }
}

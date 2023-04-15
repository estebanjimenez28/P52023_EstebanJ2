using System.Windows.Forms;

namespace P52023_EstebanJ
{
    public class Globales
    {


        public static Form MiFormPrincipal = new Formularios.FrmMDI();
        public static Formularios.FrmUsuariosGestion MiFormGestionUsuarios = new Formularios.FrmUsuariosGestion();
    
        //Debemos tener un objeto de tipo Usuario que permita almacenar los datos del usuario
        //que se haya logueado correctamente
        public static Logica.Models.Usuario MiUsuarioGlobal = new Logica.Models.Usuario();
        public static Formularios.FrmRegistroCompra MiFormRegistroCompra = new Formularios.FrmRegistroCompra();
    }
}
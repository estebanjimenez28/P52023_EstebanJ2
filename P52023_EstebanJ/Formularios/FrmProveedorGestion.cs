using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P52023_EstebanJ.Formularios
{
    public partial class FrmProveedorGestion : Form
    {

        private Logica.Models.Proveedor MiProveedorLocal { get; set; }

        private DataTable ListaProveedor{ get; set; }

        public FrmProveedorGestion()
        {
            InitializeComponent();
            MiProveedorLocal = new Logica.Models.Proveedor();
            ListaProveedor = new DataTable();
        }

        private void FrmProveedorGestion_Load(object sender, EventArgs e)
        {
            MdiParent = Globales.MiFormPrincipal;

            CargarListaRoles();

            CargarListaDeProveedores();
            ActivarAgregar();


        }
        private void CargarListaDeProveedores()
        {
           
            ListaProveedor = new DataTable();

            string FiltroBusqueda = "";

            if (!string.IsNullOrEmpty(TxtBuscar.Text.Trim()) && TxtBuscar.Text.Count() >= 3)
            {
                FiltroBusqueda = TxtBuscar.Text.Trim();
            }

            if (CboxVerActivos.Checked)
            {
                //si en el cuadro de texto de busqueda hay 3 o mas caracteres se filtra la lista




                ListaProveedor = MiProveedorLocal.ListarActivos(FiltroBusqueda);
            }
            else
            {
                ListaProveedor = MiProveedorLocal.ListarInactivos(FiltroBusqueda);
            }

            DgLista.DataSource = ListaProveedor;

        }



        private void CargarListaRoles()
        {
            Logica.Models.TipoProveedor MiRol = new Logica.Models.TipoProveedor();

            DataTable dt = new DataTable();
            dt = MiRol.Listar();

            if (dt != null && dt.Rows.Count > 0)
            {
                CbTiposProveedor.ValueMember = "ID";
                CbTiposProveedor.DisplayMember = "Descrip";
                CbTiposProveedor.DataSource = dt;
                CbTiposProveedor.SelectedIndex = -1;
            }
        }

        private void DgLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgLista.SelectedRows.Count == 1)
            {
                LimpiarFormulario();

                //de la colección de filas seleccionadas (que en este caso es solo una) 
                //seleccionamos la fila en indice 0, o sea la primera 
                DataGridViewRow MiFila = DgLista.SelectedRows[0];

                //lo que necesito es el valor del ID del usuario para realizaer la consulta 
                //y traer todos los datos para llenar el objeto de usuario local 
                int IdProveedor = Convert.ToInt32(MiFila.Cells["CProveedorID"].Value);

                //para no asumir riesgos se reinstancia el usuario local 
                MiProveedorLocal = new Logica.Models.Proveedor();

                //ahora le agregarmos el valor obtenido por la fila al ID del usuario local
                MiProveedorLocal.ProveedorID = IdProveedor;

                //una vez que tengo el objeto local con el valor del id, puedo ir a consultar
                //el usuario por ese id y llenar el resto de atributos. 
                MiProveedorLocal = MiProveedorLocal.ConsultarPorIDRetornaProveedor();

                //validamos que el usuario local tenga datos 

                if (MiProveedorLocal != null && MiProveedorLocal.ProveedorID > 0)
                {
                    //si cargamos correctamente el usuario local llenamos los controles 

                    TxtProveedorID.Text = Convert.ToString(MiProveedorLocal.ProveedorID);

                    TxtProveedorNombre.Text = MiProveedorLocal.ProveedorNombre;

                    TxtProveedorCedula.Text = MiProveedorLocal.ProveedorCedula;

                    TxtProveedorNotas.Text = MiProveedorLocal.ProveedorNotas;

                    TxtProveedorEmail.Text = MiProveedorLocal.ProveedorEmail;

                    TxtProveedorDireccion.Text = MiProveedorLocal.ProveedorDireccion;

                    //combobox 
                    CbTiposProveedor.SelectedValue = MiProveedorLocal.MiRolTipo.ProveedorTipo;

                    ActivarEditarEliminar();

                }



            }
        }

        private void DgLista_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgLista.ClearSelection();
        }
        private void ActivarAgregar()
        {
            BtnAgregar.Enabled = true;
           
        }

        private void ActivarEditarEliminar()
        {
            BtnAgregar.Enabled = false;

        }
        private bool ValidarDatosDigitados(bool OmitirEmail= false)
        {
            //evalúa que se hayan digitado los campos de texto en el formulario 
            bool R = false;

            if (!string.IsNullOrEmpty(TxtProveedorNombre.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProveedorCedula.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProveedorNotas.Text.Trim()) &&
                !string.IsNullOrEmpty(TxtProveedorEmail.Text.Trim()) &&
                CbTiposProveedor.SelectedIndex > -1)
            {

                if (OmitirEmail)
                {
                    //(PARA EDITAR) Si el password se omite entonces ya pasó la evaluacíón a este punto, retorna true
                    R = true;
                }
                else
                {
                    //(PARA AGREGAR) en caso en el que haya que evaluar la contraseña se debe agregar otra condición 
                    //logica
                    if (!string.IsNullOrEmpty(TxtProveedorEmail.Text.Trim()))
                    {
                        R = true;
                    }
                    else
                    {
                        //en caso en el que haga falta la contraseña, se le indica al usuario
                        MessageBox.Show("Debe digitar un email para el proveedor", "Error de validación", MessageBoxButtons.OK);
                        TxtProveedorEmail.Focus();
                        return false;

                    }

                }

            }
            else
            {
                //qué pasa cuando algo falta? 
                if (string.IsNullOrEmpty(TxtProveedorNombre.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un nombre para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtProveedorNombre.Focus();
                    return false;
                }

                //cedula
                if (string.IsNullOrEmpty(TxtProveedorCedula.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una cédula para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtProveedorCedula.Focus();
                    return false;
                }

                //telefono
                if (string.IsNullOrEmpty(TxtProveedorNotas.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar una nota para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtProveedorNotas.Focus();
                    return false;
                }

                //correo
                if (string.IsNullOrEmpty(TxtProveedorEmail.Text.Trim()))
                {
                    MessageBox.Show("Debe digitar un correo para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    TxtProveedorEmail.Focus();
                    return false;
                }


                //rol de usuario
                if (CbTiposProveedor.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar una categoria para el proveedor", "Error de validación", MessageBoxButtons.OK);
                    CbTiposProveedor.Focus();
                    return false;
                }

            }

            return R;
        }







        private void TxtProveedorNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);

        }

        private void TxtProveedorCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresNumeros(e, true);
        }

        private void TxtProveedorNotas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e);
        }

        private void TxtProveedorEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, false, true);
        }

    

        private void TxtProveedorDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Validaciones.CaracteresTexto(e, true);
        }

        private void TxtProveedorEmail_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtProveedorEmail.Text.Trim()))
            {
                if (Validaciones.ValidarEmail(TxtProveedorEmail.Text.Trim()))
                {
                    MessageBox.Show("El formato del correo electronico es incorrecto", "Error de Validacion", MessageBoxButtons.OK);
                    TxtProveedorEmail.Focus();
                }
            }
        }

        private void TxtProveedorEmail_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtProveedorEmail.Text.Trim()))
            {
                TxtProveedorEmail.SelectAll();
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

            DgLista.ClearSelection();

            ActivarAgregar();
        }
        private void LimpiarFormulario()
        {
            TxtProveedorID.Clear();
            TxtProveedorNombre.Clear();
            TxtProveedorCedula.Clear();
            TxtProveedorNotas.Clear();
            TxtProveedorEmail.Clear();

            CbTiposProveedor.SelectedIndex = -1;

            TxtProveedorDireccion.Clear();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarDatosDigitados())
            {

                //estas variables almacenan el resultado de las consultas por correo y cedula

                bool CedulaOK;
                bool EmailOK;

                //pasos 1.1 y 1.2
                MiProveedorLocal = new Logica.Models.Proveedor();

                //llenar los valores de los atributos con los datos digitados en el form
                MiProveedorLocal.ProveedorNombre = TxtProveedorNombre.Text.Trim();
                MiProveedorLocal.ProveedorCedula = TxtProveedorCedula.Text.Trim();
                MiProveedorLocal.ProveedorNotas = TxtProveedorNotas.Text.Trim();
                MiProveedorLocal.ProveedorEmail = TxtProveedorEmail.Text.Trim();
                //composición del rol 
                MiProveedorLocal.MiRolTipo.ProveedorTipo = Convert.ToInt32(CbTiposProveedor.SelectedValue);
                MiProveedorLocal.ProveedorDireccion = TxtProveedorDireccion.Text.Trim();

                //Realizar las consultas por email y por cedula 
                //pasos 1.3 y 1.3.6
                CedulaOK = MiProveedorLocal.ConsultarPorCedula();

                //pasos 1.4 y 1.4.6 
                EmailOK = MiProveedorLocal.ConsultarPorEmail();

                //paso 1.5
                if (CedulaOK == false && EmailOK == false)
                {
                   
                    string msg = string.Format("¿Está seguro que desea agregar al proveedor {0}?", MiProveedorLocal.ProveedorNombre);

                    DialogResult respuesta = MessageBox.Show(msg, "???", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {

                        bool ok = MiProveedorLocal.Agregar();

                        if (ok)
                        {
                            MessageBox.Show("Proveedor guardado correctamente!", ":)", MessageBoxButtons.OK);

                            LimpiarFormulario();

                            CargarListaDeProveedores();

                        }
                        else
                        {
                            MessageBox.Show("El Proveedor no se pudo guardar!", ":/", MessageBoxButtons.OK);
                        }



                    }


                }
                else
                {
    

                    if (CedulaOK)
                    {
                        MessageBox.Show("Ya existe un proveedor con la cédula digitada", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                    if (EmailOK)
                    {
                        MessageBox.Show("Ya existe un proveedor con el correo digitado", "Error de Validación", MessageBoxButtons.OK);
                        return;
                    }

                }


            }
        }

        private void CboxVerActivos_CheckedChanged(object sender, EventArgs e)
        {
            CargarListaDeProveedores();
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarListaDeProveedores();
        }
    }
}

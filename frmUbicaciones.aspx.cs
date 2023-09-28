using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//CAPAS
using BLL;
using DAL;

namespace CrudUbicaciones_SLE
{
    public partial class frmUbicaciones : System.Web.UI.Page
    {
        ubicaciones_BLL oUbicacionesBLL;
        ubicaciones_DAL oUbicacionesDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarUbicaciones();
            }


        }


        //METODO ENCARGADO DE LISTAR LOS DATO DE LA BD EN UN GRIDVIEW
        public void ListarUbicaciones()
        {
            oUbicacionesDAL = new ubicaciones_DAL();
            gvUbicaciones.DataSource = oUbicacionesDAL.Listar();
            gvUbicaciones.DataBind();
        }

        //METODO ENCARGADO DE LOS DATOS DE NUESTRA INTERFAZ
        public ubicaciones_BLL datosUbicacion()
        {
            int ID = 0;
            int.TryParse(txtID.Value, out ID);
            oUbicacionesBLL = new ubicaciones_BLL();
            //RECOLETAR DATOS DE LA CAPA DE PRESENTACION
            oUbicacionesBLL.ID = ID;
            oUbicacionesBLL.Ubicacion = txtUbicacion.Text;
            oUbicacionesBLL.Latitud = txtLat.Text;
            oUbicacionesBLL.Longitud= txtLong.Text;

            return oUbicacionesBLL;
        }

        protected void AgregarRegistro(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicaciones_DAL();
            oUbicacionesDAL.Agregar(datosUbicacion());
            ListarUbicaciones(); //PARA MOSTRARLO EN EL GV
        }

        protected void SeleccionRegistros(object sender, GridViewCommandEventArgs e)
        {
            
            int FilaSeleccionada = int.Parse(e.CommandArgument.ToString());

            txtID.Value = gvUbicaciones.Rows[FilaSeleccionada].Cells[1].Text;
            txtUbicacion.Text = gvUbicaciones.Rows[FilaSeleccionada].Cells[2].Text;
            txtLat.Text = gvUbicaciones.Rows[FilaSeleccionada].Cells[3].Text;
            txtLong.Text = gvUbicaciones.Rows[FilaSeleccionada].Cells[4].Text;

            //AHORA, AL MOMENTO QUE DEMOS CLICK EN "SELECCIONAR" HABILITAREMOS
            //LOS BOTONES ELIMINAR Y MODIFICAR, Y A LA VEZ, INHABILITAREMOS
            //EL BOTON DE AGREGAR 

            btnEliminar.Enabled = true;
            btnModificar.Enabled = true;
            btnAgregar.Enabled = false;


        }

        protected void EliminarRegistro(object sender, EventArgs e)
        {
            oUbicacionesDAL = new ubicaciones_DAL();
            oUbicacionesDAL.Eliminar(datosUbicacion());
            ListarUbicaciones();

        }
    }
}
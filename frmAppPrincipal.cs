using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;





namespace pryInventarioApp
{
    public partial class frmAppPrincipal : Form
    {
        OleDbConnection conexion;

        public frmAppPrincipal()
        {
            // Semana 1 - Conexión y estructura base

            InitializeComponent();
            CargarCategorias();
            btnPrueba.Enabled = false;
            CargarProductos();


        }
        // Semana 2 - ADO.NET con OleDbConnection

        private void ProbarConexion()
        {
            string rutaBD = "BDsem1.mdb"; // ruta del archico en bin/debug
            string cadenaConexion = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBD};";

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
                {
                    conexion.Open();
                    MessageBox.Show("Conexión exitosa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar:\n" + ex.Message);
            }
        }

        private void btnPrueba_Click(object sender, EventArgs e)
        {
            //ProbarConexion(); 
            CargarProductos();
        }
        private void CargarProductos()
        {
            string rutaBD = "BDsem1.mdb"; // ruta del archico en bin/debug
            string cadenaConexion = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBD};";

            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string consulta = @"
                SELECT Productos.Codigo, Productos.Nombre, Productos.Descripcion, Productos.Precio, Productos.Stock, Categorias.Nombre AS Categoria
                FROM Productos
                INNER JOIN Categorias ON Productos.IdCategoria = Categorias.IdCategoria";

                    OleDbCommand comando = new OleDbCommand(consulta, conexion);
                    OleDbDataReader lector = comando.ExecuteReader();

                    DataTable tabla = new DataTable();
                    tabla.Load(lector);

                    dgvProductos.DataSource = tabla;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar productos: " + ex.Message);
                }
            }
        }
        private void CargarCategorias()
        {
            string rutaBD = "BDsem1.mdb"; // ruta del archico en bin/debug
            string cadenaConexion = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBD};";

            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string consulta = "SELECT IdCategoria, Nombre FROM Categorias";

                    OleDbCommand comando = new OleDbCommand(consulta, conexion);
                    OleDbDataReader lector = comando.ExecuteReader();

                    DataTable tabla = new DataTable();
                    tabla.Load(lector);

                    cboCategorias.DataSource = tabla;
                    cboCategorias.DisplayMember = "Nombre";
                    cboCategorias.ValueMember = "IdCategoria";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar categorías: " + ex.Message);
                }
            }
        }
        // Semana 3 - CRUD usando parámetros

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string rutaBD = "BDsem1.mdb"; // ruta del archico en bin/debug
            string cadenaConexion = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBD};";

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
                {
                    conexion.Open();

                    string sql = @"INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, IdCategoria)
                           VALUES (@nombre, @descripcion, @precio, @stock, @idcategoria)";

                    OleDbCommand cmd = new OleDbCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                    cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text));
                    cmd.Parameters.AddWithValue("@idcategoria", cboCategorias.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto agregado correctamente.");

                    CargarProductos(); // Refresca la tabla
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
                txtPrecio.Text = fila.Cells["Precio"].Value.ToString();
                txtStock.Text = fila.Cells["Stock"].Value.ToString();
                cboCategorias.Text = fila.Cells["Categoria"].Value.ToString();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un producto primero.");
                return;
            }

            int codigo = Convert.ToInt32(dgvProductos.CurrentRow.Cells["Codigo"].Value);
            string rutaBD = "BDsem1.mdb"; // ruta del archico en bin/debug
            string cadenaConexion = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBD};";

            try
            {
                using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
                {
                    conexion.Open();

                    string sql = @"UPDATE Productos 
                           SET Nombre = @nombre, Descripcion = @descripcion, Precio = @precio, Stock = @stock, IdCategoria = @idcategoria
                           WHERE Codigo = @codigo";

                    OleDbCommand cmd = new OleDbCommand(sql, conexion);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                    cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text));
                    cmd.Parameters.AddWithValue("@idcategoria", cboCategorias.SelectedValue);
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto modificado.");

                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un producto para eliminar.");
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que querés eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                int codigo = Convert.ToInt32(dgvProductos.CurrentRow.Cells["Codigo"].Value);
                string rutaBD = "BDsem1.mdb"; // ruta del archico en bin/debug
                string cadenaConexion = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBD};";

                try
                {
                    using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
                    {
                        conexion.Open();

                        string sql = "DELETE FROM Productos WHERE Codigo = @codigo";
                        OleDbCommand cmd = new OleDbCommand(sql, conexion);
                        cmd.Parameters.AddWithValue("@codigo", codigo);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto eliminado.");

                        CargarProductos(); // Refresca el DataGridView
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Archivo de texto (*.txt)|*.txt";
            saveFile.Title = "Guardar reporte de inventario";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string rutaBD = "BDsem1.mdb"; // ruta del archico en bin/debug
                string cadenaConexion = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rutaBD};";

                try
                {
                    using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
                    {
                        conexion.Open();

                        string consulta = @"
                    SELECT Productos.Codigo, Productos.Nombre, Productos.Precio, Productos.Stock, Categorias.Nombre AS Categoria
                    FROM Productos
                    INNER JOIN Categorias ON Productos.IdCategoria = Categorias.IdCategoria";

                        OleDbCommand cmd = new OleDbCommand(consulta, conexion);
                        OleDbDataReader reader = cmd.ExecuteReader();

                        using (StreamWriter writer = new StreamWriter(saveFile.FileName))
                        {
                            writer.WriteLine("Reporte del Inventario");
                            writer.WriteLine("----------------------------");
                            writer.WriteLine("Código\tNombre\t\tPrecio\tStock\tCategoría");

                            while (reader.Read())
                            {
                                writer.WriteLine($"{reader["Codigo"]}\t{reader["Nombre"],-20}\t${reader["Precio"],6}\t{reader["Stock"],5}\t{reader["Categoria"]}");
                            }

                            writer.WriteLine();
                            writer.WriteLine("Fecha del reporte: " + DateTime.Now.ToShortDateString());
                        }

                        MessageBox.Show("Reporte generado correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar reporte: " + ex.Message);
                }
            }
        }



        //dgvProductos

    }
}

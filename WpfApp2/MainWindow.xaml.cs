using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace WpfApp2
{
    public partial class MainWindow : Window {
        //Mismos parametros que tenemos en el WorkBench, es decir el server, el nombre del Database y nuestras credenciales
        string connectionString= "Server=localhost;Port=3306;Database=bdventa;Uid=root;Pwd=;";

        public MainWindow() {
            InitializeComponent();
        }

        void Select() {
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query =  @"SELECT id AS ID, nombre AS 'Oficina Sucursal',
                            direccion AS Direccion FROM oficina
                            WHERE estado=1 ORDER BY 2";

            MySqlCommand comando1 = new MySqlCommand(query,connection);

            try {
                connection.Open(); 
                MySqlDataAdapter adadpter = new MySqlDataAdapter(comando1);
                DataTable table = new DataTable(); 
                adadpter.Fill(table);

                lbl_info.Content = table.Rows.Count+" Registros Encontrados";

                dgv_data.ItemsSource = null;       
                dgv_data.ItemsSource = table.DefaultView;  
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
            finally {
                connection.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Select();
        }

        private void btn_insertar_Click(object sender, RoutedEventArgs e)
            {
            string query = @"INSERT INTO oficina(nombre,direccion)
                            VALUES (@nombre,@direccion)";

            MySqlConnection connection=new MySqlConnection(connectionString);
            MySqlCommand comand=new MySqlCommand(query, connection);
            comand.Parameters.AddWithValue("@nombre",txt_nombre.Text);
            comand.Parameters.AddWithValue("@direccion", txt_direccion.Text); //para evitar inyeccion sql
            try {
                connection.Open();
                int n= comand.ExecuteNonQuery();
                if (n > 0) {
                    MessageBox.Show("Datos insertados con extio");
                    Select();
                }
                else {
                    MessageBox.Show("Datos no insertados");
                }
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
            finally {
                connection.Close();
            }
        }

        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            string query =  @"UPDATE oficina SET nombre=@nombre, direccion=@direccion, 
                            fechaActualizacion=CURRENT_TIMESTAMP WHERE id=@id";

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(query,connection);
            command.Parameters.AddWithValue("@nombre",txt_nombre.Text);
            command.Parameters.AddWithValue("@direccion", txt_direccion.Text);
            command.Parameters.AddWithValue("@id", byte.Parse(txt_id.Text)); //de acuerdo a la tabla, atributo
            try {
                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    Select();
                    MessageBox.Show("Registro Modificado");
                }
                else {
                    MessageBox.Show("No se modificaron registros");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                connection.Close();
            }
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            string query =  @"UPDATE oficina SET estado=0, 
                            fechaActualizacion=CURRENT_TIMESTAMP WHERE id=@id;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", txt_nombre.Text);
            command.Parameters.AddWithValue("@direccion", txt_direccion.Text);
            command.Parameters.AddWithValue("@id", byte.Parse(txt_id.Text)); 
            try
            {
                connection.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    Select();
                    MessageBox.Show("Registro Eliminado");
                }
                else
                {
                    MessageBox.Show("No se eliminaron registros");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            string query = @"SELECT id, nombre AS oficina, direccion AS sucursal FROM oficina 
	WHERE id=@ide";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand comando=new MySqlCommand(query, connection);
            comando.Parameters.AddWithValue("@ide", byte.Parse(txt_id.Text));
            try{
                connection.Open();
                MySqlDataAdapter adadpter = new MySqlDataAdapter(comando);
                DataTable table = new DataTable();
                adadpter.Fill(table);

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el ID");
                }
                else
                {
                    DataRow row = table.Rows[0];
                    string oficina = row["oficina"].ToString();
                    string sucursal = row["sucursal"].ToString();
                    MessageBox.Show($"Se encontró el ID. Oficina: {oficina}, Sucursal: {sucursal}");
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
                
            }
            finally { 
                connection.Close(); 
            }
        }

        void plantilla() {
            string query = @"";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand comando=new MySqlCommand(query,connection);

            //comando.Parameters.AddWithValue("",);

            try{
                connection.Open();
                //if () {

                //}
                //else { 
                
                //}
                
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
            finally{ 
                connection.Close() ;
            }
        }

        private void btn_buscar1_Click(object sender, RoutedEventArgs e){
            string query = @"SELECT id, nombre, direccion FROM oficina
                            WHERE id=@id";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand comando=new MySqlCommand(query,connection);
            comando.Parameters.AddWithValue("@id",byte.Parse(txt_id.Text));
            
            try{
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count > 0) {
                    string id = table.Rows[0][0].ToString();
                    string nombre = table.Rows[0][1].ToString();
                    string direccion = table.Rows[0][2].ToString();
                    MessageBox.Show($"ENCONTRADO!\nID: {id}\nNombre: {nombre}\n" +
                        $"DIreccion: {direccion}");
                }
                else {
                    MessageBox.Show("ID no encontrado");
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message); 
            }
            finally {
                connection.Close();
            }
        }

        private void btn_fecha_Click(object sender, RoutedEventArgs e){
            string query = @"SELECT id AS ID, nombre AS 'Oficina Sucursal',
                            direccion AS Direccion FROM oficina
                            WHERE estado=1 AND fechaRegistro BETWEEN '@date1' 
								AND '@date2';";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand comando = new MySqlCommand(query, connection);

            comando.Parameters.AddWithValue("@date1",DateOnly.Parse(dtp_inicio.ToString()));
            comando.Parameters.AddWithValue("@date2", DateTime.Parse(dtp_fin.ToString()));

            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable table=new DataTable();
                adapter.Fill(table);
                dgv_data.ItemsSource = null;
                dgv_data.ItemsSource = table.DefaultView;
                MessageBox.Show($"{DateTime.Parse(dtp_inicio.ToString())}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
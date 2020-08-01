using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WinForms;

namespace ReportePaises
{
    public partial class ReporteAlumnos : Form
    {
        public ReporteAlumnos()
        {
            InitializeComponent();
        }

        private void ReporteAlumnos_Load(object sender, EventArgs e)
        {
            string connstr = "server=localhost; user=root; password=3354; database= afatse; port=3306";
            MySqlConnection conn = new MySqlConnection(connstr);
            try
            {
                conn.Open();
                string sql = "select * from afatse.alumnos;";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<Alumno> listaAlumnos = new List<Alumno>();
                while (rdr.Read())
                {
                    Alumno al = new Alumno();
                    al.DNI = Convert.ToInt32(rdr[0]);
                    al.Nombre = rdr[1].ToString();
                    al.Apellido = rdr[2].ToString();
                    al.Tel = rdr[3].ToString();
                    al.Email =rdr[4].ToString();
                    al.Direccion =rdr[5].ToString();
                    listaAlumnos.Add(al);
                    al = null;
                }
                rdr.Close();
                ReportDataSource rds = new ReportDataSource("ReporteAlumnos", listaAlumnos);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ReportePaises.ReporteAlumnos.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.RefreshReport();
                this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();

        }
    }
}

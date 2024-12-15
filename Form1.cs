using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Configuration;
using System.Text.RegularExpressions;

namespace otdel_kadrov_beta_v2
{
    public partial class Form1 : Form
    {
        private SqlConnection SqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Otdel"].ConnectionString);
            SqlConnection.Open();

        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Сотрудники sotrudnicForm = new Сотрудники(SqlConnection);
            sotrudnicForm.ShowDialog();
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Должности doljnostForm = new Должности(SqlConnection);
            doljnostForm.ShowDialog();
        }

        private void уровниСпециалистовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Уровни lvlForm = new Уровни(SqlConnection);
            lvlForm.ShowDialog();
        }

        private void навыкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Навыки skillForm = new Навыки(SqlConnection);
            skillForm.ShowDialog();
        }

        private void проектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Проекты prodForm = new Проекты(SqlConnection);
            prodForm.ShowDialog();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Отчет report = new Отчет(SqlConnection, toolStripMenuItem4.Text, 2);
            report.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Отчет report = new Отчет(SqlConnection, toolStripMenuItem2.Text, 0);
            report.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            
            Отчет report = new Отчет(SqlConnection,toolStripMenuItem3.Text, 1);
            report.ShowDialog();
        }
    }
}

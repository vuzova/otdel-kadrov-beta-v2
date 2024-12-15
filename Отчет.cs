using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otdel_kadrov_beta_v2
{
    public partial class Отчет : Form
    {
        private SqlConnection sqlConnection;
        private int currentStatus = 0;
        public Отчет(SqlConnection connection, string reportName = "", int status = 0)
        {
            InitializeComponent();
            groupBoxContainer.Text = reportName;
            sqlConnection = connection;


            currentStatus = status;
            settingView();

        }

       
        private void Отчет_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        public void settingView()
        {
            dataGridView.AllowUserToResizeRows = false;

            if (currentStatus == 0)
            {
                this.Size = new Size(385, 430);

                //604; 464
                groupBoxContainer.Size = new Size(350, 370) ;

                groupBox2.Size = new Size(300, 54);
                dataGridView.Size = new Size(300, 54);
                
                dataGridView.Size = new Size(275, 188);
                groupBoxDataGrid.Size = new Size(300, 232);

                //470; 411
                button1.Location = new Point(200, 330);

                groupBoxPeriod.Visible = false;
                groupBoxDataGrid.Location = new Point(20, 90);

                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                LoadLevelReport();
            }
            if (currentStatus == 1)
            {
                LoadExpiredProject();
            }
            if (currentStatus == 2)
            {
                //470; 411
                button1.Location = new Point(470, 330);
                groupBoxContainer.Size = new Size(604, 368);

                //644; 532
                this.Size = new Size(644, 435);

                groupBoxPeriod.Visible = false;
                groupBoxDataGrid.Location = new Point(20, 90);
                LoadSalary();
            }
            //RemoveEmptyRows();
        }

       

        private void LoadSalary()
        {
            string query = @"
            SELECT 
                e.last_name + ' ' + e.first_name + 
                CASE 
                    WHEN e.patronymic IS NOT NULL THEN ' ' + e.patronymic 
                    ELSE '' 
                END AS ФИО,
                p.name AS Должность,
                l.coef AS Коэффициент,
                p.salary AS Оклад,
                (p.salary * l.coef) AS К_выплате
            FROM 
                dbo.Employee e
            JOIN 
                dbo.Position p ON e.position_id = p.Id
            JOIN 
                dbo.Level l ON e.level_id = l.Id;";

        using (SqlCommand command = new SqlCommand(query, sqlConnection))
        {
            // Add parameters for the query
            command.Parameters.AddWithValue("@startDate", dateTimePickerFrom.Value);
            command.Parameters.AddWithValue("@endDate", dateTimePickerTo.Value);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            try
            {
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable; 
                dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
        }
        }

        private void LoadExpiredProject()
        {
            if (dateTimePickerFrom.Value > dateTimePickerTo.Value) 
            {
                var temp = dateTimePickerFrom.Value;
                dateTimePickerFrom.Value = dateTimePickerTo.Value;
                dateTimePickerTo.Value = temp;
            }

            string query = @"
            SELECT 
                p.Name AS Название,
                e.first_name + ' ' + e.last_name AS Ответственный,
                p.date_end_plan AS 'Дата завершения план',
                p.date_end_fact AS 'Дата завершения факт'
            FROM 
                dbo.Project p
            JOIN 
                dbo.Employee_Project ep ON p.Id = ep.project_id
            JOIN 
                dbo.Employee e ON ep.employee_id = e.Id
            WHERE 
                p.date_end_plan < p.date_end_fact
                AND p.date_end_plan BETWEEN @startDate AND @endDate
                AND ep.is_cap = 1;";

            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                // Add parameters for the query
                command.Parameters.AddWithValue("@startDate", dateTimePickerFrom.Value);
                command.Parameters.AddWithValue("@endDate", dateTimePickerTo.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    adapter.Fill(dataTable);
                    dataGridView.DataSource = dataTable;
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        private void LoadLevelReport()
        {

            string query = @"SELECT 
                    l.Name AS Уровень, 
                    COUNT(e.Id) AS 'Кол-во сотрудников' 
                FROM 
                    dbo.Employee e
                JOIN 
                    dbo.Level l ON e.level_id = l.Id
                GROUP BY 
                    l.Name;";

            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    adapter.Fill(dataTable);
                    dataGridView.DataSource = dataTable; 
                    //dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }

            }
        }


        private void LoadComboBox()
        {
            string query = "SELECT id, CONCAT(first_name, ' ', last_name, ' ', patronymic) AS full_name FROM employee";
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                DataTable table = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }

                comboBox.DataSource = table;
                comboBox.DisplayMember = "full_name";
                comboBox.ValueMember = "id";

                comboBox.SelectedIndex = -1;
            }
        }

        private string getEmployeePosition(int employeeId) 
        {
            string position = "";

            string query = @"SELECT p.name AS Должность
            FROM dbo.Position p
            JOIN dbo.Employee e ON e.position_id = p.Id
            WHERE e.Id = @employeeId;";

            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@employeeId", employeeId.ToString());
                position = (string)command.ExecuteScalar();
            } 

            return position;
        }

        private string getEmployeeName(int employeeId)
        {
            string name = "";

            string query = @"SELECT 
            e.last_name + ' ' + 
            LEFT(e.first_name, 1) + '.' + 
            CASE 
                WHEN e.patronymic IS NOT NULL THEN ' ' + LEFT(e.patronymic, 1) + '.' 
                ELSE '' 
            END AS ФИО
            FROM dbo.Employee e
            WHERE e.Id = @employeeId;";

            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@employeeId", employeeId.ToString());
                name = (string)command.ExecuteScalar();
            }

            return name;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadExpiredProject();
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            LoadExpiredProject();
        }

        private bool validate() 
        {
            if (comboBox.SelectedIndex == -1) 
            {
                MessageBox.Show("Выберите отвественного за отчет, пожалуйста.");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validate()) return;

            var position = getEmployeePosition((int)comboBox.SelectedValue);
            var name = getEmployeeName((int)comboBox.SelectedValue);

            if (currentStatus == 0 || currentStatus == 2)
            { 
                ReportGenerator report = new ReportGenerator(groupBoxContainer.Text, 
                                                            dataGridView, 
                                                            DateTime.Now, 
                                                            position, 
                                                            name);
                report.CreateReport();
            }

            if (currentStatus == 1)
            {
                var d1 = dateTimePickerFrom.Value.ToString("dd.MM.yyyy");
                var d2 = dateTimePickerTo.Value.ToString("dd.MM.yyyy");

                ReportGenerator report = new ReportGenerator(groupBoxContainer.Text,
                                                            dataGridView, 
                                                            DateTime.Now, 
                                                            position, 
                                                            name, 
                                                            d1, 
                                                            d2 );
                report.CreateReport();
            }

        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "Дата завершения план" ||
        dataGridView.Columns[e.ColumnIndex].Name == "Дата завершения факт")
            {
                if (e.Value != null)
                {
                    // Преобразуем значение в DateTime и форматируем его
                    DateTime dateValue;
                    if (DateTime.TryParse(e.Value.ToString(), out dateValue))
                    {
                        e.Value = dateValue.ToString("dd.MM.yyyy"); // Форматируем дату
                        e.FormattingApplied = true; // Указываем, что форматирование применено
                    }
                }
            }
        }
    }
}

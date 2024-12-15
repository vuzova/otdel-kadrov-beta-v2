using DocumentFormat.OpenXml.Office.SpreadSheetML.Y2023.MsForms;
using DocumentFormat.OpenXml.Office2010.Excel;
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
using static System.Net.WebRequestMethods;

namespace otdel_kadrov_beta_v2
{
    public partial class Проекты : Form
    {
        private SqlConnection sqlConnection;
        private Status currentStatus;

        private enum Status
        {
            View,
            Edit,
            Add,
            More

        }
        public Проекты(SqlConnection connection, bool openedFromForm = false)
        {
            InitializeComponent();
            sqlConnection = connection;
            currentStatus = Status.View;
        }
        private void Проекты_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();

            date_end_fact.KeyDown += new KeyEventHandler(dateTimePicker_KeyDown);
            date_end_plan.KeyDown += new KeyEventHandler(dateTimePicker_KeyDown);
            date_start_fact.KeyDown += new KeyEventHandler(dateTimePicker_KeyDown);
            date_start_plan.KeyDown += new KeyEventHandler(dateTimePicker_KeyDown);

            date_end_fact.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);
            date_end_plan.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);
            date_start_fact.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);
            date_start_plan.ValueChanged += new EventHandler(dateTimePicker_ValueChanged);


            LoadForm();
        }

        private void dateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, нажата ли клавиша Backspace или Delete
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                // Устанавливаем значение в минимальную дату или в пустое значение
                DateTimePicker picker = sender as DateTimePicker;
                if (picker != null)
                {
                    picker.CustomFormat = " ";
                    picker.Format = DateTimePickerFormat.Custom;
                }
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker picker = sender as DateTimePicker;
            if (picker != null)
            {
                // Устанавливаем формат даты
                picker.CustomFormat = "dd/MM/yyyy";
                picker.Format = DateTimePickerFormat.Long; // Используем Custom для отображения формата
            }
        }

        private void LoadForm()
        {
            try
            {
                string query = "SELECT id, name as Название, " +
                    "description as Описание, date_created as Создано," +
                    " date_start_plan as Дата_начала_план, date_start_fact as Дата_начала_факт, " +
                    "date_end_plan as Дата_конец_план, date_end_fact as Дата_конец_факт " +
                    "FROM Project";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                dataGrid.DataSource = ds;

                //Визуал
                dataGrid.Columns["Id"].Visible = false;
                dataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void visibleAddForm(string groupName = "Добавить проект")
        {
            groupBoxAddSpravka.Text = groupName;

            comboBox.Enabled = true;
            buttonOpenEmployeeForm.Visible = true; //...

            buttonOpenEmployee.Visible = true; //+
            buttonDeleteEmployee.Visible = true; //-

            buttonAddOk.Visible = true; //ok
            buttonAddClose.Visible = true; //close

            groupBoxAddSpravka.Visible = true;
            groupBoxAction.Enabled = false;
            groupBoxSpravka.Visible = false;
        }

        private void closeVisibleAddForm()
        {
            currentStatus = Status.View;

            comboBox.Enabled = false;
            buttonOpenEmployeeForm.Visible = false; //...

            buttonOpenEmployee.Visible = false; //+
            buttonDeleteEmployee.Visible = false; //-

            buttonAddOk.Visible = false; //ok
            buttonAddClose.Visible = false; //close

            groupBoxAddSpravka.Visible = false;
            groupBoxAction.Enabled = true;
            groupBoxSpravka.Visible = true;
        }


        private int GetEmployeeIdByProjectId(int projectId)
        {
            int employeeId = -1; // Значение по умолчанию, если не найдено

            string query = "SELECT employee_id FROM employee_project WHERE project_id = @projectId";
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@projectId", projectId);
                object result = command.ExecuteScalar(); // Выполняем запрос и получаем одно значение

                if (result != null)
                {
                    employeeId = Convert.ToInt32(result); // Преобразуем результат в int
                }
            }

            return employeeId; // Возвращаем найденный employee_id или -1, если не найден
        }

        private int GetEmployeNameByProjectId(int projectId, int is_cap = 1)
        {
            int employeeId = -1; // Значение по умолчанию, если не найдено

            string query = "SELECT employee_id FROM employee_project WHERE project_id = @projectId AND is_cap = @is_cap";
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@projectId", projectId);
                command.Parameters.AddWithValue("@is_cap", is_cap);

                object result = command.ExecuteScalar(); // Выполняем запрос и получаем одно значение

                if (result != null)
                {
                    employeeId = Convert.ToInt32(result); // Преобразуем результат в int
                }
            }

            return employeeId; // Возвращаем найденный employee_id или -1, если не найден
        }

        private void LoadComboBox(ComboBox comboBox, int index = -1)
        {
            string query = "SELECT id, CONCAT(first_name, ' ', last_name) AS full_name FROM employee";
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

                if (index != -1)
                {
                    var employeeId = GetEmployeeIdByProjectId(index); // Получаем employee_id по project_id
                                                                      // Ищем индекс по ValueMember
                    foreach (DataRowView row in comboBox.Items)
                    {
                        if (Convert.ToInt32(row[comboBox.ValueMember]) == employeeId)
                        {
                            comboBox.SelectedItem = row; // Устанавливаем выбранный элемент
                            break; // Выходим из цикла, если нашли
                        }
                    }
                }
                else 
                {
                    comboBox.SelectedIndex = -1;
                }
            }

        }

        private void updateEnableBox() 
        {
            name.Enabled = true;
            description.Enabled = true;

            date_end_fact.Enabled = true;
            date_end_plan.Enabled = true;

            date_start_plan.Enabled = true;
            date_start_fact.Enabled = true;
        }

        private void buttonAddForm_Click(object sender, EventArgs e)
        {
            updateEnableBox();

            dataGridEmployee.Rows.Clear(); 
            visibleAddForm();
            clearAll();
            LoadComboBox(comboBox);
            currentStatus = Status.Add;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentStatus == Status.More) 
            {
                closeVisibleAddForm();

                name.Enabled = true;
                description.Enabled = true;

                date_end_fact.Enabled = true;
                date_end_plan.Enabled = true;

                date_start_plan.Enabled = true;
                date_start_fact.Enabled = true;

                currentStatus = Status.View;
            }

            if ((!string.IsNullOrWhiteSpace(name.Text.Trim()) ||
              !string.IsNullOrWhiteSpace(description.Text.Trim()) ||
              date_start_plan.Text.Trim() != "" ||
              date_start_fact.Text.Trim() != "" ||
              date_end_plan.Text.Trim() != "" ||
              date_end_fact.Text.Trim() != "" 
              )
              && currentStatus == Status.Add)
            {

                //////////
                DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти? Введенные данные не сохранятся.", "Подтверждение", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    dataGridEmployee.Rows.Clear();
                    closeVisibleAddForm();
                    clearAll();
                    currentStatus = Status.View;
                }

                else
                {
                    return;
                }
            }


            dataGridEmployee.Rows.Clear();
            closeVisibleAddForm();
            clearAll();
            currentStatus = Status.View;
        }

        private bool validate()
        {

            int responsibleEmployeeId = comboBox.SelectedValue != null ? (int)comboBox.SelectedValue : -1;

            bool isResponsibleInTeam = false;

            foreach (DataGridViewRow row in dataGridEmployee.Rows)
            {
                if (row.Cells["Id"].Value != null &&
                    (int)row.Cells["Id"].Value == responsibleEmployeeId)
                {
                    isResponsibleInTeam = true;
                    break;
                }
            }

            // Если ответственный уже в команде, выводим сообщение
            if (isResponsibleInTeam)
            {
                MessageBox.Show("Уберите ответственного из состава команды.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(name.Text) || comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return false;
            }


            if (!string.IsNullOrWhiteSpace(date_start_plan.Text) && !string.IsNullOrWhiteSpace(date_end_plan.Text))
            {
                if (date_end_plan.Value < date_start_plan.Value)
                {
                    MessageBox.Show("Дата окончания плана не должна быть меньше даты начала плана.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(date_start_fact.Text) && !string.IsNullOrWhiteSpace(date_end_fact.Text))
            {
                if (date_end_fact.Value < date_start_fact.Value)
                {
                    MessageBox.Show("Дата окончания факта не должна быть меньше даты начала факта.");
                    return false;
                }
            }

            // Дополнительные проверки между датами начала и окончания
            if (!string.IsNullOrWhiteSpace(date_start_fact.Text) && !string.IsNullOrWhiteSpace(date_end_plan.Text))
            {
                if (date_end_plan.Value < date_start_fact.Value)
                {
                    MessageBox.Show("Дата окончания плана не должна быть меньше даты начала факта.");
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(date_start_plan.Text) && !string.IsNullOrWhiteSpace(date_end_fact.Text))
            {
                if (date_end_fact.Value < date_start_plan.Value)
                {
                    MessageBox.Show("Дата окончания факта не должна быть меньше даты начала плана.");
                    return false;
                }
            }


            return true;
        }

        private void buttonAddOk_Click(object sender, EventArgs e)
        {

            if (!validate()) return;

            if (currentStatus == Status.Add)
            {
                try
                {
                    string query = @"
                        INSERT INTO Project (name, description, date_created, date_start_plan, date_start_fact, date_end_plan, date_end_fact) 
                        VALUES (@name, @description, @date_created, @date_start_plan, @date_start_fact, @date_end_plan, @date_end_fact);
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@name", name.Text.Trim());
                        command.Parameters.AddWithValue("@description", description.Text.Trim());

                        command.Parameters.AddWithValue("@date_created", DateTime.Now);

                        // Проверяем каждое поле и устанавливаем значение в DBNull, если оно не заполнено
                        command.Parameters.AddWithValue("@date_start_plan",
                            date_start_plan.Text.Trim() == "" ? (object)DBNull.Value : date_start_plan.Value);
                        command.Parameters.AddWithValue("@date_start_fact",
                            date_start_fact.Text.Trim() == "" ? (object)DBNull.Value : date_start_fact.Value);
                        command.Parameters.AddWithValue("@date_end_plan",
                            date_end_plan.Text.Trim() == "" ? (object)DBNull.Value : date_end_plan.Value);
                        command.Parameters.AddWithValue("@date_end_fact",
                            date_end_fact.Text.Trim() == "" ? (object)DBNull.Value : date_end_fact.Value);

                        int project_id = Convert.ToInt32(command.ExecuteScalar());

                        string queryCap = "INSERT INTO Employee_Project (employee_id, project_id, is_cap) " +
                       "VALUES (@employee_id, @project_id, @is_cap)";
                        int? employee_id = comboBox.SelectedValue as int?;

                        using (SqlCommand commandEmployee = new SqlCommand(queryCap, sqlConnection))
                        {
                            commandEmployee.Parameters.AddWithValue("@employee_id", employee_id);
                            commandEmployee.Parameters.AddWithValue("@project_id", project_id);
                            commandEmployee.Parameters.AddWithValue("@is_cap", 1);
                            commandEmployee.ExecuteNonQuery();
                        }

                        foreach (DataGridViewRow selectedRow in dataGridEmployee.Rows)
                        {
                            // Получаем ID выбранного навыка (предполагается, что у вас есть столбец ID)
                            int employeeId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            string queryCommand = "INSERT INTO Employee_Project (employee_id, project_id, is_cap) " +
                                    "VALUES (@employee_id, @project_id, @is_cap)";

                            using (SqlCommand commandSkill = new SqlCommand(queryCommand, sqlConnection))
                            {
                                commandSkill.Parameters.AddWithValue("@employee_id", employeeId);
                                commandSkill.Parameters.AddWithValue("@project_id", project_id);
                                commandSkill.Parameters.AddWithValue("@is_cap", 0);
                                commandSkill.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Проект добавлен успешно.");
                    LoadForm();
                    clearAll();

                    closeVisibleAddForm();

                    dataGrid.ClearSelection();
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Selected = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении проекта: " + ex.Message);
                }
            }

            if (currentStatus == Status.Edit)
            {
                try
                {
                    int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                    var id = Convert.ToInt32(dataGrid.Rows[selectedRowIndex].Cells["Id"].Value);
                    var date_created = dataGrid.Rows[selectedRowIndex].Cells["Создано"].Value;

                    string query = @"
                        UPDATE project 
                        SET name = @name, 
                            description = @description, 
                            date_created = @date_created, 
                            date_start_plan = @date_start_plan, 
                            date_start_fact = @date_start_fact, 
                            date_end_plan = @date_end_plan, 
                            date_end_fact = @date_end_fact 
                        WHERE id = @id;";

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        command.Parameters.AddWithValue("@name", name.Text.Trim());
                        command.Parameters.AddWithValue("@description", description.Text.Trim());

                        command.Parameters.AddWithValue("@date_created", date_created);

                        command.Parameters.AddWithValue("@date_start_plan",
                            date_start_plan.Text.Trim() == "" ? (object)DBNull.Value : date_start_plan.Value);
                        command.Parameters.AddWithValue("@date_start_fact",
                            date_start_fact.Text.Trim() == "" ? (object)DBNull.Value : date_start_fact.Value);
                        command.Parameters.AddWithValue("@date_end_plan",
                            date_end_plan.Text.Trim() == "" ? (object)DBNull.Value : date_end_plan.Value);
                        command.Parameters.AddWithValue("@date_end_fact",
                            date_end_fact.Text.Trim() == "" ? (object)DBNull.Value : date_end_fact.Value);

                        command.ExecuteNonQuery();

                        string queryCap = @"DELETE FROM employee_project 
                            WHERE employee_id = @employee_id 
                            AND project_id = @project_id 
                            AND is_cap = 1;";

                        //старое значение
                        int? employee_id_new = comboBox.SelectedValue as int?;

                        using (SqlCommand commandEmployee = new SqlCommand(queryCap, sqlConnection))
                        {
                            commandEmployee.Parameters.AddWithValue("@employee_id", GetEmployeNameByProjectId(id));
                            commandEmployee.Parameters.AddWithValue("@project_id", id);
                            //commandEmployee.Parameters.AddWithValue("@is_cap", 1);
                            commandEmployee.ExecuteNonQuery();
                        }

                        string queryCapAdd = "INSERT INTO Employee_Project (employee_id, project_id, is_cap) " +
                       "VALUES (@employee_id, @project_id, @is_cap)";

                        using (SqlCommand commandEmployee = new SqlCommand(queryCapAdd, sqlConnection))
                        {
                            commandEmployee.Parameters.AddWithValue("@employee_id", employee_id_new);
                            commandEmployee.Parameters.AddWithValue("@project_id", id);
                            commandEmployee.Parameters.AddWithValue("@is_cap", 1);
                            commandEmployee.ExecuteNonQuery();
                        }

                        delCap(id.ToString(), "0");

                        foreach (DataGridViewRow selectedRow in dataGridEmployee.Rows)
                        {
                            int employeeId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            string queryCommand = "INSERT INTO Employee_Project (employee_id, project_id, is_cap) " +
                                    "VALUES (@employee_id, @project_id, @is_cap)";

                            using (SqlCommand commandSkill = new SqlCommand(queryCommand, sqlConnection))
                            {
                                commandSkill.Parameters.AddWithValue("@employee_id", employeeId);
                                commandSkill.Parameters.AddWithValue("@project_id", id);
                                commandSkill.Parameters.AddWithValue("@is_cap", 0);
                                commandSkill.ExecuteNonQuery();
                            }
                        }
                    }


                    MessageBox.Show("Проект обновлен успешно.");
                    clearAll();
                    LoadForm();

                    closeVisibleAddForm();
      
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении проекта: " + ex.Message);
                }
            }
        }


        private void delCap(string project_id, string is_cap)
        {
            string query = "DELETE FROM employee_project WHERE project_id = @project_id AND is_cap = @is_cap";

            try
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@project_id", project_id);
                    command.Parameters.AddWithValue("@is_cap", is_cap);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при удалении: " + ex.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранные строки?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Проходим по всем выделенным строкам
                        foreach (DataGridViewRow selectedRow in dataGrid.SelectedRows)
                        {
                            // Получаем ID выбранного навыка (предполагается, что у вас есть столбец ID)
                            int positionId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            // Выполняем SQL-запрос на удаление
                            string query = "DELETE FROM Project WHERE Id = @ID";
                            using (SqlCommand command = new SqlCommand(query, sqlConnection))
                            {
                                command.Parameters.AddWithValue("@ID", positionId);
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Выбранные проекты удалены успешно.");
                        LoadForm();
                        clearAll();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении должности: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строки для удаления.");
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView(textBoxSearch.Text);
        }

        private void FilterDataGridView(string searchText)
        {
            try
            {
                string query = "SELECT id, name as Название, " +
                    "description as Описание, date_created as Создано," +
                    " date_start_plan as Дата_начала_план, date_start_fact as Дата_начала_факт, " +
                    "date_end_plan as Дата_конец_план, date_end_fact as Дата_конец_факт " +
                    "FROM Project WHERE name LIKE @SearchTerm";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
                string searchTerm = searchText + "%";
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                dataGrid.DataSource = ds;

                // Визуал
                dataGrid.Columns["Id"].Visible = false;
                dataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void settingDateForEdit(int selectedRowIndex, string cells, DateTimePicker dtp) 
        {
            if (dataGrid.Rows[selectedRowIndex].Cells[cells].Value != DBNull.Value)
            {
                dtp.Value = (DateTime)dataGrid.Rows[selectedRowIndex].Cells[cells].Value;
            }
            else
            {
                dtp.CustomFormat = " "; // Устанавливаем формат для отображения пустоты
                dtp.Format = DateTimePickerFormat.Custom; // Устанавливаем формат как пользовательский
            }
        }

        private void buttonEditForm_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count == 1)
            {
                currentStatus = Status.Edit;
                updateEnableBox();

                visibleAddForm("Редактировать проект");

                int selectedRowIndex = dataGrid.SelectedRows[0].Index;

                name.Text = dataGrid.Rows[selectedRowIndex].Cells["Название"].Value.ToString();
                description.Text = dataGrid.Rows[selectedRowIndex].Cells["Описание"].Value.ToString();

                settingDateForEdit(selectedRowIndex, "Дата_начала_план", date_start_plan);
                settingDateForEdit(selectedRowIndex, "Дата_начала_факт", date_start_fact);
                settingDateForEdit(selectedRowIndex, "Дата_конец_план", date_end_plan);
                settingDateForEdit(selectedRowIndex, "Дата_конец_факт", date_end_fact);

                var selectId = Convert.ToInt32(dataGrid.Rows[selectedRowIndex].Cells["ID"].Value);

                LoadComboBox(comboBox, selectId);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите проект для редактирования.");
            }
        }

        private bool capCap(int id) 
        {
            
            foreach (DataGridViewRow row in dataGridEmployee.Rows)
            {
                // Проверяем, совпадают ли ID и название навыка
                if (Convert.ToInt32(row.Cells[0].Value) == id)
                {
                    MessageBox.Show("Ответственный уже в команде");
                    return false;
                }
            }
            return true;
        }

        private void buttonOpenEmployeeForm_Click(object sender, EventArgs e)
        {
            Сотрудники employeeForm = new Сотрудники(sqlConnection, true);

            employeeForm.EmployeeSelected += (id) =>
            {
                LoadComboBox(comboBox);
                if (capCap(id)) 
                {
                    comboBox.SelectedValue = id;
                }
                //comboBox.SelectedValue = id;

            };

            employeeForm.EmployeeUpdate += () =>
            {
                var index = comboBox.SelectedIndex;
                LoadComboBox(comboBox);
                comboBox.SelectedIndex = index;
            };

            employeeForm.ShowDialog();
        }

        private void clearAll() 
        {
            name.Clear();
            description.Clear();
            
            comboBox.SelectedIndex = -1;

            dataGrid.ClearSelection();

            date_start_plan.CustomFormat = " ";
            date_start_fact.CustomFormat = " "; 

            date_end_fact.CustomFormat = " ";
            date_end_plan.CustomFormat = " ";
            

            date_start_fact.Format = DateTimePickerFormat.Custom;
            date_start_plan.Format = DateTimePickerFormat.Custom;
            date_end_fact.Format = DateTimePickerFormat.Custom;
            date_end_plan.Format = DateTimePickerFormat.Custom;

         }

        private void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int selectProjectId = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);

                LoadSkillForEmployee(selectProjectId);
                LoadCapProject(selectProjectId);
            }
            else
            {
                dataGridEmployee.DataSource = null;
            }

            //combobox
        }

     
        private void LoadSkillForEmployee(int selectProjectId)
        {
            string query = @"
                SELECT 
                    e.id AS Id, -- Добавляем ID в выборку
                    e.last_name AS Фамилия,
                    e.first_name AS Имя,
                    p.name AS Должность,
                    l.name AS Уровень
                FROM 
                    Employee e
                JOIN 
                    Position p ON e.position_id = p.id
                JOIN 
                    Level l ON e.level_id = l.id
                LEFT JOIN 
                    Employee_Project ep ON e.id = ep.employee_id
                WHERE 
                    ep.project_id = @ID AND (ep.is_cap = 0 OR ep.is_cap IS NULL)";

            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@ID", selectProjectId);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);

                // Очищаем существующие строки, но не очищаем заголовки
                dataGridEmployee.Rows.Clear();

                // Добавляем новые строки из DataTable
                foreach (DataRow row in ds.Rows)
                {
                    var newRow = new DataGridViewRow();
                    newRow.CreateCells(dataGridEmployee);
                    newRow.Cells[0].Value = row["Id"]; // Заполняем скрытый столбец ID
                    newRow.Cells[1].Value = row["Фамилия"].ToString();
                    newRow.Cells[2].Value = row["Имя"].ToString();
                    newRow.Cells[3].Value = row["Должность"].ToString();
                    newRow.Cells[4].Value = row["Уровень"].ToString();
                    dataGridEmployee.Rows.Add(newRow);
                }
            }
        }
        private void LoadCapProject(int selectProjectId)
        {
            LoadComboBox(comboBox, selectProjectId);

        }

        private void buttonOpenEmployee_Click(object sender, EventArgs e)
        {
            Сотрудники employeeForm = new Сотрудники(sqlConnection, true);

            employeeForm.EmployeeSelected += (id) =>
            {

                foreach (DataGridViewRow row in dataGridEmployee.Rows)
                {
                    // Проверяем, совпадают ли ID и название навыка
                    if (Convert.ToInt32(row.Cells[0].Value) == id)
                    {
                        MessageBox.Show("Сотрудник уже есть в команде");
                        return;
                    }
                }

                if (comboBox.SelectedValue != null && id.ToString() == comboBox.SelectedValue.ToString())
                {
                    MessageBox.Show("Ответственный уже в составе команды");
                    return;
                };

                LoadEmployeeDG(id);
            };

            employeeForm.EmployeeDelete += (id) =>
            {
                for (int i = 0; i < dataGridEmployee.Rows.Count; i++)
                {
                    if (dataGridEmployee.Rows[i].Cells["Id"].Value != null &&
                        (int)dataGridEmployee.Rows[i].Cells["Id"].Value == id)
                    {
                        dataGridEmployee.Rows.RemoveAt(i);
                    }
                }

            };

            employeeForm.ShowDialog();
        }

        private void InitializeDataGridView()
        {
            dataGridEmployee.Columns.Clear(); // Очищаем существующие столбцы

            // Добавляем скрытый столбец для ID
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "Id"; // Уникальное имя столбца
            idColumn.HeaderText = "Id"; // Заголовок столбца
            idColumn.Visible = false; // Скрываем столбец
            dataGridEmployee.Columns.Add(idColumn);

            // Добавляем остальные видимые столбцы
            dataGridEmployee.Columns.Add("Фамилия", "Фамилия");
            dataGridEmployee.Columns.Add("Имя", "Имя");
            dataGridEmployee.Columns.Add("Должность", "Должность");
            dataGridEmployee.Columns.Add("Уровень", "Уровень");

            dataGridEmployee.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridEmployee.AllowUserToResizeRows = false;
            dataGrid.AllowUserToResizeRows = false;
        }

        private void LoadEmployeeDG(int employeeId)
        {
            //if (currentStatus == Status.Add)
            //{
                string query = @"
                    SELECT DISTINCT
                        e.id AS Id, -- Добавляем ID в выборку
                        e.last_name AS Фамилия,
                        e.first_name AS Имя,
                        p.name AS Должность,
                        l.name AS Уровень
                    FROM 
                        Employee e
                    JOIN 
                        Position p ON e.position_id = p.id
                    JOIN 
                        Level l ON e.level_id = l.id
                    LEFT JOIN 
                        Employee_Project ep ON e.id = ep.employee_id
                    WHERE 
                        e.id = @id";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", employeeId); // Исправлено на @id

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable ds = new DataTable();

                    dataAdapter.Fill(ds);

                    // Проверяем, есть ли данные
                    if (ds.Rows.Count > 0)
                    {
                        // Добавляем новые строки в DataGridView
                        foreach (DataRow row in ds.Rows)
                        {
                            var newRow = new DataGridViewRow();
                            newRow.CreateCells(dataGridEmployee);

                            // Убедитесь, что количество ячеек соответствует количеству столбцов
                            if (dataGridEmployee.Columns.Count >= 4)
                            {
                                newRow.Cells[0].Value = row["Id"]; 
                                newRow.Cells[1].Value = row["Фамилия"].ToString();
                                newRow.Cells[2].Value = row["Имя"].ToString();
                                newRow.Cells[3].Value = row["Должность"].ToString();
                                newRow.Cells[4].Value = row["Уровень"].ToString();
                                dataGridEmployee.Rows.Add(newRow);
                            }
                            else
                            {
                                MessageBox.Show("Недостаточно столбцов в DataGridView.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не найден.");
                    }
                }
            //}
        }

        private void buttonDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (dataGridEmployee.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridEmployee.SelectedRows)
                {
                    dataGridEmployee.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void buttonMore_Click(object sender, EventArgs e)
        {
            

            if (dataGrid.SelectedRows.Count == 1)
            {

                currentStatus = Status.Edit;

                visibleAddForm("Подробнее");

                buttonOpenEmployeeForm.Visible = false;
                buttonOpenEmployee.Visible = false;
                buttonDeleteEmployee.Visible = false;

                comboBox.Enabled = false;

                buttonAddOk.Visible = false;

                name.Enabled = false;
                description.Enabled = false;

                date_end_fact.Enabled = false;
                date_end_plan.Enabled = false;

                date_start_plan.Enabled = false;
                date_start_fact.Enabled = false;

                              
                int selectedRowIndex = dataGrid.SelectedRows[0].Index;

                name.Text = dataGrid.Rows[selectedRowIndex].Cells["Название"].Value.ToString();
                description.Text = dataGrid.Rows[selectedRowIndex].Cells["Описание"].Value.ToString();

                settingDateForEdit(selectedRowIndex, "Дата_начала_план", date_start_plan);
                settingDateForEdit(selectedRowIndex, "Дата_начала_факт", date_start_fact);
                settingDateForEdit(selectedRowIndex, "Дата_конец_план", date_end_plan);
                settingDateForEdit(selectedRowIndex, "Дата_конец_факт", date_end_fact);

                var selectId = Convert.ToInt32(dataGrid.Rows[selectedRowIndex].Cells["ID"].Value);

                LoadComboBox(comboBox, selectId);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите проект для подробного описания.");
                return;
            }
        }
    }
}

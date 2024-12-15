using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otdel_kadrov_beta_v2
{
    public partial class Уровни : Form
    {
        private SqlConnection sqlConnection;
        private bool isOpenedFromForm;

        private Status currentStatus;

        public event Action<int> LevelSelected;
        public event Action LevelUpdate;
        public Уровни(SqlConnection connection, bool openedFromForm = false)
        {
            InitializeComponent();
            sqlConnection = connection;
            isOpenedFromForm = openedFromForm;
            currentStatus = Status.View;
        }

        private void Уровни_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadForm();
        }

        private void InitializeDataGridView()
        {

            dataGrid.Columns.Clear(); // Очищаем существующие столбцы

            // Добавляем скрытый столбец для ID
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "Id"; // Уникальное имя столбца
            idColumn.HeaderText = "Id"; // Заголовок столбца
            idColumn.Visible = false; // Скрываем столбец
            dataGrid.Columns.Add(idColumn);

            // Добавляем остальные видимые столбцы
            dataGrid.Columns.Add("name", "Название");
            dataGrid.Columns.Add("coef", "Коэффициент");

            dataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGrid.AllowUserToResizeRows = false;
        }

        private enum Status
        {
            View,
            Edit,
            Add,
        }

        private void LoadForm()
        {
            try
            {
                dataGrid.Rows.Clear();

                string query = @"
                SELECT 
                    s.Id AS Id,
                    s.Name AS Название, 
                    s.Coef AS Коэффициент 
                FROM 
                    Level s"; // Измените на вашу таблицу и поля

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    // Открываем соединение, если оно еще не открыто
                    if (sqlConnection.State != ConnectionState.Open)
                    {
                        sqlConnection.Open();
                    }

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
                            newRow.CreateCells(dataGrid);

                            // Убедитесь, что количество ячеек соответствует количеству столбцов
                            if (dataGrid.Columns.Count >= 3)
                            {
                                newRow.Cells[0].Value = row["Id"];
                                newRow.Cells[1].Value = row["Название"].ToString();
                                newRow.Cells[2].Value = row["Коэффициент"].ToString();
                                dataGrid.Rows.Add(newRow);
                            }
                            else
                            {
                                MessageBox.Show("Недостаточно столбцов в DataGridView.");
                            }
                        }
                    }
                    else
                    {
                        dataGrid.Rows.Clear();
                        MessageBox.Show("Нет данных для отображения.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void visibleAddForm(string groupName = "Добавить уровень")
        {
            groupBoxAddSpravka.Text = groupName;

            groupBoxAddSpravka.Visible = true;
            groupBoxAction.Enabled = false;
            groupBoxSpravka.Visible = false;
        }

        private void closeVisibleAddForm()
        {
            textBoxAddCoef.Clear();
            textBoxAddName.Clear();

            groupBoxAddSpravka.Visible = false;
            groupBoxAction.Enabled = true;
            groupBoxSpravka.Visible = true;
        }

        

        private void buttonAddForm_Click(object sender, EventArgs e)
        {
            currentStatus = Status.Add;
            visibleAddForm();
        }

        private void buttonEditClose_Click(object sender, EventArgs e)
        {
            closeVisibleAddForm();
        }

        private void buttonEditForm_Click(object sender, EventArgs e)
        {
           

            if (dataGrid.SelectedRows.Count == 1)
            {
                visibleAddForm("Редактировать уровень");
                currentStatus = Status.Edit;

                int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                string positionName = dataGrid.Rows[selectedRowIndex].Cells["name"].Value.ToString();
                string positionSalary = dataGrid.Rows[selectedRowIndex].Cells["coef"].Value.ToString();

                textBoxAddName.Text = positionName;
                textBoxAddCoef.Text = positionSalary;
                //var selectedSkillId = Convert.ToInt32(dataGridSkill.Rows[selectedRowIndex].Cells["ID"].Value);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите навык для редактирования.");
            }
        }

        private void buttonAddClose_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(textBoxAddName.Text.Trim()) ||
               !string.IsNullOrWhiteSpace(textBoxAddCoef.Text.Trim()))
               && currentStatus == Status.Add)
            {

                //////////
                DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти? Введенные данные не сохранятся.", "Подтверждение", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    closeVisibleAddForm();
                }

                else
                {
                    return;
                }
            }

            closeVisibleAddForm();
        }

        private bool validate() 
        {
            string newLevel = textBoxAddName.Text;
            string newCoefText = textBoxAddCoef.Text;

            if (string.IsNullOrWhiteSpace(newLevel) || string.IsNullOrWhiteSpace(newCoefText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return false;
            }

            if (!decimal.TryParse(newCoefText, out decimal newCoef) || newCoef < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное значение.");
                return false;
            }

            if (!isDecimalMask(newCoefText)) 
            {
                MessageBox.Show("Коэффициент не должен превышать 10. Пожалуйста, введите корректное значение коэффициента х,ххх ");
                return false;
            }

           

            return true;
        }

        private bool isDecimalMask(string value)
        {
            string inputValue = value;
            if (Regex.IsMatch(inputValue, @"^\d(\,\d{1,3})?$"))
            {
                return true;
            }

            return false;
        }

            private void buttonAddOk_Click(object sender, EventArgs e)
        {
            string newLevel = textBoxAddName.Text;
            decimal newCoefText = Convert.ToDecimal(textBoxAddCoef.Text);

            if (!validate()) return;

            if (currentStatus == Status.Add)
            { 

                string checkQuery = "SELECT COUNT(*) FROM Level WHERE name = @LevelName";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                {
                    checkCommand.Parameters.AddWithValue("@LevelName", newLevel.Trim());
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Должность уже существует.");
                        return;
                    }
                }

                try
                {
                    string query = "INSERT INTO Level (name, coef) VALUES (@Name, @Coef)";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Name", newLevel.Trim());
                        command.Parameters.AddWithValue("@Coef", newCoefText);
                        command.ExecuteNonQuery();
                    }

                    if (isOpenedFromForm)
                    {
                        LevelUpdate?.Invoke();
                    }

                    LoadForm();
                    MessageBox.Show("Уровень добавлен успешно.");
                    closeVisibleAddForm();

                    dataGrid.ClearSelection();
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Selected = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении должности: " + ex.Message);
                }
            }

            if (currentStatus == Status.Edit) 
            {
                try
                {
                    int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                    var selectedId = Convert.ToInt32(dataGrid.Rows[selectedRowIndex].Cells["Id"].Value);

                    string checkQuery = "SELECT COUNT(*) FROM Level WHERE name = @Name";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                    {
                        checkCommand.Parameters.AddWithValue("@Name", newLevel.Trim());
                        int count = (int)checkCommand.ExecuteScalar();
                    }

                    string query = "UPDATE Level SET name  = @Name, coef = @Coef WHERE Id = @ID";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Name", newLevel.Trim());
                        command.Parameters.AddWithValue("@Coef", newCoefText);

                        command.Parameters.AddWithValue("@ID", selectedId);
                        command.ExecuteNonQuery();
                    }

                    LoadForm();
                    MessageBox.Show("Уровень обновлен успешно.");

                    if (isOpenedFromForm)
                    {
                        LevelSelected?.Invoke(selectedId);
                    }

                    closeVisibleAddForm();

                    dataGrid.ClearSelection();
                    dataGrid.Rows[selectedRowIndex].Selected = true;

                    dataGrid.ClearSelection();
                    dataGrid.Rows[selectedRowIndex].Selected = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении: " + ex.Message);
                }
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
                            // Получаем ID выбранного уровня
                            int levelId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            // Проверяем, есть ли сотрудники с этим level_id
                            string checkQuery = "SELECT COUNT(*) FROM Employee WHERE level_Id = @LevelID";
                            using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                            {
                                checkCommand.Parameters.AddWithValue("@LevelID", levelId);
                                int employeeCount = (int)checkCommand.ExecuteScalar();

                                if (employeeCount > 0)
                                {
                                    MessageBox.Show("Невозможно удалить уровень, так как существуют сотрудники с этим уровнем.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Прерываем выполнение, если есть сотрудники
                                }
                            }

                            // Выполняем SQL-запрос на удаление
                            string deleteQuery = "DELETE FROM Level WHERE Id = @LevelID";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection))
                            {
                                deleteCommand.Parameters.AddWithValue("@LevelID", levelId);
                                deleteCommand.ExecuteNonQuery();
                            }

                            if (isOpenedFromForm)
                            {
                                LevelUpdate?.Invoke();
                            }
                        }

                        LoadForm();
                        MessageBox.Show("Выбранные уровни удалены успешно.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строки для удаления.");
            }
        }
        private void buttonEditOk_Click(object sender, EventArgs e)
        {
            
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView(textBoxSearch.Text);
        }

        private void FilterDataGridView(string searchText)
        {
            string query = @"
                SELECT 
                    l.Id AS Id,
                    l.Name AS Название,
                    l.Coef AS Коэффициент
                FROM 
                    Level l
                WHERE 
                    l.Name LIKE @SearchQuery
            ";

            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@SearchQuery", "%" + searchText + "%");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Очищаем существующие строки в DataGridView
                dataGrid.Rows.Clear();

                // Добавляем новые строки, соответствующие результатам поиска
                foreach (DataRow row in dataTable.Rows)
                {
                    var newRow = new DataGridViewRow();
                    newRow.CreateCells(dataGrid);

                    if (dataGrid.Columns.Count >= 3)
                    {
                        newRow.Cells[0].Value = row["Id"];
                        newRow.Cells[1].Value = row["Название"].ToString();
                        newRow.Cells[2].Value = row["Коэффициент"];
                        dataGrid.Rows.Add(newRow);
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно столбцов в DataGridView.");
                    }
                }
            }
        }
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && isOpenedFromForm)
            {
                int selectedId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["id"].Value);
                LevelSelected?.Invoke(selectedId); 
                this.Close();
            }
        }

        private void textBoxAddCoef_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true; // Отменяем ввод, если символ не цифра и не запятая
            }

            // Проверяем, чтобы запятая вводилась только один раз
            if (e.KeyChar == ',' && textBoxAddCoef.Text.Contains(","))
            {
                e.Handled = true; // Отменяем ввод, если запятая уже есть
            }

            // Проверяем, если длина текста уже равна 4 и вводится не управляющий символ
            if (textBoxAddCoef.Text.Length >= 5 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Игнорируем ввод, если длина уже 4 и вводится не управляющий символ
            }

        }

        private void textBoxAddName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Игнорируем ввод, если это не буква и не управляющий символ
            }


            if (textBoxAddName.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод, если длина уже 7 и вводится не управляющий символ
            }

        }

        private void textBoxAddCoef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }

        private void textBoxAddName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }
    }
}

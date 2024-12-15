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
    public partial class Должности : Form
    {
        private SqlConnection sqlConnection;
        private bool isOpenedFromForm;

        public event Action<int> PositionSelected;
        public event Action PosUpdate;

        private Status currentStatus;

        public Должности(SqlConnection connection, bool openedFromForm = false)
        {
            InitializeComponent();
            sqlConnection = connection;
            isOpenedFromForm = openedFromForm;
            currentStatus = Status.View;
        }

        private void Должности_Load(object sender, EventArgs e)
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
            dataGrid.Columns.Add("salary", "Зарплата");

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
                    s.Salary AS Зарплата 
                FROM 
                    Position s"; // Измените на вашу таблицу и поля

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
                                newRow.Cells[2].Value = row["Зарплата"].ToString();
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

        private void clearAll() 
        { 
            textBoxAddSalary.Clear();
            textBoxAddName.Clear();
        }

        private void visibleAddForm(string groupName = "Добавить должность")
        {
            groupBoxAddSpravka.Text = groupName;

            groupBoxAddSpravka.Visible = true;
            groupBoxAction.Enabled = false;
            groupBoxSpravka.Visible = false;
        }

        private void closeVisibleAddForm()
        {
            clearAll();

            groupBoxAddSpravka.Visible = false;
            groupBoxAction.Enabled = true;
            groupBoxSpravka.Visible = true;
        }

        private void buttonAddPosition_Click(object sender, EventArgs e)
        {
            currentStatus = Status.Add;
            visibleAddForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if ((!string.IsNullOrWhiteSpace(textBoxAddName.Text.Trim()) || 
                !string.IsNullOrWhiteSpace(textBoxAddSalary.Text.Trim()))
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


        private void buttonEditPosition_Click(object sender, EventArgs e)
        {
            //visibleEditForm();

            if (dataGrid.SelectedRows.Count == 1)
            {
                visibleAddForm("Редактировать должность");
                currentStatus = Status.Edit;

                int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                string positionName = dataGrid.Rows[selectedRowIndex].Cells["name"].Value.ToString();
                string positionSalary = dataGrid.Rows[selectedRowIndex].Cells["salary"].Value.ToString();

                textBoxAddName.Text = positionName;
                textBoxAddSalary.Text = positionSalary;
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите навык для редактирования.");
            }
        }

        private bool validate() 
        {
            string newPosition = textBoxAddName.Text;
            string newSalaryText = textBoxAddSalary.Text;

            if (string.IsNullOrWhiteSpace(newPosition) || string.IsNullOrWhiteSpace(newSalaryText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return false;
            }

            if (!decimal.TryParse(newSalaryText, out decimal newSalary) || newSalary < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное значение зарплаты.");
                return false;
            }

            return true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string newPosition = textBoxAddName.Text;
            string newSalaryText = textBoxAddSalary.Text;

            if (!validate()) { return; }

            if (currentStatus == Status.Add)
            {
                string checkQuery = "SELECT COUNT(*) FROM Position WHERE name = @PositionName";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                {
                    checkCommand.Parameters.AddWithValue("@PositionName", newPosition.Trim());
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Должность уже существует.");
                        return;
                    }
                }

                try
                {
                    string query = "INSERT INTO Position (name, salary) VALUES (@PositionName, @Salary)";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@PositionName", newPosition.Trim());
                        command.Parameters.AddWithValue("@Salary", newSalaryText);
                        command.ExecuteNonQuery();
                    }

                    if (isOpenedFromForm)
                    {
                        PosUpdate?.Invoke();
                    }

                    MessageBox.Show("Должность добавлена успешно.");
                    clearAll();
                    LoadForm();

                    closeVisibleAddForm();
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
                    var selectedPosId = Convert.ToInt32(dataGrid.Rows[selectedRowIndex].Cells["Id"].Value);

                    string checkQuery = "SELECT COUNT(*) FROM Position WHERE name = @PositionName";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                    {
                        checkCommand.Parameters.AddWithValue("@PositionName", newPosition.Trim());
                        int count = (int)checkCommand.ExecuteScalar();

                        //if (count > 0)
                        //{
                        //    MessageBox.Show("Должность уже существует.");
                        //    return;
                        //}
                    }

                    string query = "UPDATE Position SET name  = @PositionName, salary = @PositionSalary WHERE Id = @PosID";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@PositionName", newPosition.Trim());
                        command.Parameters.AddWithValue("@PositionSalary", newSalaryText);

                        command.Parameters.AddWithValue("@PosID", selectedPosId);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Должность обновлена успешно.");
                    clearAll();
                    LoadForm();

                    if (isOpenedFromForm)
                    {
                        PositionSelected?.Invoke(selectedPosId);
                    }

                    closeVisibleAddForm();

                    dataGrid.ClearSelection();
                    dataGrid.Rows[selectedRowIndex].Selected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении: " + ex.Message);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView(textBoxSearch.Text);
        }

        private void FilterDataGridView(string searchText)
        {
            string query = @"
                SELECT 
                    p.Id AS Id,
                    p.Name AS Название,
                    p.Salary AS Зарплата
                FROM 
                    Position p
                WHERE 
                    p.Name LIKE @SearchQuery
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
                        newRow.Cells[2].Value = row["Зарплата"];
                        dataGrid.Rows.Add(newRow);
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно столбцов в DataGridView.");
                    }
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
                            // Получаем ID выбранной должности
                            int positionId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            // Проверяем, есть ли сотрудники с этой должностью
                            string checkQuery = "SELECT COUNT(*) FROM Employee WHERE position_id = @PositionID";
                            using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                            {
                                checkCommand.Parameters.AddWithValue("@PositionID", positionId);
                                int employeeCount = (int)checkCommand.ExecuteScalar();

                                if (employeeCount > 0)
                                {
                                    MessageBox.Show("Невозможно удалить должность, так как существуют сотрудники с этой должностью.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Прерываем выполнение, если есть сотрудники
                                }
                            }

                            // Выполняем SQL-запрос на удаление
                            string deleteQuery = "DELETE FROM Position WHERE Id = @PositionID";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection))
                            {
                                deleteCommand.Parameters.AddWithValue("@PositionID", positionId);
                                deleteCommand.ExecuteNonQuery();
                            }

                            if (isOpenedFromForm)
                            {
                                PosUpdate?.Invoke();
                            }
                        }

                        LoadForm();
                        MessageBox.Show("Выбранные должности удалены успешно.");
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

        private void dataGrid_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && isOpenedFromForm)
            {
                int selectedId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["id"].Value);
                PositionSelected?.Invoke(selectedId);
                this.Close();
            }
        }

        private void textBoxAddSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой или является ли это управляющим символом (например, Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод
            }

            if (textBoxAddSalary.Text.Length >= 7 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод, если длина уже 7 и вводится не управляющий символ
            }
        }

        private void textBoxAddSalary_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAddName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            //{
            //    e.Handled = true; // Игнорируем ввод, если это не буква и не управляющий символ
            //}


            if (textBoxAddName.Text.Length >= 40 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод, если длина уже 7 и вводится не управляющий символ
            }
        }

        private void textBoxAddName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }

        private void textBoxAddSalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }
    }
}

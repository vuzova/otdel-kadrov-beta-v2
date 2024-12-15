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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace otdel_kadrov_beta_v2
{
    public partial class Навыки : Form
    {
        private SqlConnection sqlConnection;
        private bool isOpenedFromForm;

        public event Action<int, string, int> SkillSelected;
        public event Action<int> SkillDelete;
        public event Action<int, string> SkillUpdate;

        private Status currentStatus;

        public Навыки(SqlConnection connection, bool openedFromForm = false)
        {
            InitializeComponent();
            sqlConnection = connection;
            isOpenedFromForm = openedFromForm;

            currentStatus = Status.View;
        }

        private enum Status
        {
            View,
            Edit,
            Add,
        }

        private void Навыки_Load(object sender, EventArgs e)
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

            dataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGrid.AllowUserToResizeRows = false;
        }
        
        private void LoadForm()
        {
            try
            {
                dataGrid.Rows.Clear();

                string query = @"
                SELECT 
                    s.Id AS Id,
                    s.Name AS Название 
                FROM 
                    Skill s"; // Измените на вашу таблицу и поля

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {


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
                            if (dataGrid.Columns.Count >= 2)
                            {
                                newRow.Cells[0].Value = row["Id"];
                                newRow.Cells[1].Value = row["Название"].ToString();
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

        private bool validate() 
        
        {
            string newSkill = textBoxAdd.Text;

            if (string.IsNullOrWhiteSpace(newSkill))
            {
                MessageBox.Show("Пожалуйста, введите данные.");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newSkill = textBoxAdd.Text;
            if (!validate()) return;

            if (currentStatus == Status.Add)
            {
                
                string checkQuery = "SELECT COUNT(*) FROM Skill WHERE name = @SkillName";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                {
                    checkCommand.Parameters.AddWithValue("@SkillName", newSkill.Trim());
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Навык уже существует.");
                        return;
                    }
                }

                try
                {
                    string query = "INSERT INTO Skill (name) VALUES (@SkillName)";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@SkillName", newSkill.Trim());
                        command.ExecuteNonQuery();
                    }

                    LoadForm();
                    MessageBox.Show("Навык добавлен успешно.");
                    textBoxAdd.Clear();
                    closeVisibleAddForm();

                    dataGrid.ClearSelection();
                    dataGrid.Rows[dataGrid.Rows.Count - 1].Selected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении навыка: " + ex.Message);
                }
            }

            if (currentStatus == Status.Edit) 
            {

                try
                {
                    int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                    var selectedSkillId = Convert.ToInt32(dataGrid.Rows[selectedRowIndex].Cells["Id"].Value);

                    //var selectedSkillName = dataGrid.Rows[selectedRowIndex].Cells["name"].Value;

                    string checkQuery = "SELECT COUNT(*) FROM Skill WHERE name = @SkillName";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                    {
                        checkCommand.Parameters.AddWithValue("@SkillName", newSkill.Trim());
                        int count = (int)checkCommand.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Навык уже существует.");
                            return;
                        }
                    }

                    string query = "UPDATE Skill SET name = @SkillName WHERE Id = @SkillID";
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        var newSkillNameTrim = newSkill.Trim();
                        command.Parameters.AddWithValue("@SkillName", newSkillNameTrim);
                        command.Parameters.AddWithValue("@SkillID", selectedSkillId);
                        command.ExecuteNonQuery();

                        if (isOpenedFromForm)
                        {
                            SkillUpdate?.Invoke(selectedSkillId, newSkillNameTrim);
                            this.Close();
                        }
                    }

                    MessageBox.Show("Навык обновлен успешно.");
                    LoadForm();
                    textBoxAdd.Clear();
                    closeVisibleAddForm();

                    dataGrid.ClearSelection();
                    dataGrid.Rows[selectedRowIndex].Selected = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении навыка: " + ex.Message);
                }

            }
        }

        private void buttonAddSkill_Click(object sender, EventArgs e)
        {
            currentStatus = Status.Add;
            visibleAddForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxAdd.Text))
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть форму добавления?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (result == DialogResult.Yes)
                {
                    closeVisibleAddForm();
                }
            }

            closeVisibleAddForm();
        }

        private void buttonDeleteSkill_Click(object sender, EventArgs e)
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
                            int skillId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            // Выполняем SQL-запрос на удаление
                            string query = "DELETE FROM Skill WHERE Id = @SkillID";
                            using (SqlCommand command = new SqlCommand(query, sqlConnection))
                            {
                                command.Parameters.AddWithValue("@SkillID", skillId);
                                command.ExecuteNonQuery();
                            }

                            if (isOpenedFromForm)
                            {
                                SkillDelete?.Invoke(skillId);
                                this.Close();
                            }
                        }

                        LoadForm(); 
                        MessageBox.Show("Выбранные навыки удалены успешно.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении навыков: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите навыки для удаления.");
            }
        }

        private void buttonEditSkill_Click(object sender, EventArgs e)
        {
            currentStatus = Status.Edit;
            if (dataGrid.SelectedRows.Count == 1)
            {
                visibleAddForm("Редактировать навык");

                int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                string skillName = dataGrid.Rows[selectedRowIndex].Cells["name"].Value.ToString();
                textBoxAdd.Text = skillName;
                //var selectedSkillId = Convert.ToInt32(dataGridSkill.Rows[selectedRowIndex].Cells["ID"].Value);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите навык для редактирования.");
            }

        }

        private void buttonEditOk_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonEditClose_Click(object sender, EventArgs e)
        {
            closeVisibleAddForm();
        }

 
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView(textBoxSearch.Text);
        }

        
        private void FilterDataGridView(string searchText)
        {

                    string query = @"
                SELECT 
                    s.Id AS Id,
                    s.Name AS Название
                FROM 
                    Skill s
                WHERE 
                    s.Name LIKE @SearchQuery
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

                    if (dataGrid.Columns.Count >= 2)
                    {
                        newRow.Cells[0].Value = row["Id"];
                        newRow.Cells[1].Value = row["Название"].ToString();
                        dataGrid.Rows.Add(newRow);
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно столбцов в DataGridView.");
                    }
                }
            }

        }

        private void visibleAddForm(string groupName = "Добавить навык")
        {
            groupBoxAddSpravka.Text = groupName;

            groupBoxAddSpravka.Visible = true;
            groupBoxAction.Enabled = false;
            groupBoxSpravka.Visible = false;
        }

        private void closeVisibleAddForm()
        {
            groupBoxAddSpravka.Visible = false;
            groupBoxAction.Enabled = true;
            groupBoxSpravka.Visible = true;
        }


        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && isOpenedFromForm)
            {
                int selectedId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["id"].Value);
                int level = -1;
                string selectedName = dataGrid.Rows[e.RowIndex].Cells["name"].Value.ToString();  


                Диалог dialog = new Диалог();
                dialog.SkillAddLevel+= (num) => 
                {
                    level = num;
                };

                DialogResult result = dialog.ShowDialog();

                // Проверяем, был ли нажата кнопка OK
                if (result == DialogResult.OK)
                {
                    SkillSelected?.Invoke(selectedId, selectedName, level);
                    this.Close();
                }
                
            }
        }

        private void textBoxAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            //{
            //    e.Handled = true; // Игнорируем ввод, если это не буква и не управляющий символ
            //}


            if (textBoxAdd.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Игнорируем ввод, если длина уже 7 и вводится не управляющий символ
            }
        }

        private void textBoxAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }
    }
}

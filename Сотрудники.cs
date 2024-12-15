using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ComboBox = System.Windows.Forms.ComboBox;

namespace otdel_kadrov_beta_v2
{
    public partial class Сотрудники : Form
    {
        private SqlConnection sqlConnection;
        private bool isOpenedFromForm;

        public event Action<int> EmployeeSelected;
        public event Action<int> EmployeeDelete;
        public event Action EmployeeUpdate;

        private Status currentStatus;
        public Сотрудники(SqlConnection connection, bool openedFromForm = false)
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

        private void Сотрудники_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadForm();
        }


        private void FIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли нажатая клавиша буквой
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменяем ввод, если это не буква
            }
        }

        private void InitializeDataGridView()
        {

            dataGridSkillEmployee.Columns.Clear(); // Очищаем существующие столбцы

            // Добавляем скрытый столбец для ID
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.Name = "Id"; // Уникальное имя столбца
            idColumn.HeaderText = "Id"; // Заголовок столбца
            idColumn.Visible = false; // Скрываем столбец
            dataGridSkillEmployee.Columns.Add(idColumn);

            // Добавляем остальные видимые столбцы
            dataGridSkillEmployee.Columns.Add("Название", "Название");
            dataGridSkillEmployee.Columns.Add("Уровень", "Уровень");

            dataGridSkillEmployee.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGrid.AllowUserToResizeRows = false;
            dataGridSkillEmployee.AllowUserToResizeRows = false;

        }

        private void LoadForm()
        {
            try
            {
                string query = @"SELECT 
                    e.Id,
                    e.last_name as Фамилия,
                    e.first_name as Имя,
                    e.patronymic as Отчество,
                    p.name AS Должность,
                    l.name AS Уровень,
                    e.pass_serial as Серия_паспорта,
                    e.pass_numb as Номер_паспорта,
                    e.pass_issued_by as Кем_выдан ,
                    e.pass_date as Когда_выдан,
                    e.address_register as Адрес_регистрации,
                    e.address_live as Адрес_проживания,
                    e.tel as Телефон,
                    e.email as Почта,
                    e.tg as Телеграм
                FROM 
                    dbo.Employee e
                LEFT JOIN 
                    dbo.Position p ON e.position_id = p.Id
                LEFT JOIN 
                    dbo.Level l ON e.level_id = l.Id";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                dataGrid.DataSource = ds;

                //Визуал
                dataGrid.Columns["Id"].Visible = false;
                //foreach (DataGridViewColumn column in dataGrid.Columns)
                //{
                //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //}

                

                //dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dataGrid.Columns[13].Width = 100;
                dataGrid.Columns[14].Width = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного сотрудника
                int selectedEmployeeId = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);
                LoadSkillForEmployee(selectedEmployeeId, dataGridSkillEmployee);
            }
            else
            {
                // Если ничего не выбрано, очищаем dataGridSkillEmployee
                dataGridSkillEmployee.DataSource = null; // Очищаем источник данных
            }
        }

        private void LoadSkillForEmployee(int employeeId, DataGridView dg)
        {
            string query = @"
            SELECT 
                s.Id AS Id,
                s.Name AS Название, 
                es.Level AS Уровень 
            FROM 
                Employee_Skill es
            JOIN 
                Skill s ON es.skill_id = s.Id 
            WHERE 
                es.employee_id = @ID";

            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@ID", employeeId);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);

                // Проверяем, есть ли данные
                if (ds.Rows.Count > 0)
                {
                    // Очищаем DataGridView перед добавлением новых строк
                    dg.Rows.Clear();

                    // Добавляем новые строки в DataGridView
                    foreach (DataRow row in ds.Rows)
                    {
                        var newRow = new DataGridViewRow();
                        newRow.CreateCells(dg);

                        // Убедитесь, что количество ячеек соответствует количеству столбцов
                        if (dg.Columns.Count >= 3)
                        {
                            newRow.Cells[0].Value = row["Id"];
                            newRow.Cells[1].Value = row["Название"].ToString();
                            newRow.Cells[2].Value = row["Уровень"].ToString();
                            dg.Rows.Add(newRow);
                        }
                        else
                        {
                            MessageBox.Show("Недостаточно столбцов в DataGridView.");
                        }
                    }
                }
                else
                {
                    dg.Rows.Clear();
                    //MessageBox.Show("Навыки для сотрудника не найдены.");
                }
                
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
                string query = @"SELECT 
                 e.Id,
                 e.last_name as Фамилия,
                 e.first_name as Имя,
                 e.patronymic as Отчество,
                 p.name AS Должность,
                 l.name AS Уровень,
                 e.pass_serial as Серия_паспорта,
                 e.pass_numb as Номер_паспорта,
                 e.pass_issued_by as Кем_выдан,
                 e.pass_date as Когда_выдан,
                 e.address_register as Адрес_регистрации,
                 e.address_live as Адрес_проживания,
                 e.tel as Телефон,
                 e.email as Почта,
                 e.tg as Телеграм
             FROM 
                 dbo.Employee e
             LEFT JOIN 
                 dbo.Position p ON e.position_id = p.Id
             LEFT JOIN 
                 dbo.Level l ON e.level_id = l.Id
             WHERE
                 e.last_name LIKE @SearchTerm
                 OR e.first_name LIKE @SearchTerm
                 OR e.patronymic LIKE @SearchTerm";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
                string searchTerm = searchText + "%"; 
                dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);
                DataTable ds = new DataTable();
                dataAdapter.Fill(ds);
                dataGrid.DataSource = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при поиске: " + ex.Message);
            }
        }

        private void visibleAddForm(string groupName = "Добавить проект")
        {
            groupBoxAddSpravka.Text = groupName;
            clearAll();

            buttonAddOk.Visible = true; //ok
            buttonAddClose.Visible = true; //close

            buttonOpenSkillForm.Visible = true; //plus
            buttonDeleteSkill.Visible = true; //minus

            groupBoxAddSpravka.Visible = true;
            groupBoxAction.Enabled = false;
            groupBoxSpravka.Visible = false;
        }

        private void closeVisibleAddForm()
        {
            buttonAddOk.Visible = false; //ok
            buttonAddClose.Visible = false; //close

            buttonOpenSkillForm.Visible = false; //plus
            buttonDeleteSkill.Visible = false; //minus

            groupBoxAddSpravka.Visible = false;
            groupBoxAction.Enabled = true;
            groupBoxSpravka.Visible = true;
        }

        private void buttonAddForm_Click(object sender, EventArgs e)
        {
            currentStatus = Status.Add;
            dataGridSkillEmployee.Rows.Clear();

            visibleAddForm();
            LoadComboBoxLevel(level_name);
            LoadComboBoxPosition(position_name);

        }

        private void clearAll() 
        {
            last_name.Clear();
            first_name.Clear();
            patronymicс.Clear();
            pass_serial.Clear();
            pass_numb.Clear();
            pass_issued_by.Clear();
            address_register.Clear();
            addres_live.Clear();
            tell.Clear();
            emaill.Clear();
            tgg.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string lastName = last_name.Text.Trim();
            string firstName = first_name.Text.Trim();
            string patronymic = patronymicс.Text.Trim();
            int? positionId = position_name.SelectedValue as int?; // Предполагается, что это значение id
            int? levelId = level_name.SelectedValue as int?; // Используем nullable int для level_id
            string passSerial = pass_serial.Text.Trim();
            string passNumb = pass_numb.Text.Trim();
            string passIssuedBy = pass_issued_by.Text.Trim();
            DateTime passDate = pass_date.Value;
            string addressRegister = address_register.Text.Trim();
            string addressLive = addres_live.Text.Trim();
            string tel = tell.Text.Trim();
            string email = emaill.Text.Trim();
            string tg = tgg.Text.Trim();

            if (!string.IsNullOrWhiteSpace(lastName) ||
                !string.IsNullOrWhiteSpace(firstName) ||
                !string.IsNullOrWhiteSpace(patronymic) ||
                !string.IsNullOrWhiteSpace(passSerial) ||
                !string.IsNullOrWhiteSpace(passNumb) ||
                !string.IsNullOrWhiteSpace(passIssuedBy) ||
                !string.IsNullOrWhiteSpace(addressRegister) ||
                !string.IsNullOrWhiteSpace(addressLive) ||
                !string.IsNullOrWhiteSpace(tel) ||
                !string.IsNullOrWhiteSpace(email) ||
                !string.IsNullOrWhiteSpace(tg) ||
                position_name.SelectedIndex != -1 ||
                level_name.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти? Введенные данные не сохранятся.", "Подтверждение", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    closeVisibleAddForm();
                    LoadForm();
                    dataGridSkillEmployee.Rows.Clear();
                }

                else
                {
                    return;
                }

            }

            closeVisibleAddForm();
            LoadForm();
            dataGridSkillEmployee.Rows.Clear();
        }

        private void LoadComboBoxLevel(ComboBox comboBox)
        {
            string queryLevel = "SELECT id, name FROM level";
            using (SqlCommand command = new SqlCommand(queryLevel, sqlConnection))
            {
                DataTable levelTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(levelTable);
                }

                comboBox.DataSource = levelTable;
                comboBox.DisplayMember = "name";
                comboBox.ValueMember = "id"; 
                comboBox.SelectedIndex = -1;
                
            }
        }

        private void LoadComboBoxPosition(ComboBox comboBox) {
            string queryPosition = "SELECT id, name FROM position";
            using (SqlCommand command = new SqlCommand(queryPosition, sqlConnection))
            {
                DataTable positionTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(positionTable);
                }

                comboBox.DataSource = positionTable;
                comboBox.DisplayMember = "name";
                comboBox.ValueMember = "id";
                comboBox.SelectedIndex = -1;
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
                            // Получаем ID выбранного сотрудника
                            int employeeId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            // Проверяем, является ли сотрудник капитаном проекта
                            string checkCapQuery = "SELECT p.name FROM employee_project ep JOIN project p ON ep.project_id = p.id WHERE ep.employee_id = @EmployeeID AND ep.is_cap = 1";
                            using (SqlCommand checkCapCommand = new SqlCommand(checkCapQuery, sqlConnection))
                            {
                                checkCapCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                                var projectName = checkCapCommand.ExecuteScalar();

                                if (projectName != null)
                                {
                                    MessageBox.Show($"Невозможно удалить сотрудника, так как он является ответственным проекта '{projectName}'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Прерываем выполнение, если сотрудник капитан
                                }
                            }

                            // Проверяем, участвует ли сотрудник в команде
                            string checkTeamQuery = "SELECT p.name FROM employee_project ep JOIN project p ON ep.project_id = p.id WHERE ep.employee_id = @EmployeeID";
                            using (SqlCommand checkTeamCommand = new SqlCommand(checkTeamQuery, sqlConnection))
                            {
                                checkTeamCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                                var teamProjectName = checkTeamCommand.ExecuteScalar();

                                if (teamProjectName != null)
                                {
                                    DialogResult confirmResult = MessageBox.Show($"Вы уверены, что хотите удалить сотрудника? Он участвует в команде '{teamProjectName}'.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (confirmResult != DialogResult.Yes)
                                    {
                                        return; // Прерываем выполнение, если пользователь не подтвердил
                                    }
                                }
                            }

                            // Выполняем SQL-запрос на удаление
                            string deleteQuery = "DELETE FROM Employee WHERE Id = @ID";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection))
                            {
                                deleteCommand.Parameters.AddWithValue("@ID", employeeId);
                                deleteCommand.ExecuteNonQuery();
                            }

                            if (isOpenedFromForm)
                            {
                                EmployeeDelete?.Invoke(employeeId);
                            }
                        }

                        if (isOpenedFromForm)
                        {
                            EmployeeUpdate?.Invoke();
                        }

                        MessageBox.Show("Выбранные сотрудники удалены успешно.");
                        LoadForm();
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

        private void buttonOpenLevelForm_Click(object sender, EventArgs e)
        {
            Уровни lvlForm = new Уровни(sqlConnection, true);
            lvlForm.LevelSelected += (id) =>
            {
                LoadComboBoxLevel(level_name);
                level_name.SelectedValue = id;
            };


            lvlForm.LevelUpdate += () =>
            {
                var index = level_name.SelectedIndex;
                LoadComboBoxLevel(level_name);
                level_name.SelectedValue = index;
            };

            lvlForm.ShowDialog();
        }

        private void buttonOpenPositionForm_Click(object sender, EventArgs e)
        {
            Должности posForm = new Должности(sqlConnection, true);
            posForm.PositionSelected += (id) =>
            {
                LoadComboBoxPosition(position_name);
                position_name.SelectedValue = id; // Установка выбранного значения по id
            };

            posForm.PosUpdate += () =>
            {
                var index = position_name.SelectedIndex;
                LoadComboBoxPosition(position_name);
                position_name.SelectedValue = index;
            };


            posForm.ShowDialog();
        }


        private void buttonOpenSkillFormEdit_Click(object sender, EventArgs e)
        {
            
        }


        private void buttonOpenSkillForm_Click(object sender, EventArgs e)
        {
            Навыки skillForm = new Навыки(sqlConnection, true);

            skillForm.SkillSelected += (id, name, level) =>
            {
                bool skillExists = false;

                // Перебираем все строки в DataGridView
                foreach (DataGridViewRow row in dataGridSkillEmployee.Rows)
                {
                    // Проверяем, совпадают ли ID и название навыка
                    if (Convert.ToInt32(row.Cells[0].Value) == id)
                    {
                        skillExists = true;
                        break; // Выходим из цикла, если навык найден
                    }
                }

                // Если навык не найден, добавляем новую строку
                if (!skillExists)
                {
                    var newRow = new DataGridViewRow();
                    newRow.CreateCells(dataGridSkillEmployee);
                    newRow.Cells[0].Value = id; // ID навыка
                    newRow.Cells[1].Value = name; // Название навыка
                    newRow.Cells[2].Value = level; // Уровень владения
                    dataGridSkillEmployee.Rows.Add(newRow);

                    //LoadSkillForEmployee(id, dataGridSkillEmployee);
                }
                else
                {
                    // Если навык уже существует, показываем сообщение
                    DialogResult result = MessageBox.Show(
                       "Навык уже выбран. Перезаписать уровень владения?", // Текст вопроса
                       "Подтверждение", // Заголовок окна
                       MessageBoxButtons.YesNo, // Кнопки "Да" и "Нет"
                       MessageBoxIcon.Question // Иконка вопроса
                   );

                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridSkillEmployee.Rows)
                        {
                            // Проверяем, совпадают ли ID и название навыка
                            if (Convert.ToInt32(row.Cells[0].Value) == id)
                            {
                                row.Cells[2].Value = level;
                            }
                        }
                    }
                }
            };


            skillForm.SkillDelete += (id) =>
            {
                foreach (DataGridViewRow row in dataGridSkillEmployee.Rows)
                {
                    // Проверяем, совпадают ли ID и название навыка
                    if (Convert.ToInt32(row.Cells[0].Value) == id)
                    {
                        dataGridSkillEmployee.Rows.Remove(row);
                        break;
                    }
                }
            };

            skillForm.SkillUpdate += (id, name) =>
            {
                foreach (DataGridViewRow row in dataGridSkillEmployee.Rows)
                {

                    if (Convert.ToInt32(row.Cells[0].Value) == id)
                    {
                        row.Cells[1].Value = name;
                        break;
                    }
                }
            };


            skillForm.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                addres_live.Text = address_register.Text;
                addres_live.Enabled = false;
            }
            else
            {
                addres_live.Text = "";
                addres_live.Enabled = true;
            }
        }

        private bool IsValidEmail(string email)
        {
            // Регулярное выражение для проверки формата email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email.Trim(), pattern);
        }

        private bool validate() 
        {
            string lastName = last_name.Text.Trim();
            string firstName = first_name.Text.Trim();
            string patronymic = patronymicс.Text.Trim();
            int? positionId = position_name.SelectedValue as int?; // Предполагается, что это значение id
            int? levelId = level_name.SelectedValue as int?; // Используем nullable int для level_id
            string passSerial = pass_serial.Text.Trim();
            string passNumb = pass_numb.Text.Trim();
            string passIssuedBy = pass_issued_by.Text.Trim();
            DateTime passDate = pass_date.Value;
            string addressRegister = address_register.Text.Trim();
            string addressLive = addres_live.Text.Trim();
            string tel = tell.Text.Trim();
            string email = emaill.Text.Trim();
            string tg = tgg.Text.Trim();

            if (string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(passSerial) ||
                string.IsNullOrWhiteSpace(passNumb) ||
                string.IsNullOrWhiteSpace(passIssuedBy) ||
                string.IsNullOrWhiteSpace(addressRegister) ||
                position_name.SelectedIndex == -1 ||
                level_name.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tel) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(tg))
            {
                MessageBox.Show("Пожалуйста, заполните хотя бы один контакт для связи");
                return false;
            }

            if (tel.Length != 10 && tel.Length > 0)
            {
                MessageBox.Show("Пожалуйста, проверьте номер телефона.");
                return false;
            }


            if (!IsValidEmail(emaill.Text) && email.Length > 0)
            {
                MessageBox.Show("Пожалуйста, проверьте email.");
                return false;
            }

            if (passSerial.Length != 4)
            {
                MessageBox.Show("Пожалуйста, проверьте серию паспорта.");
                return false;
            }

            if (passNumb.Length != 6)
            {
                MessageBox.Show("Пожалуйста, проверьте номер паспорта.");
                return false;
            }

            if (addressRegister.Length < 1)
            {
                MessageBox.Show("Мало символов в поле адреса регистрации.");
                return false;
            }


            if (addressLive.Length < 1)
            {
                MessageBox.Show("Мало символов в поле адреса проживания.");
                return false;
            }

            if (passIssuedBy.Length < 1)
            {
                MessageBox.Show("Поле \"Кем выдан\" требуется больше символов.");
                return false;
            }

            if (lastName.Length < 1)
            {
                MessageBox.Show("Мало символов в фамилии.");
                return false;
            }

            if (firstName.Length < 1)
            {
                MessageBox.Show("Мало символов в имени.");
                return false;
            }


            return true;
        }
        private void ОК_Click(object sender, EventArgs e)
        {
            string lastName = last_name.Text.Trim();
            string firstName = first_name.Text.Trim();
            string patronymic = patronymicс.Text.Trim();
            int? positionId = position_name.SelectedValue as int?; // Предполагается, что это значение id
            int? levelId = level_name.SelectedValue as int?; // Используем nullable int для level_id
            string passSerial = pass_serial.Text.Trim();
            string passNumb = pass_numb.Text.Trim();
            string passIssuedBy = pass_issued_by.Text.Trim();
            DateTime passDate = pass_date.Value;
            string addressRegister = address_register.Text.Trim();
            string addressLive = addres_live.Text.Trim();
            string tel = tell.Text.Trim();
            string email = emaill.Text.Trim();
            string tg = tgg.Text.Trim();

            if (!validate()) return;

            if (currentStatus == Status.Add)
            {

                

                string query = @"
                INSERT INTO employee (last_name, first_name, patronymic, position_id, level_id, pass_serial, pass_numb, pass_issued_by, pass_date, address_register, address_live, tel, email, tg)
                VALUES (@last_name, @first_name, @patronymic, @position_id, @level_id, @pass_serial, @pass_numb, @pass_issued_by, @pass_date, @address_register, @address_live, @tel, @email, @tg);
                SELECT SCOPE_IDENTITY();"; // Добавляем SELECT SCOPE_IDENTITY()

                try
                {
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@last_name", lastName);
                        command.Parameters.AddWithValue("@first_name", firstName);
                        command.Parameters.AddWithValue("@patronymic", string.IsNullOrWhiteSpace(patronymic) ? (object)DBNull.Value : patronymic);
                        command.Parameters.AddWithValue("@position_id", positionId);
                        command.Parameters.AddWithValue("@level_id", levelId);
                        command.Parameters.AddWithValue("@pass_serial", passSerial);
                        command.Parameters.AddWithValue("@pass_numb", passNumb);
                        command.Parameters.AddWithValue("@pass_issued_by", passIssuedBy);
                        command.Parameters.AddWithValue("@pass_date", passDate);
                        command.Parameters.AddWithValue("@address_register", addressRegister);
                        command.Parameters.AddWithValue("@address_live", addressLive);

                        command.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(tel) ? (object)DBNull.Value : tel);
                        command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                        command.Parameters.AddWithValue("@tg", string.IsNullOrWhiteSpace(tg) ? (object)DBNull.Value : tg);

                        int employeeId = Convert.ToInt32(command.ExecuteScalar());

                        foreach (DataGridViewRow selectedRow in dataGridSkillEmployee.Rows)
                        {
                            // Получаем ID выбранного навыка (предполагается, что у вас есть столбец ID)
                            int SkillID = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                            string Level = selectedRow.Cells["Уровень"].Value.ToString();

                            // Выполняем SQL-запрос на удаление
                            string querySkill = "INSERT INTO Employee_Skill (employee_id, skill_id, level) VALUES (@EmployeeID, @SkillID, @Level)";
                            using (SqlCommand commandSkill = new SqlCommand(querySkill, sqlConnection))
                            {
                                commandSkill.Parameters.AddWithValue("@EmployeeID", employeeId);
                                commandSkill.Parameters.AddWithValue("@SkillID", SkillID);
                                commandSkill.Parameters.AddWithValue("@Level", Level);
                                commandSkill.ExecuteNonQuery();
                                
                            }
                        }


                        MessageBox.Show("Сотрудник добавлен успешно.");
                        LoadForm();
                        closeVisibleAddForm();

                        if (isOpenedFromForm)
                        {
                            EmployeeUpdate?.Invoke();
                        }


                        dataGrid.ClearSelection();
                        dataGrid.Rows[dataGrid.Rows.Count - 1].Selected = true;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении: " + ex.Message);
                }
            }


            if (currentStatus == Status.Edit)
            {
                int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                string employeeId = dataGrid.Rows[selectedRowIndex].Cells["id"].Value.ToString();


                string query = @"
                    UPDATE employee 
                    SET last_name = @last_name, 
                        first_name = @first_name, 
                        patronymic = @patronymic, 
                        position_id = @position_id, 
                        level_id = @level_id, 
                        pass_serial = @pass_serial, 
                        pass_numb = @pass_numb, 
                        pass_issued_by = @pass_issued_by, 
                        pass_date = @pass_date, 
                        address_register = @address_register, 
                        address_live = @address_live, 
                        tel = @tel, 
                        email = @email, 
                        tg = @tg 
                    WHERE id = @id;";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@id", employeeId);
                        command.Parameters.AddWithValue("@last_name", lastName);
                        command.Parameters.AddWithValue("@first_name", firstName);
                        command.Parameters.AddWithValue("@patronymic", string.IsNullOrWhiteSpace(patronymic) ? (object)DBNull.Value : patronymic);
                        command.Parameters.AddWithValue("@position_id", positionId);
                        command.Parameters.AddWithValue("@level_id", levelId);
                        command.Parameters.AddWithValue("@pass_serial", passSerial);
                        command.Parameters.AddWithValue("@pass_numb", passNumb);
                        command.Parameters.AddWithValue("@pass_issued_by", passIssuedBy);
                        command.Parameters.AddWithValue("@pass_date", passDate);
                        command.Parameters.AddWithValue("@address_register", addressRegister);
                        command.Parameters.AddWithValue("@address_live", addressLive);
                        command.Parameters.AddWithValue("@tel", string.IsNullOrWhiteSpace(tel) ? (object)DBNull.Value : tel);
                        command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                        command.Parameters.AddWithValue("@tg", string.IsNullOrWhiteSpace(tg) ? (object)DBNull.Value : tg);

                        command.ExecuteNonQuery();
                        skillId(employeeId);

                        foreach (DataGridViewRow selectedRow in dataGridSkillEmployee.Rows)
                        {
                            // Получаем ID выбранного навыка (предполагается, что у вас есть столбец ID)
                            int SkillID = Convert.ToInt32(selectedRow.Cells["Id"].Value);

                            //??
                            string Level = selectedRow.Cells["Уровень"].Value.ToString();

                            // Выполняем SQL-запрос на удаление
                            string querySkill = "INSERT INTO Employee_Skill (employee_id, skill_id, level) VALUES (@EmployeeID, @SkillID, @Level)";
                            using (SqlCommand commandSkill = new SqlCommand(querySkill, sqlConnection))
                            {
                                commandSkill.Parameters.AddWithValue("@EmployeeID", employeeId);
                                commandSkill.Parameters.AddWithValue("@SkillID", SkillID);
                                commandSkill.Parameters.AddWithValue("@Level", Level);
                                commandSkill.ExecuteNonQuery();
                            }
                        }

                       
                        
                        MessageBox.Show("Сотрудник обновлен успешно.");
                        LoadForm();
                       
                        closeVisibleAddForm();

                        if (isOpenedFromForm)
                        {
                            EmployeeUpdate?.Invoke();
                        }


                        dataGrid.ClearSelection();
                        dataGrid.Rows[selectedRowIndex].Selected = true;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении: " + ex.Message);
                }
            }


        }

        private void buttonDeleteSkillDG(DataGridView dg) 
        {
            if (dg.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dg.SelectedRows)
                {
                    dg.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.");
            }
        }

        private void buttonDeleteSkill_Click(object sender, EventArgs e)
        {
            buttonDeleteSkillDG(dataGridSkillEmployee);
        }


        private void buttonDeleteSkillEdit_Click(object sender, EventArgs e)
        {
            buttonDeleteSkillDG(dataGridSkillEmployee);
        }

        private void buttonEditForm_Click(object sender , EventArgs e)
        {
            if (dataGrid.SelectedRows.Count == 1)
            {
                currentStatus = Status.Edit;
                visibleAddForm("Редактировать сотрудника");

                LoadComboBoxLevel(level_name);
                LoadComboBoxPosition(position_name);

                
                int selectedRowIndex = dataGrid.SelectedRows[0].Index;
                string employeeId = dataGrid.Rows[selectedRowIndex].Cells["id"].Value.ToString();

                first_name.Text = dataGrid.Rows[selectedRowIndex].Cells["Имя"].Value.ToString();
                last_name.Text = dataGrid.Rows[selectedRowIndex].Cells["Фамилия"].Value.ToString();
                patronymicс.Text = dataGrid.Rows[selectedRowIndex].Cells["Отчество"].Value.ToString();

                tell.Text = dataGrid.Rows[selectedRowIndex].Cells["Телефон"].Value.ToString();
                emaill.Text = dataGrid.Rows[selectedRowIndex].Cells["Почта"].Value.ToString();
                tgg.Text = dataGrid.Rows[selectedRowIndex].Cells["Телеграм"].Value.ToString();

                pass_serial.Text = dataGrid.Rows[selectedRowIndex].Cells["Серия_паспорта"].Value.ToString();
                pass_numb.Text = dataGrid.Rows[selectedRowIndex].Cells["Номер_паспорта"].Value.ToString();
                pass_issued_by.Text = dataGrid.Rows[selectedRowIndex].Cells["Кем_выдан"].Value.ToString();
                pass_date.Value = (DateTime)dataGrid.Rows[selectedRowIndex].Cells["Когда_выдан"].Value;

                addres_live.Text = dataGrid.Rows[selectedRowIndex].Cells["Адрес_проживания"].Value.ToString();
                address_register.Text = dataGrid.Rows[selectedRowIndex].Cells["Адрес_регистрации"].Value.ToString();


                //
                //LoadSkillForEmployee(Convert.ToInt32(employeeId), dataGridSkillEmployee);
                ///

                string idToFindLevel = dataGrid.Rows[selectedRowIndex].Cells["Уровень"].Value.ToString();
                string idToFindPos = dataGrid.Rows[selectedRowIndex].Cells["Должность"].Value.ToString();

                selectedIndexFromCB(idToFindLevel, level_name);
                selectedIndexFromCB(idToFindPos, position_name);

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите навык для редактирования.");
            }
        }

        private void selectedIndexFromCB(string id, ComboBox cb) 
        {
            int index = cb.FindStringExact(id);
            if (index != -1)
            {
                cb.SelectedIndex = index; // Устанавливаем выбранный индекс
            }
            else
            {
                cb.SelectedIndex = -1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            closeVisibleAddForm(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void skillId(string employeeId) 
        {
            string query = "DELETE FROM employee_skill WHERE employee_id = @id;";

            try
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", employeeId); 
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при удалении: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && isOpenedFromForm)
            {
                int selectedId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["Id"].Value);
                EmployeeSelected?.Invoke(selectedId);
                this.Close();
            }
        }

        ////ВАЛИДАЦИЯ////
        private void pass_serial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод
            }

            if (pass_serial.Text.Length >= 4 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод, если длина уже 4 и вводится не управляющий символ
            }
        }
        private void pass_numb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод
            }

            if (pass_numb.Text.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Игнорируем ввод, если длина уже 4 и вводится не управляющий символ
            }
        }

        private void last_name_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }


            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Отменяем ввод, если это не буква или пробел
            }

            if (last_name.Text.Length >= 30 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void first_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }

            if (first_name.Text.Length >= 12 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }


            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Отменяем ввод, если это не буква или пробел
            }
        }

        private void patronymicс_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Отменяем ввод, если это не буква или пробел
            }

            if (patronymicс.Text.Length >= 25 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void last_name_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменяем ввод, если это не буква или пробел
            }

            if (last_name.Text.Length >= 25 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }
            // Позволяем вводить только управляющие символы (например, Backspace)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Проверяем, является ли символ цифрой
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Отменяем ввод, если это не цифра
            }

            // Проверяем, если длина текста уже 10 символов
            if (tell.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменяем ввод, если длина уже 10
            }

            // Проверяем, если это первый символ и он равен '0'
            if (tell.Text.Length == 0 && e.KeyChar == '0')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он '0'
            }
        }

        private void emaill_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }
        }

        private void tgg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }
        }

        private void address_register_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (address_register.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }
            if (checkBox1.Checked) 
            { 
                addres_live.Text = address_register.Text;
            }

        }

        private void addres_live_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (addres_live.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }

        }

        private void pass_issued_by_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (pass_issued_by.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true; // Отменяем ввод, если это первый символ и он пробел
            }
        }

        private void tell_TextChanged(object sender, EventArgs e)
        {
            if (tell.Text.Length > 10)
            {
                tell.Text = tell.Text.Substring(0, 10);
                tell.SelectionStart = tell.Text.Length; // Устанавливаем курсор в конец
            }
        }

        private void tell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }

        private void pass_serial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }

        private void pass_numb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }

        private void last_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }

        private void first_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }

        private void patronymicс_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true; // Запрещаем вставку
            }
        }
    }
}

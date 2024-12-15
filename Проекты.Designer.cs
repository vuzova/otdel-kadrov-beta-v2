namespace otdel_kadrov_beta_v2
{
    partial class Проекты
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxSpravka = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.buttonEditForm = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddForm = new System.Windows.Forms.Button();
            this.groupBoxAddSpravka = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.date_end_fact = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.date_end_plan = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.date_start_fact = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.date_start_plan = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.RichTextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteEmployee = new System.Windows.Forms.Button();
            this.buttonOpenEmployee = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonAddClose = new System.Windows.Forms.Button();
            this.buttonOpenEmployeeForm = new System.Windows.Forms.Button();
            this.buttonAddOk = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridEmployee = new System.Windows.Forms.DataGridView();
            this.buttonMore = new System.Windows.Forms.Button();
            this.groupBoxSpravka.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBoxAction.SuspendLayout();
            this.groupBoxAddSpravka.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployee)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxSpravka
            // 
            this.groupBoxSpravka.Controls.Add(this.label9);
            this.groupBoxSpravka.Controls.Add(this.textBoxSearch);
            this.groupBoxSpravka.Controls.Add(this.dataGrid);
            this.groupBoxSpravka.Location = new System.Drawing.Point(12, 15);
            this.groupBoxSpravka.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxSpravka.Name = "groupBoxSpravka";
            this.groupBoxSpravka.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxSpravka.Size = new System.Drawing.Size(764, 350);
            this.groupBoxSpravka.TabIndex = 13;
            this.groupBoxSpravka.TabStop = false;
            this.groupBoxSpravka.Text = "Справочник проектов";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "Поиск";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(19, 58);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(729, 22);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.Tag = "";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(19, 97);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(731, 240);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.SelectionChanged += new System.EventHandler(this.dataGrid_SelectionChanged);
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.buttonMore);
            this.groupBoxAction.Controls.Add(this.buttonEditForm);
            this.groupBoxAction.Controls.Add(this.buttonDelete);
            this.groupBoxAction.Controls.Add(this.buttonAddForm);
            this.groupBoxAction.Location = new System.Drawing.Point(795, 14);
            this.groupBoxAction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxAction.Size = new System.Drawing.Size(179, 649);
            this.groupBoxAction.TabIndex = 12;
            this.groupBoxAction.TabStop = false;
            // 
            // buttonEditForm
            // 
            this.buttonEditForm.Location = new System.Drawing.Point(5, 60);
            this.buttonEditForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonEditForm.Name = "buttonEditForm";
            this.buttonEditForm.Size = new System.Drawing.Size(167, 31);
            this.buttonEditForm.TabIndex = 2;
            this.buttonEditForm.Text = "Редактировать";
            this.buttonEditForm.UseVisualStyleBackColor = true;
            this.buttonEditForm.Click += new System.EventHandler(this.buttonEditForm_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(6, 191);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(167, 31);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAddForm
            // 
            this.buttonAddForm.Location = new System.Drawing.Point(5, 23);
            this.buttonAddForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddForm.Name = "buttonAddForm";
            this.buttonAddForm.Size = new System.Drawing.Size(167, 31);
            this.buttonAddForm.TabIndex = 1;
            this.buttonAddForm.Text = "Добавить";
            this.buttonAddForm.UseVisualStyleBackColor = true;
            this.buttonAddForm.Click += new System.EventHandler(this.buttonAddForm_Click);
            // 
            // groupBoxAddSpravka
            // 
            this.groupBoxAddSpravka.Controls.Add(this.groupBox3);
            this.groupBoxAddSpravka.Controls.Add(this.groupBox2);
            this.groupBoxAddSpravka.Location = new System.Drawing.Point(12, 15);
            this.groupBoxAddSpravka.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxAddSpravka.Name = "groupBoxAddSpravka";
            this.groupBoxAddSpravka.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxAddSpravka.Size = new System.Drawing.Size(764, 350);
            this.groupBoxAddSpravka.TabIndex = 2;
            this.groupBoxAddSpravka.TabStop = false;
            this.groupBoxAddSpravka.Text = "Добавить проект";
            this.groupBoxAddSpravka.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.date_end_fact);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.date_end_plan);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.date_start_fact);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.date_start_plan);
            this.groupBox3.Location = new System.Drawing.Point(560, 23);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(189, 305);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Планирование";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Дата завершения факт";
            // 
            // date_end_fact
            // 
            this.date_end_fact.CustomFormat = " ";
            this.date_end_fact.Location = new System.Drawing.Point(19, 240);
            this.date_end_fact.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.date_end_fact.MaxDate = new System.DateTime(2040, 12, 31, 0, 0, 0, 0);
            this.date_end_fact.MinDate = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            this.date_end_fact.Name = "date_end_fact";
            this.date_end_fact.Size = new System.Drawing.Size(145, 22);
            this.date_end_fact.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Дата завершения план";
            // 
            // date_end_plan
            // 
            this.date_end_plan.CustomFormat = " ";
            this.date_end_plan.Location = new System.Drawing.Point(19, 186);
            this.date_end_plan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.date_end_plan.MaxDate = new System.DateTime(2040, 12, 31, 0, 0, 0, 0);
            this.date_end_plan.MinDate = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            this.date_end_plan.Name = "date_end_plan";
            this.date_end_plan.Size = new System.Drawing.Size(145, 22);
            this.date_end_plan.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дата начала факт";
            // 
            // date_start_fact
            // 
            this.date_start_fact.CustomFormat = " ";
            this.date_start_fact.Location = new System.Drawing.Point(20, 110);
            this.date_start_fact.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.date_start_fact.MaxDate = new System.DateTime(2040, 12, 31, 0, 0, 0, 0);
            this.date_start_fact.MinDate = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            this.date_start_fact.Name = "date_start_fact";
            this.date_start_fact.Size = new System.Drawing.Size(144, 22);
            this.date_start_fact.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Дата начала план";
            // 
            // date_start_plan
            // 
            this.date_start_plan.CustomFormat = " ";
            this.date_start_plan.Location = new System.Drawing.Point(19, 60);
            this.date_start_plan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.date_start_plan.MaxDate = new System.DateTime(2040, 12, 31, 0, 0, 0, 0);
            this.date_start_plan.MinDate = new System.DateTime(2000, 12, 31, 0, 0, 0, 0);
            this.date_start_plan.Name = "date_start_plan";
            this.date_start_plan.Size = new System.Drawing.Size(145, 22);
            this.date_start_plan.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.description);
            this.groupBox2.Controls.Add(this.name);
            this.groupBox2.Location = new System.Drawing.Point(17, 23);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(525, 305);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Основная информация";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Описание";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Название*";
            // 
            // description
            // 
            this.description.Location = new System.Drawing.Point(12, 110);
            this.description.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(495, 176);
            this.description.TabIndex = 2;
            this.description.Text = "";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(12, 46);
            this.name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(289, 22);
            this.name.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonDeleteEmployee);
            this.groupBox4.Controls.Add(this.buttonOpenEmployee);
            this.groupBox4.Controls.Add(this.comboBox);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.buttonAddClose);
            this.groupBox4.Controls.Add(this.buttonOpenEmployeeForm);
            this.groupBox4.Controls.Add(this.buttonAddOk);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.dataGridEmployee);
            this.groupBox4.Location = new System.Drawing.Point(8, 369);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(768, 293);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Команда";
            // 
            // buttonDeleteEmployee
            // 
            this.buttonDeleteEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDeleteEmployee.Location = new System.Drawing.Point(552, 149);
            this.buttonDeleteEmployee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDeleteEmployee.Name = "buttonDeleteEmployee";
            this.buttonDeleteEmployee.Size = new System.Drawing.Size(27, 23);
            this.buttonDeleteEmployee.TabIndex = 15;
            this.buttonDeleteEmployee.Text = "-";
            this.buttonDeleteEmployee.UseVisualStyleBackColor = true;
            this.buttonDeleteEmployee.Visible = false;
            this.buttonDeleteEmployee.Click += new System.EventHandler(this.buttonDeleteEmployee_Click);
            // 
            // buttonOpenEmployee
            // 
            this.buttonOpenEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenEmployee.Location = new System.Drawing.Point(552, 122);
            this.buttonOpenEmployee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOpenEmployee.Name = "buttonOpenEmployee";
            this.buttonOpenEmployee.Size = new System.Drawing.Size(27, 23);
            this.buttonOpenEmployee.TabIndex = 14;
            this.buttonOpenEmployee.Text = "+";
            this.buttonOpenEmployee.UseVisualStyleBackColor = true;
            this.buttonOpenEmployee.Visible = false;
            this.buttonOpenEmployee.Click += new System.EventHandler(this.buttonOpenEmployee_Click);
            // 
            // comboBox
            // 
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox.Enabled = false;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(21, 57);
            this.comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(296, 24);
            this.comboBox.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Ответственный*";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // buttonAddClose
            // 
            this.buttonAddClose.Location = new System.Drawing.Point(585, 247);
            this.buttonAddClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddClose.Name = "buttonAddClose";
            this.buttonAddClose.Size = new System.Drawing.Size(167, 31);
            this.buttonAddClose.TabIndex = 6;
            this.buttonAddClose.Text = "Отмена";
            this.buttonAddClose.UseVisualStyleBackColor = true;
            this.buttonAddClose.Visible = false;
            this.buttonAddClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonOpenEmployeeForm
            // 
            this.buttonOpenEmployeeForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenEmployeeForm.Location = new System.Drawing.Point(325, 57);
            this.buttonOpenEmployeeForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOpenEmployeeForm.Name = "buttonOpenEmployeeForm";
            this.buttonOpenEmployeeForm.Size = new System.Drawing.Size(36, 26);
            this.buttonOpenEmployeeForm.TabIndex = 2;
            this.buttonOpenEmployeeForm.Text = "...";
            this.buttonOpenEmployeeForm.UseVisualStyleBackColor = true;
            this.buttonOpenEmployeeForm.Visible = false;
            this.buttonOpenEmployeeForm.Click += new System.EventHandler(this.buttonOpenEmployeeForm_Click);
            // 
            // buttonAddOk
            // 
            this.buttonAddOk.Location = new System.Drawing.Point(585, 210);
            this.buttonAddOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAddOk.Name = "buttonAddOk";
            this.buttonAddOk.Size = new System.Drawing.Size(167, 31);
            this.buttonAddOk.TabIndex = 5;
            this.buttonAddOk.Text = " OK";
            this.buttonAddOk.UseVisualStyleBackColor = true;
            this.buttonAddOk.Visible = false;
            this.buttonAddOk.Click += new System.EventHandler(this.buttonAddOk_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Участники";
            // 
            // dataGridEmployee
            // 
            this.dataGridEmployee.AllowUserToAddRows = false;
            this.dataGridEmployee.AllowUserToDeleteRows = false;
            this.dataGridEmployee.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEmployee.Location = new System.Drawing.Point(21, 122);
            this.dataGridEmployee.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridEmployee.Name = "dataGridEmployee";
            this.dataGridEmployee.ReadOnly = true;
            this.dataGridEmployee.RowHeadersVisible = false;
            this.dataGridEmployee.RowHeadersWidth = 51;
            this.dataGridEmployee.RowTemplate.Height = 24;
            this.dataGridEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridEmployee.Size = new System.Drawing.Size(525, 150);
            this.dataGridEmployee.TabIndex = 3;
            // 
            // buttonMore
            // 
            this.buttonMore.Location = new System.Drawing.Point(5, 125);
            this.buttonMore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMore.Name = "buttonMore";
            this.buttonMore.Size = new System.Drawing.Size(167, 31);
            this.buttonMore.TabIndex = 4;
            this.buttonMore.Text = "Подробнее";
            this.buttonMore.UseVisualStyleBackColor = true;
            this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
            // 
            // Проекты
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 678);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxAddSpravka);
            this.Controls.Add(this.groupBoxSpravka);
            this.Controls.Add(this.groupBoxAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Проекты";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник проектов";
            this.Load += new System.EventHandler(this.Проекты_Load);
            this.groupBoxSpravka.ResumeLayout(false);
            this.groupBoxSpravka.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBoxAction.ResumeLayout(false);
            this.groupBoxAddSpravka.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEmployee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSpravka;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.Button buttonEditForm;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAddForm;
        private System.Windows.Forms.GroupBox groupBoxAddSpravka;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker date_start_plan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker date_end_fact;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker date_end_plan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker date_start_fact;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.DataGridView dataGridEmployee;
        private System.Windows.Forms.Button buttonOpenEmployeeForm;
        private System.Windows.Forms.Button buttonAddClose;
        private System.Windows.Forms.Button buttonAddOk;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonOpenEmployee;
        private System.Windows.Forms.Button buttonDeleteEmployee;
        private System.Windows.Forms.Button buttonMore;
    }
}
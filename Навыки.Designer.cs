namespace otdel_kadrov_beta_v2
{
    partial class Навыки
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEditForm = new System.Windows.Forms.Button();
            this.buttonAddForm = new System.Windows.Forms.Button();
            this.groupBoxAddSpravka = new System.Windows.Forms.GroupBox();
            this.buttonAddClose = new System.Windows.Forms.Button();
            this.buttonAddOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAdd = new System.Windows.Forms.TextBox();
            this.groupBoxSpravka.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBoxAction.SuspendLayout();
            this.groupBoxAddSpravka.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSpravka
            // 
            this.groupBoxSpravka.Controls.Add(this.label3);
            this.groupBoxSpravka.Controls.Add(this.textBoxSearch);
            this.groupBoxSpravka.Controls.Add(this.dataGrid);
            this.groupBoxSpravka.Location = new System.Drawing.Point(12, 13);
            this.groupBoxSpravka.Name = "groupBoxSpravka";
            this.groupBoxSpravka.Size = new System.Drawing.Size(446, 277);
            this.groupBoxSpravka.TabIndex = 8;
            this.groupBoxSpravka.TabStop = false;
            this.groupBoxSpravka.Text = "Справочник навыков";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Поиск";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.ForeColor = System.Drawing.Color.Black;
            this.textBoxSearch.Location = new System.Drawing.Point(10, 46);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(418, 22);
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(10, 78);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(418, 179);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellDoubleClick);
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.buttonDelete);
            this.groupBoxAction.Controls.Add(this.buttonEditForm);
            this.groupBoxAction.Controls.Add(this.buttonAddForm);
            this.groupBoxAction.Location = new System.Drawing.Point(476, 12);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(162, 278);
            this.groupBoxAction.TabIndex = 7;
            this.groupBoxAction.TabStop = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(6, 158);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(150, 31);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDeleteSkill_Click);
            // 
            // buttonEditForm
            // 
            this.buttonEditForm.Location = new System.Drawing.Point(6, 58);
            this.buttonEditForm.Name = "buttonEditForm";
            this.buttonEditForm.Size = new System.Drawing.Size(150, 31);
            this.buttonEditForm.TabIndex = 3;
            this.buttonEditForm.Text = "Редактировать";
            this.buttonEditForm.UseVisualStyleBackColor = true;
            this.buttonEditForm.Click += new System.EventHandler(this.buttonEditSkill_Click);
            // 
            // buttonAddForm
            // 
            this.buttonAddForm.Location = new System.Drawing.Point(6, 21);
            this.buttonAddForm.Name = "buttonAddForm";
            this.buttonAddForm.Size = new System.Drawing.Size(150, 31);
            this.buttonAddForm.TabIndex = 2;
            this.buttonAddForm.Text = "Добавить";
            this.buttonAddForm.UseVisualStyleBackColor = true;
            this.buttonAddForm.Click += new System.EventHandler(this.buttonAddSkill_Click);
            // 
            // groupBoxAddSpravka
            // 
            this.groupBoxAddSpravka.Controls.Add(this.buttonAddClose);
            this.groupBoxAddSpravka.Controls.Add(this.buttonAddOk);
            this.groupBoxAddSpravka.Controls.Add(this.label1);
            this.groupBoxAddSpravka.Controls.Add(this.textBoxAdd);
            this.groupBoxAddSpravka.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAddSpravka.Name = "groupBoxAddSpravka";
            this.groupBoxAddSpravka.Size = new System.Drawing.Size(446, 277);
            this.groupBoxAddSpravka.TabIndex = 2;
            this.groupBoxAddSpravka.TabStop = false;
            this.groupBoxAddSpravka.Text = "Добавить навык";
            this.groupBoxAddSpravka.Visible = false;
            // 
            // buttonAddClose
            // 
            this.buttonAddClose.Location = new System.Drawing.Point(353, 213);
            this.buttonAddClose.Name = "buttonAddClose";
            this.buttonAddClose.Size = new System.Drawing.Size(75, 23);
            this.buttonAddClose.TabIndex = 3;
            this.buttonAddClose.Text = "Отмена";
            this.buttonAddClose.UseVisualStyleBackColor = true;
            this.buttonAddClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonAddOk
            // 
            this.buttonAddOk.Location = new System.Drawing.Point(272, 213);
            this.buttonAddOk.Name = "buttonAddOk";
            this.buttonAddOk.Size = new System.Drawing.Size(75, 23);
            this.buttonAddOk.TabIndex = 2;
            this.buttonAddOk.Text = "ОК";
            this.buttonAddOk.UseVisualStyleBackColor = true;
            this.buttonAddOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Навык";
            // 
            // textBoxAdd
            // 
            this.textBoxAdd.Location = new System.Drawing.Point(10, 51);
            this.textBoxAdd.Name = "textBoxAdd";
            this.textBoxAdd.Size = new System.Drawing.Size(240, 22);
            this.textBoxAdd.TabIndex = 0;
            this.textBoxAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAdd_KeyDown);
            this.textBoxAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAdd_KeyPress);
            // 
            // Навыки
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 303);
            this.Controls.Add(this.groupBoxAddSpravka);
            this.Controls.Add(this.groupBoxSpravka);
            this.Controls.Add(this.groupBoxAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Навыки";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник навыков";
            this.Load += new System.EventHandler(this.Навыки_Load);
            this.groupBoxSpravka.ResumeLayout(false);
            this.groupBoxSpravka.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBoxAction.ResumeLayout(false);
            this.groupBoxAddSpravka.ResumeLayout(false);
            this.groupBoxAddSpravka.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSpravka;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEditForm;
        private System.Windows.Forms.Button buttonAddForm;
        private System.Windows.Forms.GroupBox groupBoxAddSpravka;
        private System.Windows.Forms.TextBox textBoxAdd;
        private System.Windows.Forms.Button buttonAddClose;
        private System.Windows.Forms.Button buttonAddOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
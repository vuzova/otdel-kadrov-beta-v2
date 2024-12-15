namespace otdel_kadrov_beta_v2
{
    partial class Уровни
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
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.groupBoxAddSpravka = new System.Windows.Forms.GroupBox();
            this.buttonAddClose = new System.Windows.Forms.Button();
            this.buttonAddOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAddCoef = new System.Windows.Forms.TextBox();
            this.textBoxAddName = new System.Windows.Forms.TextBox();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEditForm = new System.Windows.Forms.Button();
            this.buttonAddForm = new System.Windows.Forms.Button();
            this.groupBoxSpravka.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBoxAddSpravka.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSpravka
            // 
            this.groupBoxSpravka.Controls.Add(this.label5);
            this.groupBoxSpravka.Controls.Add(this.textBoxSearch);
            this.groupBoxSpravka.Controls.Add(this.dataGrid);
            this.groupBoxSpravka.Location = new System.Drawing.Point(12, 13);
            this.groupBoxSpravka.Name = "groupBoxSpravka";
            this.groupBoxSpravka.Size = new System.Drawing.Size(446, 277);
            this.groupBoxSpravka.TabIndex = 8;
            this.groupBoxSpravka.TabStop = false;
            this.groupBoxSpravka.Text = "Справочник уровней";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Поиск";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(9, 46);
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
            this.dataGrid.Location = new System.Drawing.Point(9, 78);
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
            // groupBoxAddSpravka
            // 
            this.groupBoxAddSpravka.Controls.Add(this.buttonAddClose);
            this.groupBoxAddSpravka.Controls.Add(this.buttonAddOk);
            this.groupBoxAddSpravka.Controls.Add(this.label2);
            this.groupBoxAddSpravka.Controls.Add(this.label1);
            this.groupBoxAddSpravka.Controls.Add(this.textBoxAddCoef);
            this.groupBoxAddSpravka.Controls.Add(this.textBoxAddName);
            this.groupBoxAddSpravka.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAddSpravka.Name = "groupBoxAddSpravka";
            this.groupBoxAddSpravka.Size = new System.Drawing.Size(446, 278);
            this.groupBoxAddSpravka.TabIndex = 2;
            this.groupBoxAddSpravka.TabStop = false;
            this.groupBoxAddSpravka.Text = "Добавить уровень";
            this.groupBoxAddSpravka.Visible = false;
            // 
            // buttonAddClose
            // 
            this.buttonAddClose.Location = new System.Drawing.Point(359, 230);
            this.buttonAddClose.Name = "buttonAddClose";
            this.buttonAddClose.Size = new System.Drawing.Size(75, 23);
            this.buttonAddClose.TabIndex = 11;
            this.buttonAddClose.Text = "Отмена";
            this.buttonAddClose.UseVisualStyleBackColor = true;
            this.buttonAddClose.Click += new System.EventHandler(this.buttonAddClose_Click);
            // 
            // buttonAddOk
            // 
            this.buttonAddOk.Location = new System.Drawing.Point(273, 230);
            this.buttonAddOk.Name = "buttonAddOk";
            this.buttonAddOk.Size = new System.Drawing.Size(75, 23);
            this.buttonAddOk.TabIndex = 10;
            this.buttonAddOk.Text = "ОК";
            this.buttonAddOk.UseVisualStyleBackColor = true;
            this.buttonAddOk.Click += new System.EventHandler(this.buttonAddOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Коэффициент";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Название";
            // 
            // textBoxAddCoef
            // 
            this.textBoxAddCoef.Location = new System.Drawing.Point(11, 119);
            this.textBoxAddCoef.Name = "textBoxAddCoef";
            this.textBoxAddCoef.Size = new System.Drawing.Size(131, 22);
            this.textBoxAddCoef.TabIndex = 7;
            this.textBoxAddCoef.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAddCoef_KeyDown);
            this.textBoxAddCoef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAddCoef_KeyPress);
            // 
            // textBoxAddName
            // 
            this.textBoxAddName.Location = new System.Drawing.Point(11, 55);
            this.textBoxAddName.Name = "textBoxAddName";
            this.textBoxAddName.Size = new System.Drawing.Size(272, 22);
            this.textBoxAddName.TabIndex = 6;
            this.textBoxAddName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxAddName_KeyDown);
            this.textBoxAddName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAddName_KeyPress);
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
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEditForm
            // 
            this.buttonEditForm.Location = new System.Drawing.Point(6, 58);
            this.buttonEditForm.Name = "buttonEditForm";
            this.buttonEditForm.Size = new System.Drawing.Size(150, 31);
            this.buttonEditForm.TabIndex = 3;
            this.buttonEditForm.Text = "Редактировать";
            this.buttonEditForm.UseVisualStyleBackColor = true;
            this.buttonEditForm.Click += new System.EventHandler(this.buttonEditForm_Click);
            // 
            // buttonAddForm
            // 
            this.buttonAddForm.Location = new System.Drawing.Point(6, 21);
            this.buttonAddForm.Name = "buttonAddForm";
            this.buttonAddForm.Size = new System.Drawing.Size(150, 31);
            this.buttonAddForm.TabIndex = 2;
            this.buttonAddForm.Text = "Добавить";
            this.buttonAddForm.UseVisualStyleBackColor = true;
            this.buttonAddForm.Click += new System.EventHandler(this.buttonAddForm_Click);
            // 
            // Уровни
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 319);
            this.Controls.Add(this.groupBoxAddSpravka);
            this.Controls.Add(this.groupBoxSpravka);
            this.Controls.Add(this.groupBoxAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Уровни";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник уровней";
            this.Load += new System.EventHandler(this.Уровни_Load);
            this.groupBoxSpravka.ResumeLayout(false);
            this.groupBoxSpravka.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBoxAddSpravka.ResumeLayout(false);
            this.groupBoxAddSpravka.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
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
        private System.Windows.Forms.Button buttonAddClose;
        private System.Windows.Forms.Button buttonAddOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAddCoef;
        private System.Windows.Forms.TextBox textBoxAddName;
        private System.Windows.Forms.Label label5;
    }
}
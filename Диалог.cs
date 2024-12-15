using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otdel_kadrov_beta_v2
{
    public partial class Диалог : Form
    {
        public event Action<int> SkillAddLevel;
        public Диалог()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SkillAddLevel?.Invoke(Convert.ToInt32(numericLevel.Value));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Диалог_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

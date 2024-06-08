using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JungleMCTS.UI
{
    public partial class ChooseTimeForAutoplayer : Form
    {
        int choosenTime = 0;
        readonly Action<int> func;
        public ChooseTimeForAutoplayer(Action<int> func)
        {
            InitializeComponent();
            this.func = func;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            func(choosenTime);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                choosenTime = (int)numericUpDown1.Value;
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            
        }
    }
}

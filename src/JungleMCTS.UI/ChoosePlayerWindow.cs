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
    public partial class ChoosePlayerWindow : Form
    {
        string firstPlayer, secondPlayer;
        readonly Action<string, string> func;
        public ChoosePlayerWindow(Action<string, string> func)
        {
            InitializeComponent();
            firstPlayer = "";
            secondPlayer = "";
            this.func = func;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            func(firstPlayer, secondPlayer);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == null)
            {
                return;
            }
            firstPlayer = comboBox1.SelectedItem.ToString();
            if (secondPlayer != "" && firstPlayer != "")
            {
                button1.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                return;
            }
            secondPlayer = comboBox2.SelectedItem.ToString();
            if (secondPlayer != "" && firstPlayer != "")
            {
                button1.Enabled = true;
            }
        }
    }
}

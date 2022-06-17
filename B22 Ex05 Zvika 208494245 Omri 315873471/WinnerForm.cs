using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B22_Ex05_Zvika_208494245_Omri_315873471
{
    public partial class WinnerForm : Form
    {
        private string m_WinnerName;
        private Button m_Yes, m_No;
        public WinnerForm(string i_Winner)
        {
            m_WinnerName = i_Winner;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

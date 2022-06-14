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
    public partial class GameBoardForm : Form
    {
        private int m_GameSize;
        public GameBoardForm(string i_GameSize)
        {
            m_GameSize = getBoardSize(i_GameSize);
            InitializeComponent();
        }

        private void GameBoardForm_Load(object sender, EventArgs e)
        {
            
        }

        private int getBoardSize(string i_BoardSize)
        {
            int size = 0;
            switch(i_BoardSize)
            {
                case "6X6":
                    {
                        size = 6;
                        break;
                    }
                case "8X8":
                    {
                        size = 8;
                        break;
                    }
                case "10X10":
                    {
                        size = 10;
                        break;
                    }
            }
            return size;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B22_Ex05_Zvika_208494245_Omri_315873471
{
    public class UpgradedButton : Button
    {
        public Point m_PositionOnBoard{get;set;}
        public bool m_CanEatUpRight = false;
        public bool m_CanEatDownRight = false;
        public bool m_CanEatUpLeft = false;
        public bool m_CanEatDownLeft = false;
        public bool m_CanMoveUpRight = false;
        public bool m_CanMoveDownRight = false;
        public bool m_CanMoveUpLeft = false;
        public bool m_CanMoveDownLeft = false;
        
        public UpgradedButton(Point i_PointOnBoard)
        {
            m_PositionOnBoard = i_PointOnBoard;
        }

        public bool CanMove
        {
            get
            {
                bool canMove = m_CanMoveUpRight || m_CanMoveDownRight || m_CanMoveUpLeft || m_CanMoveDownLeft;
                bool canEat = m_CanEatDownLeft || m_CanEatUpLeft || m_CanEatDownRight || m_CanEatUpRight;
                return (canEat || canMove);
            }
        }
    }
}

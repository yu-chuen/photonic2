using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GT_App
{
    public partial class UserCtrl_PortHeader : UserControl
    {
        private const int BAR_HEIGHT = 28;
        public UserCtrl_PortHeader()
        {
            InitializeComponent();
        }

        public void Set_Sizes(int iInW, int iOutW, int iTotalHeight)
        {
            this.Size = new Size(iInW + 50 + iOutW, iTotalHeight);

            lbl_DI.Location = new Point(0, 2);
            lbl_DI.Size = new Size(iInW, BAR_HEIGHT);
            
            lbl_DO.Location = new Point(iInW + 50, 2);
            lbl_DO.Size = new Size(iOutW, BAR_HEIGHT);

            lbl_Line.Location = new Point(iInW + 24, 2);
            lbl_Line.Size = new Size(2, iTotalHeight - 2);
        }

        public void Set_Texts(String strDI, String strDO)
        {
            lbl_DI.Text = strDI;
            lbl_DO.Text = strDO;
        }
    }
}

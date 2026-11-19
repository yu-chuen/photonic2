using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
    public partial class AlignmentData_Dlg : Form
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl;
        System.Windows.Forms.Timer m_Timer = new Timer();

        const String VALUE_FORMAT = "0.000";

        //value labels grouped in the order x_yaw, z_yaw, y_pitch, z_pitch, x_roll, y_roll
        Label[] m_lblPreset_L;
        Label[] m_lblPreset_C;
        Label[] m_lblPreset_P;
        Label[] m_lblBD_L;
        Label[] m_lblBD_C;
        Label[] m_lblBD_P;
        Label[] m_lblBD_R;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public AlignmentData_Dlg()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            m_lblPreset_L = new Label[] { lbl_L_X_Yaw, lbl_L_Z_Yaw, lbl_L_Y_Pitch, lbl_L_Z_Pitch, lbl_L_X_Roll, lbl_L_Y_Roll };
            m_lblPreset_C = new Label[] { lbl_C_X_Yaw, lbl_C_Z_Yaw, lbl_C_Y_Pitch, lbl_C_Z_Pitch, lbl_C_X_Roll, lbl_C_Y_Roll };
            m_lblPreset_P = new Label[] { lbl_P_X_Yaw, lbl_P_Z_Yaw, lbl_P_Y_Pitch, lbl_P_Z_Pitch, lbl_P_X_Roll, lbl_P_Y_Roll };
            m_lblBD_L = new Label[] { lbl_BD_L_X_Yaw, lbl_BD_L_Z_Yaw, lbl_BD_L_Y_Pitch, lbl_BD_L_Z_Pitch, lbl_BD_L_X_Roll, lbl_BD_L_Y_Roll };
            m_lblBD_C = new Label[] { lbl_BD_C_X_Yaw, lbl_BD_C_Z_Yaw, lbl_BD_C_Y_Pitch, lbl_BD_C_Z_Pitch, lbl_BD_C_X_Roll, lbl_BD_C_Y_Roll };
            m_lblBD_P = new Label[] { lbl_BD_P_X_Yaw, lbl_BD_P_Z_Yaw, lbl_BD_P_Y_Pitch, lbl_BD_P_Z_Pitch, lbl_BD_P_X_Roll, lbl_BD_P_Y_Roll };
            m_lblBD_R = new Label[] { lbl_BD_R_X_Yaw, lbl_BD_R_Z_Yaw, lbl_BD_R_Y_Pitch, lbl_BD_R_Z_Pitch, lbl_BD_R_X_Roll, lbl_BD_R_Y_Roll };

            Update_UI(null, null);

            //set timer
            m_Timer.Interval = 200;
            m_Timer.Tick += new EventHandler(Update_UI);
            m_Timer.Start();
        }

        //buttons control//////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AlignmentData_Dlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Timer.Stop();
            m_Timer.Dispose();
        }

        //Update UI//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_UI(object sender, EventArgs e)
        {
            Module_AlignmentData d = m_pSeqMachineCtrl.m_Module_AlignmentData;
            if (d == null) return;

            double[] l = { d.m_dL_X_Yaw, d.m_dL_Z_Yaw, d.m_dL_Y_Pitch, d.m_dL_Z_Pitch, d.m_dL_X_Roll, d.m_dL_Y_Roll };
            double[] c = { d.m_dC_X_Yaw, d.m_dC_Z_Yaw, d.m_dC_Y_Pitch, d.m_dC_Z_Pitch, d.m_dC_X_Roll, d.m_dC_Y_Roll };
            double[] p = { d.m_dP_X_Yaw, d.m_dP_Z_Yaw, d.m_dP_Y_Pitch, d.m_dP_Z_Pitch, d.m_dP_X_Roll, d.m_dP_Y_Roll };

            //pre-set
            Set_Values(m_lblPreset_L, l);
            Set_Values(m_lblPreset_C, c);
            Set_Values(m_lblPreset_P, p);

            //input
            Set_Value(lbl_In_Rot_Yaw, d.m_dIn_Yaw);
            Set_Value(lbl_In_Rot_Pitch, d.m_dIn_Pitch);
            Set_Value(lbl_In_Rot_Roll, d.m_dIn_Roll);
            Set_Value(lbl_In_ExtTrans_X, d.m_dIn_ExtTrans_X);
            Set_Value(lbl_In_ExtTrans_Y, d.m_dIn_ExtTrans_Y);
            Set_Value(lbl_In_ExtTrans_Z, d.m_dIn_ExtTrans_Z);
            Set_Value(lbl_In_IntTrans_X, d.m_dIn_IntTrans_X);
            Set_Value(lbl_In_IntTrans_Y, d.m_dIn_IntTrans_Y);
            Set_Value(lbl_In_IntTrans_Z, d.m_dIn_IntTrans_Z);

            //output
            Set_Value(lbl_Out_Rot_Yaw, d.m_dOut_Yaw);
            Set_Value(lbl_Out_Rot_Pitch, d.m_dOut_Pitch);
            Set_Value(lbl_Out_Rot_Roll, d.m_dOut_Roll);
            Set_Value(lbl_Out_Trans_X, d.m_dOut_dX);
            Set_Value(lbl_Out_Trans_Y, d.m_dOut_dY);
            Set_Value(lbl_Out_Trans_Z, d.m_dOut_dZ);

            //offset breakdown r = l + c + p
            Set_Values(m_lblBD_L, l);
            Set_Values(m_lblBD_C, c);
            Set_Values(m_lblBD_P, p);
            for (int i = 0; i < 6; i++) Set_Value(m_lblBD_R[i], l[i] + c[i] + p[i]);

            //String strUpdated = "Last updated: " + d.m_dtLastUpdated.ToString("dd/MM/yyyy HH:mm:ss");
            //if (lbl_LastUpdated.Text != strUpdated) lbl_LastUpdated.Text = strUpdated;
        }

        private static void Set_Values(Label[] lbls, double[] dValues)
        {
            for (int i = 0; i < lbls.Length; i++) Set_Value(lbls[i], dValues[i]);
        }

        private static void Set_Value(Label lbl, double dValue)
        {
            String strText = dValue.ToString(VALUE_FORMAT);
            if (lbl.Text != strText) lbl.Text = strText;       //only repaint when the value changes
        }
    }
}

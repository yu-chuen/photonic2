using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class Module_AlignmentData
    {
        SeqMachineControl m_SeqMachineCtrl;
        //public string m_strLabel = "Module Alignnment Data";

        //Pre-set : OTC from ITC (l)-----------------------------------------------------------------------------------
        public double m_dL_X_Yaw = 0;
        public double m_dL_Z_Yaw = -6.40423;
        public double m_dL_Y_Pitch = 1.524048;
        public double m_dL_Z_Pitch = -0.76515;
        public double m_dL_X_Roll = 0;
        public double m_dL_Y_Roll = 6.600001;

        //Pre-set : RTC from OTC (c)-----------------------------------------------------------------------------------
        public double m_dC_X_Yaw = 0;
        public double m_dC_Z_Yaw = 0;
        public double m_dC_Y_Pitch = 0;
        public double m_dC_Z_Pitch = 0;
        public double m_dC_X_Roll = 0;
        public double m_dC_Y_Roll = 0;

        //Pre-set : PTC from RTC (p)-----------------------------------------------------------------------------------
        public double m_dP_X_Yaw = 0;
        public double m_dP_Z_Yaw = 0;
        public double m_dP_Y_Pitch = 0;
        public double m_dP_Z_Pitch = 0;
        public double m_dP_X_Roll = 0;
        public double m_dP_Y_Roll = 0;

        //Input : Extrinsic rotation (deg)-----------------------------------------------------------------------------
        public double m_dIn_Yaw = 7;        //θy
        public double m_dIn_Pitch = -66;      //θp
        public double m_dIn_Roll = -23;      //θr

        //Input : Extrinsic translation (mm)---------------------------------------------------------------------------
        public double m_dIn_ExtTrans_X = 0;        //d(x_e)
        public double m_dIn_ExtTrans_Y = 0;        //d(y_e)
        public double m_dIn_ExtTrans_Z = 0;        //d(z_e)

        //Input : Intrinsic translation (mm)---------------------------------------------------------------------------
        public double m_dIn_IntTrans_X = -26;      //d(x)
        public double m_dIn_IntTrans_Y = 15;       //d(y)
        public double m_dIn_IntTrans_Z = -21;      //d(z)

        //Output : Machine rotation (deg)------------------------------------------------------------------------------
        public double m_dOut_Yaw = 0;        //θy_m
        public double m_dOut_Pitch = 0;        //θp_m
        public double m_dOut_Roll = 0;        //θr_m

        //Output : Machine translation (mm)----------------------------------------------------------------------------
        public double m_dOut_dX = 0;        //Δx
        public double m_dOut_dY = 0;        //Δy
        public double m_dOut_dZ = 0;        //Δz

        //Time the backend last updated the values
        public DateTime m_dtLastUpdated = DateTime.Now;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public Module_AlignmentData(SeqMachineControl mSeqMachineCtrl)
        {
            //m_SeqMachineCtrl = mSeqMachineCtrl;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Offset breakdown r = l + c + p
        public double Get_R_X_Yaw() { return m_dL_X_Yaw + m_dC_X_Yaw + m_dP_X_Yaw; }
        public double Get_R_Z_Yaw() { return m_dL_Z_Yaw + m_dC_Z_Yaw + m_dP_Z_Yaw; }
        public double Get_R_Y_Pitch() { return m_dL_Y_Pitch + m_dC_Y_Pitch + m_dP_Y_Pitch; }
        public double Get_R_Z_Pitch() { return m_dL_Z_Pitch + m_dC_Z_Pitch + m_dP_Z_Pitch; }
        public double Get_R_X_Roll() { return m_dL_X_Roll + m_dC_X_Roll + m_dP_X_Roll; }
        public double Get_R_Y_Roll() { return m_dL_Y_Roll + m_dC_Y_Roll + m_dP_Y_Roll; }
    }
}

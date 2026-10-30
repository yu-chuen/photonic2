/******************************************************************************************************************************************************************
 * Watch_Timer Class
 * Desc:    Timer for data collection. Count start and stop time and etc...
 *                Timer for global stop watch and reset flag timers.
 ******************************************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace StaticCoreLib
{
    public class Watch_Timer
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
        Data_UnitTempStorage m_DataStore = null;

        TimerCallback m_tCallback = new TimerCallback(Timer_Callback);
        System.Threading.Timer m_Timer;

        DateTime m_Dt_ProcessStart = new DateTime();
        DateTime m_Dt_ProcessStop = new DateTime();

        TimeSpan m_ts_TotalRun = new TimeSpan();
        TimeSpan m_ts_TotalStop = new TimeSpan();

        bool m_bFlag_1stStarted = false;
        private readonly object m_lockFlag_1stStarted = new object();
        private bool Flag_1stStarted { set { lock (m_lockFlag_1stStarted) { m_bFlag_1stStarted = value; } } get { lock (m_lockFlag_1stStarted) { return m_bFlag_1stStarted; } } }

        //Constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Watch_Timer(SeqMachineControl mSeqMachineCtrl)
        {
            m_SeqMachineControl = mSeqMachineCtrl;
            m_DataStore = m_SeqMachineControl.m_DataUnit;

            m_Timer = new System.Threading.Timer (m_tCallback, this, 0, 1000);

            Reset_Time();
        }

        //Clean Up: Stop all timers///////////////////////////////////////////////////
        public void Clean_Up()
        {
            m_Timer.Dispose();
        }

        
        //Set Start Process Time////////////////////////////////////////////////////
        public void Set_StartTime()
        {
            m_Dt_ProcessStart = DateTime.Now;
            m_DataStore.ProcessRun_Start = m_Dt_ProcessStart.ToString("hh:mm:ss");

            Flag_1stStarted = true;
        }

        //Set Stop Process Time///////////////////////////////////////////////////
        public void Set_StopTime()
        {
            m_Dt_ProcessStop = DateTime.Now;
            m_DataStore.ProcessRun_Stop = m_Dt_ProcessStop.ToString("hh:mm:ss");
        }

        //Reset timer///////////////////////////////////////////////////////////////////////
        public void Reset_Time()
        {
            m_ts_TotalRun = TimeSpan.Zero;
            m_ts_TotalStop = TimeSpan.Zero;
        }

        //Call back timer//////////////////////////////////////////////////////////////////
        static void Timer_Callback(object info)
        {
            Watch_Timer wt = (Watch_Timer)info;
            wt.Update_Param();
        }

        //Update parameters///////////////////////////////////////////////////////
        public void Update_Param()
        {
            DateTime mDT = DateTime.Now;

            //Update total run time
            if (m_SeqMachineControl.Flag_InitOK && Flag_1stStarted == true)
            {
                if (m_SeqMachineControl.Flag_Run == true)
                {
                    m_ts_TotalRun = m_ts_TotalRun.Add(TimeSpan.FromSeconds(1));
                    m_DataStore.Total_RunTime = m_ts_TotalRun.Hours.ToString() + ":" + m_ts_TotalRun.Minutes.ToString() + ":" + m_ts_TotalRun.Seconds.ToString();
                }
                //Update total stop time
                else
                {
                    m_ts_TotalStop = m_ts_TotalStop.Add(TimeSpan.FromSeconds(1));
                    m_DataStore.Total_StopTime = m_ts_TotalStop.Hours.ToString() + ":" + m_ts_TotalStop.Minutes.ToString() + ":" + m_ts_TotalStop.Seconds.ToString();
                }
            }
        }
    }
}

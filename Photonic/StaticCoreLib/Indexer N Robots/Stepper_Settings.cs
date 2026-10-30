using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    //Stepper data storing class
    [Serializable]
    public class Stepper_Settings
    {
        public int      m_iVelocity				    = 10000;	//velocity of the motor
        public int      m_iHomeDir				    = 1;		//home direction
        public int      m_iHomeVelocity			    = 10000;	//Homing Max Velocity
		public int      m_iTimeOut				    = 5000;		//time out for motor movement
        public int      m_iTimeOutHome			    = 10000;	//time out for homing
        public double   m_dbPulsePerRevolution      = 1000;	    //pulse per revolution
        public double   m_dbmmPerRevolution	        = 1000;		//mm per revolution
		public int      m_iAcceleration			    = 100000;	//Acceleration
		public int      m_iDeceleration			    = 100000;	//Deceleration
        public int      m_iHomeAcceleration         = 100000;	//Home Acceleration
        public int      m_iHomeVO                   = 50000;	//Homing Velocity

		public int      m_iMainSpeed_Multiplier	    = 100;		//Global set all speed.
		public int      m_iPositionMultiplier	    = 0;		//10-powered based. to shift the ethercat int value to double
    }
}

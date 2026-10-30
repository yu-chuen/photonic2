using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
	interface INDEXER_Interface
	{

		/*******************************************************************************************
		 * Set Motor Settings
		 *******************************************************************************************/
		/// <summary>
		/// Set Home direction (+ / -)
		/// </summary>
		void Set_HomeDir( int value );

		/// <summary>
		/// set Velocity for both Homing speed and motor speed (non percentage. depends on the motor spec)
		/// </summary>
		void Set_Velocity( int iHomeSpeed, int iVelocity );

		/// <summary>
		/// set the accelaration and deccelaration
		/// </summary>
		void Set_AccDec( int iAcc, int iDec );

		/// <summary>
		/// Set Stepper label
		/// </summary>
		void Set_Label( String strLabel );

		/// <summary>
		/// Set the number of pulse per full revolution
		/// </summary>
		void Set_PulsePerRevolution( int pulse );

		/// <summary>
		/// Set the length (mm) of a full revolution
		/// </summary>
		void Set_mmPerRevolution( double dbmm );

		/// <summary>
		/// Select Motor Speed, iRunSpeed=1=>default (primary)
		/// </summary>
		void Set_RunSpeed( int iRunSpeed );

		/// <summary>
		/// Update this class's variables according to the motor setting file
		/// </summary>
		void Update_ToStepper( Stepper_Settings mSettings );


		/*******************************************************************************************
		 * Movement Control
		 *******************************************************************************************/
		/// <summary>
		/// Turn on and off the motor
		/// </summary>
		void Servo_On( int iServoOn );

		/// <summary>
		/// Initalize the motor to home (0) position
		/// </summary>
		int Home();

		/// <summary>
		/// Move relative / Jogging
		/// </summary>
		int MoveRel( int iPulseValue );

		/// <summary>
		/// Move absolute / goto position
		/// </summary>
		int MoveAbs( int iPulsePosition );

		/// <summary>
		/// Stop Motor
		/// </summary>
		void Stop();

		/// <summary>
		/// Reset the alarm of the motor
		/// </summary>
		/// <returns></returns>
		int Reset_Alarm();



		/*******************************************************************************************
		 * Get Motor Settings
		 *******************************************************************************************/
		/// <summary>
		/// Get the motor status and update the motor status variables such as home, servo on or off, in position bit and etc.
		/// </summary>
		int GetMotorStatus();

		/// <summary>
		/// Get Motor position in pulse
		/// </summary>
		double Get_MotorPosition_Pulse();

		/// <summary>
		/// Get Motor position in mm
		/// </summary>
		double Get_MotorPosition_mm();

		/// <summary>
		/// Get the min step
		/// </summary>
		double GetMinStep();

		/// <summary>
		/// Convert Pulse to mm
		/// </summary>
		double PulseToMm( double iPulse );

		/// <summary>
		/// Convert mm to pulse
		/// </summary>
		int MmToPulse( double dbMm );



	}
}

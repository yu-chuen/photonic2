/****************************************************************************/
/*  Copyright (c) 2011, ADLink Technology Inc.  All rights reserved.        */
/*                                                                          */
/*  File Name   :   HSL_Motion.cs                                           */
/*  Purpose     :   Header file for HSL_Motion.C module                     */
/*  Date        :   2011/4/11                                               */
/*  Programmer  :   Chang-Zhi Lin	                                        */
/****************************************************************************/


using System;
//using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;


namespace HSL_Motion_W32
{
	public class HSL_Motion
	{
	[DllImport("HSL.dll")]public static extern Int16  HSL_M_get_version(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, ref System.Byte CPLD_Ver, ref System.UInt32 Kernel_Ver, ref System.UInt32 Software_Ver);
		
		
		// Pulse I/O Configuration
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_pls_outmode(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 pls_outmode);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_pls_iptmode(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 pls_iptmode, System.Int16 pls_logic);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_feedback_src(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 Src);


    // Velocity Mode motion
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_tv_move(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double StrVel, System.Double MaxVel, System.Double Tacc);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_sv_move(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double SVacc);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_v_change(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double NewVel, System.Double Tacc);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_sd_stop(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_emg_stop(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_fix_speed_range(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_unfix_speed_range(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_get_current_speed(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Double speed);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_verify_speed(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double StrVel, System.Double MaxVel, ref System.Double minAccTime, ref System.Double maxAccT, System.Double MaxSpeed);


    // Single Axis Motion
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_tr_move(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Dist, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_ta_move(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Pos, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sr_move(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Dist, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sa_move(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Pos, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_move_ratio(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double move_ratio);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_get_move_ratio(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Double move_ratio);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_p_change(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double NewPos);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_backlash_comp(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 BCompPulse, System.Int16 Mode);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_suppress_vibration(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.UInt16 T1, System.UInt16 T2);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_idle_pulse(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 idl_pulse);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_fa_speed(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double FA_Speed);


    // Point Table
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_point_data(System.Int16 card_ID, System.Int16 connect_index, System.Int16 Dimension, ref System.Int16 AxisArray, System.Int16 point_index, System.Int16 cmd_func, ref System.Double DistArray, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_set_arc_data(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Int16 point_index, System.Int16 cmd_func, ref System.Double CenterArray, ref System.Double EndArray, System.Int16 DIR, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_move_point_range(System.Int16 card_ID, System.Int16 connect_index, System.Int16 Dimension, ref System.Int16 AxisArray, System.Int16 start_index, System.Int16 last_index);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_move_point_table(System.Int16 card_ID, System.Int16 connect_index, System.Int16 Dimension, ref System.Int16 AxisArray, ref System.Int16 PointArray, System.Int16 total_points);


    // Linear Interpolated Motion
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_tr_move_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistX, System.Double DistY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_ta_move_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosX, System.Double PosY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sr_move_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistX, System.Double DistY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sa_move_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosX, System.Double PosY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_tr_move_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistX, System.Double DistY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_ta_move_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosX, System.Double PosY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sr_move_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistX, System.Double DistY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sa_move_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosX, System.Double PosY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_tr_line2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double DistX, System.Double DistY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_ta_line2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double PosX, System.Double PosY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sr_line2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double DistX, System.Double DistY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sa_line2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double PosX, System.Double PosY, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_tr_line3(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double DistX, System.Double DistY, System.Double DistZ, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_ta_line3(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double PosX, System.Double PosY, System.Double PosZ, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sr_line3(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double DistX, System.Double DistY, System.Double DistZ, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sa_line3(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double PosX, System.Double PosY, System.Double PosZ, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_tr_line4(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistX, System.Double DistY, System.Double DistZ, System.Double DistU, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec); 
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_ta_line4(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosX, System.Double PosY, System.Double PosZ, System.Double PosU, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sr_line4(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistX, System.Double DistY, System.Double DistZ, System.Double DistU, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16  HSL_M_start_sa_line4(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosX, System.Double PosY, System.Double PosZ, System.Double PosU, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    
    
    // Circular Interpolation Motion    
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_r_arc_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_a_arc_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_r_arc_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_a_arc_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_r_arc2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_a_arc2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double MaxVel);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_tr_arc_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_ta_arc_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_sr_arc_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_sa_arc_xy(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_tr_arc_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_ta_arc_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_sr_arc_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_sa_arc_zu(System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_tr_arc2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_ta_arc2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_sr_arc2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);
    [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_sa_arc2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec);


    // Home Return Mode
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_home_config(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 home_mode, System.Int16 org_logic, System.Int16 ez_logic, System.Int16 ez_count, System.Int16 erc_out);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_org_offset(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double ORG_ESC_Offset);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_home_move(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double StrVel, System.Double MaxVel, System.Double Tacc);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_escape_home(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double StrVel, System.Double MaxVel, System.Double Tacc);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_home_search(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double ORGOffset);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_home_move_all(System.Int16 card_ID,  System.Int16 connect_index, System.Int16 TotalAxes, ref System.Int16 AxisArray, ref System.Double StrVelA, ref System.Double MaxVelA, ref System.Double TaccA, System.Int16 mode);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_home_move_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 FirstAxisNo);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_home_search_all(System.Int16 card_ID,  System.Int16 connect_index, System.Int16 TotalAxes, ref System.Int16 AxisArray, ref System.Double StrVelA, ref System.Double MaxVelA, ref System.Double TaccA, ref System.Double ORGOffset, System.Int16 mode);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_home_search_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 FirstAxisNo);


   // Motion Status
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_motion_done(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_abnormal_stop_status(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Int32 status);
   
     
   // Motion Interface I/O
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_alm(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 alm_logic, System.Int16 alm_mode);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_el(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 el_mode);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_el_logic(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 el_logic);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_inp(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 inp_enable, System.Int16 inp_logic);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_erc(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 erc_logic, System.Int16 erc_on_time);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_servo(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 on_off);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_sd(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 enable, System.Int16 sd_logic, System.Int16 sd_latch, System.Int16 sd_mode);                               
   
   
   // Motion I/O Monitoring
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_io_status(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.UInt16 io_sts);
   
   
   // Position Control and Counters
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_position(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Double pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_position(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_command(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Int32 cmd);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_command(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int32 cmd);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_error_counter(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Int16 error_counter);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_reset_error_counter(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_general_counter(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Double CntValue);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_general_counter(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 CntSrc, System.Double CntValue);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_target_pos(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Double pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_reset_target_pos(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_rest_command(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Int32 rest_command);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_check_rdp(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Int32 rdp_command);
   
   
   //Only 7853/4 card & 8ID 4XMO Module use
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_position_ex(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Double pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_io_status_ex(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.UInt16 io_sts);


   // Position Compare and Latch
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_ltc_logic(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 ltc_logic);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_get_latch_data(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 LatchNo, ref System.Double Pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_soft_limit(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int32 PLimit, System.Int32 NLimit);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_enable_soft_limit(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 Action);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_disable_soft_limit(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_error_counter_check(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 Tolerance, System.Int16 On_Off);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_general_comparator(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 CmpSrc, System.Int16 CmpMethod, System.Int16 CmpAction, System.Double Data);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_trigger_comparator(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 CmpSrc, System.Int16 CmpMethod, System.Double Data);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_trigger_logic(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 Logic);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_check_compare_data(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 CompType, ref System.Double Pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_check_compare_status(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.UInt16 cmp_sts);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_build_compare_table(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, ref System.Double TableArray, System.Int16 ArraySize);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_build_compare_function(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double StartPosition, System.Double EndPosition, System.Double Interval);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_auto_compare(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 Enable);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_compare_dir(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 Inverse);


   // Multiple Axes Simultaneous Operation
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_tr_move_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 TotalAxes, ref System.Int16 AxisArray, ref System.Double DistA, ref System.Double StrVelA, ref System.Double MaxVelA, ref System.Double TaccA, ref System.Double TdecA);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_ta_move_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 TotalAxes, ref System.Int16 AxisArray, ref System.Double PosA, ref System.Double StrVelA, ref System.Double MaxVelA, ref System.Double TaccA, ref System.Double TdecA);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_sa_move_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 TotalAxes, ref System.Int16 AxisArray, ref System.Double PosA, ref System.Double StrVelA, ref System.Double MaxVelA, ref System.Double TaccA, ref System.Double TdecA, ref System.Double SVaccA, ref System.Double SVdecA);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_sr_move_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 TotalAxes, ref System.Int16 AxisArray, ref System.Double DistA, ref System.Double StrVelA, ref System.Double MaxVelA, ref System.Double TaccA, ref System.Double TdecA, ref System.Double SVaccA, ref System.Double SVdecA);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_start_move_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 FirstAxisNo);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_stop_move_all(System.Int16 card_ID, System.Int16 connect_index, System.Int16 FirstAxisNo);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_sync_option(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 sync_stop_on, System.Int16 cstop_output_on, System.Int16 sync_option1, System.Int16 sync_option2);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_sync_stop_mode(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Int16 stop_mode);
   
   
   // General-purposed DIO
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_write_do(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.UInt16 value);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_read_di(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo);
   
   //ODM Gantry function
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_config( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Int16 AxesGroup, System.Int16 LinearAxis, System.Int16 enable );

   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_tr_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Dist, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_ta_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Pos, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_sr_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Dist, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_sa_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double Pos, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec, System.Double SVacc, System.Double SVdec );

   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_tv_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double StrVel, System.Double MaxVel, System.Double Tacc );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_sv_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double SVacc );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_stop_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No );

   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_tr_linear_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistG, System.Double DistL, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_ta_linear_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosG, System.Double PosL, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_sr_linear_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double DistG, System.Double DistL, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec , System.Double SVacc, System.Double SVdec );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_sa_linear_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double PosG, System.Double PosL, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double Tdec , System.Double SVacc, System.Double SVdec );

   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_home_config( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Int16 home_mode, System.Int16 org_logic, System.Int16 ez_logic, System.Int16 ez_count, System.Int16 erc_out);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_home_move( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double StrVel, System.Double MaxVel, System.Double Tacc);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_home_search( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Double StrVel, System.Double MaxVel, System.Double Tacc, System.Double ORGOffset);

   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_get_errcmd( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, ref System.Int32 errcmd );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_get_errpos( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, ref System.Int32 errpos );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_get_maxerrcmd( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, ref System.Int32 maxerrcmd );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_get_maxerrpos( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, ref System.Int32 maxerrpos );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_reset_maxerrcmd( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_reset_maxerrpos( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_set_deviation( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, System.Int32 deviation );
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_GTY_get_deviation( System.Int16 card_ID, System.Int16 connect_index, System.Int16 M_No, ref System.Int32 deviation );
   
   
   // Parameter Setting
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_str_velocity(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double StrVel);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_max_velocity(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double MaxVel);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_arc_velocity(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Arc_MaxVel);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_Tacc(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Tacc);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_Tdec(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Tdec);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_SVacc(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double SVacc);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_set_SVdec(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double SVdec);
   
   
   // Single Axis Motion
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_t_distance(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Dist);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_t_position(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Pos);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_s_distance(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Dist);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_s_position(System.Int16 card_ID, System.Int16 connect_index, System.Int16 AxisNo, System.Double Pos);
      
      
   // Linear Interpolated Motion
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_t_distance2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double DistX, System.Double DistY);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_t_position2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double PosX, System.Double PosY);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_s_distance2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double DistX, System.Double DistY);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_s_position2(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double PosX, System.Double PosY); 
      
      
   // Circular Interpolation Motion
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_t_arc2_distance(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_t_arc2_position(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_s_arc2_distance(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double OffsetCx, System.Double OffsetCy, System.Double OffsetEx, System.Double OffsetEy, System.Int16 DIR);
   [DllImport("HSL.dll")]public static extern Int16 HSL_M_move_s_arc2_position(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 AxisArray, System.Double Cx, System.Double Cy, System.Double Ex, System.Double Ey, System.Int16 DIR);       
        
        
                                    				
	}
	

    
}


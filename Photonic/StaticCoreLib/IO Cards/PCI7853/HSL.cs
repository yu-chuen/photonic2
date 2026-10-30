/***************************************************************************
   Copyright (c) 2011 ADLink Technology Inc.  All rights reserved.   
                                                                          
   File Name   :   HSL.cs		                                            
   Purpose     :   Header file for HSL-init.C module                       
   Date        :   2011/4/8
   Programmer  :   Chang-Zhi Lin	                                            
***************************************************************************/

using System;
//using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;





namespace HSL_W32
{
	public class HSL
	{
				
		// Initialization and System Information Section
    [DllImport("HSL.dll")]public static extern Int16  HSL_initial(System.Int16 card_ID);
    [DllImport("HSL.dll")]public static extern Int16  HSL_close(System.Int16 card_ID);
    [DllImport("HSL.dll")]public static extern Int16  HSL_auto_start(System.Int16 card_ID, System.Int16 connect_index);
    [DllImport("HSL.dll")]public static extern Int16  HSL_start(System.Int16 card_ID, System.Int16 connect_index, System.Int16 max_slave_No);
    [DllImport("HSL.dll")]public static extern Int16  HSL_stop(System.Int16 card_ID, System.Int16 connect_index);
    [DllImport("HSL.dll")]public static extern Int16  HSL_connect_status(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 sts_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_slave_live(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 live_data); 
    [DllImport("HSL.dll")]public static extern void   HSL_get_irq_channel(System.Int16 card_ID, ref System.Int16 irq_no); 
    [DllImport("HSL.dll")]public static extern Int16  HSL_get_slave_type(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.UInt16 type_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_get_slave_ID_types(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 type_data, ref System.Int16 count);
    [DllImport("HSL.dll")]public static extern Int16  HSL_get_start_axisno(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 start_axis);  
    [DllImport("HSL.dll")]public static extern Int16  HSL_set_scan_condition(System.Int16 card_ID, System.Int16 connect_index, System.Int16 comm_type, System.Int16 transfer_rate, System.Int16 hub_number);
    [DllImport("HSL.dll")]public static extern Int16  HSL_get_scan_condition(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 comm_type, ref System.Int16 transfer_rate, ref System.Int16 hub_number); /* ONLY FOR 7853/54) */
    
    
    /* Reserved function */
    [DllImport("HSL.dll")]public static extern Int16  HSL_initial_without_reset(System.Int16 card_ID);
    [DllImport("HSL.dll")]public static extern Int16  HSL_auto_start_without_reset(System.Int16 card_ID, System.Int16 connect_index);
    [DllImport("HSL.dll")]public static extern Int16  HSL_start_without_reset(System.Int16 card_ID, System.Int16 connect_index, System.Int16 max_slave_No);
    [DllImport("HSL.dll")]public static extern Int16  HSL_initial_sw(ref System.Int32 card_ID_inBit, System.Int16 sw_enable );
    [DllImport("HSL.dll")]public static extern Int16  HSL_get_version(System.Int16 card_ID, ref System.Int32 Firmware_Version, ref System.Int32 Driver_Version, ref System.Int32 Software_Version);
    [DllImport("HSL.dll")]public static extern Int16  HSL_set_handshake_method(System.Int16 card_ID, System.Int16 connect_index, System.Int16 Handshake_Method); 
    [DllImport("HSL.dll")]public static extern Int16  HSL_get_handshake_method(System.Int16 card_ID, System.Int16 connect_index, ref System.Int16 Handshake_Method);  
    
    
    // Timer Control Section
    [DllImport("HSL.dll")]public static extern Int16  HSL_set_timer(System.Int16 card_ID, System.Int16 c1, System.Int16 c2);	/* ONLY FOR 7851/2 & RTX don't support */		      
    //I16 FNTYPE HSL_enable_timer_interrupt(I16 card_ID, HANDLE *phEvent);  
    [DllImport("HSL.dll")]public static extern Int16  HSL_disable_timer_interrupt(System.Int16 card_ID);/* ONLY FOR 7851/2 & RTX don't support */
    [DllImport("HSL.dll")]public static extern Int16  HSL_set_int_timer(System.Int16 card_ID, System.UInt16 c1); /* ONLY FOR 7853/54 */		 
    [DllImport("HSL.dll")]public static extern Int16  HSL_set_int_timer_enable(System.Int16 card_ID, System.Int16 enable); /* ONLY FOR 7853/54 */
    [DllImport("HSL.dll")]public static extern Int16  HSL_wait_timer_interrupt(System.Int16 card_ID, System.Int32 time_out_ms);	/* ONLY FOR 7853/54 */
       
                                                    
    // DIO
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_write_output(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.UInt32 out_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_write_channel_output(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 channel, System.UInt16 out_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_read_input(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.UInt32 in_data); 
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_read_channel_input(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 channel, ref System.UInt16 in_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_read_output(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.UInt32 out_data_in_ram);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_read_all_slave_input(System.Int16 card_ID, System.Int16 connect_index, ref System.UInt16 in_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_write_all_slave_output(System.Int16 card_ID, System.Int16 connect_index, ref System.UInt16 out_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_set_input_logic(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 input_logic);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_set_output_logic(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 output_logic);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_write_slave_output(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.UInt16 out_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_read_slave_output(System.UInt16 card_ID, System.UInt16 connect_index, System.Int16 slave_No, ref System.UInt16 data_out);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_read_slave_input(System.UInt16 card_ID, System.UInt16 connect_index, System.Int16 slave_No, ref System.UInt16 data_in);     
    
    
    //DI INTERRUPT (ONLY FOR 7853/54)
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_set_int_renewal_type(System.Int16 card_ID, System.Int16 connect_index, System.Int16 renewal_type);/* ONLY FOR 7853/54 */	
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_set_int_renewal_bit(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.UInt16 bitsOfCheck);/* ONLY FOR 7853/54 */
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_set_int_control(System.Int16 card_ID, System.Int16 connect_index, System.Int16 enable);	 /* ONLY FOR 7853/54 */
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_wait_di_interrupt(System.Int16 card_ID, System.Int32 time_out_ms);
    				         
             						                    		                                          
    //DI Interrupt for call back function
    //I16 FNTYPE HSL_D_link_interrupt(I16 card_ID,void ( __stdcall *callbackAddr)(void));	/* ONLY FOR 7853/54 & RTX don't support */    
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_manual_reset_dievent(System.Int16 card_ID);		
    
    
    // FOR DI16-UL
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_set_di_latch_function(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 channel, System.Int16 active_mode, System.Int16 duration);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_set_di_latch_functionA(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 edge, System.Int16 duration);
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_get_di_latch_function(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 channel, ref System.Int16 active_mode, ref System.Int16 duration);
    
    
    // AIO
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_start_read(System.Int16 card_ID, System.Int16 connect_index);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_stop_read(System.Int16 card_ID, System.Int16 connect_index);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_set_signal_range(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 signal_range);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_get_signal_range(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 signal_range);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_get_input_mode(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 mode);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_set_last_channel(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 last_channel);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_get_last_channel(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 last_channel);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_read_input(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ai_channel, ref System.Double ai_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_write_output(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ao_channel, System.Double ao_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_read_output(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ao_channel, ref System.Double ao_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_sync_rw(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ai_channel, ref System.Double ai_data, System.Int16 ao_channel, System.Double ao_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_get_version(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, ref System.Int16 ver);   
     
    
    // AO4
    [DllImport("HSL.dll")]public static extern Int16  HSL_set_keep_mode(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 Mode);
    
    
    //ODM function
    [DllImport("HSL.dll")]public static extern Int16  HSL_D_write_channel_output_SP(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 channel, System.UInt16 out_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_AIO_scan_mode(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 Enable);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_AIO_scan(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_write_output_i(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ao_channel, System.Double ao_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_read_output_i(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ao_channel, ref System.Double ao_data);
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_read_input_i(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ai_channel, ref System.Double ai_data ); 
    [DllImport("HSL.dll")]public static extern Int16  HSL_A_sync_rw_i(System.Int16 card_ID, System.Int16 connect_index, System.Int16 slave_No, System.Int16 ai_channel, ref System.Double ai_data, System.Int16 ao_channel, System.Double ao_data);
    
       
    // The following function declarations are old							
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Initial(System.UInt16 card_ID);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Close(System.UInt16 card_ID);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Auto_Start(System.UInt16 card_ID, System.UInt16 connect_index);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Start(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 max_slave_No);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Stop(System.UInt16 card_ID, System.UInt16 connect_index);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Connect_Status(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.Byte sts_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Slave_Live(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.Byte live_data);
    
        
    [DllImport("HSL.dll")]public static extern void  W_HSL_Software_Reset(System.UInt16 card_ID);
    [DllImport("HSL.dll")]public static extern void  W_HSL_Get_IRQ_Channel(System.UInt16 card_ID, ref System.UInt16 irq_no);
      
      
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DIO_In(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.UInt32 in_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DIO_Channel_In(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, ref System.UInt16 in_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DIO_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt32 out_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DIO_Channel_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, System.UInt16 out_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DI8DO8_DIO_In(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.UInt16 in_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DI8DO8_DIO_Channel_In(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, ref System.UInt16 in_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DI8DO8_DIO_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 out_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DI8DO8_DIO_Channel_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, System.UInt16 out_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Read_DIO_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.UInt32 out_data_in_ram);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Read_DIO_Channel_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, ref System.UInt16 out_channel_data_in_ram);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Read_DI8DO8_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.UInt16 out_data_in_ram);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Read_DI8DO8_Channel_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, ref System.UInt16 out_channel_data_in_ram);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Set_In_Out_Logic(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 Input_Logic, System.UInt16 Output_Logic);
        
      
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_Start_Read(System.UInt16 card_ID, System.UInt16 connect_index);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_Stop_Read(System.UInt16 card_ID, System.UInt16 connect_index);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_Channel_In(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, ref System.Double ai_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_SetConfig(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 signal_range, System.UInt16 signal_type, System.UInt16 cjc_status);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_GetConfig(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.UInt16 signal_range, ref System.UInt16 signal_type, ref System.UInt16 cjc_status);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_OffsetCalib(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_SpanCalib(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_WriteDefault(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_SetCJCOffset(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.Double CJC_data2);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_GetCJCOffset(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.Double CJC_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_SetChannelStatus(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.Double enable_chans);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_GetChannelStatus(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.UInt32 enable_chans);
    
    
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_Timer_Set(System.UInt16 card_ID, System.UInt16 c1, System.UInt16 c2);
    //U16 FNTYPE W_HSL_TMRINT_Enable(U16 card_ID, HANDLE *phEvent);        
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_TMRINT_Disable(System.UInt16 card_ID);
    
    
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DIO_Memory_In(System.UInt16 card_ID, System.UInt16 connect_index, ref System.UInt16 data_in);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_DIO_Memory_Out(System.UInt16 card_ID, System.UInt16 connect_index, ref System.UInt16 data_out);
    

    // AI16AO2 Module Use
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_Set_Last_Channel(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 Last_Channel); // Used to set last channel
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_Get_Last_Channel(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.UInt16 Last_Channel); // Used to set last channel
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AI_Get_Version(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, ref System.Byte Ver);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AIO_LoadConfig(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AIO_SaveConfig(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AO_Channel_Out(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 AO_Ch_No, System.Double AO_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AIO_Channel_InOut(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 AI_CH_No, ref System.Double AI_data, System.UInt16 AO_Ch_No, System.Double AO_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AO_Channel_In(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 AO_Ch_No, ref System.UInt16 AO_data);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AO_OffsetCalib(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 DA_CH);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_AO_GainCalib(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 DA_CH);


    // TC08 Use
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_TC08_Offset_Calib(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_TC08_Span_Calib(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel);
    [DllImport("HSL.dll")]public static extern UInt16  W_HSL_TC08_Get_Temperature(System.UInt16 card_ID, System.UInt16 connect_index, System.UInt16 slave_No, System.UInt16 channel, System.UInt16 TC_type, ref System.Double Temperature);
    [DllImport("HSL.dll")]public static extern IntPtr  W_HSL_DIO_Memory_Ptr(System.UInt16 card_ID, System.UInt16 connect_index);  

                                    
                                    				
	}
	

    
}
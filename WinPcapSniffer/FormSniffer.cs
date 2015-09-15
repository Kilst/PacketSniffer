using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//..
using System.Threading;
using SharpPcap;
using SharpPcap.WinPcap;
using PacketDotNet;
using System.Diagnostics;

namespace WinPcapSniffer
{
    public partial class FormSniffer : Form
    {

        public FormSniffer()
        {
            InitializeComponent();
            rtxtOutput.Text = PcapHelper.GetVersion();
            lblListening.ForeColor = Color.Red;
            CaptureDeviceList devices = PcapHelper.GetDeviceList();
            foreach (ICaptureDevice dev in devices)
            {
                cmbDevice.Items.Add(dev.Description.Replace("Network adapter", string.Empty));
            }
        }
        System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
        Color colour;
        Thread thread;
        MyDevice _instance = MyDevice.GetInstance();
        private bool stopped = true;
        private bool packetEvent = false;

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (stopped == true && _instance.device == null)
            {
                try
                {
                    // Get a device from the list
                    try
                    {
                        _instance.device = PcapHelper.GetDevice(cmbDevice.SelectedIndex);
                        colour = System.Drawing.Color.LimeGreen;
                        AppendTextBoxColoured("\nDevice Set:: " + _instance.device.Description);
                        lblListening.Text = _instance.device.Description;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("That's not a device!");
                        _instance.device = null;
                        return;
                    }
                    
                    // Register our handler function to the
                    // 'packet arrival' event
                    if(packetEvent == false)
                        _instance.device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);
                    packetEvent = true;
                    // Open the device for capturing
                    int readTimeoutMilliseconds = 1000;
                    _instance.device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

                    colour = System.Drawing.Color.LimeGreen;
                    AppendTextBoxColoured("\n\n-- Listening on: " + _instance.device.Description);
                    lblListening.Text = "Listening on: " + _instance.device.Description;
                    lblListening.ForeColor = Color.LimeGreen;
                    try
                    {
                        _instance.device.Filter = txtFilters.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Incorrect Filter!\n\n" + ex.ToString());
                        _instance.device = null;
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("No Device Selected!");
                    _instance.device = null;
                    return;
                }
                stopped = false;
                // Start the capturing process
                thread = new Thread(new ThreadStart(CapturePackets));
                thread.Start();
            }
            else
            {
                MessageBox.Show("Already listening..");
            }
        }

        public void CapturePackets()
        {
            _instance.device.StartCapture();
            while (stopped != true)
            {
                Thread.Sleep(100);
            }
            try
            {
                // Stop the capturing process
                _instance.device.StopCapture();

                // Close the pcap device
                _instance.device.Close();
                colour = System.Drawing.Color.Red;
                AppendTextBoxColoured("\n\n-- STOPPED listening on: " + _instance.device.Description + "\n\n");
            }
            catch (Exception ex)
            {
                colour = System.Drawing.Color.Red;
                AppendTextBoxColoured("\n\n-- STOPPED listening on: " + _instance.device.Description + "\n" + ex.ToString() + "\n\n");
            }
            NewLabelText("Not listening on: " + _instance.device.Description);
            _instance.device = null;
            thread.Abort();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Remove event handler from device
            _instance.device.OnPacketArrival -= new SharpPcap.PacketArrivalEventHandler(device_OnPacketArrival);
            packetEvent = false;
            stopped = true;
            lblListening.ForeColor = Color.Red;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            colour = Color.Black;
            AppendTextBoxColoured(PcapHelper.ListDevices());
        }

        /// <summary>
        /// Prints the time, type, data and length of each received packet
        /// </summary>
        private void device_OnPacketArrival(object sender, CaptureEventArgs packet)
        {
            //Packet pData = PacketDotNet.Packet.ParsePacket(packet.Packet.LinkLayerType, packet.Packet.Data);
            //UdpPacket udp = UdpPacket.GetEncapsulated(pData);
            Byte[] data = packet.Packet.Data;
            //DateTime time = packet.Packet.Timeval.Date;
            DateTime time = System.DateTime.Now;
            int len = packet.Packet.Data.Length;
            LinkLayers type = packet.Packet.LinkLayerType;
            string seconds = PcapHelper.ConvertTime(time.Second);
            string minutes = PcapHelper.ConvertTime(time.Minute);

            colour = System.Drawing.Color.LimeGreen;
            AppendTextBoxColoured("\n\n###NEW PACKET###");
            colour = Color.Orange;
            AppendTextBoxColoured("\nLength of Packet:" + len + "\n");
            colour = Color.Purple;
            AppendTextBoxColoured("Protocol: " + PcapHelper.GetProtocol(type, data));
            colour = Color.Black;
            AppendTextBoxColoured("\n\nPACKET INFO::\n\nTYPE: " + type.ToString()
                + "\nHEADER Info: " + PacketDotNet.Packet.ParsePacket(type, data) + "\n");

            AppendTextBoxColoured("\nTIME Received: " + time.Hour + ":" + minutes + ":" + seconds + ":" + time.Millisecond
                + "\nALL Bytes to HEX:: \n" + BitConverter.ToString(data) + "\n");

            string output = EQPacket.EQPacketOP_Target(data);
            colour = System.Drawing.Color.LimeGreen;
            AppendTextBoxColoured(output);
        }

        public void AppendTextBoxColoured(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(AppendTextBoxColoured), new object[] { value });
                    return;
                }
                int length = rtxtOutput.TextLength;  // at end of text
                rtxtOutput.AppendText(value);
                rtxtOutput.SelectionStart = length;
                rtxtOutput.SelectionLength = value.Length;
                rtxtOutput.SelectionColor = colour;
            }
            catch (Exception)
            {

            }
        }

        public void NewLabelText(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(NewLabelText), new object[] { value });
                return;
            }
            lblListening.Text = value;
        }

        private void rtxtOutput_TextChanged(object sender, EventArgs e)
        {
            rtxtOutput.SelectionStart = rtxtOutput.Text.Length; //Set the current caret position at the end
            rtxtOutput.ScrollToCaret(); //Now scroll it automatically
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            rtxtOutput.Text = PcapHelper.GetVersion();
        }


        // Method was manually set in Form1.Designer, so I could stop the thread on window close
        // this.FormClosing += this.FormSniffer_FormClosing;
        private void FormSniffer_FormClosing(Object sender, FormClosingEventArgs e)
        {
            stopped = true;
            Application.Exit();
        }

        private void btnReSend_Click(object sender, EventArgs e)
        {
            AppendTextBoxColoured(EQPacket.ResendPacket());
        }
    }
}

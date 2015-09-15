using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//..
using System.IO;
using SharpPcap;
using SharpPcap.WinPcap;
using PacketDotNet;
using zlib;

namespace WinPcapSniffer
{
    public class PcapHelper
    {
        public static string GetVersion()
        {
            string ver = SharpPcap.Version.VersionString;
            return "SharpPcap::\nVersion::" + ver + "\n\n";
        }

        public static CaptureDeviceList GetDeviceList()
        {
            return CaptureDeviceList.Instance;
        }

        public static string ListDevices()
        {
            CaptureDeviceList devices = CaptureDeviceList.Instance;
            if (devices.Count < 1)
            {
                return "No devices were found on this machine";
            }
            else
            {
                string deviceList = "";
                int id = 1;
                // Print out the available network devices
                foreach (ICaptureDevice dev in devices)
                {
                    deviceList += "Device ID: " + id + "\n" + dev + "\n";
                    id++;
                }
                id = 0;
                return "DEVICE LIST::\n\n" + deviceList;
            }
        }

        public static ICaptureDevice GetDevice(int id)
        {
            CaptureDeviceList devices = CaptureDeviceList.Instance;
            return devices[id];
        }

        public static string GetProtocol(LinkLayers type, Byte[] data)
        {
            string packet = "" + PacketDotNet.Packet.ParsePacket(type, data);
            if (packet.Contains("UDPPacket"))
            {
                return "UDP";
            }
            else if (packet.Contains("Protocol=TCP"))
            {
                return "TCP";
            }
            else if (packet.Contains("ARPPacket"))
            {
                return "ARP";
            }
            else
            {
                return "I didn't think of it..";
            }
        }

        public static string ConvertTime(int time)
        {
            if (time < 10)
            {
                string newTime = "";
                newTime = "0" + time;
                return newTime;
            }
            else
            {
                return time.ToString();
            }
        }
    }
}

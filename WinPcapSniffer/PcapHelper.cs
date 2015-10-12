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
            ip_address ip;
            ip.byte1 = data[0];
            ip.byte2 = data[1];
            ip.byte3 = data[2];
            ip.byte4 = data[3];
            ip_header ip_h;
            ip_h.proto = data[13];
            string packet = "" + PacketDotNet.Packet.ParsePacket(type, data);
            switch (ip_h.proto)
            {
                case 0:
                    return "TCP" + ip_h.proto.ToString("X2");
                case 6:
                    return "ARP" + ip_h.proto.ToString("X2");
                case 221:
                    return "UDP" + ip_h.proto.ToString("X2");
            }
            return "Not Known" + ip_h.proto.ToString("X2");
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

        /* 4 bytes IP address */
        struct ip_address
        {
            public byte byte1;
            public byte byte2;
            public byte byte3;
            public byte byte4;
        }

                /* IPv4 header */
        struct ip_header
        {
            public byte ver_ihl;        // Version (4 bits) + Internet header length (4 bits)
            public byte tos;            // Type of service 
            public ushort tlen;           // Total length 
            public ushort identification; // Identification
            public ushort flags_fo;       // Flags (3 bits) + Fragment offset (13 bits)
            public byte ttl;            // Time to live
            public byte proto;          // Protocol
            public ushort crc;            // Header checksum
            public ip_address saddr;      // Source address
            public ip_address daddr;      // Destination address
            public int op_pad;         // Option + Padding
        }

        /* UDP header*/
        struct udp_header
        {
            public ushort sport;          // Source port
            public ushort dport;          // Destination port
            public ushort len;            // Datagram length
            public ushort crc;            // Checksum
        }

    }
}

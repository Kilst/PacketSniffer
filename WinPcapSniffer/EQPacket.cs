using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//..
using System.IO;
using System.Net;
using System.Net.Sockets;
using SharpPcap;
using SharpPcap.WinPcap;
using PacketDotNet;
using zlib;

namespace WinPcapSniffer
{
    public class EQPacket : Packet
    {
        const string OP_CodeTargetMouse = "6241";
        const string OP_CodeTargetCommand = "FE41";

        //public static int count = 0;
        public static Byte[] output = new Byte[58];

        public static string EQPacketOP_Target(Byte[] data)
        {
            Byte[] OP_Code = new Byte[2];
            string OPCode = "";
            string names = "";
            string returnString = "";

            if (data.Length == 58)
            {
                //if (count == 0)
                //    output = data;
                names = BitConverter.ToString(data, 52).Replace("-", string.Empty);
                Array.Copy(data, 50, OP_Code, 0, 2);
                OPCode = BitConverter.ToString(OP_Code).Replace("-", string.Empty);
                switch (OPCode)
                {
                    case OP_CodeTargetMouse:
                        OPCode = "0x" + OPCode + " OP_CodeTargetMouse";
                        break;
                    case OP_CodeTargetCommand:
                        OPCode = "0x" + OPCode + " OP_CodeTargetCommand";
                        break;
                    default:
                        OPCode = "0x" + OPCode + " Unknown OP_Code";
                        break;
                }
                //DecompressData(data, out output);
                //count++;

                returnString = "\n" + OPCode + "\n\n" + names;
            }

            return returnString;
        }

        public static string ResendPacket()
        {
            SendUDPPacket("10.1.1.1", 15900, output, 1);
            return BitConverter.ToString(output, 52).Replace("-", string.Empty);
        }

        public static void CompressData(byte[] inData, out byte[] outData)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream outZStream = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION))
            using (Stream inMemoryStream = new MemoryStream(inData))
            {
                CopyStream(inMemoryStream, outZStream);
                outZStream.finish();
                outData = outMemoryStream.ToArray();
            }
        }

        public static void DecompressData(byte[] inData, out byte[] outData)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream outZStream = new ZOutputStream(outMemoryStream))
            using (Stream inMemoryStream = new MemoryStream(inData))
            {
                CopyStream(inMemoryStream, outZStream);
                outZStream.finish();
                outData = outMemoryStream.ToArray();
            }
        }

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }

        public static void SendUDPPacket(string hostNameOrAddress, int destinationPort, byte[] buffer, int count)
        {
            // Validate the destination port number
            if (destinationPort < 1 || destinationPort > 65535)
                throw new ArgumentOutOfRangeException("destinationPort", "Parameter destinationPort must be between 1 and 65,535.");

            // Resolve the host name to an IP Address
            IPAddress ipAddresses = IPAddress.Parse(hostNameOrAddress);
            //if (ipAddresses.Length == 0)
            //throw new ArgumentException("Host name or address could not be resolved.", "hostNameOrAddress");

            // Use the first IP Address in the list
            IPAddress destination = ipAddresses;
            IPEndPoint endPoint = new IPEndPoint(destination, destinationPort);
            //byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Send the packets
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            for (int i = 0; i < count; i++)
                socket.SendTo(buffer, endPoint);
            socket.Close();
        }
    }
}

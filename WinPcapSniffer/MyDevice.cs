﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//..
using SharpPcap;
using SharpPcap.WinPcap;
using PacketDotNet;

namespace WinPcapSniffer
{
    public class MyDevice
    {
        private static MyDevice _instance;
        public ICaptureDevice device { get; set; }

        private MyDevice()
        {

        }


        public static MyDevice GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MyDevice();
            }
            return _instance;
        }
    }
}

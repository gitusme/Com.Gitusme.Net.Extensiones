/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Com.Gitusme.Net.Extensiones.Wpf.IO
{
    /// <summary>
    /// 串口通信组件，支持 WinForm、WPF、Console 等应用集成。
    /// </summary>
    public sealed class SerialPort : IDisposable
    {
        private Com.Gitusme.IO.Ports.SerialPort _serialPort
            = new Com.Gitusme.IO.Ports.SerialPort();

        public bool IsOpen
        {
            get
            {
                return _serialPort.IsOpen;
            }
        }

        public SerialPortSettings Settings
        {
            get
            {
                return new SerialPortSettings()
                {
                    Synchronizable = _serialPort.Settings.Synchronizable,
                    BaudRate = _serialPort.Settings.BaudRate,
                    DataBits = _serialPort.Settings.DataBits,
                    StopBits = _serialPort.Settings.StopBits,
                    Parity = Enum.Parse<Parity>(_serialPort.Settings.Parity.ToString())
                };
            }
            set
            {
                _serialPort.Settings = new Com.Gitusme.IO.Ports.SerialPortSettings()
                {
                    Synchronizable = value.Synchronizable,
                    BaudRate = value.BaudRate,
                    DataBits = value.DataBits,
                    StopBits = value.StopBits,
                    Parity = Enum.Parse<Com.Gitusme.IO.Ports.Parity>(value.Parity.ToString())
                };
            }
        }

        public bool Open(string portName)
        {
            if (portName == null)
            {
                throw new NullReferenceException("portName is null");
            }
            return _serialPort.Open(portName);
        }

        public void Close()
        {
            _serialPort.Close();
        }

        public int Read(byte[] buffer)
        {
            return _serialPort.Read(buffer);
        }

        public int Write(byte[] buffer)
        {
            return _serialPort.Write(buffer);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                _serialPort.Dispose();
            }
        }
    }

    /// <summary>
    /// 校验位
    /// </summary>
    public enum Parity
    {
        /// <summary>
        /// 无校验
        /// </summary>
        None = 0,
        /// <summary>
        /// 奇校验
        /// </summary>
        Odd = 1,
        /// <summary>
        /// 偶校验
        /// </summary>
        Even = 2,
        /// <summary>
        /// 标记校验
        /// </summary>
        Mark = 3,
        /// <summary>
        /// 空校验
        /// </summary>
        Space = 4
    };

    /// <summary>
    /// 串口参数配置接口
    /// </summary>
    public class SerialPortSettings
    {
        /// <summary>
        /// 使用同步模式，默认false为异步
        /// </summary>
        public bool Synchronizable = true;
        /// <summary>
        /// 波特率，9600、19200、38400、43000、56000、57600、115200
        /// </summary>
        public int BaudRate = 115200;
        /// <summary>
        /// 校验位，0为无校验，1为奇校验，2为偶校验，3为标记校验
        /// </summary>
        public Parity Parity = Parity.None;
        /// <summary>
        /// 数据位，4-8，通常为8位
        /// </summary>
        public int DataBits = 8;
        /// <summary>
        /// 停止位，1为1位停止位，2为2位停止位,3为1.5位停止位
        /// </summary>
        public int StopBits = 1;
    };
}

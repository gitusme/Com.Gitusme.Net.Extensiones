using Com.Gitusme.Net.Extensiones.Core;
using System.Xml.Serialization;
using Com.Gitusme.Net.Extensiones.Demos;

// Object Exensiones Demo
IExtensionesDemo objectExensiones = new ObjectExensionesDemo();
objectExensiones.Execute();

// String Exensiones Demo
IExtensionesDemo stringExensiones = new StringExtensionesDemo();
stringExensiones.Execute();

// Logger Demo
IExtensionesDemo loggerDemo = new LoggerDemo();
loggerDemo.Execute();

// Serial Port Demo
IExtensionesDemo serialPortDemo = new SerialPortDemo();
serialPortDemo.Execute();

// Socket Demo
IExtensionesDemo socketDemo = new SocketDemo();
socketDemo.Execute();

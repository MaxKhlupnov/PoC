using System;
using System.Threading.Tasks;
using System.Threading;
using TXCommunication;
using TXTCommunication.Fischertechnik;
using TXTCommunication.Fischertechnik.Txt;


namespace PoCApp
{
    class Program
    {
        private const string IP = TxtInterface.ControllerUsbIp;//ControllerBluetoothIp; //"192.168.8.2";
        static TxtInterface txtLink = null;

        static void Main(string[] args)
        {
            InitConnection();
            Console.WriteLine("Start motor test...");
            TestMotor();
            Console.WriteLine("Motor test done...");
            Console.ReadLine();
            Console.WriteLine("Disconnecting...");

            // Stop the inline mode
            txtLink.StopOnlineMode();

            // Disconnect from the interface
            if (txtLink.Connection == ConnectionStatus.Connected)
                txtLink.Disconnect();
            

            // Don't forget to dispose
            txtLink.Dispose();

            
        }

        static void InitConnection()
        {
            txtLink = new TxtInterface();            
            txtLink.IsDebugEnabled = true;
            txtLink.InputValueChanged += TxtLink_InputValueChanged;            
            txtLink.Connected += TxtLink_Connected;
            txtLink.Disconnected += TxtLink_Disconnected;
            txtLink.ConnectionLost += TxtLink_ConnectionLost;
            txtLink.OnlineStarted += (sender, eventArgs) => Console.WriteLine("Online mode started");
            txtLink.OnlineStopped += (sender, eventArgs) => Console.WriteLine("Online mode stopped");
           
            txtLink.Connect(IP);
            txtLink.StartOnlineMode();
            ConfigureIOPorts();
        }

        private static void ConfigureIOPorts()
        {
            // Configure the input ports
            int inputPort = 0;
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeUltrasonic, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort, InputMode.ModeUltrasonic, true);

            int outputIndex = 0;
            txtLink.ConfigureOutputMode(outputIndex++, true);
           // txtLink.ConfigureOutputMode(outputIndex++, true);
         //   txtLink.ConfigureOutputMode(outputIndex++, true);
        }

        private static void TestMotor()
        {
            // txtLink.SetOutputValue(0, 512);
            int motor = txtLink.GetMotorIndex(0);
             txtLink.SetMotorValue(motor, 300, MotorDirection.Left);
            Thread.Sleep(4000);
            txtLink.SetMotorValue(0, 300, MotorDirection.Right);
            Thread.Sleep(3000);
            txtLink.SetMotorValue(0, 0, MotorDirection.Right);
            // 
        }

        private static void TxtLink_InputValueChanged(object sender, FtApp.Fischertechnik.Txt.Events.InputValueChangedEventArgs e)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            for (int i = 0; i < txtLink.GetInputCount(); i++)
            {
                Console.Write("I{0,1} {1, 5}  |", i + 1, txtLink.GetInputValue(i));
            }
            Console.WriteLine();
        }

        private static void TxtLink_ConnectionLost(object sender, EventArgs e)
        {
            Console.WriteLine("Connection lost");
        }

        private static void TxtLink_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnected");
        }

        private static void TxtLink_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Connected");
        }
    }
}

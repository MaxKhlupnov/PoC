using System;
using System.Threading.Tasks;
using TXCommunication;
using TXTCommunication.Fischertechnik;
using TXTCommunication.Fischertechnik.Txt;


namespace PoCApp
{
    class Program
    {
        private const string IP = TxtInterface.ControllerBluetoothIp; //"192.168.8.2";
        static TxtInterface txtLink = null;

        static void Main(string[] args)
        {
            InitConnection();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
            if (txtLink.Connection == ConnectionStatus.Connected)
                    txtLink.Disconnect();
        }

        static void InitConnection()
        {
            txtLink = new TxtInterface();            
            txtLink.IsDebugEnabled = true;
            txtLink.InputValueChanged += TxtLink_InputValueChanged;            
            txtLink.Connected += TxtLink_Connected;
            txtLink.Disconnected += TxtLink_Disconnected;
            txtLink.ConnectionLost += TxtLink_ConnectionLost;
            Task.Run(() => {
                txtLink.Connect(IP);
                txtLink.StartOnlineMode();
            });
            
        }

        private static void TxtLink_InputValueChanged(object sender, FtApp.Fischertechnik.Txt.Events.InputValueChangedEventArgs e)
        {
           
        }

        private static void TxtLink_ConnectionLost(object sender, EventArgs e)
        {
            
        }

        private static void TxtLink_Disconnected(object sender, EventArgs e)
        {
           
        }

        private static void TxtLink_Connected(object sender, EventArgs e)
        {
            txtLink.SetMotorValue(0, 30, MotorDirection.Left);
            txtLink.SetMotorValue(0, 40, MotorDirection.Right);
        }
    }
}

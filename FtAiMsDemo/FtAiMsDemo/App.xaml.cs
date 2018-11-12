using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TXTCommunication.Fischertechnik;
using System.Collections.Generic;
using System.Threading.Tasks;
using TXTCommunication.Fischertechnik.Txt;
using FtAiMsDemo.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FtAiMsDemo
{
    public partial class App : Application
    {
        private const string IP = TxtInterface.ControllerUsbIp;//.ControllerBluetoothIp;

        public static LogProvider Log = new LogProvider();

        /**
         * Connection to FischerTechnik controller
         */
        public static TxtInterface TxtLink{get; private set;}

        public App()
        {         
            InitializeComponent();
            if (TxtLink == null)
            {
                TxtLink = new TxtInterface();
            }
            MainPage = new MainPage();
            
        }

        static void InitConnection()
        {

            TxtLink.IsDebugEnabled = true;
            // TxtLink.InputValueChanged += TxtLink_InputValueChanged;
            TxtLink.Disconnected += (sender, eventArgs) => Log.WriteInfo("TXT Controller connected");
            TxtLink.Disconnected += (sender, eventArgs) => Log.WriteInfo("TXT Controller disconnected");
            TxtLink.ConnectionLost += (sender, eventArgs) => Log.WriteInfo("Connection lost");
            TxtLink.OnlineStarted += (sender, eventArgs) => Log.WriteInfo("Online mode started");
            TxtLink.OnlineStopped += (sender, eventArgs) => Log.WriteInfo("Online mode stopped");
            TxtLink.CounterChanged += (sender, e) => Log.WriteInfo(String.Format("Counters updated {0}", e.ToString()));
            try
            {
                TxtLink.Connect(IP);
                TxtLink.StartOnlineMode();                              
            }
            catch(Exception ex)
            {               
                Log.WriteError(ex);
            }
        }

        protected override void OnStart()
        {
                     
            InitConnection();
            //ConfigureIOPorts();                       
        }
        

        protected override void OnSleep()
        {
            if (TxtLink != null)
            {
                
                // Handle when your app sleeps
                TxtLink.StopOnlineMode();

                TxtLink.TxtCamera.StopCamera();

                //// Disconnect from the interface
                if (TxtLink.Connection == ConnectionStatus.Connected)
                    TxtLink.Disconnect();


                //// Don't forget to dispose
                TxtLink.Dispose();
            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            InitConnection();
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using TXCommunication;
using TXTCommunication.Fischertechnik;
using TXTCommunication.Fischertechnik.Txt;


namespace PoCApp
{
    class Program
    {
        private const string IP = TxtInterface.ControllerUsbIp;//ControllerBluetoothIp; //"192.168.8.2"; 
        static TxtInterface txtLink = null;
        static int[] I = new int[8];
        static int[] C = new int[4];


        static void Main(string[] args)
        {
            InitConnection();
            Thread.Sleep(500);
            int position = 1;
            while (true)
            {
                Console.WriteLine("Input position 1 or 2 or 3");
                int.TryParse(Console.ReadLine(), out position);

                switch (position)
                {
                    case 1:
                        MouveToOnePosition();
                        break;
                    case 2:
                        MouveToTwoPosition();
                        break;
                    case 3:
                        MouveToThreePosition();
                        break;
                    default:
                        //Console.WriteLine("Input number from 1 to 3");

                        txtLink.TxtCamera.StopCamera();
                        // Stop the inline mode
                        txtLink.StopOnlineMode();

                        // Disconnect from the interface
                        if (txtLink.Connection == ConnectionStatus.Connected 
                            || txtLink.Connection == ConnectionStatus.Online)
                            txtLink.Disconnect();


                        // Don't forget to dispose
                        txtLink.Dispose();

                        break;
                }
            }

            //// Stop the inline mode
            //txtLink.StopOnlineMode();

            //// Disconnect from the interface
            //if (txtLink.Connection == ConnectionStatus.Connected)
            //    txtLink.Disconnect();


            //// Don't forget to dispose
            //txtLink.Dispose();


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
            txtLink.CounterChanged += TxtLink_CounterChanged;

            txtLink.Connect(IP);
            txtLink.StartOnlineMode();
            ConfigureIOPorts();
            txtLink.TxtCamera.StartCamera();
            txtLink.TxtCamera.FrameReceived += TxtCamera_FrameReceived;

        }

        private static void TxtCamera_FrameReceived(object sender, TXTCommunication.Fischertechnik.Txt.Camera.FrameReceivedEventArgs e)
        {
            CustomVisionWrapper.PredictImage(new MemoryStream(e.FrameData));
        }

        private static void ConfigureIOPorts()
        {
            // Configure the input ports
            int inputPort = 0;
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort++, InputMode.ModeR, true);
            txtLink.ConfigureInputMode(inputPort, InputMode.ModeR, true);

            int outputIndex = 0;
            txtLink.ConfigureOutputMode(outputIndex++, true);
            txtLink.ConfigureOutputMode(outputIndex++, true);
            txtLink.ConfigureOutputMode(outputIndex++, true);
            txtLink.ConfigureOutputMode(outputIndex++, false);
            // txtLink.ConfigureOutputMode(outputIndex++, true);
            //   txtLink.ConfigureOutputMode(outputIndex++, true);
        }

        private static void TestMotor()
        {
            int currentCountMotor0 = C[0];
            Console.WriteLine("Start Count motor {0}", currentCountMotor0);
            do
            {
                txtLink.SetMotorValue(0, 512, MotorDirection.Left);

            } while (C[0] < currentCountMotor0 + 450);
            txtLink.SetMotorValue(0, 0, MotorDirection.Left);

            Thread.Sleep(500);

            int currentCountMotor2 = C[2];

            Console.WriteLine("Start Count motor {0}", currentCountMotor2);
            do
            {
                txtLink.SetMotorValue(2, 512, MotorDirection.Left);

            } while (C[2] < currentCountMotor2 + 350);
            txtLink.SetMotorValue(2, 0, MotorDirection.Left);

            Thread.Sleep(500);

            int currentCountMotor1 = C[1];

            Console.WriteLine("Start Count motor {0}", currentCountMotor1);
            do
            {
                txtLink.SetMotorValue(1, 512, MotorDirection.Left);

            } while (C[1] < currentCountMotor1 + 700);
            txtLink.SetMotorValue(1, 0, MotorDirection.Left);

            // txtLink.SetOutputValue(0, 512);
            //int motor = txtLink.GetMotorIndex(0);
            //txtLink.SetMotorValue(motor, 300, MotorDirection.Left);
            //Thread.Sleep(3000);
            //txtLink.SetMotorValue(0, 300, MotorDirection.Right);
            //while(I[0] != 1)
            //{
            //    Thread.Sleep(1);
            //}            
            //txtLink.SetMotorValue(0, 0, MotorDirection.Right);

            //Thread.Sleep(2000);
            //motor = txtLink.GetMotorIndex(1);
            //txtLink.SetMotorValue(1, 400, MotorDirection.Left);
            //Thread.Sleep(2000);
            //txtLink.SetMotorValue(1, 200, MotorDirection.Left);
            //Thread.Sleep(2000);
            //txtLink.SetMotorValue(1, 0, MotorDirection.Left);
            //Thread.Sleep(2000);
            //motor = txtLink.GetMotorIndex(2);
            //txtLink.SetMotorValue(2, 400, MotorDirection.Left);
            //Thread.Sleep(2000);
            //txtLink.SetMotorValue(2, 300, MotorDirection.Left);
            //Thread.Sleep(2000);
            //txtLink.SetMotorValue(2, 0, MotorDirection.Left);
            // 
        }

        public static void MouveToOnePosition()
        {
            //-- Move motor 2 (Вылет)
            int motor = 2;

            SetMotorToNull();

            Thread.Sleep(500);

            int currentCountMotor = C[motor];

            Console.WriteLine("Start Count motor {0}", currentCountMotor);

            while (C[motor] < currentCountMotor + 300)
            {
                txtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                txtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(1000);

            //-- Start vacuum capture
            VacuumCaptureOn();

            Thread.Sleep(1000);

            //-- Move motor 1 (Lift Up)
            motor = 1;
            while (I[motor] != 1)
            {
                txtLink.SetMotorValue(1, 512, MotorDirection.Right);
            }
            txtLink.SetMotorValue(1, 0, MotorDirection.Right);

            Thread.Sleep(500);

            //move motor 0 (Rotation left)
            motor = 0;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 450)
            {
                txtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                txtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Stop vaacuum capture
            VacuumCaptureOff();
        }

        public static void MouveToTwoPosition()
        {
            //-- Move motor 2 (Вылет)
            int motor = 2;

            SetMotorToNull();

            Thread.Sleep(500);

            int currentCountMotor = C[motor];

            Console.WriteLine("Start Count motor {0}", currentCountMotor);

            while (C[motor] < currentCountMotor + 300)
            {
                txtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                txtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(1000);

            //-- Start vacuum capture
            VacuumCaptureOn();

            Thread.Sleep(1000);

            //-- Move motor 1 (Lift Up)
            motor = 1;
            while (I[motor] != 1)
            {
                txtLink.SetMotorValue(1, 512, MotorDirection.Right);
            }
            txtLink.SetMotorValue(1, 0, MotorDirection.Right);

            Thread.Sleep(500);

            //move motor 0 (Rotation left)
            motor = 0;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 900)
            {
                txtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            //-- Move motor 2 (Вылет)
            motor = 2;

            Thread.Sleep(500);

            currentCountMotor = C[motor];

            Console.WriteLine("Start Count motor {0}", currentCountMotor);

            while (C[motor] < currentCountMotor + 600)
            {
                txtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                txtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Stop vaacuum capture
            VacuumCaptureOff();
        }

        public static void MouveToThreePosition()
        {
            //-- Move motor 2 (Вылет)
            int motor = 2;

            SetMotorToNull();

            Thread.Sleep(500);

            int currentCountMotor = C[motor];

            Console.WriteLine("Start Count motor {0}", currentCountMotor);

            while (C[motor] < currentCountMotor + 300)
            {
                txtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                txtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(1000);

            //-- Start vacuum capture
            VacuumCaptureOn();

            Thread.Sleep(1000);

            //-- Move motor 1 (Lift Up)
            motor = 1;
            while (I[motor] != 1)
            {
                txtLink.SetMotorValue(1, 512, MotorDirection.Right);
            }
            txtLink.SetMotorValue(1, 0, MotorDirection.Right);

            Thread.Sleep(500);

            //move motor 0 (Rotation left)
            motor = 0;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 1370)
            {
                txtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                txtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            txtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Stop vaacuum capture
            VacuumCaptureOff();
        }

        private static void VacuumCaptureOn()
        {
            txtLink.SetOutputValue(6, 512);
            Thread.Sleep(1000);
            txtLink.SetOutputValue(7, 512);
        }

        private static void VacuumCaptureOff()
        {
            txtLink.SetOutputValue(7, 0);
            Thread.Sleep(500);
            txtLink.SetOutputValue(6, 0);
        }



        private static void SetMotorToNull()
        {
            Console.WriteLine("Moving to start position");
            int motor = 1;
            int sensor = 1;

            while (I[sensor] != 1)
            {
                Thread.Sleep(200);
               // Console.WriteLine("Start motor");
                txtLink.SetMotorValue(motor, 512, MotorDirection.Right);

            }
            //Console.WriteLine("Stop motor");
            txtLink.SetMotorValue(motor, 0, MotorDirection.Right);

            Thread.Sleep(200);

            motor = 0;
            sensor = 0;
            while (I[sensor] != 1)
            {
                Thread.Sleep(200);
              //  Console.WriteLine("Start motor");
                txtLink.SetMotorValue(motor, 512, MotorDirection.Right);

            }
           // Console.WriteLine("Stop motor");
            txtLink.SetMotorValue(motor, 0, MotorDirection.Right);

            Thread.Sleep(200);

            motor = 2;
            sensor = 2;
            while (I[sensor] != 1)
            {
                Thread.Sleep(200);
               // Console.WriteLine("Start motor");
                txtLink.SetMotorValue(motor, 512, MotorDirection.Right);

            }
           // Console.WriteLine("Stop motor");
            txtLink.SetMotorValue(motor, 0, MotorDirection.Right);
        }


        private static void TestCompressor()
        {
            txtLink.SetOutputValue(6, 512);
            Thread.Sleep(2000);
            txtLink.SetOutputValue(7, 512);
            Thread.Sleep(4000);
            txtLink.SetOutputValue(7, 0);
            Thread.Sleep(500);
            txtLink.SetOutputValue(7, 512);
            Thread.Sleep(5000);
            txtLink.SetOutputValue(7, 0);
            Thread.Sleep(2000);
            txtLink.SetOutputValue(6, 0);
        }

        private static void TxtLink_CounterChanged(object sender, FtApp.Fischertechnik.Events.CounterChangedEventArgs e)
        {
            //Console.SetCursorPosition(0, Console.CursorTop - 1);

            for (int i = 0; i < e.Counters.Length; i++)
            {
              //  Console.Write("C{0} {1}  |", i, e.Counters[i]);
                C[i] = e.Counters[i];

            }
         //   Console.WriteLine();
        }

        private static void TxtLink_InputValueChanged(object sender, FtApp.Fischertechnik.Txt.Events.InputValueChangedEventArgs e)
        {
            //Console.SetCursorPosition(0, Console.CursorTop - 1);

            for (int i = 0; i < txtLink.GetInputCount(); i++)
            {
             //   Console.Write("I{0,1} {1, 5}  |", i + 1, txtLink.GetInputValue(i));
                I[i] = txtLink.GetInputValue(i);
            }
         //   Console.WriteLine();
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

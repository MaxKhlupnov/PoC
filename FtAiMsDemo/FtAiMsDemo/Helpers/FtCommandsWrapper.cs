using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TXCommunication;
using TXTCommunication.Fischertechnik;
using TXTCommunication.Fischertechnik.Txt;

namespace FtAiMsDemo.Helpers
{
    public class FtCommandsWrapper
    {

        private static void TxtLink_CounterChanged(object sender, FtApp.Fischertechnik.Events.CounterChangedEventArgs e)
        {
            //Console.SetCursorPosition(0, Console.CursorTop - 1);

            for (int i = 0; i < e.Counters.Length; i++)
            {
              App.Log.WriteInfo(String.Format("C{0} {1}  |", i, e.Counters[i]));
                C[i] = e.Counters[i];

            }
            
        }

        private static void TxtLink_InputValueChanged(object sender, FtApp.Fischertechnik.Txt.Events.InputValueChangedEventArgs e)
        {           
            for (int i = 0; i < App.TxtLink.GetInputCount(); i++)
            {
                App.Log.WriteInfo(String.Format("I{0,1} {1, 5}  |", i + 1, App.TxtLink.GetInputValue(i)));
                I[i] = App.TxtLink.GetInputValue(i);
            }
            Console.WriteLine();
        }

        static int[] I = new int[8];
        static int[] C = new int[4];

        public static void MouveToOnePosition()
        {
            //-- Move motor 2 (Вылет)
            int motor = 2;

            SetMotorToNull();

            Thread.Sleep(500);

            int currentCountMotor = C[motor];

            App.Log.WriteInfo(String.Format("Start Count motor {0}", currentCountMotor));

            while (C[motor] < currentCountMotor + 300)
            {
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                App.TxtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(1000);

            //-- Start vacuum capture
            VacuumCaptureOn();

            Thread.Sleep(1000);

            //-- Move motor 1 (Lift Up)
            motor = 1;
            while (I[motor] != 1)
            {
                App.TxtLink.SetMotorValue(1, 512, MotorDirection.Right);
            }
            App.TxtLink.SetMotorValue(1, 0, MotorDirection.Right);

            Thread.Sleep(500);

            //move motor 0 (Rotation left)
            motor = 0;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 450)
            {
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                App.TxtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

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

            App.Log.WriteInfo(String.Format("Start Count motor {0}", currentCountMotor));

            while (C[motor] < currentCountMotor + 300)
            {
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                App.TxtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(1000);

            //-- Start vacuum capture
            VacuumCaptureOn();

            Thread.Sleep(1000);

            //-- Move motor 1 (Lift Up)
            motor = 1;
            while (I[motor] != 1)
            {
                App.TxtLink.SetMotorValue(1, 512, MotorDirection.Right);
            }
            App.TxtLink.SetMotorValue(1, 0, MotorDirection.Right);

            Thread.Sleep(500);

            //move motor 0 (Rotation left)
            motor = 0;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 900)
            {
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            //-- Move motor 2 (Вылет)
            motor = 2;

            Thread.Sleep(500);

            currentCountMotor = C[motor];

            App.Log.WriteInfo(String.Format("Start Count motor {0}", currentCountMotor));

            while (C[motor] < currentCountMotor + 600)
            {
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                App.TxtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

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

            App.Log.WriteInfo(String.Format("Start Count motor {0}", currentCountMotor));

            while (C[motor] < currentCountMotor + 300)
            {
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                App.TxtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(1000);

            //-- Start vacuum capture
            VacuumCaptureOn();

            Thread.Sleep(1000);

            //-- Move motor 1 (Lift Up)
            motor = 1;
            while (I[motor] != 1)
            {
                App.TxtLink.SetMotorValue(1, 512, MotorDirection.Right);
            }
            App.TxtLink.SetMotorValue(1, 0, MotorDirection.Right);

            Thread.Sleep(500);

            //move motor 0 (Rotation left)
            motor = 0;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 1370)
            {
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            //-- Move motor 1 (Lift down)
            motor = 1;

            currentCountMotor = C[motor];

            while (C[motor] < currentCountMotor + 850)
            {
                App.TxtLink.SetMotorValue(motor, 450, MotorDirection.Left);
            }
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Left);

            Thread.Sleep(500);

            //-- Stop vaacuum capture
            VacuumCaptureOff();
        }

        private static void VacuumCaptureOn()
        {
            App.TxtLink.SetOutputValue(6, 512);
            Thread.Sleep(1000);
            App.TxtLink.SetOutputValue(7, 512);
        }

        private static void VacuumCaptureOff()
        {
            App.TxtLink.SetOutputValue(7, 0);
            Thread.Sleep(500);
            App.TxtLink.SetOutputValue(6, 0);
        }

        private static void SetMotorToNull()
        {
            int motor = 1;
            int sensor = 1;

            while (I[sensor] != 1)
            {
                Thread.Sleep(200);
                App.Log.WriteInfo("Start motor");
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Right);

            }
            App.Log.WriteInfo("Stop motor");
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Right);

            Thread.Sleep(200);

            motor = 0;
            sensor = 0;
            while (I[sensor] != 1)
            {
                Thread.Sleep(200);
                App.Log.WriteInfo("Start motor");
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Right);

            }
            App.Log.WriteInfo("Stop motor");
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Right);

            Thread.Sleep(200);

            motor = 2;
            sensor = 2;
            while (I[sensor] != 1)
            {
                Thread.Sleep(200);
                App.Log.WriteInfo("Start motor");
                App.TxtLink.SetMotorValue(motor, 512, MotorDirection.Right);

            }
            App.Log.WriteInfo("Stop motor");
            App.TxtLink.SetMotorValue(motor, 0, MotorDirection.Right);
        }
    }
}

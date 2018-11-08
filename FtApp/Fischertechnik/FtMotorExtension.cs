using System;
using System.Collections.Generic;
using System.Text;

namespace TXTCommunication.Fischertechnik.Txt
{
    // Hold values of motors for single controller
    class FtMotorExtension
    {
        public short[] DistanceValues { get; internal set; }
        public short[] CommandId { get; internal set; }
        public short[] CounterResetCommandId { get; internal set; }

        public short[] CounterInput { get; internal set; }
        public short[] CounterValue { get; internal set; }

        public short[] CounterCommandId { get; internal set; }
        public short[] MotorCommandId { get; internal set; }

        public FtMotorExtension(int extensionId)
        {


            DistanceValues = new short[TxtInterface.MotorOutputs];
            CommandId = new short[TxtInterface.MotorOutputs];
        }

        public void SetMotorDistance(int motorIndex, short distance)
        {
            if (DistanceValues.Length < motorIndex)
            {
                throw new InvalidOperationException($"O{motorIndex * 2} or O{motorIndex * 2 + 1} is not a motor");
            }

            DistanceValues[motorIndex] = distance;
            CommandId[motorIndex]++;
        }

        public void ResetValues()
        {
            DistanceValues.Initialize();
            CommandId.Initialize();
        }
    }
}

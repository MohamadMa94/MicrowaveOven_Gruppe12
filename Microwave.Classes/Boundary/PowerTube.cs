using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        public enum PowerLevel : int
        {
            Low = 500,
            Medium = 800,
            High = 1000
        }

        public PowerLevel PowerLevelState { get; set; }

        public PowerTube(IOutput output, PowerLevel powerLevelState)
        {
            myOutput = output;
            PowerLevelState = powerLevelState;
        }

        

        public void TurnOn(int power)
        {
            if (power < 1 || (int)PowerLevelState < power)
            {
                //throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 700 (incl.)");
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 700  (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}
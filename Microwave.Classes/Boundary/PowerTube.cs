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
            if (PowerLevelState == PowerLevel.Low)
            {
                if (power <= 0 || (int)PowerLevelState < power)
                {
                    throw new ArgumentOutOfRangeException("power", power, $"Must be between 1 and {(int)PowerLevel.Low} (incl.)");
                }
            }
            if (PowerLevelState == PowerLevel.Medium)
            {
                if (power < (int)PowerLevel.Low || (int)PowerLevel.Medium < power)
                {
                    throw new ArgumentOutOfRangeException("power", power, $"Must be between {(int)PowerLevel.Low} and {(int)PowerLevel.Medium} (incl.)");
                }
            }
            if (PowerLevelState == PowerLevel.High)
            {
                if (power < (int)PowerLevel.Medium || (int)PowerLevel.High < power)
                {
                    throw new ArgumentOutOfRangeException("power", power, $"Must be between {(int)PowerLevel.Medium} and {(int)PowerLevel.High} (incl.)");
                }
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
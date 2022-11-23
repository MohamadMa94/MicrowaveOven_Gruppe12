﻿using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new PowerTube(output, PowerTube.PowerLevel.Low); 
        }

        [TestCase(PowerTube.PowerLevel.Low, 1)]
        [TestCase(PowerTube.PowerLevel.Low, 1)]
        [TestCase(PowerTube.PowerLevel.Low, 50)]
        [TestCase(PowerTube.PowerLevel.Low, 100)]
        [TestCase(PowerTube.PowerLevel.Low, (int)PowerTube.PowerLevel.Low - 1)]
        [TestCase(PowerTube.PowerLevel.Low, (int)PowerTube.PowerLevel.Low)]
        [TestCase(PowerTube.PowerLevel.Medium, 500)]
        [TestCase(PowerTube.PowerLevel.Medium, 699)]
        [TestCase(PowerTube.PowerLevel.Medium, 700)]
        [TestCase(PowerTube.PowerLevel.Medium, (int)PowerTube.PowerLevel.Medium - 1)]
        [TestCase(PowerTube.PowerLevel.Medium, (int)PowerTube.PowerLevel.Medium)]
        [TestCase(PowerTube.PowerLevel.High, 810)]
        [TestCase(PowerTube.PowerLevel.High, 910)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(PowerTube.PowerLevel powerLevel,int power)
        {
            uut.PowerLevelState = powerLevel;
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(PowerTube.PowerLevel.Low, -5)]
        [TestCase(PowerTube.PowerLevel.Low, -1)]
        [TestCase(PowerTube.PowerLevel.Low, 0)]
        [TestCase(PowerTube.PowerLevel.Low, 501)]
        [TestCase(PowerTube.PowerLevel.Medium, 801)]
        [TestCase(PowerTube.PowerLevel.High, 1001)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(PowerTube.PowerLevel powerLevel,int power)
        {
            uut.PowerLevelState = powerLevel;
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }
    }
}
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Controllers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microwave.Test.Unit
{
    class BuzzerTest
    {
        private Buzzer uut;
        private IOutput output;
        private IUserInterface ui;
        private ICookController cookController;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            ui = Substitute.For<IUserInterface>();
            cookController = Substitute.For<ICookController>();



            uut = new Buzzer(output);
        }

        [Test]
        public void BuzzerSound_When_CookingIsDone()
        {
            uut.StartBuzzing();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("BEEP BEEP BEEP")));
        }
    }
}

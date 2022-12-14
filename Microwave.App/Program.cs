using System;
using System.Collections.Generic;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static PowerTube.PowerLevel power = PowerTube.PowerLevel.Medium;
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();
            Console.WriteLine("Buzz");

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output,power);

            Light light = new Light(output);

            Buzzer buzzer = new Buzzer(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube, buzzer);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, buzzer, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            System.Console.ReadLine();

            //run test
        }
    }
}

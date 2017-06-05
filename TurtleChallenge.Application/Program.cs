using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.GameObjects;
using YamlDotNet.Serialization;

namespace TurtleChallenge.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($" ---- Welcome to the Turtle Challenge ---- ");
            Console.ResetColor();
            Console.WriteLine();

            var configFile = "config";
            var actionsFile = "actions";

            if (args.Length > 0)
            {
                configFile = args[0];

                if (args.Length > 1)
                {
                    actionsFile = args[1];
                }
            }

            var stage = Game.Configure(configFile);
            if (stage != null) { 
                Start(stage, actionsFile);
            }
        }

        public static void Start(Stage stage, string actionsFile)
        {
            var sequence = Game.GetSequence(actionsFile);

            if (sequence != null)
            {
                var turtle = stage.Turtle;
                var board = stage.Board;
                var gameover = false;

                foreach (var action in sequence.Actions.Select((value, i) => new { i, value }))
                {
                    var act = action.i + 1;

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"Turtle");
                    Console.Write($" | Position: { turtle.PosX }-{ turtle.PosY }");
                    Console.WriteLine($" | Direction: { turtle.Direction.ToString() }");
                    Console.ResetColor();

                    Console.Write($"Action { act } -> ");

                    if (action.value == GameObjects.Action.Move)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }

                    Console.WriteLine($"{ action.value.ToString() }");

                    Console.ForegroundColor = ConsoleColor.White;

                    switch (action.value)
                    {
                        case GameObjects.Action.Move:
                            gameover = Move(act, turtle, board);
                            break;
                        case GameObjects.Action.Rotate:
                            Rotate(act, turtle);
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine();

                    if (gameover)
                    {
                        break;
                    }
                }

                if (!gameover)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"** Sadly the Turtle never found the exit and got lost forever... **");
                    Console.ResetColor();
                }

                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Little Turtle we got a problem! Action file do not exist or it is invalid.");
                Console.ResetColor();
            }
        }

        private static void Rotate(int act, Turtle turtle)
        {
            Console.Write($"Turtle was pointing to { turtle.Direction.ToString() } and rotated to ");
            turtle.Rotate();
            Console.WriteLine($"{ turtle.Direction.ToString() }.");
        }

        private static bool Move(int act, Turtle turtle, Board board)
        {
            var gameover = false;

            if (board.IsMovingOut(turtle))
            {
                Console.WriteLine($"The Turtle hitted the wall and stayed in place.");
            }
            else
            {
                Console.Write($"The turtle moved from { turtle.PosX  }-{ turtle.PosY } to ");
                turtle.Move();
                Console.Write($"{ turtle.PosX  }-{ turtle.PosY } and ");

                var cell = board.GetActualCell(turtle);
                if (cell.IsMine)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"triggered a mine!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"** YOU DIED **");
                    Console.ResetColor();
                    gameover = true;
                }
                else if (cell.IsExit)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"found the exit!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"** GOOD JOB **");
                    Console.ResetColor();
                    gameover = true;
                }
                else
                {
                    Console.WriteLine($"it is safe.");
                }
            }

            return gameover;
        }
    }
}

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
    public class Game
    {
        private static Configuration GetConfiguration(string configFile)
        {
            try
            {
                var file = File.ReadAllText(configFile + ".yml");
                var deserializerBuilder = new DeserializerBuilder();
                var deserializer = deserializerBuilder.Build();
                var configuration = deserializer.Deserialize<Configuration>(file);
                return configuration;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public static Sequence GetSequence(string actionsFile)
        {
            try
            {
                var file = File.ReadAllText(actionsFile + ".yml");
                var deserializerBuilder = new DeserializerBuilder();
                var deserializer = deserializerBuilder.Build();
                var sequence = deserializer.Deserialize<Sequence>(file);
                return sequence;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public static Stage Configure(string configFile)
        {
            var configuration = GetConfiguration(configFile);

            if (configuration != null)
            {
                if (configuration.Validate(configuration.Board,
                                            configuration.Turtle,
                                            configuration.Mines,
                                            configuration.Exit)
                )
                {
                    var board = configuration.Board;
                    board.Populate(configuration.Mines, configuration.Exit);

                    var turtle = configuration.Turtle;

                    return new Stage()
                    {
                        Board = board,
                        Turtle = turtle
                    };
                }
                else
                {
                    Console.WriteLine("Configuration is invalid.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Little Turtle we got a problem! Configuration file do not exist or it is invalid.");
                Console.ResetColor();
            }

            return null;
        }
    }
}

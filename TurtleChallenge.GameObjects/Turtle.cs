using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace TurtleChallenge.GameObjects
{
    public class Turtle : Position
    {
        [YamlMember(Alias = "direction")]
        public Direction Direction { get; set; }

        public void Rotate()
        {
            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.South;
                    break;
                case Direction.South:
                    Direction = Direction.West;
                    break;
                case Direction.West:
                    Direction = Direction.North;
                    break;
                default:
                    break;
            }
        }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.North:
                    PosY--;
                    break;
                case Direction.East:
                    PosX++;
                    break;
                case Direction.South:
                    PosY++;
                    break;
                case Direction.West:
                    PosX--;
                    break;
                default:
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace TurtleChallenge.GameObjects
{
    public class Board
    {
        [YamlMember(Alias = "size_x")]
        public int SizeX { get; set; }
        [YamlMember(Alias = "size_y")]
        public int SizeY { get; set; }

        public Cell[,] Cells { get; set; }

        public Board()
        {

        }

        public Board(int SizeX, int SizeY, List<Position> Mines, Position Exit)
        {
            Cells = new Cell[SizeX, SizeY];

            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    Cells[x, y] = new Cell();
                }
            }

            foreach (var Mine in Mines)
            {
                Cells[Mine.PosX, Mine.PosY].IsMine = true;
            }

            Cells[Exit.PosX, Exit.PosY].IsExit = true;
        }

        public void Populate(List<Position> Mines, Position Exit)
        {
            Cells = new Cell[SizeX, SizeY];

            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    Cells[x, y] = new Cell();
                }
            }

            foreach (var Mine in Mines)
            {
                Cells[Mine.PosX, Mine.PosY].IsMine = true;
            }

            Cells[Exit.PosX, Exit.PosY].IsExit = true;
        }

        public bool IsMovingOut(Turtle turtle)
        {
            var posX = turtle.PosX;
            var posY = turtle.PosY;

            switch (turtle.Direction)
            {
                case Direction.North:
                    posY--;
                    break;
                case Direction.East:
                    posX++;
                    break;
                case Direction.South:
                    posY++;
                    break;
                case Direction.West:
                    posX--;
                    break;
                default:
                    break;
            }

            return (posX < 0 || posX+1 > SizeX || posY < 0 || posY+1 > SizeY) ? true : false;
        }

        public Cell GetActualCell(Turtle turtle)
        {
            return Cells[turtle.PosX, turtle.PosY];
        }
    }
}

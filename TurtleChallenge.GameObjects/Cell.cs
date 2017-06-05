using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.GameObjects
{
    public class Cell
    {
        public Cell()
        {
            IsMine = false;
            IsExit = false;
        }

        public bool IsMine { get; set; }
        public bool IsExit { get; set; }
    }
}

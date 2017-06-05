using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace TurtleChallenge.GameObjects
{
    public class Position
    {
        [YamlMember(Alias = "pos_x")]
        public int PosX { get; set; }
        [YamlMember(Alias = "pos_y")]
        public int PosY { get; set; }
    }
}

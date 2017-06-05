using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace TurtleChallenge.GameObjects
{
    public class Sequence
    {
        [YamlMember(Alias = "actions")]
        public List<Action> Actions { get;set; }
    }
}

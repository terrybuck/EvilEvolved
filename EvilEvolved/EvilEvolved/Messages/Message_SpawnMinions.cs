using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{
    class Message_SpawnMinions : GenericMessage
    {
        public Message_SpawnMinions(int number)
            :base("Spawn")
        {
            NumberOfMinions = number;
        }

        public int NumberOfMinions { get; set; }
    }
}

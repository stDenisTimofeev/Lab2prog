using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    struct QueueObjects
    {
        public int newId;
        public int newX;
        public int newY;

        public int ID { get { return newId; } set { newId = value; } }
        public int x { get { return newX; } set { newX = value; } }
        public int y { get { return newY; } set { newY = value; } }

    }
}

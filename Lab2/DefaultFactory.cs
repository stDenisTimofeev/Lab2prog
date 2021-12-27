using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class DefaultFactory : IGraphicFactory
    {
        static Random rand = new Random();
        int nameIndex = rand.Next(0, 2);
        string name = rand.Next(0, 2) == 0 ? "Rectangle" : "Ellipse";

        public GraphObject CreateGraphObject()
        {
            if (name == "Rectangle")
                return new Rectangle();
            else 
                return new Ellipse();
        }

    }



}

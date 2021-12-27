using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class RandomObjectFactory : IGraphicFactory
    {
        string current_type;
        static Random rand = new Random();
        public GraphObject CreateGraphObject()
        {

            if (rand.Next(0, 2) == 0)
            {
                current_type = "Rectangle";
                return new Rectangle();
            }
            else
            {
                current_type = "Ellipse";
                return new Ellipse();
            }
        }
    }
}

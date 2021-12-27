using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class TwoTypeFactory : IGraphicFactory
    {
        private bool type = false;
        public GraphObject CreateGraphObject()
        {
            type = !type;
            if (type)
            {
                return new Rectangle();
            }
            else
            {
                return new Ellipse();
            }

        }
    }
}

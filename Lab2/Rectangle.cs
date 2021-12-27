using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Rectangle : GraphObject
    {
        protected int h, w;
        public int H
        {
            get { return h; }
            set { h = value; }
        }

        public int W
        {
            get { return w; }
            set { w = value; }
        }

      
        public Rectangle() : base()
        {
            h = 50;
            w = 50;
        }

        public override bool ContainsPoint(Point p)
        {
            return (p.X <= x + w) & (p.X >= x) & (p.Y >= y) & (p.Y <= y + h);

        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(brush, x, y, w, h);

            if (Selected)
            {
                g.DrawRectangle(Pens.Cyan, x, y, w, h);
            }
            else
            {
                g.DrawRectangle(Pens.Black, x, y, w, h);
            }

        }
    }
}

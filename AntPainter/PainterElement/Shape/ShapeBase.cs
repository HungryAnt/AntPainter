using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntPainter
{
    public class ShapeBase : IDisplayObject
    {
        public Point Location { get; set; }

        public Size Size { get; set; }

        public Rectangle ClientRectangle
        {
            get
            {
                return new Rectangle(Location, Size);
            }
        }

        public virtual void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.BlueViolet, ClientRectangle);
        }

        public virtual bool Contains(Point pt)
        {
            return ClientRectangle.Contains(pt);
        }
    }
}

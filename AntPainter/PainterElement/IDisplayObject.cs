using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntPainter
{
    public interface IDisplayObject
    {
        Point Location { get; set; }

        Size Size { get; set; }

        Rectangle ClientRectangle { get; }

        void Draw(Graphics g);

        bool Contains(Point pt);
    }
}

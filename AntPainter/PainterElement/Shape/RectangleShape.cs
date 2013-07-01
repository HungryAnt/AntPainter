using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntPainter
{
    public class RectangleShape : ShapeBase
    {
        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.AliceBlue, ClientRectangle);

            base.Draw(g);
        }
    }
}

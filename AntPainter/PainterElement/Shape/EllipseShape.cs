using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AntPainter
{
    class EllipseShape : ShapeBase
    {
        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.AliceBlue, ClientRectangle);
            base.Draw(g);
        }

        public override bool Contains(Point pt)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(ClientRectangle);
                return path.IsVisible(pt);
            }
        }
    }
}

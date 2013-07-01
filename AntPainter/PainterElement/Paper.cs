using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AntPainter
{
    public class Paper
    {
        public Paper()
        {
            Size = new Size(500, 400);
            Layers = new List<Layer>();
            Layers.Add(new Layer(this));
        }

        public List<Layer> Layers { get; set; }

        public Size Size { get; set; }

        public Rectangle ClientRectangle
        {
            get
            {
                return new Rectangle(new Point(0, 0), Size);
            }
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.White, ClientRectangle);

            foreach (Layer layer in Layers)
            {
//                 if (layer.Focus)
//                 {
// //                         using (Pen pen = new Pen(Color.Red, 1))
// //                         {
// //                             //pen.Alignment = PenAlignment.Inset;
// //                             //Rectangle rect = new Rectangle(paper.Bound.Left, paper.Bound.Top, paper.Bound.Width - 1, paper.Bound.Height - 1);
// //                             g.DrawRectangle(pen, paper.Bound);
// //                         }
//                     System.Windows.Forms.ControlPaint.DrawFocusRectangle(g, ClientRectangle);
//                 }

                layer.Draw(g);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntPainter
{
    interface IMouseTool
    {
        void OnMouseDown(object sender, MouseEventArgs e);

        void OnMouseUp(object sender, MouseEventArgs e);

        void OnMouseMove(object sender, MouseEventArgs e);
    }
}

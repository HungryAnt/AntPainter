using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntPainter
{
    public class PaperModel
    {
        public PaperModel(Paper paper)
        {
            Paper = paper;

            CurrentLayer = paper.Layers.Last();
        }

        Size _size;
        Point _mouseLocation;

        public Paper Paper { get; set; }

        public Layer CurrentLayer { get; set; }

        public void SetSize(Size size)
        {
            _size = size;
        }

//         public void SetMouseLocation(Point location)
//         {
//             _mouseLocation = location;
//         }
// 
//         public void ShowFocusElement()
//         {
//             if (Paper.Layers.Count > 0)
//             {
//                 Paper.Layers.Last().Focus = true;
//             }
//         }

        public bool TryGetFocusDisplayObject(Point pt, out IDisplayObject displayObject)
        {
            if (CurrentLayer != null)
            {
                foreach (IDisplayObject dispObj in CurrentLayer.DisplayObjects.Reverse<IDisplayObject>())
                {
                    if (dispObj.Contains(pt))
                    {
                        displayObject = dispObj;
                        return true;
                    }
                }
            }

            displayObject = null;
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AntPainter
{
    public class Layer : IDisplayObject
    {
        public Layer(Paper paper)
        {
            DisplayObjects = new List<IDisplayObject>();
        }

        Paper _paper;

        public Point Location
        {
            get
            {
                return new Point(0, 0);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Size Size
        {
            get
            {
                return _paper.Size;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Rectangle ClientRectangle
        {
            get { return _paper.ClientRectangle; }
        }

        public bool Contains(Point pt)
        {
            return ClientRectangle.Contains(pt);
        }

        public string Name { get; set; }

        //public bool Focus { get; set; }

        public List<IDisplayObject> DisplayObjects { get; private set; }

        public void Add(IDisplayObject displayObject)
        {
            DisplayObjects.Add(displayObject);
        }

        public void Remove(IDisplayObject displayObject)
        {
            DisplayObjects.Remove(displayObject);
        }

        public void Draw(Graphics g)
        {
            foreach (IDisplayObject displayObject in DisplayObjects)
            {
                displayObject.Draw(g);
            }
        }
    }
}

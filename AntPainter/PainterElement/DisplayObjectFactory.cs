using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntPainter
{
    public static class DisplayObjectFactory
    {
        public static IDisplayObject CreateDisplayObject(ObjectType objType)
        {
            switch (objType)
            {
                case ObjectType.None:
                    return null;
                case ObjectType.RectangleShape:
                    return new RectangleShape();
                case ObjectType.EllipseShape:
                    return new EllipseShape();
            }

            throw new NotImplementedException();
        }
    }
}

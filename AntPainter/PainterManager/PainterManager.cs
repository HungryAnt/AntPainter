using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntPainter
{
    public class PainterManager
    {
        static PainterManager _instance = new PainterManager();

        public static PainterManager Instance
        {
            get
            {
                return _instance;
            }
        }

        PainterManager()
        {

        }


        public ObjectType ObjectType;

        public IDisplayObject CreateDisplayObject()
        {
            return DisplayObjectFactory.CreateDisplayObject(ObjectType);
        }

    }
}

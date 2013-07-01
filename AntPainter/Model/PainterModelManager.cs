using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntPainter
{
    public class ModelsManager
    {
        static ModelsManager _instance = new ModelsManager();

        public static ModelsManager Instance
        {
            get
            {
                return _instance;
            }
        }

        ModelsManager()
        {
            PainterModel = new PainterModel();
        }

        public PainterModel PainterModel { get; private set; }

        public void CreateNewPaperModel()
        {
            PaperModel paperModel = new PaperModel(new Paper());

            PainterModel.PaperModel = paperModel;
        }

    }
}

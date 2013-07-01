using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntPainter
{
    public partial class FormPainter : Form
    {
        public FormPainter()
        {
            InitializeComponent();
        }

        private void miNew_Click(object sender, EventArgs e)
        {
            ModelsManager.Instance.CreateNewPaperModel();

            PaperControl paperControl = new PaperControl(ModelsManager.Instance.PainterModel.PaperModel)
            {
                Dock = DockStyle.Fill
            };

            panelPaper.Controls.Add(paperControl);
        }

        private void toolStripButtonCursor_Click(object sender, EventArgs e)
        {
            PainterManager.Instance.ObjectType = ObjectType.None;
        }

        private void toolStripButtonRectangleShape_Click(object sender, EventArgs e)
        {
            PainterManager.Instance.ObjectType = ObjectType.RectangleShape;
        }

        private void toolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            PainterManager.Instance.ObjectType = ObjectType.EllipseShape;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Drawing.Drawing2D;

namespace AntPainter
{
    public partial class PaperControl : UserControl
    {
        public PaperControl()
        {
            InitializeComponent();
        }

        public PaperControl(PaperModel paperModel)
        {
            PaperModel = paperModel;
            InitializeComponent();
            InitAllRickMouseTool();
        }

        PaperModel PaperModel { get; set; }

        #region Mouse Tool

        IMouseTool MouseTool { get; set; }

        Dictionary<MouseToolType, IMouseTool> _allMouseTools = new Dictionary<MouseToolType, IMouseTool>();

        void InitAllRickMouseTool()
        {
            _allMouseTools.Add(MouseToolType.SelectObject, GetSelectObjectMouseTool());
            _allMouseTools.Add(MouseToolType.MoveObject, GetMoveObjectMouseTool());
            _allMouseTools.Add(MouseToolType.CreateObject, GetCreateObjectMouseTool());

            ChangeMouseTool(MouseToolType.SelectObject);
        }

        void ChangeMouseTool(MouseToolType mouseToolType)
        {
            MouseTool = _allMouseTools[mouseToolType];
        }

        Point _originalMouseLocation = new Point();

        Point _originalSelectObjectLocation = new Point();

        IDisplayObject _focusedDisplayObject;

        NormalMouseTool GetSelectObjectMouseTool()
        {
            NormalMouseTool normalMouseTool = new NormalMouseTool();

            normalMouseTool.MouseDown += new MouseEventHandler(
                (sender, e) =>
                {
                    if (PaperModel != null)
                    {
                        // e.Location为相对坐标
//                         PaperModel.SetMouseLocation(e.Location);
//                         PaperModel.ShowFocusElement();
//                         Invalidate();

                        IDisplayObject displayObject;

                        if (PaperModel.TryGetFocusDisplayObject(e.Location, out displayObject))
                        {
                            _focusedDisplayObject = displayObject;
                            _originalSelectObjectLocation = _focusedDisplayObject.Location;
                        }
                        else
                        {
                            _focusedDisplayObject = null;
                        }
                    }

                    _originalMouseLocation = e.Location;
                }
                );
            normalMouseTool.MouseMove += new MouseEventHandler(
                (sender, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (Math.Abs(_originalMouseLocation.X - e.Location.X) > 3
                            || Math.Abs(_originalMouseLocation.Y - e.Location.Y) > 3)
                        {
                            ChangeMouseTool(MouseToolType.MoveObject);
                        }
                    }
                }
                );

            return normalMouseTool;
        }

        NormalMouseTool GetMoveObjectMouseTool()
        {
            NormalMouseTool normalMouseTool = new NormalMouseTool();

            normalMouseTool.MouseMove += new MouseEventHandler(
                (sender, e) =>
                {
                    if (_focusedDisplayObject != null)
                    {
                        int offsetX = e.Location.X - _originalMouseLocation.X;
                        int offsetY = e.Location.Y - _originalMouseLocation.Y;

                        _focusedDisplayObject.Location = _originalSelectObjectLocation + new Size(offsetX, offsetY);

                        Invalidate();
                    }
                }
                );
            normalMouseTool.MouseUp += new MouseEventHandler(
                (sender, e) =>
                {
                    ChangeMouseTool(MouseToolType.SelectObject);
                }
                );

            return normalMouseTool;
        }


        IDisplayObject _shadowDisplayObject;

        static void SetDisplayObjectArea(IDisplayObject displayObject, Point beginLocation, Point endLocation)
        {
            int left = Math.Min(beginLocation.X, endLocation.X);
            int right = Math.Max(beginLocation.X, endLocation.X);
            int top = Math.Min(beginLocation.Y, endLocation.Y);
            int bottom = Math.Max(beginLocation.Y, endLocation.Y);

            displayObject.Location = new Point(left, top);
            displayObject.Size = new Size(Math.Max(right - left, 3), Math.Max(bottom - top, 3));
        }

        NormalMouseTool GetCreateObjectMouseTool()
        {
            NormalMouseTool normalMouseTool = new NormalMouseTool();

            Point beginLocation = new Point();
            Point endLocation = new Point();

            normalMouseTool.MouseDown += new MouseEventHandler(
                (sender, e) =>
                {
                    Cursor = Cursors.Cross;

                    beginLocation = e.Location;

                    _shadowDisplayObject = PainterManager.Instance.CreateDisplayObject();
                }
                );

            normalMouseTool.MouseMove += new MouseEventHandler(
                (sender, e) =>
                {
                    if (_shadowDisplayObject != null)
                    {
                        endLocation = e.Location;

                        SetDisplayObjectArea(_shadowDisplayObject, beginLocation, endLocation);

                        Invalidate();
                    }
                }
                );
            normalMouseTool.MouseUp += new MouseEventHandler(
                (sender, e) =>
                {
                    if (_shadowDisplayObject != null)
                    {
                        SetDisplayObjectArea(_shadowDisplayObject, beginLocation, endLocation);

                        Cursor = Cursors.Default;

                        PaperModel.CurrentLayer.Add(_shadowDisplayObject);

                        _shadowDisplayObject = null;

                        Invalidate();

                        ChangeMouseTool(MouseToolType.SelectObject);
                        PainterManager.Instance.ObjectType = ObjectType.None;
                    }
                }
                );

            return normalMouseTool;
        }

        #endregion

        #region 鼠标事件处理
        private void PaperControl_Click(object sender, EventArgs e)
        {

        }

        private void PaperControl_SizeChanged(object sender, EventArgs e)
        {
            if (PaperModel != null)
            {
                PaperModel.SetSize(this.Size);
            }
        }

        private void PaperControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (PainterManager.Instance.ObjectType != ObjectType.None)
            {
                ChangeMouseTool(MouseToolType.CreateObject);
            }
            else
            {
                ChangeMouseTool(MouseToolType.SelectObject);
            }

            MouseTool.OnMouseDown(sender, e);
        }

        private void PaperControl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseTool.OnMouseMove(sender, e);
        }

        private void PaperControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseTool.OnMouseUp(sender, e);
        }

        #endregion

        #region 绘图逻辑

        private void PaperControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (PaperModel != null)
            {
                PaperModel.Paper.Draw(g);
            }

            if (_shadowDisplayObject != null)
            {
                _shadowDisplayObject.Draw(g);
            }
        }

        #endregion

    }
}

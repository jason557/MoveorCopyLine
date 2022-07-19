using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace move_or_copy_a_line
{
    public partial class Form1 : Form
    {
        private List<LineList> MyLines = new List<LineList>();
        public Point MouseDownLocation;
        private bool IsMouseDown = false;
        private int m_StartX;
        private int m_StartY;
        private int m_CurX;
        private int m_CurY;
        private string DrawCase = "Line";
        Point Point1 = new Point();
        Point Point2 = new Point();
        Point StartDownLocation = new Point();
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;

            m_StartX = e.X;
            m_StartY = e.Y;
            m_CurX = e.X;
            m_CurY = e.Y;
            StartDownLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen dashed_pen = new Pen(Color.Green, 1);
            dashed_pen.DashStyle = DashStyle.Dash;
            if (IsMouseDown == false) return;
            m_CurX = e.X;
            m_CurY = e.Y;
            switch (DrawCase)
            {
                case "Line":
                    {
                        break;
                    }
                case "CopyLine":
                    {
                        int i;
                        i = MyLines.Count - 1;
                        if (i >= 0)
                        {
                            Point1.X = e.X + MyLines[i].X1 - StartDownLocation.X;
                            Point1.Y = e.Y + MyLines[i].Y1 - StartDownLocation.Y;
                            Point2.X = e.X + MyLines[i].X2 - StartDownLocation.X;
                            Point2.Y = e.Y + MyLines[i].Y2 - StartDownLocation.Y;
                        }
                        break;

                    }
                case "MoveLine":
                    {
                        int i;
                        i = MyLines.Count - 1;
                        if (i >= 0)
                        {
                            Point1.X = e.X + MyLines[i].X1 - StartDownLocation.X;
                            Point1.Y = e.Y + MyLines[i].Y1 - StartDownLocation.Y;
                            Point2.X = e.X + MyLines[i].X2 - StartDownLocation.X;
                            Point2.Y = e.Y + MyLines[i].Y2 - StartDownLocation.Y;
                        }
                        break;

                    }
            }

            pictureBox1.Invalidate();

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;

            if (e.Button == MouseButtons.Left)
            {
                switch (DrawCase)
                {
                    case "Line":
                        {
                            LineList DrawLine = new LineList();
                            DrawLine.X1 = m_StartX;
                            DrawLine.Y1 = m_StartY;
                            DrawLine.X2 = m_CurX;
                            DrawLine.Y2 = m_CurY;
                            MyLines.Add(DrawLine);
                            break;
                        }
                    case "CopyLine":
                        {
                            LineList DrawLine = new LineList();
                            DrawLine.X1 = Point1.X;
                            DrawLine.Y1 = Point1.Y;
                            DrawLine.X2 = Point2.X;
                            DrawLine.Y2 = Point2.Y;
                            MyLines.Add(DrawLine);
                            break;
                        }
                    case "MoveLine":
                        {
                            LineList DrawLine = new LineList();
                            DrawLine.X1 = Point1.X;
                            DrawLine.Y1 = Point1.Y;
                            DrawLine.X2 = Point2.X;
                            DrawLine.Y2 = Point2.Y;
                            MyLines.Add(DrawLine);
                            int count = MyLines.Count - 1;
                            MyLines.RemoveAt(count - 1);
                            break;
                        }
                }
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int i, x1, y1, x2, y2;

            for (i = 0; i <= MyLines.Count - 1; i++)
            {
                Pen LinePen = new Pen(Color.FromArgb(255, 255, 0, 0), 3);
                x1 = MyLines[i].X1;
                x2 = MyLines[i].X2;
                y1 = MyLines[i].Y1;
                y2 = MyLines[i].Y2;
                e.Graphics.DrawLine(LinePen, x1, y1, x2, y2);
            }


            if (IsMouseDown == true)
            {
                switch (DrawCase)
                {
                    case "Line":
                        {
                            Pen dashed_pen = new Pen(Color.Blue, 1);
                            e.Graphics.DrawLine(dashed_pen, m_StartX, m_StartY, m_CurX, m_CurY);
                            break;
                        }
                    case "CopyLine":
                        {
                            Pen dashed_pen = new Pen(Color.Blue, 1);
                            e.Graphics.DrawLine(dashed_pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
                            break;
                        }
                    case "MoveLine":
                        {
                            Pen dashed_pen = new Pen(Color.Blue, 1);
                            e.Graphics.DrawLine(dashed_pen, Point1.X, Point1.Y, Point2.X, Point2.Y);
                            break;
                        }

                }


            }

        }

        private void copyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCase = "CopyLine";
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCase = "Line";
        }

        private void moveLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawCase = "MoveLine";

        }
    }
}

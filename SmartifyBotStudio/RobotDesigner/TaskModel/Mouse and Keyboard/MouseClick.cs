using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard
{
    [Serializable]
    public class MouseClick : RobotActionBase, ITask
    {

        public int X { get; set; }
        public int Y { get; set; }

        public int Speed { get; set; }

        public int SpeedComboIndex = 0;
        public int ClickIndex { get; set; }

        public bool BeforeEventCheck = true;


        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);



        public int Execute()
        {
            try
            {
                if (BeforeEventCheck)
                {
                    Point target = new Point(X, Y);

                    LinearSmoothMove(target, Speed);

                    Click();
                }
                else
                {
                    Click();

                }


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public void Click()
        {
            if (ClickIndex == 0)  //left
            {
                LeftClick();
            }
            if (ClickIndex == 1)  //right
            {
                RightClick();
            }
            if (ClickIndex == 2) //double
            {
                //LeftClick();
                //LeftClick();
                mouse_event(0x00000002, X,Y,0,0);
                mouse_event(0x00000004, X, Y, 0, 0);

                mouse_event(0x00000002, X, Y, 0, 0);
                mouse_event(0x00000004, X, Y, 0, 0);
            }
            if (ClickIndex == 3) // left down
            {
                LeftDown();
            }
            if (ClickIndex == 4)  // left up
            {
                LefttUp();
            }
            if (ClickIndex == 5)  // right down
            {
                RightDown();
            }
            if (ClickIndex == 6)  // right up
            {
                RightUp();
            }
        }

        public void RightClick()
        {
            MouseEvent(MouseEventFlags.RightDown);
            MouseEvent(MouseEventFlags.RightUp);
        }

        public void LeftClick()
        {
            MouseEvent(MouseEventFlags.LeftDown);
            MouseEvent(MouseEventFlags.LeftUp);
        }

        public void RightUp()
        {
            MouseEvent(MouseEventFlags.RightUp);

        }

        public void RightDown()
        {
            MouseEvent(MouseEventFlags.RightDown);

        }

        public void LefttUp()
        {
            MouseEvent(MouseEventFlags.LeftUp);

        }

        public void LeftDown()
        {
            MouseEvent(MouseEventFlags.LeftDown);

        }

        public void MiddleUp()
        {
            MouseEvent(MouseEventFlags.MiddleUp);

        }
        public void MiddleDown()
        {
            MouseEvent(MouseEventFlags.MiddleDown);

        }

        public void MiddleClick()
        {
            MouseEvent(MouseEventFlags.MiddleDown);
            MouseEvent(MouseEventFlags.MiddleUp);

        }
        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        public void LinearSmoothMove(Point newPosition, int steps)
        {
            Point start = System.Windows.Forms.Cursor.Position;// GetCursorPosition();
            PointF iterPoint = start;

            // Find the slope of the line segment defined by start and newPosition
            PointF slope = new PointF(newPosition.X - start.X, newPosition.Y - start.Y);

            // Divide by the number of steps
            slope.X = slope.X / steps;
            slope.Y = slope.Y / steps;

            // Move the mouse to each iterative point.
            for (int i = 0; i < steps; i++)
            {
                iterPoint = new PointF(iterPoint.X + slope.X, iterPoint.Y + slope.Y);
                System.Windows.Forms.Cursor.Position = (Point.Round(iterPoint));
                Thread.Sleep(100);
            }

            // Move the mouse to the final destination.
            System.Windows.Forms.Cursor.Position = newPosition;
        }


    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }



}

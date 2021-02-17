using SmartifyBotStudio.Models;
using SmartifyBotStudio.RobotDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartifyBotStudio.RobotDesigner.TaskModel.Mouse_and_Keyboard
{
    [Serializable]
    public class MoveMouse : RobotActionBase, ITask
    {

        public int X { get; set; }
        public int Y { get; set; }

        public int Speed { get; set; }

        public int SpeedComboIndex = 0;

        public int Execute()
        {
            try
            {


                Point target = new Point(X, Y);

                LinearSmoothMove(target, Speed);


                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

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
}

using MahApps.Metro.Controls;
using SmartifyBotStudio.RobotDesigner.TaskModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace SmartifyBotStudio.RobotDesigner.TaskView.Conditional
{
    /// <summary>
    /// Interaction logic for IfImageExists.xaml
    /// </summary>
    public partial class IfImageExists : UserControl
    {
        public RobotDesigner.TaskModel.Conditional.IfImageExists IfImageExistsData { get; set; }

        public double x;
        public double y;
        public double width;
        public double height;
        public bool isMouseDown = false;

        public IfImageExists()
        {
            InitializeComponent();
            Loaded += IfImageExistsData_loaded;
        }

        private void IfImageExistsData_loaded(object sender, RoutedEventArgs e)
        {

        }

        private void more_button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ok_button_Click(object sender, RoutedEventArgs e)
        {


            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;


        }
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            ((((this.Parent as StackPanel).Parent as Grid).Parent as TaskViewFrameUC).Parent as MetroWindow).DialogResult = false;
        }

        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            //if (handle) Handle();
            //handle = true;

            //switch (searchMode_combo.SelectedIndex)
            //{
            //    case 0:
            //        testGrid.SetValue(Grid.RowProperty, 6);
            //        ifExists_userControl.Height = 600;
            //        break;
            //    case 1:
            //        testGrid.SetValue(Grid.RowProperty, 7);
            //        ifExists_userControl.Height = 500;
            //        break;

            //}

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //System.Windows.Forms.ComboBox cmb = sender as System.Windows.Forms.ComboBox;
            //handle = !cmb.IsDropDownOpen;
            ////Handle();

            //switch (cmb.SelectedIndex)
            //{
            //    case 0:
            //        testGrid.SetValue(Grid.RowProperty, 6);
            //        break;
            //    case 1:
            //        testGrid.SetValue(Grid.RowProperty, 7);
            //        //ifExists_userControl.Height=500;
            //        //default_tab.Height =400;
            //        break;

            //}
        }

        private void addImageFromRepository_btn(object sender, RoutedEventArgs e)
        {
            

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            x = e.GetPosition(null).X;
            y = e.GetPosition(null).Y;
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.isMouseDown)
            {
                this.Cursor = System.Windows.Input.Cursors.Cross;
                double curx = e.GetPosition(null).X;
                double cury = e.GetPosition(null).Y;

                System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
                SolidColorBrush brush = new SolidColorBrush(Colors.White);
                r.Stroke = brush;
                r.Fill = brush;
                r.StrokeThickness = 1;
                r.Width = Math.Abs(curx - x);
                r.Height = Math.Abs(cury - y);
                cnv.Children.Clear();
                cnv.Children.Add(r);
                Canvas.SetLeft(r, x);
                Canvas.SetTop(r, y);
                if (e.LeftButton == MouseButtonState.Released)
                {
                    cnv.Children.Clear();
                    width = e.GetPosition(null).X - x;
                    height = e.GetPosition(null).Y - y;
                    this.CaptureScreen(x, y, width, height);
                    this.x = this.y = 0;
                    this.isMouseDown = false;
                    //this.Close();
                }
            }
        }
        public void CaptureScreen(double x, double y, double width, double height)
        {
            int ix, iy, iw, ih;
            ix = Convert.ToInt32(x);
            iy = Convert.ToInt32(y);
            iw = Convert.ToInt32(width);
            ih = Convert.ToInt32(height);
            Bitmap image = new Bitmap(iw, ih, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(image);
            g.CopyFromScreen(ix, iy, ix, iy, new System.Drawing.Size(iw, ih), CopyPixelOperation.SourceCopy);
            System.Windows.Forms.SaveFileDialog dlg = new  System.Windows.Forms.SaveFileDialog();
            dlg.DefaultExt = "png";
            dlg.Filter = "Png Files|*.png";
            System.Windows.Forms.DialogResult res = dlg.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
                image.Save(dlg.FileName, ImageFormat.Png);
        }

        public void SaveScreen(double x, double y, double width, double height)
        {
            int ix, iy, iw, ih;
            ix = Convert.ToInt32(x);
            iy = Convert.ToInt32(y);
            iw = Convert.ToInt32(width);
            ih = Convert.ToInt32(height);
            try
            {
                Bitmap myImage = new Bitmap(iw, ih);

                Graphics gr1 = Graphics.FromImage(myImage);
                IntPtr dc1 = gr1.GetHdc();
                IntPtr dc2 = NativeMethods.GetWindowDC(NativeMethods.GetForegroundWindow());
                NativeMethods.BitBlt(dc1, ix, iy, iw, ih, dc2, ix, iy, 13369376);
                gr1.ReleaseHdc(dc1);
                System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
                dlg.DefaultExt = "png";
                dlg.Filter = "Png Files|*.png";
                System.Windows.Forms.DialogResult res = dlg.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                    myImage.Save(dlg.FileName, ImageFormat.Png);
            }
            catch { }
        }
        internal class NativeMethods
        {

            [DllImport("user32.dll")]
            public extern static IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hwnd);
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("gdi32.dll")]
            public static extern UInt64 BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, System.Int32 dwRop);

        }


    }
}

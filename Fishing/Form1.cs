using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Fishing
{
    public partial class Form1 : Form
    {
        // mouse_event import
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
          int dwExtraInfo);
        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        //Use the values of this enum for the 'dwData' parameter
        //to specify an X button when using MouseEventFlags.XDOWN or
        //MouseEventFlags.XUP for the dwFlags parameter.
        public enum MouseEventDataXButtons : uint
        {
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }




        //center coordinates of fishRing
        //initialized with (0,0)
        public Point center_fishRing=new Point(0, 0);


        //bar[0] contains Upper-Left coordinate of bar
        //bar[1] contains Lower-Right coordinate of bar
        //NOT initialized
        public Point[] bar = new Point[2];


        //radius of fishRing
        //initialized with 150 (Warning:it can be changed)
        public int radius_fishRing = 150;


        //if calibrated 1 else 0
        //initialized with 0
        public int calibrated=0;

        //start:1 stop:0
        //initialized with 0
        public int start=0;

        //resolution X
        //initialized with 1280
        public int resX = 1280;


        //resolution Y
        //initialized with 720
        public int resY = 720;




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Takes screenshot of the Full Screen<para />
        /// Finds center of Fish ring <para />
        /// Assigns that value to global variable center_fishRing<para />
        /// Moves mouse to center_fishRing<para />
        /// Clicks<para />
        /// Wait until Fish appears<para />
        /// Click<para />
        /// Find Left Bar and Right Bar<para />
        /// Calculate Right Bar's Lower-Right end point<para />
        /// Assign Left and Right Bars to global variable bar<para />
        /// calibrated=1<para />
        /// MassageBox("Calibration Completed");
        /// </summary>
        private void calibrator()
        {
            Bitmap bmpScreenShot = screenShot(new Point(0, 0), new Point(resX, resY));
            bool isFoundCoolFisherman = searchBitmap(Properties.Resources.bmpCoolFisherman1, bmpScreenShot, out center_fishRing)
                || searchBitmap(Properties.Resources.bmpCoolFisherman2, bmpScreenShot, out center_fishRing); 

            if(isFoundCoolFisherman== false)
            {
                MessageBox.Show("cool fisherman yok");
                return;
            }
            mouseMove(center_fishRing);
            mouseClick();
            int i=0;
            while(i<1000)
            {
                i++;
                Thread.Sleep(10);
                if(searchFish()==true)
                {
                    break;
                }
            }

            if(i==1000)
            {
                MessageBox.Show("bmpFish could not found!");
                return;
            }

            mouseClick();

            delay(1000, 2000);
            if (searchLeftBar(out bar[0]) == false)
            {
                MessageBox.Show("left bar yok");
                return;
            }
            
            if (searchRightBar(out bar[1]) == false)
            {
                MessageBox.Show("right bar yok");
                return;
            }
            bar[0] += new Size(center_fishRing.X, center_fishRing.Y);
            bar[1] += new Size(center_fishRing.X, center_fishRing.Y);
            //calculate upper right
            bar[1] = bar[1] + new Size(0, 6);
            calibrated = 1;
            MessageBox.Show("calibrated");
            return;
        }


        /// <summary> 
        /// Moves mouse to center_fishRing<para />
        /// mouseClick<para /> 
        /// while(!searchFish)<para /> 
        ///     delay<para />
        /// Search for Left and Right boundaries<para /> 
        /// while(searchFishStick(out loc))<para />
        ///     check whether stick is in the boundaries <para /> 
        ///     do what must be done <para />> 
        /// release mouse 
        /// </summary> 
        private void fishing()
        {
            mouseMove(center_fishRing);
            mouseClick();
            int i = 0;
            while (i < 1000)
            {
                i++;
                Thread.Sleep(10);
                if (searchFish() == true)
                {
                    break;
                }
            }

            if(i==1000)
            {
                MessageBox.Show("bmpFish could not found!");
                return;
            }

            mouseClick();
            Point[] boundary= new Point[2];
            if (searchLeftBoundary(out boundary[0])==false)
            {
                MessageBox.Show("left boundary yok");
                return;
            }
            if (searchRightBoundary(out boundary[1]) == false)
            {
                MessageBox.Show("right boundary yok");
                return;
            }
            Point stick_location= new Point(0,0);
            delay(50, 100);
            while (findFishStick(out stick_location))
            {
                if(stick_location.X>boundary[1].X-3)
               {
                   clickDown();
               }
               if(stick_location.X<boundary[0].X+3)
               {
                   clickUp();
               }
                delay(20,30);

            }
            clickUp();
        }

        /// <summary>
        /// checks whether calibrated<para />
        /// assign 1 to global variable start<para />
        /// while(start==1)<para />
        /// {<para />
        ///     fishing();<para />
        ///     while(!searchCoolFisherman())<para />
        ///     {<para />
        ///         delay;<para />
        ///     }<para />
        /// }<para />
        /// </summary>
        private void startFishing()
        {
            if(calibrated==0)
            {
                MessageBox.Show("Please run calibrator");
                return;
            }
            start = 1;
            while(start==1)
            {
                fishing();
                while(!searchCoolFisherman())
                {
                    delay(50, 60);
                }
            }
            MessageBox.Show("fishing stopped");
        }

        /// <summary>
        /// assign 0 to global variable start
        /// </summary>
        private void stopFishing()
        {
            start = 0;
        }



        /// <summary>
        /// Creates a new Bitmap bmpScreenShot<para />
        /// Creates a Graphic object g constructed with bmpScreenShot<para />
        /// Takes a screenshot<para />
        /// </summary>
        /// <param name="upper_left">Upper Left Coordinates of SS</param>
        /// <param name="lower_right">Lower Right Coordinates of SS</param>
        /// <returns>Bitmap bmpScreenShot</returns>
        private Bitmap screenShot(Point upper_left, Point lower_right)
        {
            Bitmap bmpScreenShot = new Bitmap(lower_right.X - upper_left.X, lower_right.Y - upper_left.Y);
            Graphics g = Graphics.FromImage(bmpScreenShot);
            Size size_of_screen = new Size(lower_right.X - upper_left.X, lower_right.Y - upper_left.Y);
            g.CopyFromScreen(upper_left.X, upper_left.Y, 0, 0, size_of_screen);
            //delete size_of_screen;
            return bmpScreenShot;
        }


        /// <summary>
        /// Search bmpNeedle in bmpHaystack<para />
        /// If found, found coordinates are assigned to (out)location<para />
        /// else (0, 0) is assigned to location<para />
        /// </summary>
        /// <param name="bmpNeedle">Bitmap to be found</param>
        /// <param name="bmpHaystack">Bitmap to be found in</param>
        /// <param name="location">Upper-Left Coordinates of found Bitmap</param>
        /// <returns>found?true:false</returns>
        private bool searchBitmap(Bitmap bmpNeedle, Bitmap bmpHaystack, out Point location)
        {
            for (int outerX = 0; outerX < bmpHaystack.Width - bmpNeedle.Width; outerX++)
            {
                for(int outerY=0; outerY<bmpHaystack.Height-bmpNeedle.Height;outerY++)
                {
                    for(int innerX=0; innerX<bmpNeedle.Width;innerX++)
                    {
                        for(int innerY=0;innerY<bmpNeedle.Height;innerY++)
                        {
                            Color cHaystack = bmpHaystack.GetPixel(outerX + innerX, outerY + innerY);
                            Color cNeedle = bmpNeedle.GetPixel(innerX, innerY);
                            if(cNeedle.R!=cHaystack.R || cNeedle.G!=cHaystack.G || cNeedle.B!=cHaystack.B)
                            {
                                goto notFound;
                            }
                        }
                    }
                    location = new Point(outerX, outerY);
                    return true;
                notFound:
                    continue;
                }
            }
            location = Point.Empty;
            return false;
        }


    /*
     * 
     * Search Image
     * 
     */


        /// <summary>
        /// Searches The Cool Fisherman
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchCoolFisherman()
        {
            Bitmap bmpScreenShot = screenShot(new Point(center_fishRing.X - radius_fishRing, center_fishRing.Y - radius_fishRing), 
                new Point(center_fishRing.X + radius_fishRing, center_fishRing.Y + radius_fishRing));
            Point temp;
            if (searchBitmap(Properties.Resources.bmpCoolFisherman1, bmpScreenShot, out temp) || 
                searchBitmap(Properties.Resources.bmpCoolFisherman2, bmpScreenShot, out temp))
            {
                return true;
            }
            return false;
        }




        /// <summary>
        /// Search from left for the desired colour change<para />
        /// If found assign that point to location<para />
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchLeftBoundary(out Point location)
        {
            Bitmap bmpScreenShot = screenShot(bar[0]+new Size(5, 3), bar[1]+new Size(0, -3));
            for(int i=0; i<bmpScreenShot.Width;i++)
            {
                Color cPixel = bmpScreenShot.GetPixel(i, 0);
                if(cPixel.R<180)
                {
                    location = bar[0] + new Size(5 + i, 3);
                    return true;
                }
            }
            location = Point.Empty;
            return false;
        }



        /// <summary>
        /// Search from right for the desired colour change<para />
        /// If found assign that point to location<para />
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchRightBoundary(out Point location)
        {
            Bitmap bmpScreenShot = screenShot(bar[0] + new Size(0, 3), bar[1] + new Size(-5, -3));
            for(int i=bmpScreenShot.Width-1; i>=0; i--)
            {
                Color cPixel = bmpScreenShot.GetPixel(i, 0);
                if (cPixel.R < 160)
                {
                    location = bar[1] + new Size(i-bmpScreenShot.Width-1, -3);
                    return true;
                }

            }
            location = Point.Empty;
            return false;
        }



        /// <summary>
        /// Takes a screenShot of FishRing area<para />
        /// Searchs for Fish image(s) in fishRing<para />
        /// </summary>
        /// <returns>found?true:false</returns>
        private bool searchFish()
        {
            Bitmap bmpScreenShot = screenShot(new Point(center_fishRing.X - radius_fishRing, center_fishRing.Y - radius_fishRing),
                new Point(center_fishRing.X + radius_fishRing, center_fishRing.Y + radius_fishRing));
            Point temp;
            if (searchBitmap(Properties.Resources.bmpFish1, bmpScreenShot, out temp) ||
                searchBitmap(Properties.Resources.bmpFish2, bmpScreenShot, out temp) ||
                searchBitmap(Properties.Resources.bmpFish3, bmpScreenShot, out temp) ||
                searchBitmap(Properties.Resources.bmpFish4, bmpScreenShot, out temp))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches Left Bar<para />
        /// If found assign that point to location<para />
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchLeftBar(out Point location)
        {
            Bitmap bmpScreenShot = screenShot(new Point(center_fishRing.X, center_fishRing.Y),
                new Point(resX, center_fishRing.Y + 500));
            if (searchBitmap(Properties.Resources.bmpLeftBar, bmpScreenShot, out location))
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// Searches Right Bar<para />
        /// If found assign that point to location<para />
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchRightBar(out Point location)
        {
            Bitmap bmpScreenShot = screenShot(new Point(center_fishRing.X, center_fishRing.Y),
                new Point(resX, center_fishRing.Y + 500));
            if (searchBitmap(Properties.Resources.bmpRightBar, bmpScreenShot, out location))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Search fishStick<para />
        /// If found assign that point to location<para />
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool findFishStick(out Point location)
        {
            Bitmap bmpScreenShot = screenShot(bar[0], bar[1]);
            if (searchBitmap(Properties.Resources.bmpFishStick, bmpScreenShot, out location))
            {
                return true;
            }
            return false;
        }


    /*
     * 
     * \Search Image
     * 
     */ 

        /// <summary>
        /// Moves the mouse to Coordinate
        /// </summary>
        /// <param name="Coordinate">Coordinate to be go</param>
        private void mouseMove(Point Coordinate)
        {
            //basic
            Cursor.Position = Coordinate;
        }

        /// <summary>
        /// Clicks mouse button
        /// </summary>
        private void clickDown()
        {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            
        }


        /// <summary>
        /// Releases mouse button
        /// </summary>
        private void clickUp()
        {
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0); 
        }


        /// <summary>
        /// Sleeps a(ms) to b(ms) 
        /// </summary>
        /// <param name="a">Left boundary in ms</param>
        /// <param name="b">Right boundary in ms</param>
        private void delay(int a, int b)
        {
            Thread.Sleep((new Random()).Next(a, b));
        }

        /// <summary>
        /// Clicks mouse button
        /// Delays(40ms-70ms)
        /// Releases mouse button
        /// </summary>
        private void mouseClick()
        {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            delay(50, 200);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }




        /// <summary>
        /// Randomizes a coordinate in given limits<para />
        /// </summary>
        /// <param name="coordinate">Initial coordinates</param>
        /// <param name="limit">Randomizing limiter</param>
        /// <returns></returns>
        private Point coordinateRandomizer(Point coordinate, int limit)
        {
            coordinate = coordinate + new Size(new Random().Next(-limit, limit), new Random().Next(-limit, limit));
            return coordinate;
        }

        private void b_Calibrator_Click(object sender, EventArgs e)
        {
            calibrator();
        }

        private void b_Start_Click(object sender, EventArgs e)
        {
            startFishing();
        }


    }
}

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





        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Takes screenshot of the Full Screen
        /// Finds center of Fish ring 
        /// Assigns that value to global variable center_fishRing
        /// Moves mouse to center_fishRing
        /// Clicks
        /// Wait until Fish appears
        /// Click
        /// Find Left Bar and Right Bar
        /// Calculate Right Bar's Lower-Right end point
        /// Assign Left and Right Bars to global variable bar
        /// calibrated=1
        /// MassageBox("Calibration Completed");
        /// </summary>
        private void calibrator()
        {
            if(searchCoolFisherman(out center_fishRing)== false)
            {
                MessageBox.Show("cool fisherman yok");
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
            mouseClick();
            
            if (searchLeftBar(out bar[0]) == false)
            {
                MessageBox.Show("left bar yok");
            }
            
            if (searchRightBar(out bar[1]) == false)
            {
                MessageBox.Show("right bar yok");
            }
            //calculate upper right
            bar[1] = bar[1] + new Size(0, 0);
            calibrated = 1;
            MessageBox.Show("calibrated");

        }


        /// <summary>
        /// Moves mouse to center_fishRing
        /// mouseClick
        /// while(!searchFish)
        ///     delay
        /// Search for Left and Right boundaries
        /// while(searchFishStick(out loc))
        ///     check whether stick is in the boundaries
        ///     do what must be done
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
            mouseClick();
            Point[] boundary= new Point[2];
            if (searchLeftBoundary(out boundary[0])==false)
            {
                MessageBox.Show("left boundary yok");
            }
            if (searchRightBoundary(out boundary[1]) == false)
            {
                MessageBox.Show("right boundary yok");
            }
            Point stick_location= new Point(0,0);
            while (findFishStick(out stick_location))
            {
               if(stick_location.X<boundary[1].X)
               {
                   clickDown();
               }
               if(stick_location.X<boundary[0].X+3)
               {
                   clickUp();
               }

            }
            clickUp();
        }

        /// <summary>
        /// checks whether calibrated
        /// assign 1 to global variable start
        /// while(start==1)
        /// {
        ///     fishing();
        ///     while(!searchCoolFisherman())
        ///     {
        ///         delay;
        ///     }
        /// }
        /// </summary>
        private void startFishing()
        {

        }


        /// <summary>
        /// assign 0 to global variable start
        /// </summary>
        private void stopFishing()
        {

        }



        /// <summary>
        /// Creates a new Bitmap bmpScreenShot
        /// Creates a Graphic object g constructed with bmpScreenShot
        /// Takes a screenshot
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
        /// Search bmpNeedle in bmpHaystack
        /// If found, found coordinates are assigned to (out)location
        /// else (0, 0) is assigned to location
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
                            Color cNeedle = bmpNeedle.GetPixel(outerX + innerX, outerY + innerY);
                            Color cHaystack = bmpHaystack.GetPixel(innerX, innerY);
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
        /// If found assign that point to location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchCoolFisherman(out Point location)
        {
            return true;
        }




        /// <summary>
        /// Search from left for the desired colour change
        /// If found assign that point to location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchLeftBoundary(out Point location)
        {
            return true;
        }



        /// <summary>
        /// Search from right for the desired colour change
        /// If found assign that point to location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchRightBoundary(out Point location)
        {
            return false;
        }



        /// <summary>
        /// Takes a screenShot of FishRing area
        /// Searchs for Fish image(s) in fishRing
        /// </summary>
        /// <returns>found?true:false</returns>
        private bool searchFish()
        {
            return false;
        }

        /// <summary>
        /// Searches Left Bar
        /// If found assign that point to location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchLeftBar(out Point location)
        {
            return true;
        }



        /// <summary>
        /// Searches Right Bar
        /// If found assign that point to location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool searchRightBar(out Point location)
        {
            return true;
        }


        /// <summary>
        /// Search fishStick
        /// If found assign that point to location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>found?true:false</returns>
        private bool findFishStick(out Point location)
        {
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
            delay(500, 1000);
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }




        /// <summary>
        /// Randomizes a coordinate in given limits
        /// </summary>
        /// <param name="coordinate">Initial coordinates</param>
        /// <param name="limit">Randomizing limiter</param>
        /// <returns></returns>
        private Point coordinateRandomizer(Point coordinate, int limit)
        {
            coordinate = coordinate + new Size(new Random().Next(-limit, limit), new Random().Next(-limit, limit));
            return coordinate;
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fishing
{
    public partial class Form1 : Form
    {
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
            return;
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
        /// Creates a new Bitmap bmpScreenShot
        /// Creates a Graphic object g constructed with bmpScreenShot
        /// Takes a screenshot
        /// </summary>
        /// <param name="upper_left">Upper Left Coordinates of SS</param>
        /// <param name="lower_right">Lower Right Coordinates of SS</param>
        /// <returns>Bitmap bmpScreenShot</returns>
        private Bitmap screenShot(Point upper_left, Point lower_right)
        {
            adana
            return ;
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
            return true;
        }


    /*
     * 
     * Search Image
     * 
     */





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
        private bool searhRightBar(out Point location)
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

        }

        /// <summary>
        /// Clicks mouse button
        /// </summary>
        private void clickDown()
        {

        }


        /// <summary>
        /// Releases mouse button
        /// </summary>
        private void clickUp()
        {
 
        }


        /// <summary>
        /// Sleeps a(ms) to b(ms) 
        /// </summary>
        /// <param name="a">Left boundary in ms</param>
        /// <param name="b">Right boundary in ms</param>
        private void delay(int a, int b)
        {

        }

        /// <summary>
        /// Clicks mouse button
        /// Delays(40ms-70ms)
        /// Releases mouse button
        /// </summary>
        private void mouseClick()
        {

        }




        /// <summary>
        /// Randomizes a coordinate in given limits
        /// </summary>
        /// <param name="coordinate">Initial coordinates</param>
        /// <param name="limit">Randomizing limiter</param>
        /// <returns></returns>
        private Point coordinateRandomizer(Point coordinate, int limit)
        {
            return new Point(0, 0);
        }


    }
}

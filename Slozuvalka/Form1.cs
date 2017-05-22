using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class formSlozuvalka : Form
    {
        Point point;
        ArrayList images=new ArrayList();
        public formSlozuvalka()
        {
            point.X=180;
            point.Y=180;
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Refresh();
            foreach (Button b in pnlSlozuvalka.Controls) {
                b.Enabled = true;
                Image goalImage;
                if (rbFamousArtists.Checked)
                {
                    goalImage = imageChosen("artistsChosen");
                }
                else
                {
                    goalImage = imageChosen("placesChosen");
                }
              //Image goalImage = Image.FromFile("C:\\Users\\user\\Documents\\vanGogh.jpg");
                cropGoalImage(goalImage,270,270);
                partialImagesAsButtons(images);
                Invalidate();
            }
        }

        private Image imageChosen(string chosen)
        {
            Image goalImage = null;
            if (chosen.CompareTo("artistsChosen") == 0)
            {
                string[] famousPaintings ={"C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\The_Persistence_of_Memory.jpg",
                                          "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\theKiss.jpg",
                                          "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\MonaLisa.jpg",
                                          "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\vanGogh.jpg"};
               
                var rand = new Random();
                string image = famousPaintings[rand.Next(famousPaintings.Length)];
                string famousPaintingImage = image;
                //Uri uri = new Uri(famousPaintings[rand.Next(famousPaintings.Length)], UriKind.Relative);
                bool existence = File.Exists(image);
                if (existence)
                {
                    goalImage = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), image));

                }
                //goalImage = Image.FromFile(famousPaintingImage);
            }
            else if(chosen=="placesChosen")
            {
                string[] Places ={"C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\GreatPyramidOfGiza.jpg",
                    "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\ColosseumRome.jpg",
                    "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\StatueOfLiberty.jpg",
                    "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\Pisa.jpg",
                    "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\EiffelTower.jpg",
                    "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\BigBenLondon.jpg",
                    "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\SydneyOperaHouse.jpg",
                    "C:\\Users\\user\\Documents\\Visual Studio 2010\\Projects\\Slozuvalka\\images\\GoldenGateBridge.jpg"

                                };
                var rand=new Random();
                string PlacesImages = Places[rand.Next(Places.Length)];
                goalImage = Image.FromFile(PlacesImages);
            }
            return goalImage;
        }

        private void partialImagesAsButtons(ArrayList images)
        {
           int i=0;
           int[] array = { 0, 1, 2, 3, 4, 5, 6, 7 };
           array = shuffle(array);
           foreach (Button b in pnlSlozuvalka.Controls)
           {
               if (i < array.Length)
               {
                   b.Image = (Image)images[array[i]];
                   i++;
               }
           }
        }

        private int[] shuffle(int[] array)
        {
            Random random = new Random();
            array = array.OrderBy(x=>random.Next()).ToArray();
            return array;
        }
        
        private void cropGoalImage(Image goalImage, int width, int height)
        {
            Bitmap bitmapa = new Bitmap(width,height);
            Graphics g = Graphics.FromImage(bitmapa);
            g.DrawImage(goalImage, 0, 0, width, height);
            g.Dispose();
            int moveRight = 0;
            int moveDirection = 0;
            for (int x = 0; x < 8;x++)
            {
                Bitmap partialImage = new Bitmap(90,90);
                for (int i = 0; i < 90;i++ )
                {
                    for (int j = 0; j < 90; j++) { 
                    partialImage.SetPixel(i,j,bitmapa.GetPixel(i+moveRight,j+moveDirection));
                    }
                }
                images.Add(partialImage);
                moveRight += 90;
                if (moveRight == 270) {
                    moveRight = 0;
                    moveDirection += 90;
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            moveButton((Button)sender);
        }

        private void moveButton(Button button)
        {
            if (((button.Location.X == point.X - 90 || button.Location.X == point.X + 90)&&button.Location.Y==point.Y)||(button.Location.Y == point.Y - 90 || button.Location.Y == point.Y + 90)&&button.Location.X==point.X)
            {
                Point swap = button.Location;
                button.Location = point;

                point = swap;
            }
            if(point.X==180&&point.Y==180)
            {
                validationCheck();
            }
        }

        private void validationCheck()
        {
            int count = 0;
            int index;
            foreach(Button button in pnlSlozuvalka.Controls)
            {
                index = (button.Location.Y / 90) * 3 + button.Location.X / 90;
                if(images[index]==button.Image)
                {
                    count++;
                }
            }
            if (count == 8)
            {
                MessageBox.Show("Погоди го авторот");
                    
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            string choice="";
            if (rbFamousArtists.Checked)
            {
                rbFamousPlaces.Enabled = false;
                choice = "artistsChosen";

            }
            else
            {
                rbFamousArtists.Enabled = false;
                choice = "placesChosen";
            }
            imageChosen(choice);
        }

    }
}

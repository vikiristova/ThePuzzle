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


        private void btn_Restart_Click(object sender, EventArgs e)
        {
            // Application.Restart();
            //  btnStart_Click(sender, e);

            point.X = 180;
            point.Y = 180;
            InitializeComponent();

        }

        private void Form1_load()
        {
            throw new NotImplementedException();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Image goalImage;
            // this.Refresh();
            foreach (Button b in pnlSlozuvalka.Controls)
            {
                b.Enabled = true;
                //  Image goalImage;

            }
            goalImage = rbChecked(rbFamousArtists);

                cropGoalImage(goalImage,270,270);

                partialImagesAsButtons(images);
              //  Invalidate();

            
            btnStart.Enabled = false;
            btn_Restart.Enabled = true;
           


           pictureBox1.Image = goalImage;

        }

        private Image rbChecked(RadioButton X)
        {

            if (X.Checked)
                return imageChosen("artistsChosen");

            else
                return imageChosen("placesChosen");

        }



        private Image imageChosen(string chosen)
        {
            Image goalImage = null;
            if (chosen.CompareTo("artistsChosen") == 0)
            {
                string[] famousPaintings ={"..\\..\\images\\The_Persistence_of_Memory.jpg",
                                          "..\\..\\images\\theKiss.jpg",
                                          "..\\..\\images\\MonaLisa.jpg",
                                          "..\\..\\images\\vanGogh.jpg"};
               
                var rand = new Random();
                string image = famousPaintings[rand.Next(famousPaintings.Length)];
                string famousPaintingImage = image;
               // pictureBox1.Image = image;

                //Uri uri = new Uri(famousPaintings[rand.Next(famousPaintings.Length)], UriKind.Relative);
                bool existence = File.Exists(image);
                if (existence)
                {
                    goalImage = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), image));
                   // pictureBox1.Image = goalImage;
                }
                //goalImage = Image.FromFile(famousPaintingImage);
            }
            else if(chosen=="placesChosen")
            {
                string[] Places ={"..\\..\\images\\GreatPyramidOfGiza.jpg",
                    "..\\..\\images\\ColosseumRome.jpg",
                    "..\\..\\images\\StatueOfLiberty.jpg",
                    "..\\..\\images\\Pisa.jpg",
                    "..\\..\\images\\EiffelTower.jpg",
                    "..\\..\\images\\BigBenLondon.jpg",
                    "..\\..\\images\\SydneyOperaHouse.jpg",
                    "..\\..\\images\\GoldenGateBridge.jpg"

                                };
                var rand=new Random();
                string PlacesImages = Places[rand.Next(Places.Length)];
                goalImage = Image.FromFile(PlacesImages);
               
            }

            //pictureBox1.Image = goalImage;



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

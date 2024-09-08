using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using ZXing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool flag = true;
        public Form1()
        {
            InitializeComponent();
            


        }
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;
        private void Form1_Load(object sender, EventArgs e)
        {
            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterinfo in FilterInfoCollection)
                comboBox1.Items.Add(filterinfo.Name);
            comboBox1.SelectedIndex = 0;
            VideoCaptureDevice = new VideoCaptureDevice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[comboBox1.SelectedIndex].MonikerString);
            VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            VideoCaptureDevice.Start();
            
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            Bitmap pixel_dat = (Bitmap)pictureBox1.Image;
            Color colour = pixel_dat.GetPixel(195, 172);
            pictureBox2.BackColor = colour;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                timer1.Start();
                flag = false;
            }
            else
            {
                timer1.Stop();
                flag = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "R: " + pictureBox2.BackColor.R.ToString() + 
                " G: " + pictureBox2.BackColor.G.ToString() + " B: " + 
                pictureBox2.BackColor.B.ToString();
            label2.Text = "H: " + pictureBox2.BackColor.GetHue();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(VideoCaptureDevice.IsRunning==true)
            { VideoCaptureDevice.Stop(); }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            float hue = pictureBox2.BackColor.GetHue();

            if (hue < 17.00 || hue > 351.00)
                textBox1.Text = "Red";
            else if (hue < 68 && hue > 41)
                textBox1.Text = "Yellow";
            else if (hue < 41 && hue > 17)
                textBox1.Text = "Orange";
            else if (hue < 132 && hue > 81)
                textBox1.Text = "Green";
            else if (hue < 262 && hue > 204)
                textBox1.Text = "Blue";
            else if (hue < 295 && hue > 283)
                textBox1.Text = "Lavender";
            else if (hue < 327 && hue > 295)
                textBox1.Text = "Pink";
            else if (hue < 350 && hue > 327)
                textBox1.Text = "Magenta";
            else if (hue < 289 && hue > 281)
                textBox1.Text = "Purple";
            else if (hue < 204 && hue > 166)
                textBox1.Text = "Light Blue";
            else if (hue < 33 && hue > 17)
                textBox1.Text = "Brown";
            else if (hue < 50 && hue > 33)
                textBox1.Text = "Amber";
            else if (hue < 75 && hue > 50)
                textBox1.Text = "Gold";
            else if (hue < 85 && hue > 75)
                textBox1.Text = "Lime";
            else if (hue < 105 && hue > 85)
                textBox1.Text = "Chartreuse";
            else if (hue < 135 && hue > 105)
                textBox1.Text = "Spring Green";
            else if (hue < 175 && hue > 135)
                textBox1.Text = "Cyan";
            else if (hue < 195 && hue > 175)
                textBox1.Text = "Azure";
            else if (hue < 215 && hue > 195)
                textBox1.Text = "Cerulean";
            else if (hue < 235 && hue > 215)
                textBox1.Text = "Royal Blue";
            else if (hue < 265 && hue > 235)
                textBox1.Text = "Indigo";
            else if (hue < 285 && hue > 265)
                textBox1.Text = "Violet";
            else if (hue < 305 && hue > 285)
                textBox1.Text = "Mauve";
            else if (hue < 325 && hue > 305)
                textBox1.Text = "Plum";
            else if (hue < 345 && hue > 325)
                textBox1.Text = "Burgundy";
            else
                textBox1.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Start();
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                BarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode((Bitmap)pictureBox1.Image);
                if(result!=null)
                    textBox2.Text = result.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

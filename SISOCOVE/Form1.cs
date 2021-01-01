using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISOCOVE
{
    public partial class Form1 : Form
    {

        public Graphics figuras;
        Point[] puntos = new Point[3];
        Pen lapiz = new Pen(Color.Red);
        private static int x;
        private static int y;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            int ancho = 170;
            int largo = 170;
            figuras = pictureBox1.CreateGraphics();
            figuras.DrawEllipse(lapiz, x - ancho / 2, y - largo / 2, ancho, largo);

            figuras.FillEllipse(new SolidBrush(Color.FromArgb(50, 243, 19, 19)), x - ancho / 2, y - largo / 2, ancho, largo);
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            
            
            label1.Text = "X = " + e.X + "Y = " + e.Y;
            x = e.X;
            y = e.Y;
            
            //pictureCirculo.SetBounds(e.X-50, e.Y-50, 100, 100);

            int ancho = 170;
            int largo = 170;
            figuras = pictureBox1.CreateGraphics();
            figuras.DrawEllipse(lapiz, e.X-ancho/2, e.Y-largo/2, ancho, largo);
         
            figuras.FillEllipse(new SolidBrush(Color.FromArgb(50, 243,19,19)), e.X - ancho/2, e.Y - largo/2, ancho, largo);
          
            Console.WriteLine(e.Button);
            
            

            }
            pictureBox1.Refresh();


            //  if(OnMouseMove(e){

            //}
            //pictureBox1.Refresh();


            //figuras.Clear(Color.Transparent);


            //pictureBox1.Image = null;

        }

        public void circulo(PictureBox cuadro)
        {
            //figuras = cuadro.CreateGraphics();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //label1.Text = "X = " + e.X + "Y = " + e.Y;
            //pictureCirculo.SetBounds(e.X - 50, e.Y - 50, 100, 100);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
           
        }
    }
}

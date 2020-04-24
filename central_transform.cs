using Accord.Imaging;
using Accord.Imaging.Converters;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string path;
        public Form1()
        {
            InitializeComponent();
        }
        string featuredata = "cendata,class";
        private int label = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;

            }
            string[] file = Directory.GetFiles(path);

            foreach (string s in file)
            {

                string dupImagePath = s;
                Bitmap org1 = (Bitmap)Accord.Imaging.Image.FromFile(dupImagePath);
                featuredata = featuredata+census(org1) + ","+label+"\n";

             //   Console.WriteLine(census(org1));
               // gabordataextract(s);
            }
            label++;
            MessageBox.Show("Done"+label);
        }
        
        public static int BitStringToInt(string bits)
        {
            var reversedBits = bits.Reverse().ToArray();
            var num = 0;
            for (var power = 0; power < reversedBits.Count(); power++)
            {
                var currentBit = reversedBits[power];
                if (currentBit == '1')
                {
                    var currentNum = (int)Math.Pow(2, power);
                    num += currentNum;
                }
            }

            return num;
        }
        private double census(Bitmap myBitmap)
        {
            int[,] bi = new int[myBitmap.Width,myBitmap.Height];

            for (int x = 0; x < myBitmap.Width; x++)
            {
                for (int y = 0; y < myBitmap.Height; y++)
                {
                    Color pixelColor = myBitmap.GetPixel(x, y);
                    int mainvalue = pixelColor.R;

                    string s = "";
                    try
                    {
                        int a0 = myBitmap.GetPixel(x - 1, y - 1).R;
                        if (a0 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }

                    try
                    {
                        int a1 = myBitmap.GetPixel(x - 1, y).R; 
                        if (a1 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }
                    try
                    {
                        int a2 = myBitmap.GetPixel(x - 1, y + 1).R;
                        if (a2 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }
                    try
                    {
                        int a3 = myBitmap.GetPixel(x, y - 1).R; 
                        if (a3 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }
                    try
                    {
                        int a4 = myBitmap.GetPixel(x, y + 1).R; 
                        if (a4 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }
                    try
                    {

                        int a6 = myBitmap.GetPixel(x + 1, y + 1).R; 
                        if (a6 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }
                    try
                    {
                        int a7 = myBitmap.GetPixel(x + 1, y).R;
                        if (a7 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }
                    try
                    {
                        int a8 = myBitmap.GetPixel(x + 1, y - 1).R; 
                        if (a8 >= mainvalue) s = s + 1;
                        else s = s + 0;
                    }
                    catch (Exception ex)
                    {

                        s = s + "";
                    }

                    bi[x, y] = BitStringToInt(s);


                    //      neighbors(1) = img(r - 1, c - 1); % Upper left.r = row, c = column.
                    // neighbors(2) = img(r - 1, c); % Upper middle.r = row, c = column.
                    //  neighbors(3) = img(r - 1, c + 1); % Upper right.r = row, c = column.
                    //   neighbors(4) = img(r, c - 1); % left.r = row, c = column.
                    //    neighbors(5) = img(r, c + 1); % right.r = row, c = column.

                    //    neighbors(6) = img(r + 1, c + 1); % Lowerleft.r = row, c = column.
                    //   neighbors(7) = img(r + 1, c); % lower middle.r = row, c = column.
                    //  neighbors(8) = img(r + 1, c - 1); % Lower left.r = row, c = column.

                    // things we do with pixelColor
                }
            }

            MatrixToImage conv = new MatrixToImage(min: 0, max: 255);

            // Declare an image and store the pixels on it
            Bitmap image; conv.Convert(bi, out image);
            Accord.Imaging.ImageStatistics statistics = new Accord.Imaging.ImageStatistics(image);
            var histogram = statistics.Gray;
            return histogram.Mean;
        }

      

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            File.WriteAllText("fgsm_cen.csv", featuredata);
        }
    }
}

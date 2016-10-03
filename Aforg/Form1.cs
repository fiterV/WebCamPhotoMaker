using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using AForge.Vision;
using System.Threading;

namespace Aforg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FilterInfoCollection videodevices;
        VideoCaptureDevice videoSource;
        Thread saver;
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            videoSource = new VideoCaptureDevice(videodevices[comboBox1.SelectedIndex].MonikerString);
            comboBox1.Visible = true;
            videoSourcePlayer1.VideoSource = videoSource;
            videoSource.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource!=null) videoSource.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Stabilize();
            videodevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (int i = 0; i < videodevices.Count; i++) comboBox1.Items.Add(videodevices[i].Name);
            comboBox1.SelectedIndex = 0;
        }

        private void videoSourcePlayer1_ParentChanged(object sender, EventArgs e)
        {
            
        }
        Bitmap bmp;
        private void button2_Click(object sender, EventArgs e)
        {
            bmp = videoSourcePlayer1.GetCurrentVideoFrame();
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            bmp.Save(saveFileDialog1.FileName);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Stabilize();
        }


        private void Stabilize()
        {
            button1.Left = this.ClientSize.Width / 2 - button1.Width;
            button1.Top = this.ClientSize.Height - button1.Height - 10;

            button2.Left = button1.Left + button1.Width + 10;
            button2.Top = button1.Top;

            comboBox1.Left = this.ClientSize.Width - comboBox1.Width - 10;
            videoSourcePlayer1.Width = Math.Min(comboBox1.Left - 10, button1.Top - 10);
            videoSourcePlayer1.Height = Math.Min(comboBox1.Left - 10, button1.Top - 10);
        }
    }
}

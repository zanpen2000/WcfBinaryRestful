using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WcfBinaryRestful.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        System.Net.WebClient wc;

        private void button1_Click(object sender, EventArgs e)
        {
            wc = new System.Net.WebClient();
            SaveFileDialog op = new SaveFileDialog();
            op.Filter = "jpg file(*.jpg)|*.jpg";
            if (op.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                wc.DownloadFileAsync(new Uri("http://192.168.1.8:9000/ReadImg"), op.FileName);
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;

                button2.Enabled = true;
                button1.Enabled = false;
            }
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                button2.Enabled = false;
                button1.Enabled = true;
                MessageBox.Show("用户已经取消下载！");
                return;
            }
            using (System.Net.WebClient wc = e.UserState as System.Net.WebClient)
            {
                button2.Enabled = false;
                button1.Enabled = true;
                MessageBox.Show("下载完毕!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (wc!=null)
            {
                wc.CancelAsync();
            }
        }

        System.Net.WebClient uploadWc = new System.Net.WebClient();
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "jpg file(*.jpg)|*.jpg";
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                uploadWc.Headers.Add("Content-Type", "image/jpeg");
                System.IO.FileStream fs = new System.IO.FileStream(op.FileName, System.IO.FileMode.Open);
                byte[] buffer = new byte[(int)fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                fs.Close();
                uploadWc.UploadDataCompleted += uploadWc_UploadDataCompleted;
                uploadWc.UploadDataAsync(new Uri("http://192.168.1.8:9000/ReceiveImg"), buffer);

                button3.Enabled = false;
                button4.Enabled = true;

            }
        }

        void uploadWc_UploadDataCompleted(object sender, System.Net.UploadDataCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                button3.Enabled = true;
                button4.Enabled = false;
                MessageBox.Show("用户已经取消上传！");
                return;
            }

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                MessageBox.Show("完成上传！");
            }
            button3.Enabled = true;
            button4.Enabled = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            uploadWc.CancelAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Base;
using Core.Config;

namespace Core.Win
{
    public partial class ImgCom : Form
    {
       
        public ImgCom()
        {

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
     
        }

        private void bntfind_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim().Length == 0) return;

            if (this.textBox1.Text.IndexOf('.') > 0)
            {
                try
                {
                    string item = this.textBox1.Text.Trim();
                    Bitmap bt = new Bitmap(item);
                    SaveImageForSpecifiedQuality(bt
                        , item.Substring(0, item.LastIndexOf('.')) + "_c" + item.Substring(item.LastIndexOf('.'), item.Length - item.LastIndexOf('.'))
                        , int.Parse(textBox3.Text));
                    MessageBox.Show("完成");
                }
                catch
                {
                    MessageBox.Show("路径错误");
                }
            }
          

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim().Length == 0) return;



            foreach (string item in this.textBox2.Lines)
            {
                if (item.Length == 0) continue;
                Bitmap bt = new Bitmap(item);
                SaveImageForSpecifiedQuality(bt
                    , item.Substring(0, item.LastIndexOf('.')) + "_c" + item.Substring(item.LastIndexOf('.'), item.Length- item.LastIndexOf('.'))
                    , int.Parse(textBox3.Text));
            }

            MessageBox.Show("完成");
        }

        public bool SaveImageForSpecifiedQuality(System.Drawing.Image sourceImage, string savePath, int imageQualityValue)
        {
            //以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParameters = new EncoderParameters();
            EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, imageQualityValue);
            encoderParameters.Param[0] = encoderParameter;
            try
            {
                ImageCodecInfo[] ImageCodecInfoArray = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegImageCodecInfo = null;
                for (int i = 0; i < ImageCodecInfoArray.Length; i++)
                {
                    if (ImageCodecInfoArray[i].FormatDescription.Equals("JPEG"))
                    {
                        jpegImageCodecInfo = ImageCodecInfoArray[i];
                        break;
                    }
                }
                sourceImage.Save(savePath, jpegImageCodecInfo, encoderParameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim().Length == 0) return;

            if (this.textBox1.Text.IndexOf('.') < 0)
            {
                DirectoryInfo root = new DirectoryInfo(this.textBox1.Text.Trim());
                FileInfo[] files = root.GetFiles();
                foreach (var f in files)
                    this.textBox2.Text+=f.FullName+"\r\n";
            }
            
        }
    }
}

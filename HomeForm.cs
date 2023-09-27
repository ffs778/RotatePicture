using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RotatePicture
{
    public partial class HomeForm : Form
    {
        Image originPicture;
        float originAngle;
        float deltaAngle;
        float finalAngle;
        readonly static string PATH = $".\\RatatePictures";   // 图片位置
        public HomeForm()
        {
            InitializeComponent();
            originPictureAngle_tbx.Text = "0";  // 默认当前图片的旋转角度为0°
            deltaAngle_tbx.Text = "1";   // 默认每次增加1°;
            finalPictureAngle_tbx.Text = "360";  // 默认最后一张图片的旋转调度为180°;
            GetPictures();
        }
        /// <summary>
        /// 显示文件夹中的图片
        /// </summary>
        private void GetPictures()
        {
            var files = Directory.GetFiles(PATH);
            Array.Sort(files,new FileNameSort());   // 对文件按名称进行排序，默认情况排序是1,11,111,2，所以需要使用自定义排序
            for (int i = 0; i < files.Length; i++)
            {
                PictureBox pictureBox = new PictureBox()
                {
                    Size = new Size(80, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Common.ReadImageFile(files[i])
                };
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }
        
        /// <summary>
        /// 点击显示当前剪切板中的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            originPicture = Common.GetClipBoardImage();
            pictureBox1.Image = originPicture;
        }

        #region 按指定角度生成旋转后的图片,保存图片
        private void Create_btn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(PATH)) Directory.CreateDirectory(PATH);
            float currentAngle = originAngle;
            SaveImage(currentAngle.ToString(), pictureBox1.Image);
            while (currentAngle < finalAngle)
            {
                currentAngle += deltaAngle;
                Image newImage = Common.KiRotate((Bitmap)originPicture, currentAngle, Color.Transparent);
                pictureBox1.Image = newImage;
                SaveImage(currentAngle.ToString(), pictureBox1.Image);
                PictureBox pictureBox = new()
                {
                    Image = newImage,
                    Size = new Size(80, 80),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }
        private static void SaveImage(string pictureName, Image image)
        {
            string fileName = PATH + $"\\_{ pictureName}.png";
            image.Save(fileName, ImageFormat.Png);  // 将当前图片保存
        }
        #endregion

        #region 修改参数

        private void CurrentPictureAngle_tbx_TextChanged(object sender, EventArgs e)
        {
            originAngle = float.Parse(originPictureAngle_tbx.Text);
        }

        private void DeltaAngle_TextChanged(object sender, EventArgs e)
        {
            deltaAngle = float.Parse(deltaAngle_tbx.Text);
        }

        private void LastPictureAngle_tbx_TextChanged(object sender, EventArgs e)
        {
            finalAngle = float.Parse(finalPictureAngle_tbx.Text);
        }
        #endregion

        #region 打开文件位置

        private void OpenFileLocation_Click(object sender, EventArgs e)
        {
            var path = Environment.CurrentDirectory;
            Common.OpenFile(path);
        }
        #endregion

        #region 播放旋转图片
        int index = 0;
        private void PlayRotatePics_btn_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 10;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            playPics_picb.Image = (flowLayoutPanel1.Controls[index] as PictureBox).Image;
            if (index < flowLayoutPanel1.Controls.Count - 1)
            {
                index++;
            }
            else
            {
                timer1.Stop();
                index = 0;
            }
        }
        #endregion

        #region 删除文件夹中的图片

        private void DeletePictures_btn_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(PATH))
            {
                DirectoryInfo directoryInfo = new(PATH);
                directoryInfo.Empty();
            }

            // 删除流布局中的图片
            flowLayoutPanel1.RemoveAllControls();
        } 
        #endregion
    }
}

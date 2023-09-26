using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        Image originPicture;
        float originAngle;
        float deltaAngle;
        float finalAngle;
        public Form1()
        {
            InitializeComponent();
            originPictureAngle_tbx.Text = "0";  // 默认当前图片的旋转角度为0°
            deltaAngle_tbx.Text = "1";   // 默认每次增加1°;
            finalPictureAngle_tbx.Text = "360";  // 默认最后一张图片的旋转调度为180°;
        }
        /// <summary>
        /// 点击显示当前剪切板中的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            originPicture = GetClipBoardImage();
            pictureBox1.Image = originPicture;
        }

        #region 按指定角度生成旋转后的图片
        private void Create_btn_Click(object sender, EventArgs e)
        {
            float currentAngle = originAngle;
            while (currentAngle < finalAngle)
            {
                Image newImage = KiRotate((Bitmap)originPicture, currentAngle, Color.Transparent);
                pictureBox1.Image = newImage;
                newImage.Save($"_{currentAngle}.png", System.Drawing.Imaging.ImageFormat.Png);
                PictureBox pictureBox = new()
                {
                    Image = newImage,
                    Size = new Size(80, 80),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                flowLayoutPanel1.Controls.Add(pictureBox);
                currentAngle += deltaAngle;
            }
        }
        #region 图片任意角度旋转
        /// <summary>
        /// 图片任意角度旋转
        /// </summary>
        /// <param name="bmp">原始图Bitmap</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="bkColor">背景色</param>
        /// <returns>输出Bitmap</returns>
        public static Bitmap KiRotate(Bitmap bmp, float angle, Color bkColor)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            PixelFormat pf;

            if (bkColor == Color.Transparent)
            {
                pf = PixelFormat.Format32bppArgb;
            }
            else
            {
                pf = bmp.PixelFormat;
            }

            Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix mtrx = new Matrix();
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);

            Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            g = Graphics.FromImage(dst);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();
            tmp.Dispose();
            GC.Collect();
            return dst;
        }
        #endregion

        #region 旋转任意角度方法二

        /// <summary>
        /// 获取原图像绕中心任意角度旋转后的图像
        /// </summary>
        /// <param name="rawImg"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Image GetRotateImage(Image srcImage, int angle)
        {
            angle %= 360;
            //原图的宽和高
            int srcWidth = srcImage.Width;
            int srcHeight = srcImage.Height;
            //图像旋转之后所占区域宽和高
            Rectangle rotateRec = GetRotateRectangle(srcWidth, srcHeight, angle);
            int rotateWidth = rotateRec.Width;
            int rotateHeight = rotateRec.Height;
            //目标位图
            Bitmap destImage = null;
            Graphics graphics = null;
            try
            {
                //定义画布，宽高为图像旋转后的宽高
                destImage = new Bitmap(rotateWidth, rotateHeight);
                //graphics根据destImage创建，因此其原点此时在destImage左上角
                graphics = Graphics.FromImage(destImage);
                //要让graphics围绕某矩形中心点旋转N度，分三步
                //第一步，将graphics坐标原点移到矩形中心点,假设其中点坐标（x,y）
                //第二步，graphics旋转相应的角度(沿当前原点)
                //第三步，移回（-x,-y）
                //获取画布中心点
                Point centerPoint = new Point(rotateWidth / 2, rotateHeight / 2);
                //将graphics坐标原点移到中心点
                graphics.TranslateTransform(centerPoint.X, centerPoint.Y);
                //graphics旋转相应的角度(绕当前原点)
                graphics.RotateTransform(angle);
                //恢复graphics在水平和垂直方向的平移(沿当前原点)
                graphics.TranslateTransform(-centerPoint.X, -centerPoint.Y);
                //此时已经完成了graphics的旋转

                //计算:如果要将源图像画到画布上且中心与画布中心重合，需要的偏移量
                Point Offset = new Point((rotateWidth - srcWidth) / 2, (rotateHeight - srcHeight) / 2);
                //将源图片画到rect里（rotateRec的中心）
                graphics.DrawImage(srcImage, new Rectangle(Offset.X, Offset.Y, srcWidth, srcHeight));
                //重至绘图的所有变换
                graphics.ResetTransform();
                graphics.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (graphics != null)
                    graphics.Dispose();
            }
            return destImage;
        }
        /// <summary>
        /// 计算矩形绕中心任意角度旋转后所占区域矩形宽高
        /// </summary>
        /// <param name="width">原矩形的宽</param>
        /// <param name="height">原矩形高</param>
        /// <param name="angle">顺时针旋转角度</param>
        /// <returns></returns>
        public static Rectangle GetRotateRectangle(int width, int height, float angle)
        {
            double radian = angle * Math.PI / 180; ;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //只需要考虑到第四象限和第三象限的情况取大值(中间用绝对值就可以包括第一和第二象限)
            int newWidth = Convert.ToInt32(Math.Max(Math.Abs(width * cos - height * sin), Math.Abs(width * cos + height * sin)));
            int newHeight = Convert.ToInt32(Math.Max(Math.Abs(width * sin - height * cos), Math.Abs(width * sin + height * cos)));
            return new Rectangle(0, 0, newWidth, newHeight);
        }
        #endregion
        #endregion

        #region 获得图片

        /// <summary>
        /// 获取剪切板中的图片
        /// </summary>
        private static Image GetClipBoardImage()
        {
            Image image = null;
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.MetafilePict))
            {
                var img = Clipboard.GetImage();
                image = img;
            }
            else if (iData.GetDataPresent(DataFormats.FileDrop))
            {
                var files = Clipboard.GetFileDropList();
                if (files.Count == 0) { return null; }
                image = Image.FromFile(files[0]);
            }

            else if (iData.GetDataPresent(DataFormats.Text))
            {
                var path = iData.GetData(DataFormats.Text) as string;
                var chars = Path.GetInvalidPathChars();
                if (path.IndexOfAny(chars) >= 0)
                {
                    MessageBox.Show("路径中包含非法字符！");
                    return null;
                }
                if (System.IO.File.Exists(path))
                {
                    var extension = path.Substring(path.LastIndexOf("."));
                    string imgType = ".png|.jpg|.jpeg";
                    if (imgType.Contains(extension.ToLower()))
                    {
                        image = Image.FromFile(path);
                    }
                    else
                    {
                        MessageBox.Show("格式错误！");
                    }
                }
                else
                {
                    MessageBox.Show("文件不存在！");
                }
            }
            return image;
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
            var info = new ProcessStartInfo(path)
            {
                UseShellExecute = true,
            };
            Process.Start(info);    // 打开指定文件夹，选中指定文件.
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
    }
}

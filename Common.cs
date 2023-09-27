using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatePicture
{
    public static class Common
    {
        #region 从剪切板中获得图片

        /// <summary>
        /// 获取剪切板中的图片
        /// </summary>
        public static Image GetClipBoardImage()
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

        #region 从文件中读取图片（流读取，不锁定文件）
        /// <summary>
        /// 通过FileStream 来打开文件，这样就可以实现不锁定Image文件，到时可以让多用户同时访问Image文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap ReadImageFile(string path)
        {
            if (!File.Exists(path))
            {
                return null;//文件不存在
            }
            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] image = new Byte[filelength]; //建立一个字节数组 
            fs.Read(image, 0, filelength); //按字节流读取 
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            Bitmap bit = new Bitmap(result);
            return bit;
        }
        #endregion

        #region 打开文件夹或文件
        public static void OpenFile(string path)
        {
            var info = new ProcessStartInfo(path)
            {
                UseShellExecute = true,
            };
            Process.Start(info);    // 打开指定文件夹，选中指定文件.
        }
        #endregion

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

        #region 删除文件夹下的全部文件
        public static void Empty(this DirectoryInfo directoryInfo)
        {
            foreach (System.IO.FileInfo file in directoryInfo.GetFiles()) file.Delete();    // 当前目录下的全部文件
            foreach (System.IO.DirectoryInfo subDirectory in directoryInfo.GetDirectories()) subDirectory.Delete(true); // 当前目录下的全部文件夹
        }
        #endregion

        public static void RemoveAllControls(this Control control)
        {
            while (control.Controls.Count > 0)
            {
                Control ct = control.Controls[0];
                control.Controls.Remove(ct);
                ct.Dispose();
            }
        }
    }

    /// <summary>
    /// 字符串排序规则
    /// </summary>
    public class FileNameSort : IComparer
    {
        //调用DLL
        [System.Runtime.InteropServices.DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string param1, string param2);


        //前后文件名进行比较。Array.Sort将会使用此Compare方式执行排序。
        public int Compare(object name1, object name2)
        {
            if (null == name1 && null == name2)
            {
                return 0;
            }
            if (null == name1)
            {
                return -1;
            }
            if (null == name2)
            {
                return 1;
            }
            return StrCmpLogicalW(name1.ToString(), name2.ToString());
        }
    }
}

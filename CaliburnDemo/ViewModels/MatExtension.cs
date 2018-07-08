using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace ImageToolDemo
{
    public static class MatExtension
    {
        #region Mat操作
        /*
        * Caution!
        * The following method may leak memory and cause unexcepted errors.
        * Plase use GetArray after calling GetImage methods.
        */
        public static double[] GetDoubleArray(this Mat mat)
        {
            double[] temp = new double[mat.Height * mat.Width];
            Marshal.Copy(mat.DataPointer, temp, 0, mat.Height * mat.Width);
            return temp;
        }

        public static float[] GetFloatArray(this Mat mat)
        {
            var byteArray = mat.GetData();
            float[] temp = new float[byteArray.Length];
            Marshal.Copy(mat.DataPointer, temp, 0, byteArray.Length);
            return temp;
        }

        /*
        * Caution!
        * The following method may leak memory and cause unexcepted errors.
        * Plase use GetArray after calling GetImage methods.
        */
        public static int[] GetIntArray(this Mat mat)
        {
            int[] temp = new int[mat.Height * mat.Width];
            Marshal.Copy(mat.DataPointer, temp, 0, mat.Height * mat.Width);
            return temp;
        }

        /*
        * Caution!
        * The following method may leak memory and cause unexcepted errors.
        * Plase use GetArray after calling GetImage methods.
        */
        public static byte[] GetByteArray(this Mat mat)
        {
            byte[] temp = new byte[mat.Height * mat.Width];
            Marshal.Copy(mat.DataPointer, temp, 0, mat.Height * mat.Width);
            return temp;
        }

        /*
        * Caution!
        * The following method may leak memory and cause unexcepted errors.
        * Plase use GetArray after calling GetImage methods.
        */
        public static void SetDoubleArray(this Mat mat, double[] data)
        {
            Marshal.Copy(data, 0, mat.DataPointer, mat.Height * mat.Width);
        }

        /*
        * Caution!
        * The following method may leak memory and cause unexcepted errors.
        * Plase use GetArray after calling GetImage methods.
        */
        public static void SetIntArray(this Mat mat, int[] data)
        {
            Marshal.Copy(data, 0, mat.DataPointer, mat.Height * mat.Width);
        }

        /*
        * Caution!
        * The following method may leak memory and cause unexcepted errors.
        * Plase use GetArray after calling GetImage methods.
        */
        public static void SetByteArray(this Mat mat, byte[] data)
        {
            Marshal.Copy(data, 0, mat.DataPointer, mat.Height * mat.Width);
        }

        public static Image<Gray, Byte> GetGrayImage(this Mat mat)
        {
            Image<Gray, Byte> image = mat.ToImage<Gray, Byte>();
            return image;
        }

        public static Image<Bgr, Byte> GetBgrImage(this Mat mat)
        {
            Image<Bgr, Byte> image = mat.ToImage<Bgr, Byte>();
            return image;
        }

        public static Image<Xyz, Byte> GetXyzImage(this Mat mat)
        {
            Image<Xyz, Byte> image = mat.ToImage<Xyz, Byte>();
            return image;
        }

        public static Image<Bgra, Byte> GetBgraImage(this Mat mat)
        {
            Image<Bgra, Byte> image = mat.ToImage<Bgra, Byte>();
            return image;
        }

        #endregion

        #region Image操作
        public static Bgr Clone(this Bgr src)
        {
            Bgr bgr = new Bgr(src.Blue, src.Green, src.Red);
            return bgr;
        }

        public static Bgra Clone(this Bgra src)
        {
            Bgra bgr = new Bgra(src.Blue, src.Green, src.Red, src.Alpha);
            return bgr;
        }

        public static Xyz Clone(this Xyz src)
        {
            Xyz xyz = new Xyz(src.X, src.Y, src.Z);
            return xyz;
        }

        public static Mat ConvertMat(this Image<Gray, Byte> image)
        {
            Mat mat = new Mat(image.Mat, new Rectangle(0, 0, image.Width, image.Height));
            return mat;
        }

        public static Mat ConvertMat(this Image<Bgr, Byte> image)
        {
            Mat mat = new Mat(image.Mat, new Rectangle(0, 0, image.Width, image.Height));
            return mat;
        }

        public static Mat ConvertMat(this Image<Xyz, Byte> image)
        {
            Mat mat = new Mat(image.Mat, new Rectangle(0, 0, image.Width, image.Height));
            return mat;
        }

        public static Mat ConvertMat(this Image<Bgra, Byte> image)
        {
            Mat mat = new Mat(image.Mat, new Rectangle(0, 0, image.Width, image.Height));
            return mat;
        }

        public static void SubChannelsOfImage(this Image<Bgr, Byte> image,
            out Image<Gray, Byte> blue, out Image<Gray, Byte> green, out Image<Gray, Byte> red)
        {
            Image<Gray, Byte>[] subchannels = image.Split();
            blue = subchannels[0];
            green = subchannels[1];
            red = subchannels[2];
        }

        public static void SubChannelsOfImage(this Image<Xyz, Byte> image,
            out Image<Gray, Byte> x, out Image<Gray, Byte> y, out Image<Gray, Byte> z)
        {
            Image<Gray, Byte>[] subchannels = image.Split();
            x = subchannels[0];
            y = subchannels[1];
            z = subchannels[2];
        }

        public static void SubChannelsOfImage(this Image<Bgra, Byte> image,
            out Image<Gray, Byte> blue, out Image<Gray, Byte> green, out Image<Gray, Byte> red, out Image<Gray, Byte> alpha)
        {
            Image<Gray, Byte>[] subchannels = image.Split();
            blue = subchannels[0];
            green = subchannels[1];
            red = subchannels[2];
            alpha = subchannels[3];
        }

        public static double GetBlue(this Image<Bgr, Byte> image, Point point)
        {
            return image[point].Blue;
        }

        public static double GetGreen(this Image<Bgr, Byte> image, Point point)
        {
            return image[point].Green;
        }

        public static double GetRed(this Image<Bgr, Byte> image, Point point)
        {
            return image[point].Red;
        }

        public static void SetBlue(this Image<Bgr, Byte> image, Point point, double blue)
        {
            Bgr bgr = image[point].Clone();
            bgr.Blue = blue;
            image[point] = bgr;
        }

        public static void SetGreen(this Image<Bgr, Byte> image, Point point, double green)
        {
            Bgr bgr = image[point].Clone();
            bgr.Green = green;
            image[point] = bgr;
        }

        public static void SetRed(this Image<Bgr, Byte> image, Point point, double red)
        {
            Bgr bgr = image[point].Clone();
            bgr.Red = red;
            image[point] = bgr;
        }

        public static double GetBlue(this Image<Bgra, Byte> image, Point point)
        {
            return image[point].Blue;
        }

        public static double GetGreen(this Image<Bgra, Byte> image, Point point)
        {
            return image[point].Green;
        }

        public static double GetRed(this Image<Bgra, Byte> image, Point point)
        {
            return image[point].Red;
        }

        public static double GetAlpha(this Image<Bgra, Byte> image, Point point)
        {
            return image[point].Alpha;
        }

        public static void SetBlue(this Image<Bgra, Byte> image, Point point, double blue)
        {
            Bgra bgra = image[point].Clone();
            bgra.Blue = blue;
            image[point] = bgra;
        }

        public static void SetGreen(this Image<Bgra, Byte> image, Point point, double green)
        {
            Bgra bgra = image[point].Clone();
            bgra.Green = green;
            image[point] = bgra;
        }

        public static void SetRed(this Image<Bgra, Byte> image, Point point, double red)
        {
            Bgra bgra = image[point].Clone();
            bgra.Red = red;
            image[point] = bgra;
        }

        public static void SetAlpha(this Image<Bgra, Byte> image, Point point, double alpha)
        {
            Bgra bgra = image[point].Clone();
            bgra.Alpha = alpha;
            image[point] = bgra;
        }

        public static double GetX(this Image<Xyz, Byte> image, Point point)
        {
            return image[point].X;
        }

        public static double GetY(this Image<Xyz, Byte> image, Point point)
        {
            return image[point].Y;
        }

        public static double GetZ(this Image<Xyz, Byte> image, Point point)
        {
            return image[point].Z;
        }

        public static void SetX(this Image<Xyz, Byte> image, Point point, double x)
        {
            Xyz xyz = image[point].Clone();
            xyz.X = x;
            image[point] = xyz;
        }

        public static void SetY(this Image<Xyz, Byte> image, Point point, double y)
        {
            Xyz xyz = image[point].Clone();
            xyz.Y = y;
            image[point] = xyz;
        }

        public static void SetZ(this Image<Xyz, Byte> image, Point point, double z)
        {
            Xyz xyz = image[point].Clone();
            xyz.Z = z;
            image[point] = xyz;
        }

        public static double[] GetDoubleArray(this Image<Gray, Byte> image)
        {
            double[] array = new double[image.Height * image.Width];
            Array.Copy(image.Bytes, array, image.Height * image.Width);
            return array;
        }

        public static int[] GetIntArray(this Image<Gray, Byte> image)
        {
            int[] array = new int[image.Height * image.Width];
            Array.Copy(image.Bytes, array, image.Height * image.Width);
            return array;
        }

        public static byte[] GetByteArray(this Image<Gray, Byte> image)
        {
            byte[] array = new byte[image.Height * image.Width];
            Array.Copy(image.Bytes, array, image.Height * image.Width);
            return array;
        }

        public static void SetDoubleArray(this Image<Gray, Byte> image, double[] array)
        {
            if (array.Length == image.Height * image.Width)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    for (int h = 0; h < image.Height; h++)
                    {
                        image.Data[h, w, 0] = (byte)array[w * image.Height + h];
                    }
                }
            }
        }

        public static void SetIntArray(this Image<Gray, Byte> image, int[] array)
        {
            if (array.Length == image.Height * image.Width)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    for (int h = 0; h < image.Height; h++)
                    {
                        image.Data[h, w, 0] = (byte)array[w * image.Height + h];
                    }
                }
            }
        }

        public static void SetByteArray(this Image<Gray, Byte> image, byte[] array)
        {
            if (array.Length == image.Height * image.Width)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    for (int h = 0; h < image.Height; h++)
                    {
                        image.Data[h, w, 0] = (byte)array[w * image.Height + h];
                    }
                }
            }
        }

        #endregion

        #region 图片分析
        public static void HistAnalysis(this Mat src, ref double minValue, ref double maxValue, double peakValue)
        {

        }

        #endregion

    }
}

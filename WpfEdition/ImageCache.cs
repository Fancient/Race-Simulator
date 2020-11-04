using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

[assembly: InternalsVisibleTo("WpfEdition.Test")]

namespace WpfEdition
{
    public static class ImageCache
    {
        internal static Dictionary<string, Bitmap> BitmapCache;

        public static void Initialize()
        {
            BitmapCache = new Dictionary<string, Bitmap>();
        }

        /// <summary>
        /// Gets bitmap for filename from cache.
        /// If bitmap is not in cache, load it and add to cache.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>bitmap</returns>
        public static Bitmap GetBitmap(string filename)
        {
            if (!BitmapCache.ContainsKey(filename))
                BitmapCache.Add(filename, new Bitmap(filename));
            return BitmapCache[filename];
        }

        /// <summary>
        /// Clears the cache dictionary
        /// </summary>
        public static void ClearCache()
        {
            BitmapCache.Clear();
        }

        /// <summary>
        /// Creates an empty bitmap with dimension in parameters and adds it to
        /// the dictionary with key "empty".
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>bitmap from dictionary with key "empty"</returns>
        public static Bitmap CreateEmptyBitmap(int width, int height)
        {
            // key: "empty"
            string key = "empty";
            if (!BitmapCache.ContainsKey(key))
            {
                BitmapCache.Add(key, new Bitmap(width, height));
                // teken een achtergrond op de bitmap
                Graphics g = Graphics.FromImage(BitmapCache[key]);
                g.Clear(Color.DarkGreen); // Background color
            }
            return (Bitmap)BitmapCache[key].Clone();
        }

        /// <summary>
        /// Converts Bitmap to BitmapSource
        /// This method was provided to me from school.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns>BitmapSource</returns>
        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
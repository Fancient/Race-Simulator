using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

namespace WpfEdition
{
    public static class ImageCache
    {
        private static Dictionary<string, Bitmap> _bitmapCache;

        public static void Initialize()
        {
            _bitmapCache = new Dictionary<string, Bitmap>();
        }

        /// <summary>
        /// Gets bitmap for filename from cache.
        /// If bitmap is not in cache, load it and add to cache.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>bitmap</returns>
        public static Bitmap GetBitmap(string filename)
        {
            if (!_bitmapCache.ContainsKey(filename))
                _bitmapCache.Add(filename, new Bitmap(filename));
            return _bitmapCache[filename];
        }

        /// <summary>
        /// Clears the cache dictionary
        /// </summary>
        public static void ClearCache()
        {
            _bitmapCache.Clear();
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
            if (!_bitmapCache.ContainsKey(key))
            {
                _bitmapCache.Add(key, new Bitmap(width, height));
                // teken een achtergrond op de bitmap
                Graphics g = Graphics.FromImage(_bitmapCache[key]);
                g.Clear(Color.DarkGreen); // Background color
            }
            return (Bitmap)_bitmapCache[key].Clone();
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
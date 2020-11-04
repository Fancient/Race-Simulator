using NSubstitute;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using WpfEdition;

namespace WpfEdition.Test
{
    [TestFixture]
    public class ImageCacheTests
    {


        [SetUp]
        public void SetUp()
        {
            ImageCache.Initialize();
        }

        [Test]
        public void Initialize_AfterSetup_DictionaryNotNull()
        {
            Assert.IsNotNull(ImageCache.BitmapCache);
        }

        [Test]
        public void GetBitmap_DictionaryEmptyInputValidFilename_ReturnBitmapObject()
        {
            // Arrange
            string filename = @".\Resource\TrackPieces\StraightHorizontal.png";

            // Act
            var result = ImageCache.GetBitmap(filename);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(Bitmap), result);
        }

        [Test]
        public void ClearCache_DictionaryContainsElements_ShouldClearDictionary()
        {
            // Arrange
            // add empty bitmaps to "cache"
            for (int i = 0; i < 10; i++)
            {
                ImageCache.BitmapCache.Add($"Test{i}", new Bitmap(10,10));
            }

            // Act
            ImageCache.ClearCache();

            // Assert
            Assert.IsEmpty(ImageCache.BitmapCache);
        }

        [Test]
        public void CreateEmptyBitmap_ReturnBitmapAccordingToWidthAndHeight()
        {
            // Arrange
            int width = 100;
            int height = 100;

            // Act
            var result = ImageCache.CreateEmptyBitmap(width, height);

            // Assert
            Assert.IsInstanceOf<Bitmap>(result);
            Assert.AreEqual(width, result.Width);
            Assert.AreEqual(height, result.Height);
        }

        [Test]
        public void CreateBitmapSourceFromGdiBitmap_NewBitmap_ShouldReturnBitmapSourceSameSize()
        {
            // Arrange
            Bitmap bitmap = new Bitmap(100,100);

            // Act
            var result = ImageCache.CreateBitmapSourceFromGdiBitmap(bitmap);

            // Assert
            Assert.IsInstanceOf<BitmapSource>(result);
            Assert.IsTrue(Math.Abs(result.Width - 100) < .0001);
            Assert.IsTrue(Math.Abs(result.Height - 100) < .0001);
        }
    }
}

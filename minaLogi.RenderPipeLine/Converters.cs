using Beutl.Media.Pixel;
using Beutl.Media;
using Beutl.Rendering;
using Beutl.Media.Source;
using Beutl.Graphics;
using OpenCvSharp;

namespace minaLogi.RenderPipeLine
{
    internal static class Converters
    {
        internal static IImageSource AsImageSource(this Bitmap<Bgra8888> bitmap)
            => new BitmapSource(Ref<IBitmap>.Create(bitmap), "image");
    }
}

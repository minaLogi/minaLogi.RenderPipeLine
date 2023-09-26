using Beutl.Extensibility;
using Beutl.Graphics;
using Beutl.Media;
using Beutl.Media.Pixel;
using System.Diagnostics;

namespace minaLogi.RenderPipeLine
{
    public class TestRenderer : Renderer
    {
        public override string Name => "TestRenderer";
        public override string Description => "Test";

        public override Bitmap<Bgra8888>? Render(Drawables drawables)
        {
            var bitmap = new Bitmap<Bgra8888>(Width, Height);
            bitmap.Fill(new Bgra8888(0x00, 0xff, 0x00, 0xff));
            foreach (var drawable in drawables)
            {
                Debug.WriteLine(drawable.GetType().ToString());
            }
            return bitmap;
        }
    }
    [Export]
    public class TestRendererExtension : Extension
    {
        public override string Name => "minaLogi.TestRendererExtension";

        public override string DisplayName => "TestRenderer for RenderPipeLine";

        public override void Load()
        {
            base.Load();
            RenderingAPI.Register(new TestRenderer());
        }
    }
}

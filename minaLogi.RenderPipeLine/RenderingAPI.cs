using Avalonia.Rendering;
using Beutl.Graphics;
using Beutl.Media;
using Beutl.Media.Pixel;
using Beutl.Rendering;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace minaLogi.RenderPipeLine
{
    public class RenderingAPI
    {
        internal Renderer? Renderer { get; private set; }
        public RenderingAPI(Renderer? renderer, int w, int h)
        {
            if(renderer != null)
            {
                ShiftRenderer(renderer);
            }
            if (Renderer != null)
            {
                Renderer.Width = w;
                Renderer.Height = h;
                Renderer.Load();
            }
        }

        public void ShiftRenderer(Renderer renderer)
        {
            Renderer = renderer;
            Renderer.OnSelected();
        }

        public static void Register(Renderer renderer)
        {
            RendererEnum.AddChoice(renderer);
        }

        public Bitmap<Bgra8888>? Render(Drawables drawables )
        {
            return Renderer != null ? Renderer.Render(drawables) : null;
        }
    }
}

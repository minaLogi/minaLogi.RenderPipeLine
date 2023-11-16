using Beutl.Graphics;
using Beutl.Media;
using Beutl.Media.Pixel;

namespace minaLogi.RenderPipeLine
{
    public class RenderingAPI
    {
        internal Renderer? Renderer { get; private set; }
        public RenderingAPI(RendererContainer? renderer, int w, int h)
        {
            if(renderer != null)
            {
                ShiftRenderer(renderer.ActivateRenderer());
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

        public static void Register<T>()
            where T : Renderer
        {
            RendererEnum.AddChoice(new RendererContainer(typeof(T)));
        }

        public Bitmap<Bgra8888>? Render(Drawables drawables )
        {
            return Renderer != null ? Renderer.Render(drawables) : null;
        }
    }
}

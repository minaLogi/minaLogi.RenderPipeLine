using Avalonia.Rendering;
using Beutl.Graphics;
using Beutl.Media;
using Beutl.Media.Pixel;
using Beutl.Rendering;

namespace minaLogi.RenderPipeLine
{
    public class RenderingAPI
    {
        public static List<Type> Renderers { get; private set; } = new();
        internal Renderer? Renderer { get; private set; }
        public RenderingAPI(string? rendererName, int w, int h) {
            if (NameExists(rendererName))
            {
                Renderer = (Renderer?)Activator.CreateInstance(Renderers.Where(r => r.Name.Equals(rendererName)).First());
            }
            if (Renderer != null)
            {
                Renderer.Width = w;
                Renderer.Height = h;
                Renderer.Load();
            }
        }

        public bool NameExists(string? name) => Renderers.Where(r => r.Name.Equals(name)).Any();

        public static void Register<T>()
            where T : Renderer
        {
            Renderers.Add(typeof(T));
        }

        public Bitmap<Bgra8888>? Render(Drawables drawables )
        {
            return Renderer != null ? Renderer.Render(drawables) : null;
        }
    }
}

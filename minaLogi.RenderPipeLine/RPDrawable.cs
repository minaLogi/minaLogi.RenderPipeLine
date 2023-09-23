using Avalonia.Rendering.Composition;
using Beutl;
using Beutl.Graphics;
using Beutl.Media;
using Beutl.Media.Pixel;
using Beutl.ProjectSystem;
using Beutl.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using static minaLogi.RenderPipeLine.Converters;

namespace minaLogi.RenderPipeLine
{
    public class RPDrawable : Drawable
    {
        private int _width;
        private int _height;
        private string? _renderername;

        public RenderingAPI RenderingAPI { get; private set; }

        internal Drawables Drawables { get; set; } = new Drawables();

        public RPDrawable(RPCanvasOperator parent)
        {
            RenderingAPI = new(RendererName, Width, Height);
            
        }
        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                if(RenderingAPI.Renderer != null)
                {
                    RenderingAPI.Renderer.Width = value;
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(Width)));
                }
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                if (RenderingAPI.Renderer != null)
                {
                    RenderingAPI.Renderer.Height = value;
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(Height)));
                }
            }
        }
        public string? RendererName
        {
            get => _renderername;
            set
            {
                _renderername = value;
                if(value != null)
                {
                    if (RenderingAPI.NameExists(value))
                    {
                        RenderingAPI = new(value, Width, Height);
                        RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(RendererName)));
                    }
                }
            }
        }


        protected override Size MeasureCore(Size availableSize)
        {
            return new Size(Width, Height);
        }

        protected override void OnDraw(ICanvas canvas)
        {
            if (RenderingAPI.Render(Drawables) is Bitmap<Bgra8888> bitmap)
            {
                canvas.DrawImageSource(bitmap.AsImageSource(), Brushes.White, null);
            }
        }
    }
}

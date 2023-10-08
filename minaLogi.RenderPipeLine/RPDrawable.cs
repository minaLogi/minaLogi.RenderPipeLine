using Avalonia.Rendering.Composition;
using Beutl;
using Beutl.Graphics;
using Beutl.Graphics.Effects;
using Beutl.Language;
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
        private RendererContainer? _renderer;

        public RenderingAPI RenderingAPI { get; private set; }

        internal Drawables Drawables { get; set; } = new Drawables();

        public RPDrawable()
        {
            RenderingAPI = new(Renderer, Width, Height);
            AffectsRender(
                TransformProperty,
                FilterEffectProperty,
                TransformOriginProperty,
                BlendModeProperty);
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
        public RendererContainer? Renderer
        {
            get => _renderer;
            set
            {
                _renderer = value;
                if(value != null)
                {
                    if (RenderingAPI.Renderer != null)
                    {
                        RenderingAPI.Renderer.Close();
                    }
                    var r = value.ActivateRenderer();
                    r.Width = Width;
                    r.Height = Height;
                    RenderingAPI.ShiftRenderer(r);
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(Renderer)));
                }
            }
        }
        private void AffectsRender(params CoreProperty[] properties)
        {
            foreach (CoreProperty item in properties)
            {
                item.Changed.Subscribe(e =>
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this));
                    if (e.OldValue is IAffectsRender oldAffectsRender)
                    {
                        oldAffectsRender.Invalidated -= AffectsRender_Invalidated;
                    }

                    if (e.NewValue is IAffectsRender newAffectsRender)
                    {
                        newAffectsRender.Invalidated += AffectsRender_Invalidated;
                    }
                });
            }
        }
        private void AffectsRender_Invalidated(object? sender, RenderInvalidatedEventArgs e)
        {
            RaiseInvalidated(e);
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

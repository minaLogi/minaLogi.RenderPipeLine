using Beutl.Operation;
using Beutl;
using Beutl.Graphics;
using Beutl.Media;
using System.ComponentModel.DataAnnotations;
using Beutl.Operators.Configure;

namespace minaLogi.RenderPipeLine
{
    public sealed class RPCanvasOperator : SourceOperator
    {
        private int _width;
        private int _height;
        private Renderer? _renderer;

        public static readonly CoreProperty<int> WidthProperty;
        public static readonly CoreProperty<int> HeightProperty;
        public static readonly CoreProperty<Renderer?> RendererProperty;


        internal RPDrawable RPDrawable;

        static RPCanvasOperator()
        {
            WidthProperty = ConfigureProperty<int, RPCanvasOperator>(nameof(Width))
                .Accessor(o => o.Width, (o, v) => o.Width = v)
                .DefaultValue(1280)
                .Register();
            HeightProperty = ConfigureProperty<int, RPCanvasOperator>(nameof(Height))
                .Accessor(o => o.Height, (o, v) => o.Height = v)
                .DefaultValue(720)
                .Register();
            RendererProperty = ConfigureProperty<Renderer?, RPCanvasOperator>(nameof(Renderer))
                .Accessor(o => o.Renderer, (o, v) => o.Renderer = v)
                .Register();
        }

        public RPCanvasOperator()
        {
            Properties.Add(new CorePropertyImpl<int>(WidthProperty, this));
            Properties.Add(new CorePropertyImpl<int>(HeightProperty, this));
            Properties.Add(new CorePropertyImpl<Renderer?>(RendererProperty, this));
            RPDrawable = new(this);
        }

        [Range(1, int.MaxValue)]
        public int Width
        {
            get => _width;
            set
            {
                RPDrawable.Width = value;
                if(SetAndRaise(WidthProperty, ref _width, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(Width)));
                }
            }
        }

        [Range(1, int.MaxValue)]
        public int Height
        {
            get => _height;
            set
            {
                RPDrawable.Height = value;
                if(SetAndRaise(HeightProperty, ref _height, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(Height)));
                }
            }
        }

        [ChoicesProvider(typeof(RendererEnum))]
        public Renderer? Renderer
        {
            get => _renderer;
            set
            {
                RPDrawable.Renderer = value;
                if(SetAndRaise(RendererProperty, ref _renderer, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(Renderer)));
                }
            }
        }

        public override void Evaluate(OperatorEvaluationContext context)
        {
            if(IsEnabled)
            {
                RPDrawable.Drawables.Replace(context.FlowRenderables.OfType<Drawable>()
                    .Where(d => d.GetType() != typeof(RPDrawable)).ToArray());
                context.FlowRenderables.Clear();
                context.AddFlowRenderable(RPDrawable);
            }
            
        }


    }
}

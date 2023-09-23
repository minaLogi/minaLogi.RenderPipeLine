using Beutl.Operation;
using Beutl;
using Beutl.Rendering;
using Beutl.Styling;
using Beutl.Graphics;
using Beutl.Graphics.Effects;
using Beutl.Animation;
using Beutl.Media;
using System.ComponentModel.DataAnnotations;
using DynamicData;
using Beutl.Collections.Pooled;
using Beutl.ProjectSystem;
using System.Xml.Linq;
using ExCSS;
using Beutl.Operators.Configure;

namespace minaLogi.RenderPipeLine
{
    public sealed class RPCanvasOperator : SourceOperator
    {
        private int _width;
        private int _height;
        private string? _renderername;

        public static readonly CoreProperty<int> WidthProperty;
        public static readonly CoreProperty<int> HeightProperty;
        public static readonly CoreProperty<string?> RendererNameProperty;


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
            RendererNameProperty = ConfigureProperty<string?, RPCanvasOperator>(nameof(RendererName))
                .Accessor(o => o.RendererName, (o, v) => o.RendererName = v)
                .DefaultValue("TestRenderer")
                .Register();
        }

        public RPCanvasOperator()
        {
            Properties.Add(new CorePropertyImpl<int>(WidthProperty, this));
            Properties.Add(new CorePropertyImpl<int>(HeightProperty, this));
            Properties.Add(new CorePropertyImpl<string?>(RendererNameProperty, this));
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

        public string? RendererName
        {
            get => _renderername;
            set
            {
                RPDrawable.RendererName = value;
                if(SetAndRaise(RendererNameProperty, ref _renderername, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(RendererName)));
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

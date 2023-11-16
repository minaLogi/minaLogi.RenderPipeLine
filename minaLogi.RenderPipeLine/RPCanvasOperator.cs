using Beutl.Operation;
using Beutl;
using Beutl.Graphics;
using Beutl.Media;
using System.ComponentModel.DataAnnotations;
using Beutl.Operators.Configure;
using Beutl.Graphics.Effects;
using Beutl.Language;
using Beutl.Graphics.Transformation;

namespace minaLogi.RenderPipeLine
{
    public sealed class RPCanvasOperator : SourceOperator
    {
        private int _width;
        private int _height;
        private RendererContainer? _renderer;
        private ITransform? _transform = new TransformGroup();
        private FilterEffect? _filterEffect = new FilterEffectGroup();
        private AlignmentX _alignX = AlignmentX.Center;
        private AlignmentY _alignY = AlignmentY.Center;
        private RelativePoint _transformOrigin = RelativePoint.Center;
        private BlendMode _blendMode = BlendMode.SrcOver;

        public static readonly CoreProperty<int> WidthProperty;
        public static readonly CoreProperty<int> HeightProperty;
        public static readonly CoreProperty<RendererContainer?> RendererProperty;
        public static readonly CoreProperty<ITransform?> TransformProperty;
        public static readonly CoreProperty<FilterEffect?> FilterEffectProperty;
        public static readonly CoreProperty<AlignmentX> AlignmentXProperty;
        public static readonly CoreProperty<AlignmentY> AlignmentYProperty;
        public static readonly CoreProperty<RelativePoint> TransformOriginProperty;
        public static readonly CoreProperty<BlendMode> BlendModeProperty;


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

            RendererProperty = ConfigureProperty<RendererContainer?, RPCanvasOperator>(nameof(Renderer))
                .Accessor(o => o.Renderer, (o, v) => o.Renderer = v)
                .Register();

            TransformProperty = ConfigureProperty<ITransform?, RPCanvasOperator>(nameof(Transform))
                .Accessor(o => o.Transform, (o, v) => o.Transform = v)
                .DefaultValue(new TransformGroup())
                .Register();

            FilterEffectProperty = ConfigureProperty<FilterEffect?, RPCanvasOperator>(nameof(FilterEffect))
                .Accessor(o => o.FilterEffect, (o, v) => o.FilterEffect = v)
                .DefaultValue(new FilterEffectGroup())
                .Register();

            AlignmentXProperty = ConfigureProperty<AlignmentX, RPCanvasOperator>(nameof(AlignmentX))
                .Accessor(o => o.AlignmentX, (o, v) => o.AlignmentX = v)
                .DefaultValue(AlignmentX.Center)
                .Register();

            AlignmentYProperty = ConfigureProperty<AlignmentY, RPCanvasOperator>(nameof(AlignmentY))
                .Accessor(o => o.AlignmentY, (o, v) => o.AlignmentY = v)
                .DefaultValue(AlignmentY.Center)
                .Register();

            TransformOriginProperty = ConfigureProperty<RelativePoint, RPCanvasOperator>(nameof(TransformOrigin))
                .Accessor(o => o.TransformOrigin, (o, v) => o.TransformOrigin = v)
                .DefaultValue(RelativePoint.Center)
                .Register();

            BlendModeProperty = ConfigureProperty<BlendMode, RPCanvasOperator>(nameof(BlendMode))
                .Accessor(o => o.BlendMode, (o, v) => o.BlendMode = v)
                .DefaultValue(BlendMode.SrcOver)
                .Register();
        }

        public RPCanvasOperator()
        {
            Properties.Add(new CorePropertyImpl<int>(WidthProperty, this));
            Properties.Add(new CorePropertyImpl<int>(HeightProperty, this));
            Properties.Add(new CorePropertyImpl<RendererContainer?>(RendererProperty, this));
            Properties.Add(new CorePropertyImpl<ITransform?>(TransformProperty, this));
            Properties.Add(new CorePropertyImpl<FilterEffect?>(FilterEffectProperty, this));
            Properties.Add(new CorePropertyImpl<AlignmentX>(AlignmentXProperty, this));
            Properties.Add(new CorePropertyImpl<AlignmentY>(AlignmentYProperty, this));
            Properties.Add(new CorePropertyImpl<RelativePoint>(TransformOriginProperty, this));
            Properties.Add(new CorePropertyImpl<BlendMode>(BlendModeProperty, this));
            RPDrawable = new();
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
        public RendererContainer? Renderer
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

        [Display(Name = nameof(Strings.ImageFilter), ResourceType = typeof(Strings), GroupName = nameof(Strings.ImageFilter))]
        public FilterEffect? FilterEffect
        {
            get => _filterEffect;
            set
            {
                RPDrawable.FilterEffect = value;
                if (SetAndRaise(FilterEffectProperty, ref _filterEffect, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(FilterEffect)));
                }
            }
        }

        [Display(Name = nameof(Strings.Transform), ResourceType = typeof(Strings), GroupName = nameof(Strings.Transform))]
        public ITransform? Transform
        {
            get => _transform;
            set
            {
                RPDrawable.Transform = value;
                if (SetAndRaise(TransformProperty, ref _transform, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(Transform)));
                }
            }
        }

        [Display(Name = nameof(Strings.AlignmentX), ResourceType = typeof(Strings), GroupName = nameof(Strings.Transform))]
        public AlignmentX AlignmentX
        {
            get => _alignX;
            set
            {
                RPDrawable.AlignmentX = value;
                if (SetAndRaise(AlignmentXProperty, ref _alignX, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(AlignmentX)));
                }
            }
        }

        [Display(Name = nameof(Strings.AlignmentY), ResourceType = typeof(Strings), GroupName = nameof(Strings.Transform))]
        public AlignmentY AlignmentY
        {
            get => _alignY;
            set
            {
                RPDrawable.AlignmentY = value;
                if (SetAndRaise(AlignmentYProperty, ref _alignY, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(AlignmentY)));
                }
            }
        }

        [Display(Name = nameof(Strings.TransformOrigin), ResourceType = typeof(Strings), GroupName = nameof(Strings.Transform))]
        public RelativePoint TransformOrigin
        {
            get => _transformOrigin;
            set
            {
                RPDrawable.TransformOrigin = value;
                if (SetAndRaise(TransformOriginProperty, ref _transformOrigin, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(TransformOrigin)));
                }
            }
        }

        [Display(Name = nameof(Strings.BlendMode), ResourceType = typeof(Strings))]
        public BlendMode BlendMode
        {
            get => _blendMode;
            set
            {
                RPDrawable.BlendMode = value;
                if (SetAndRaise(BlendModeProperty, ref _blendMode, value))
                {
                    RaiseInvalidated(new RenderInvalidatedEventArgs(this, nameof(BlendMode)));
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

using Beutl.Audio.Effects;
using Beutl.Audio;
using Beutl.Graphics.Effects;
using Beutl.Graphics;
using Beutl.Operation;
using Beutl.Services;
using Beutl.Media;
using Beutl.NodeTree;
using Beutl.Graphics.Transformation;
using Beutl.Animation.Easings;

namespace minaLogi.RenderPipeLine
{
    public static class Register
    {
        private static MultipleTypeLibraryItem BindSourceOperator<T>(this MultipleTypeLibraryItem self)
        where T : SourceOperator
        {
            return self.Bind<T>(KnownLibraryItemFormats.SourceOperator);
        }

        private static MultipleTypeLibraryItem BindNode<T>(this MultipleTypeLibraryItem self)
            where T : Node
        {
            return self.Bind<T>(KnownLibraryItemFormats.Node);
        }

        private static MultipleTypeLibraryItem BindEasing<T>(this MultipleTypeLibraryItem self)
            where T : Easing
        {
            return self.Bind<T>(KnownLibraryItemFormats.Easing);
        }

        private static MultipleTypeLibraryItem BindFilterEffect<T>(this MultipleTypeLibraryItem self)
            where T : FilterEffect
        {
            return self.Bind<T>(KnownLibraryItemFormats.FilterEffect);
        }

        private static MultipleTypeLibraryItem BindTransform<T>(this MultipleTypeLibraryItem self)
            where T : Transform
        {
            return self.Bind<T>(KnownLibraryItemFormats.Transform);
        }

        private static MultipleTypeLibraryItem BindDrawable<T>(this MultipleTypeLibraryItem self)
            where T : Drawable
        {
            return self.Bind<T>(KnownLibraryItemFormats.Drawable);
        }

        private static MultipleTypeLibraryItem BindSound<T>(this MultipleTypeLibraryItem self)
            where T : Sound
        {
            return self.Bind<T>(KnownLibraryItemFormats.Sound);
        }

        private static MultipleTypeLibraryItem BindSoundEffect<T>(this MultipleTypeLibraryItem self)
            where T : SoundEffect
        {
            return self.Bind<T>(KnownLibraryItemFormats.SoundEffect);
        }

        private static MultipleTypeLibraryItem BindBrush<T>(this MultipleTypeLibraryItem self)
            where T : Brush
        {
            return self.Bind<T>(KnownLibraryItemFormats.Brush);
        }

        private static MultipleTypeLibraryItem BindGeometry<T>(this MultipleTypeLibraryItem self)
            where T : Geometry
        {
            return self.Bind<T>(KnownLibraryItemFormats.Geometry);
        }

        private static GroupLibraryItem AddSourceOperator<T>(this GroupLibraryItem self, string displayName, string? description = null)
            where T : SourceOperator
        {
            return self.Add<T>(KnownLibraryItemFormats.SourceOperator, displayName, description);
        }
        public static void Execute()
        {
            LibraryService.Current
                .AddMultiple("Render pipeline", m => m
                .BindSourceOperator<RPCanvasOperator>()
                );
        }
    }
}

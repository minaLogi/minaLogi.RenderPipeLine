using Beutl.Collections;
using Beutl;

namespace minaLogi.RenderPipeLine
{
    public sealed class RendererEnum : IChoicesProvider
    {
        private static readonly CoreList<Renderer> s_choices = new();

        internal static void AddChoice(Renderer renderer)
        {
            s_choices.Add(renderer);
        }

        public static IReadOnlyList<object> GetChoices()
        {
            return s_choices;
        }

        public static ICoreReadOnlyList<Renderer> GetTypedChoices()
        {
            return s_choices;
        }
    }
}

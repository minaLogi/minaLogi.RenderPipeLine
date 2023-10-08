using Beutl.Collections;
using Beutl;

namespace minaLogi.RenderPipeLine
{
    public sealed class RendererEnum : IChoicesProvider
    {
        private static readonly CoreList<RendererContainer> s_choices = new();

        internal static void AddChoice(RendererContainer rc)
        {
            s_choices.Add(rc);
        }

        public static IReadOnlyList<object> GetChoices()
        {
            return s_choices;
        }

        public static ICoreReadOnlyList<RendererContainer> GetTypedChoices()
        {
            return s_choices;
        }
    }
}

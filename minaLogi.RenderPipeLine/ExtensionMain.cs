using Beutl.Extensibility;
namespace minaLogi.RenderPipeLine
{
    [Export]
    public class ExtensionMain : Extension
    {
        public override string Name => "minaLogi.RenderPipeLine";

        public override string DisplayName => "RenderPipeLine";

        public override void Load()
        {
            base.Load();
            Register.Execute();
        }
    }
}
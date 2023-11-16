using System.Text.Json.Serialization;

namespace minaLogi.RenderPipeLine
{
    [JsonConverter(typeof(RendererJsonConverter))]
    public class RendererContainer
    {
        public RendererContainer(Type renderer)
        {
            _renderer = renderer;
        }
        private Type _renderer;

        public string Name => _renderer.Name;

        public override string ToString() => Name;

        public Renderer ActivateRenderer()
        {
            if(Activator.CreateInstance(_renderer) is Renderer renderer)
            {
                return renderer;
            }
            else
            {
                throw new InvalidDataException();
            }
        }
    }
}

using Beutl.Media.Pixel;
using Beutl.Media;
using Beutl.Rendering;
using Beutl.Media.Source;
using Beutl.Graphics;
using OpenCvSharp;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;

namespace minaLogi.RenderPipeLine
{
    internal static class Converters
    {
        internal static IImageSource AsImageSource(this Bitmap<Bgra8888> bitmap)
            => new BitmapSource(Ref<IBitmap>.Create(bitmap), "image");
    }

    internal sealed class RendererJsonConverter : JsonConverter<Renderer>
    {
        public override Renderer? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? s = reader.GetString();
            return RendererEnum.GetTypedChoices().Where(r => r.Name == s!.ToString()).FirstOrDefault();
        }

        public override void Write(Utf8JsonWriter writer, Renderer value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}

﻿using Beutl.Graphics;
using Beutl.Media;
using Beutl.Media.Pixel;

namespace minaLogi.RenderPipeLine
{
    public abstract class Renderer
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public int Width { get; set; }
        public int Height { get; set; }

        public override string ToString() => this.Name;

        public virtual void Load() { }

        public virtual void OnSelected() { }

        public virtual void Close() { }

        public virtual Bitmap<Bgra8888>? Render(Drawables drawables)
        {
            // このメソッドをoverrideしてBitmap<Bgra8888>を返すコードを書くと描画に反映されます
            return null;
        }
    }
}

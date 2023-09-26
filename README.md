# minaLogi.RenderPipeLine
[Beutl](https://github.com/b-editor/beutl/)用のレンダーパイプラインです。この拡張機能のほかに、レンダラの拡張機能を追加することでそのレンダラを使うことができるようになります。

# 使い方
![image](https://github.com/minaLogi/minaLogi.RenderPipeLine/assets/88201103/ce6a23bb-b7ad-4b7a-9855-581cb9474d4e)
エディターの「ソース操作」タブ(画面右側)にRender pipelineを追加することで使用することができます。作者がまだレンダラを作ってないため、スクリーンショットでは緑画面を表示するTestRendererが表示されています。

## プロパティ
| 名前 | 機能 |
| --- | --- |
| Width | レンダラの描画キャンバスの横幅 |
| Height | レンダラの描画キャンバスの高さ |
| RendererName | レンダラの名前をテキストで入力して使うものを選択する |

Rendererをプルダウンリストで選択できるようになりました!

## レンダラを書く
レンダラは、第三者がBeutlの拡張機能として追加することができます。

```C#
using Beutl.Graphics;
using Beutl.Media;
using Beutl.Media.Pixel;
using minaLogi.RenderPipeline;

public class TestRenderer : Renderer
{
    // レンダラの名前
    public override string Name => "TestRenderer";
    // レンダラの説明
    public override string Description => "Test";

    public override Bitmap<Bgra8888>? Render(Drawables drawables)
    {
        // ここに描画時の処理を書きます。
        // ビットマップを作成
        var bitmap = new Bitmap<Bgra8888>(Width, Height);
        foreach (var drawable in drawables)
        {
            // 描画するオブジェクトごとの処理が書けます
        }
        return bitmap;
    }
}
```

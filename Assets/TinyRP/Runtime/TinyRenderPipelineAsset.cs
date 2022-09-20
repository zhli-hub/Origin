using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/Tiny Render Pipeline Asset")]
public class TinyRenderPipelineAsset : RenderPipelineAsset {
    //public Color clearColor;
    protected override RenderPipeline CreatePipeline() {
        return null;
    }
}



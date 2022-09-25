using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/TinyRenderPipelineAsset")]
public class TinyRenderPipelineAsset : RenderPipelineAsset {
    //public Color clearColor;
    protected override RenderPipeline CreatePipeline() {
        return new TinyRenderPipeline();
    }
}



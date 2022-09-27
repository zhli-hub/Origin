using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/TinyRenderPipelineAsset")]
public class TinyRenderPipelineAsset : RenderPipelineAsset {
    //public Color clearColor;
    [SerializeField]
    bool _useDynamicBatching = true, _useGPUInstancing = true, _useSRPBatcher = true;
    protected override RenderPipeline CreatePipeline() {
        return new TinyRenderPipeline(_useDynamicBatching, _useGPUInstancing, _useSRPBatcher);
    }
}



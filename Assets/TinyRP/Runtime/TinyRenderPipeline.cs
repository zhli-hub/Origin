using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class TinyRenderPipeline : RenderPipeline
{
    private CameraRenderer _renderer = new CameraRenderer();
    bool _useDynamicBatching, _useGPUInstancing;
    public TinyRenderPipeline(bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher)
    {
        _useDynamicBatching = useDynamicBatching;
        _useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
    }
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            _renderer.Render(context, camera, _useDynamicBatching, _useGPUInstancing);
        }
    }
}

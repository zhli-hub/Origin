using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class TinyRenderPipeline : RenderPipeline
{
    private CameraRenderer _mRenderer = new CameraRenderer();
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            _mRenderer.Render(context, camera);
        }
    }
}

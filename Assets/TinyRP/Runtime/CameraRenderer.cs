using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
    ScriptableRenderContext _context;
    Camera _camera;
    private const string MCommandBufferName = "Render Camera";
    private readonly CommandBuffer _commandBuffer = new CommandBuffer { name = MCommandBufferName };
    private CullingResults _mCullingResults;
    private static readonly ShaderTagId MUnlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
    public void Render (ScriptableRenderContext context, Camera camera, bool useDynamicBatching, bool useGPUInstancing) {
        _context = context;
        _camera = camera;
        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull())
        {
            return;
        }
 
        Setup();
        DrawVisibleGeometry(useDynamicBatching, useGPUInstancing);
        DrawUnsupportedShaders();
        DrawGizmos();
        Submit();
    }
    void DrawVisibleGeometry(bool useDynamicBatching, bool useGPUInstancing)
    {
        var sortingSettings = new SortingSettings(_camera)
        {
            criteria = SortingCriteria.CommonOpaque
        };
        var drawingSettings = new DrawingSettings(MUnlitShaderTagId, sortingSettings)
        {
            enableDynamicBatching = useDynamicBatching,
            enableInstancing = useGPUInstancing
        };
        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        _context.DrawRenderers(
            _mCullingResults, ref drawingSettings, ref filteringSettings
        );
        _context.DrawSkybox(_camera);

        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
        _context.DrawRenderers(_mCullingResults, ref drawingSettings, ref filteringSettings);
    }
    
    

    void Submit()
    {
        _commandBuffer.EndSample(SampleName);
        ExecuteBuffer();
        _context.Submit();
    }

    void Setup()
    {
        _context.SetupCameraProperties(_camera);
        CameraClearFlags flags = _camera.clearFlags;
        _commandBuffer.ClearRenderTarget(flags <= CameraClearFlags.Depth, flags == CameraClearFlags.Color, Color.clear);
        _commandBuffer.BeginSample(SampleName);
        
        ExecuteBuffer();
        
    }

    void ExecuteBuffer()
    {
        _context.ExecuteCommandBuffer(_commandBuffer);
        _commandBuffer.Clear();
    }

    bool Cull()
    {
        if (_camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            _mCullingResults = _context.Cull(ref p);
            return true;
        }
        return false;
    }
}

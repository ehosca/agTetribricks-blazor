using Microsoft.JSInterop;

namespace TetriBricks.Client.Services;

public record ViewportInfo(int Width, int Height, bool IsTouchDevice);

public class ViewportService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private DotNetObjectReference<ViewportService>? _dotNetRef;

    public ViewportInfo Current { get; private set; } = new(1024, 768, false);
    public event Action<ViewportInfo>? ViewportChanged;

    public ViewportService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        Current = await _jsRuntime.InvokeAsync<ViewportInfo>("viewport.getViewportInfo");

        _dotNetRef = DotNetObjectReference.Create(this);
        await _jsRuntime.InvokeVoidAsync("viewport.registerResizeListener", _dotNetRef);
    }

    [JSInvokable]
    public void OnViewportChanged(ViewportInfo info)
    {
        Current = info;
        ViewportChanged?.Invoke(info);
    }

    public async ValueTask DisposeAsync()
    {
        await _jsRuntime.InvokeVoidAsync("viewport.unregisterResizeListener");
        _dotNetRef?.Dispose();
    }
}

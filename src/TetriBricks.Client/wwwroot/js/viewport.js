window.viewport = {
    _resizeHandler: null,

    getViewportInfo: function () {
        return {
            width: window.innerWidth,
            height: window.innerHeight,
            isTouchDevice: ('ontouchstart' in window) || (navigator.maxTouchPoints > 0)
        };
    },

    registerResizeListener: function (dotNetRef) {
        let timeout;
        window.viewport._resizeHandler = function () {
            clearTimeout(timeout);
            timeout = setTimeout(function () {
                const info = window.viewport.getViewportInfo();
                dotNetRef.invokeMethodAsync('OnViewportChanged', info);
            }, 150);
        };
        window.addEventListener('resize', window.viewport._resizeHandler);
        window.addEventListener('orientationchange', window.viewport._resizeHandler);
    },

    unregisterResizeListener: function () {
        if (window.viewport._resizeHandler) {
            window.removeEventListener('resize', window.viewport._resizeHandler);
            window.removeEventListener('orientationchange', window.viewport._resizeHandler);
            window.viewport._resizeHandler = null;
        }
    }
};

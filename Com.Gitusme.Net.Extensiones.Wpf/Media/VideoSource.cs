/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
 *********************************************************/

using Com.Gitusme.Media.Video;
using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Com.Gitusme.Net.Extensiones.Wpf.Media
{
    /// <summary>
    /// 视频渲染组件，支持WFP和WinForm上播放YUV/RGB视频。
    /// <example>
    /// <para>
    /// Step1.创建VideoSource组件；
    /// <list type="number">
    /// <item>
    ///    布局文件中添加如下代码
    ///    <code>
    ///    <![CDATA[
    ///    <Image Stretch = "Uniform">
    ///         <Image.Source>
    ///             <local:VideoSource x:Name="source" />
    ///         </Image.Source>
    ///       </Image>
    ///       ]]>
    ///    </code>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// Step2.子线程中刷新视频帧；
    /// <list type="number">
    /// <item>
    ///    YUV 格式（默认）
    ///    <code>
    ///       source.Frame = data;
    ///       source.Frame.Width = 640;
    ///       source.Frame.Height = 480;
    ///    </code>
    /// </item>
    /// <item>
    ///    RGB 格式
    ///    <code>
    ///       source.Frame = data;
    ///       source.Frame.Format = VideoFormat.RGB;
    ///       source.Frame.Width = 1080;
    ///       source.Frame.Height = 720;
    ///    </code>
    /// </item>
    /// </list>
    /// </para>
    /// </example>
    /// </summary>
    public sealed class VideoSource : _
    {
        private VideoRender _render;

        private TimeSpan _lastRender;

        private bool _isRendering = false;

        private int _delay = 0;

        public VideoFrame? Frame { get; set; }

        public VideoSource()
            : this(System.IntPtr.Zero)
        {
        }

        public VideoSource(System.IntPtr hwnd)
        {
            _render = new VideoRender(hwnd);

            IsFrontBufferAvailableChanged += FrontBufferAvailableChanged;
        }

        ~VideoSource()
        {
            _render.Dispose();
        }

        private void FrontBufferAvailableChanged(
            object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsFrontBufferAvailable)
            {
                _delay = 120;
            }
            else
            {
                _delay = 0;
            }
        }

        private void Rendering(object sender, EventArgs e)
        {
            if (!_isRendering)
            {
                return;
            }

            if (Frame == null || Frame.Data == null)
            {
                return;
            }

            RenderingEventArgs args = (RenderingEventArgs)e;

            HRESULT.Check(_render.SetSize(Frame.Width, Frame.Height));
            HRESULT.Check(_render.SetAlpha(false));

            IntPtr pSurface = IntPtr.Zero;
            if (_delay > 0)
            {
                _delay--;
            }
            else
            {
                HRESULT.Check(_render.GetBackBufferNoRef(
                    Frame.Format, out pSurface));
            }

            if (pSurface != IntPtr.Zero)
            {
                if (_lastRender != args.RenderingTime)
                {
                    if (IsFrontBufferAvailable)
                    {
                        Lock();
                        SetBackBuffer(D3DResourceType.IDirect3DSurface9, pSurface);
                        HRESULT.Check(_render.Render(Frame));
                        AddDirtyRect(new Int32Rect(0, 0, PixelWidth, PixelHeight));
                        Unlock();
                    }
                    _lastRender = args.RenderingTime;
                }
            }
        }

        public void StartRender()
        {
            if (_isRendering)
            {
                return;
            }

            CompositionTarget.Rendering += new EventHandler(Rendering);
            _isRendering = true;
        }

        public void StopRender()
        {
            if (!_isRendering)
            {
                return;
            }

            CompositionTarget.Rendering -= new EventHandler(Rendering);
            _isRendering = false;
            Dispatcher.Invoke(() =>
            {
                Lock();
                SetBackBuffer(0, IntPtr.Zero);
                Unlock();
            });
        }
    }

    public static class HRESULT
    {
        [SecurityPermission(
            SecurityAction.Demand,
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void Check(int hr)
        {
            Marshal.ThrowExceptionForHR(hr);
        }
    }
}
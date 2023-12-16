/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Com.Gitusme.Net.Extensiones.Wpf.Media
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class VideoContainer : UserControl
    {
        /// <summary>
        /// 内容布局方式
        /// </summary>
        public enum ContentLayout
        {
            /// <summary>
            /// 表格
            /// </summary>
            Grid,
            /// <summary>
            /// 画廊
            /// </summary>
            Gallery
        }

        public ContentLayout Layout
        {
            get
            {
                return (ContentLayout)GetValue(LayoutProperty);
            }
            set
            {
                SetValue(LayoutProperty, value);
            }
        }

        public static readonly DependencyProperty LayoutProperty =
            DependencyProperty.Register("Layout", typeof(ContentLayout), typeof(VideoContainer),
                new PropertyMetadata(ContentLayout.Grid, (s, e) =>
                {
                    (s as VideoContainer).Layout = (ContentLayout)e.NewValue;
                }));

        public List<VideoSource> Items
        {
            get
            {
                return GetValue(ItemsProperty) as List<VideoSource>;
            }
            set
            {
                SetValue(ItemsProperty, value);
                InitLayout();
            }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<VideoSource>), typeof(VideoContainer),
                new PropertyMetadata(new List<VideoSource>(), (s, e) =>
                {
                    (s as VideoContainer).Items = e.NewValue as List<VideoSource>;
                }));

        #region Grid模式下的属性设置

        public int Columns
        {
            get
            {
                return (int)GetValue(ColumnsProperty);
            }
            set
            {
                SetValue(ColumnsProperty, value);
            }
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(VideoContainer),
                new PropertyMetadata(0, (s, e) =>
                {
                    (s as VideoContainer).Columns = (int)e.NewValue;
                }));

        public int Rows
        {
            get
            {
                return (int)GetValue(RowsProperty);
            }
            set
            {
                SetValue(RowsProperty, value);
            }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(VideoContainer),
                new PropertyMetadata(0, (s, e) =>
                {
                    (s as VideoContainer).Rows = (int)e.NewValue;
                }));

        #endregion

        #region Gallery模式下的属性设置

        public enum GalleryDock
        {
            Top,
            Bottom,
            Left,
            Right,
        }

        public GalleryDock Dock
        {
            get
            {
                return (GalleryDock)GetValue(DockProperty);
            }
            set
            {
                SetValue(DockProperty, value);
            }
        }

        public static readonly DependencyProperty DockProperty =
            DependencyProperty.Register("Dock", typeof(GalleryDock), typeof(VideoContainer),
                new PropertyMetadata(GalleryDock.Top, (s, e) =>
                {
                    (s as VideoContainer).Dock = (GalleryDock)e.NewValue;
                }));


        public int GallerySize
        {
            get
            {
                return (int)GetValue(GallerySizeProperty);
            }
            set
            {
                SetValue(GallerySizeProperty, value);
            }
        }

        public static readonly DependencyProperty GallerySizeProperty =
            DependencyProperty.Register("GallerySize", typeof(int), typeof(VideoContainer),
                new PropertyMetadata(150, (s, e) =>
                {
                    (s as VideoContainer).GallerySize = (int)e.NewValue;
                }));

        #endregion

        public VideoContainer()
        {
            InitializeComponent();
        }

        private void InitLayout()
        {
            StopVideo();

            topGalleryContainer.Content = null;
            bottomGalleryContainer.Content = null;
            leftGalleryContainer.Content = null;
            rightGalleryContainer.Content = null;
            gridContainer.Children.Clear();

            if(Layout == ContentLayout.Grid)
            {
                column1.Width = new GridLength(0, GridUnitType.Pixel);
                column3.Width = new GridLength(0, GridUnitType.Pixel);
                row1.Height = new GridLength(0, GridUnitType.Pixel);
                row3.Height = new GridLength(0, GridUnitType.Pixel);

                var grid = CreateUniformGrid(Items, Columns, Rows);

                gridContainer.Children.Add(grid);
            }

            if (Layout == ContentLayout.Gallery)
            {
                ScrollViewer scrollViewer = topGalleryContainer;
                int rows = 0;
                int columns = 0;
                if (Dock == GalleryDock.Top)
                {
                    column1.Width = new GridLength(0, GridUnitType.Pixel);
                    column3.Width = new GridLength(0, GridUnitType.Pixel);
                    row1.Height = new GridLength(GallerySize, GridUnitType.Pixel);
                    row3.Height = new GridLength(0, GridUnitType.Pixel);
                    scrollViewer = topGalleryContainer;
                    rows = 1;
                    columns = Items.Count;
                }
                if (Dock == GalleryDock.Bottom)
                {
                    column1.Width = new GridLength(0, GridUnitType.Pixel);
                    column3.Width = new GridLength(0, GridUnitType.Pixel);
                    row1.Height = new GridLength(0, GridUnitType.Pixel);
                    row3.Height = new GridLength(GallerySize, GridUnitType.Pixel);
                    scrollViewer = bottomGalleryContainer;
                    rows = 1;
                    columns = Items.Count;
                }
                if (Dock == GalleryDock.Left)
                {
                    column1.Width = new GridLength(GallerySize, GridUnitType.Pixel);
                    column3.Width = new GridLength(0, GridUnitType.Pixel);
                    row1.Height = new GridLength(0, GridUnitType.Pixel);
                    row3.Height = new GridLength(0, GridUnitType.Pixel);
                    scrollViewer = leftGalleryContainer;
                    rows = Items.Count;
                    columns = 1;
                }
                if (Dock == GalleryDock.Right)
                {
                    column1.Width = new GridLength(0, GridUnitType.Pixel);
                    column3.Width = new GridLength(GallerySize, GridUnitType.Pixel);
                    row1.Height = new GridLength(0, GridUnitType.Pixel);
                    row3.Height = new GridLength(0, GridUnitType.Pixel);
                    scrollViewer = rightGalleryContainer;
                    rows = Items.Count;
                    columns = 1;
                }
                scrollViewer.Content = CreateUniformGrid(Items, columns, rows);
                if(Items.Count > 0)
                {
                    Image image = new Image();
                    image.Source = Items[0];
                    image.Stretch = Stretch.Uniform;
                    gridContainer.Children.Add(image);
                }
            }

            StartVideo();
        }

        private Panel CreateUniformGrid(
            List<VideoSource> videoSources, int columns, int rows)
        {
            List<Image> images = CreateImages(videoSources);

            if (Layout == ContentLayout.Grid)
            {
                UniformGrid grid = new UniformGrid();
                grid.Columns = columns;
                grid.Rows = rows;
                grid.HorizontalAlignment = HorizontalAlignment.Center;
                grid.VerticalAlignment = VerticalAlignment.Center;

                images.ForEach((it) => grid.Children.Add(it));
                return grid;
            }

            if (Layout == ContentLayout.Gallery)
            {
                StackPanel stackPanel = new StackPanel();
                if (Dock == GalleryDock.Top || Dock == GalleryDock.Bottom)
                {
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                }
                if (Dock == GalleryDock.Left || Dock == GalleryDock.Right)
                {
                    stackPanel.Orientation = Orientation.Vertical;
                    stackPanel.VerticalAlignment = VerticalAlignment.Center;
                }
                images.ForEach((it) => stackPanel.Children.Add(it));
                return stackPanel;
            }

            return new Grid();
        }

        private List<Image> CreateImages(List<VideoSource> videoSources)
        {
            return videoSources.ConvertAll(new Converter<VideoSource, Image>((it) =>
            {
                Image image = new Image();
                if (Layout == ContentLayout.Gallery)
                {
                    if (Dock == GalleryDock.Top || Dock == GalleryDock.Bottom)
                    {
                        image.Height = GallerySize;
                    }
                    if (Dock == GalleryDock.Left || Dock == GalleryDock.Right)
                    {
                        image.Width = GallerySize;
                    }
                    image.Stretch = Stretch.UniformToFill;
                }
                image.Source = it;
                image.Stretch = Stretch.Uniform;
                return image;
            }));
        }

        public void StartVideo()
        {
            Items?.ForEach((it) => it?.StartRender());
        }

        public void StartVideo(int index)
        {
            this[index]?.StartRender();
        }

        public void StopVideo()
        {
            Items?.ForEach((it) => it?.StopRender());
        }

        public void StopVideo(int index)
        {
            this[index]?.StopRender();
        }

        private VideoSource? this[int index]
        {
            get
            {
                return (Items?.Count > index) ? Items[index] : null;
            }
        }

        private double _pressdX;
        private double _pressdY;

        private void galleryContainer_PreviewMouseLeftButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(topGalleryContainer);
            _pressdX = point.X;
            _pressdY = point.Y;
        }

        private double _offset = 0;

        private void galleryContainer_HorizontalScroll(
            object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ScrollViewer scrollViewer = (ScrollViewer)sender;

                double releasedX = e.GetPosition(scrollViewer).X;
                double offset = _pressdX - releasedX;
                if (offset > 0)
                {
                    if (_offset + scrollViewer.ViewportWidth < scrollViewer.ExtentWidth)
                    {
                        _offset += offset;
                        scrollViewer.ScrollToHorizontalOffset(_offset);
                        _pressdX = releasedX;
                    }
                }
                if (offset < 0)
                {
                    if (scrollViewer.HorizontalOffset > 0)
                    {
                        _offset += offset;
                        scrollViewer.ScrollToHorizontalOffset(_offset);
                        _pressdX = releasedX;
                    }
                }
            }
        }

        private void galleryContainer_VerticalScroll(
            object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ScrollViewer scrollViewer = (ScrollViewer)sender;

                double releasedY = e.GetPosition(scrollViewer).Y;
                double offset = _pressdY - releasedY;
                if (offset > 0)
                {
                    if (_offset + scrollViewer.ViewportHeight < scrollViewer.ExtentHeight)
                    {
                        _offset += offset;
                        scrollViewer.ScrollToVerticalOffset(_offset);
                        _pressdY = releasedY;
                    }
                }
                if (offset < 0)
                {
                    if (scrollViewer.VerticalOffset > 0)
                    {
                        _offset += offset;
                        scrollViewer.ScrollToVerticalOffset(_offset);
                        _pressdY = releasedY;
                    }
                }
            }
        }
    }
}

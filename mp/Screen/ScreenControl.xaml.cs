using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mp.Screen
{
    /// <summary>
    /// ScreenControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ScreenControl : UserControl
    {
        public ScreenControl()
        {
            InitializeComponent();

            DataContextChanged += (_1, _2) =>
            {
                (DataContext as ScreenControlViewModel)?.SetSize(RenderSize);

#if DEBUG
                var testMoviePath = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                    "..", "..", "testData",
                    "ibutama.wmv");
                PlayMovie(testMoviePath);
#endif
            };

            AllowDrop = true;

            PreviewDragOver += (_, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
                e.Handled = true;
            };

            Drop += (_, e) =>
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                PlayMovie(files.FirstOrDefault());
            };

            Focusable = true;
            MouseDown += (_1, _2) => Focus();

            PreviewKeyDown += (_, e) =>
            {
                (DataContext as ScreenControlViewModel)?.ReceiveKeyEvent(e);
                e.Handled = true;
            };
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            UpdateSize(sizeInfo.NewSize);
            base.OnRenderSizeChanged(sizeInfo);
        }

        private void PlayMovie(string filepath)
        {
            var dataContext = (DataContext as ScreenControlViewModel);
            if (dataContext != null && dataContext.SetFile(filepath))
            {
                UpdateSize(RenderSize);
            }
        }

        private void UpdateSize(Size newSize)
        {
            (DataContext as ScreenControlViewModel)?.SetSize(newSize);
        }
    }
}

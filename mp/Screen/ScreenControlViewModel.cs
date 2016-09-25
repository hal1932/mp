using HlLib.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace mp.Screen
{
    class ScreenControlViewModel : ViewModelBase
    {
        private MediaPlayer _mediaPlayer;
        public MediaPlayer MediaPlayer
        {
            get { return _mediaPlayer; }
            set { SetProperty(ref _mediaPlayer, value); }
        }

        private Rect _drawingSize;
        public Rect DrawingSize
        {
            get { return _drawingSize; }
            set { SetProperty(ref _drawingSize, value); }
        }

        public bool SetFile(string path)
        {
            if (!File.Exists(path))
            {
                ClearState();
                return false;
            }

            var mediaPlayer = new MediaPlayer();
            mediaPlayer.ScrubbingEnabled = true;
            mediaPlayer.Open(new Uri(path, UriKind.Absolute));

            while (mediaPlayer.DownloadProgress < 1.0 || mediaPlayer.NaturalVideoWidth == 0)
            {
                Thread.Sleep(10);
            }
            while (mediaPlayer.IsBuffering || mediaPlayer.IsFrozen)
            {
                Thread.Sleep(10);
            }

            MediaPlayer?.Close();
            MediaPlayer = mediaPlayer;

            if(_currentMediaState == MediaState.Play)
            {
                MediaPlayer.Play();
            }
            else
            {
                MediaPlayer.Stop();
                _currentMediaState = MediaState.Stop;
            }

            DrawingSize = new Rect(0, 0, mediaPlayer.NaturalVideoWidth, mediaPlayer.NaturalVideoHeight);

            return true;
        }

        public void SetSize(Size newSize)
        {
            // DrawingSizeとnewSizeとの縦横の比率を求めて、小さい方でDrawingSizeをスケーリングする
            var horizontal = newSize.Width / DrawingSize.Width;
            var vertial = newSize.Height / DrawingSize.Height;
            var ratio = Math.Min(horizontal, vertial);

            var newWidth = DrawingSize.Width * ratio;
            var newHeight = DrawingSize.Height * ratio;
            DrawingSize = new Rect(0, 0, newWidth, newHeight);
        }

        public void ReceiveKeyEvent(KeyEventArgs e)
        {
            KeyMediaController.ApplyControl(ref _currentMediaState, MediaPlayer, e);
        }

        private void ClearState()
        {
            MediaPlayer = null;
            DrawingSize = default(Rect);
            _currentMediaState = default(MediaState);
        }

        private MediaState _currentMediaState;
    }
}

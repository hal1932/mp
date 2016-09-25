using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace mp.Screen
{
    class KeyMediaController
    {
        public static void ApplyControl(ref MediaState state, MediaPlayer player, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                case Key.Space:
                    if (state == MediaState.Play)
                    {
                        player.Pause();
                        state = MediaState.Pause;
                    }
                    else
                    {
                        player.Play();
                        state = MediaState.Play;
                    }
                    break;

                case Key.F5:
                    player.Position = TimeSpan.FromSeconds(0);
                    break;

                default:
                    break;
            }
        }
    }
}

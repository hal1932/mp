using HlLib.Mvvm;
using mp.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp
{
    class MainWindowViewModel : ViewModelBase
    {
        public ScreenControlViewModel ScreenContext { get; set; } = new ScreenControlViewModel();
    }
}

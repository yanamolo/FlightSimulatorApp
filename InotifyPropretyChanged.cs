using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstproject
{
    public delegate void propretyChangedEventHandler(object sender, EventArgs e);
    interface InotifyPropretyChanged
    {
        event propretyChangedEventHandler PropretyChanged;
        void NotifyPropretyChanged(string propName);
    }
}

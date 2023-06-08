using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class SlikePoTipu : BindableBase
    {
        public string TipSlike { get; set; }
        public BindingList<Tip> Slike { get; set; }

        public SlikePoTipu()
        {
            Slike = new BindingList<Tip>();
        }
    }
}

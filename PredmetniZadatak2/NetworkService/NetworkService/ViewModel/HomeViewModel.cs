using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        private string title;
        private string subtitle;

        public string Title
        {
            get { return title; }
            set
            {
                if(title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Subtitle
        {
            get { return subtitle; }
            set
            {
                if (subtitle != value)
                {
                    subtitle = value;
                    OnPropertyChanged("Subtitle");
                }
            }
        }

        public HomeViewModel()
        {
            Title = "Network Service";
            Subtitle = "Predmetni zadatak 2";
        }
    }
}

using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class EntityType : ValidationBase
    {
        private string name;
        private string imgSource = "pack://application:,,,/NetworkService;component/Assets/no-image.jpg";

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    ImgSource = Data.ComboBoxItemsData.entityTypes[value];
                    OnPropertyChanged("Name");
                }
            }
        }

        public string ImgSource
        {
            get { return imgSource; }
            set
            {
                if (imgSource != value)
                {
                    imgSource = value;
                    OnPropertyChanged("ImgSource");
                }
            }
        }

        protected override void ValidateSelf()
        {
            if (Name == null)
            {
                ValidationErrors["Name"] = "Type must be selected.";
            }
        }
    }
}

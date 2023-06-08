using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Entity : ValidationBase
    {
        private string textId;

        private int id;
        private string name;
        private EntityType type;
        private double value;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string TextId
        {
            get { return textId; }
            set
            {
                if (textId != value)
                {
                    textId = value;
                    OnPropertyChanged("TextId");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public EntityType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    Type.Name = value.Name;
                    Type.ImgSource = value.ImgSource;
                    OnPropertyChanged("Type");
                }
            }
        }

        public double Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    base.OnPropertyChanged("Value");
                }
            }
        }

        public bool IsValueValidForType()
        {
            bool isValid = true;

            switch (Type.Name)
            {
                case "IA":
                    if (Value > 15000)
                    {
                        isValid = false;
                    }
                    break;
                case "IB":
                    if (Value > 7000)
                    {
                        isValid = false;
                    }
                    break;
            }
            return isValid;
        }

        protected override void ValidateSelf()
        {
            bool parsingSuccess = int.TryParse(textId, out int tempId);

            if (DoesIdAlreadyExist)
            {
                ValidationErrors["Id"] = "Id already exists.";
            }

            if (!parsingSuccess)
            {
                ValidationErrors["Id"] = "Id must be an integer.";
            }
            else if (tempId < 0)
            {
                ValidationErrors["Id"] = "Id can't be negative.";
            }

            if (string.IsNullOrWhiteSpace(textId))
            {
                ValidationErrors["Id"] = "Id is required.";
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                ValidationErrors["Name"] = "Name is required.";
            }
        }
    }
}

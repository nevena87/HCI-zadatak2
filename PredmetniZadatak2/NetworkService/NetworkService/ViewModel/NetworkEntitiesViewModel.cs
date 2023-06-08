using NetworkService.Helpers;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesViewModel : BindableBase
    {
        public List<string> ComboBoxItems { get; set; } = Data.ComboBoxItemsData.entityTypes.Keys.ToList();

        public ObservableCollection<Entity> EntitiesToShow { get; set; }
        public ObservableCollection<Entity> Entities { get; set; }
        public ObservableCollection<Entity> SearchedEntities { get; set; }
        public ObservableCollection<Entity> BackUpEntities { get; set; }

        public MyICommand AddEntityCommand { get; set; }
        public MyICommand DeleteEntityCommand { get; set; }
        public MyICommand SearchEntityCommand { get; set; }
        public MyICommand ResetSearchCommand { get; set; }
        public MyICommand UndoCommand { get; set; }

        // Unos novog entiteta
        private Entity currentEntity = new Entity();
        private EntityType currentEntityType = new EntityType();

        // Entitet selektovan u datagridu
        private Entity selectedEntity;

        // Pretrazivanje
        private bool isNameRadioButtonSelected;
        private bool isTypeRadioButtonSelected;
        private string selectedNameOrTypeToSearchText;
        private bool isOutOfRangeValuesRadioButtonSelected;
        private bool isExpectedValuesRadioButtonSelected;
        private string searchErrorMessage;

        // Prikaz broja entiteta po tipu
        private int iaRectangleWidth;
        private int iaCount = 0;
        private string iaPercentage;
        private int ibRectangleWidth;
        private int ibCount = 0;
        private string ibPercentage;

        public NetworkEntitiesViewModel()
        {
            Entities = new ObservableCollection<Entity>();
            EntitiesToShow = Entities;

            LoadDefaultValuesForDisplay();

            AddEntityCommand = new MyICommand(OnAdd);
            DeleteEntityCommand = new MyICommand(OnDelete, CanDelete);
            SearchEntityCommand = new MyICommand(OnSearch);
            ResetSearchCommand = new MyICommand(OnResetSearch);
        }

        private void LoadDefaultValuesForDisplay()
        {
            IARectangleWidth = 200;
            IBRectangleWidth = 200;

            IAPercentage = "50% (0)";
            IBPercentage = "50% (0)";
        }

        public Entity SelectedEntity
        {
            get { return selectedEntity; }
            set
            {
                selectedEntity = value;
                DeleteEntityCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsNameRadioButtonSelected
        {
            get { return isNameRadioButtonSelected; }
            set
            {
                if (isNameRadioButtonSelected != value)
                {
                    isNameRadioButtonSelected = value;
                    OnPropertyChanged("IsNameRadioButtonSelected");

                }
            }
        }

        public bool IsTypeRadioButtonSelected
        {
            get { return isTypeRadioButtonSelected; }
            set
            {
                if (isTypeRadioButtonSelected != value)
                {
                    isTypeRadioButtonSelected = value;
                    OnPropertyChanged("IsTypeRadioButtonSelected");
                }
            }
        }

        public string SelectedNameOrTypeToSearchText
        {
            get { return selectedNameOrTypeToSearchText; }
            set
            {
                selectedNameOrTypeToSearchText = value;
                OnPropertyChanged("SelectedNameOrTypeToSearchText");
            }
        }

        public bool IsOutOfRangeValuesRadioButtonSelected
        {
            get { return isOutOfRangeValuesRadioButtonSelected; }
            set
            {
                isOutOfRangeValuesRadioButtonSelected = value;
                OnPropertyChanged("IsOutOfRangeValuesRadioButtonSelected");
            }
        }

        public bool IsExpectedValuesRadioButtonSelected
        {
            get { return isExpectedValuesRadioButtonSelected; }
            set
            {
                isExpectedValuesRadioButtonSelected = value;
                OnPropertyChanged("IsExpectedValuesRadioButtonSelected");
            }
        }

        public string SearchErrorMessage
        {
            get { return searchErrorMessage; }
            set
            {
                searchErrorMessage = value;
                OnPropertyChanged("SearchErrorMessage");
            }
        }

        private void OnSearch()
        {
            SearchErrorMessage = string.Empty;

            if (!IsNameRadioButtonSelected && !IsTypeRadioButtonSelected &&
                string.IsNullOrWhiteSpace(SelectedNameOrTypeToSearchText) &&
                !IsOutOfRangeValuesRadioButtonSelected &&
                !IsExpectedValuesRadioButtonSelected)
            {
                SearchErrorMessage = "Fields can't be empty.";
                return;
            }

            List<Entity> searchedList = new List<Entity>();

            foreach (Entity entity in Entities)
            {
                searchedList.Add(entity);
            }

            if (IsNameRadioButtonSelected)
            {
                List<Entity> entitiesToDelete = new List<Entity>();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (!searchedList[i].Name.ToLower().Contains(SelectedNameOrTypeToSearchText.ToLower()))
                    {
                        entitiesToDelete.Add(searchedList[i]);
                    }
                }

                foreach (Entity entity in entitiesToDelete)
                {
                    searchedList.Remove(entity);
                }
            }


            if (IsTypeRadioButtonSelected)
            {
                List<Entity> entitiesToDelete = new List<Entity>();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (!searchedList[i].Type.Name.ToLower().Contains(SelectedNameOrTypeToSearchText.ToLower()))
                    {
                        entitiesToDelete.Add(searchedList[i]);
                    }
                }

                foreach (Entity entity in entitiesToDelete)
                {
                    searchedList.Remove(entity);
                }
            }

            if (IsOutOfRangeValuesRadioButtonSelected)
            {
                List<Entity> entitiesToDelete = new List<Entity>();

                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].IsValueValidForType())
                    {
                        entitiesToDelete.Add(searchedList[i]);
                    }
                }

                foreach (Entity entity in entitiesToDelete)
                {
                    searchedList.Remove(entity);
                }
            }
            else if (IsExpectedValuesRadioButtonSelected)
            {
                List<Entity> entitiesToDelete = new List<Entity>();

                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (!searchedList[i].IsValueValidForType())
                    {
                        entitiesToDelete.Add(searchedList[i]);
                    }
                }

                foreach (Entity entity in entitiesToDelete)
                {
                    searchedList.Remove(entity);
                }
            }

            if (searchedList.Count > 0)
            {
                SearchedEntities = new ObservableCollection<Entity>();

                foreach (Entity entity in searchedList)
                {
                    SearchedEntities.Add(entity);
                }

                EntitiesToShow = SearchedEntities;
                OnPropertyChanged("EntitiesToShow");
            }
            else
            {
                SearchErrorMessage = "No entities to show.";
                EntitiesToShow = Entities;
                OnPropertyChanged("EntitiesToShow");
            }
        }

        private void OnResetSearch()
        {
            IsNameRadioButtonSelected = false;
            IsTypeRadioButtonSelected = false;
            SelectedNameOrTypeToSearchText = String.Empty;
            IsOutOfRangeValuesRadioButtonSelected = false;
            IsExpectedValuesRadioButtonSelected = false;
            SearchErrorMessage = string.Empty;

            EntitiesToShow = Entities;
            SearchedEntities = new ObservableCollection<Entity>();
            OnPropertyChanged("EntitiesToShow");
        }

        private bool CanDelete()
        {
            return SelectedEntity != null;
        }

        private void OnDelete()
        {
            switch (SelectedEntity.Type.Name)
            {
                case "IA":
                    iaCount--;
                    break;
                case "IB":
                    ibCount--;
                    break;
            }

            Entities.Remove(SelectedEntity);

            OnDataGridUpdate();
        }

        public Entity CurrentEntity
        {
            get { return currentEntity; }
            set
            {
                currentEntity = value;
                OnPropertyChanged("CurrentEntity");
            }
        }

        public EntityType CurrentEntityType
        {
            get { return currentEntityType; }
            set
            {
                currentEntityType = value;
                OnPropertyChanged("CurrentEntityType");
            }
        }

        private void OnDataGridUpdate()
        {
            if (Entities.Count > 0)
            {
                int tempIaPercentage = iaCount * 100 / (iaCount + ibCount);
                int tempIbPercentage = ibCount * 100 / (iaCount + ibCount);

                IAPercentage = $"{tempIaPercentage}% ({iaCount})";
                IBPercentage = $"{tempIbPercentage}% ({ibCount})";

                if (tempIaPercentage == 100)
                {
                    IARectangleWidth = 400 * tempIaPercentage / 100;
                    IBRectangleWidth = 400 - IARectangleWidth;
                    IBPercentage = "";
                }
                else if (tempIbPercentage == 100)
                {
                    IARectangleWidth = 400 * tempIaPercentage / 100;
                    IBRectangleWidth = 400 - IARectangleWidth;
                    IAPercentage = "";
                }
                else
                {
                    IARectangleWidth = 400 * tempIaPercentage / 100;
                    IBRectangleWidth = 400 - IARectangleWidth;
                }
            }
            else
            {
                LoadDefaultValuesForDisplay();
            }
        }

        public void OnAdd()
        {
            try
            {
                int parsedId;
                bool canParse = int.TryParse(CurrentEntity.TextId, out parsedId);
                bool idAlreadyExists = false;

                if (canParse)
                {
                    foreach (Entity entity in Entities)
                    {
                        if (entity.Id == parsedId)
                        {
                            idAlreadyExists = true;
                            break;
                        }
                    }
                }

                CurrentEntity.DoesIdAlreadyExist = idAlreadyExists;

                CurrentEntity.Validate();
                CurrentEntityType.Validate();

                if (CurrentEntity.IsValid)
                {
                    Entities.Add(new Entity()
                    {
                        Id = int.Parse(CurrentEntity.TextId),
                        Name = CurrentEntity.Name,
                        Type = new EntityType
                        {
                            Name = CurrentEntityType.Name,
                            ImgSource = CurrentEntityType.ImgSource
                        },
                        Value = 0
                    });

                    switch (CurrentEntityType.Name)
                    {
                        case "IA":
                            iaCount++;
                            break;
                        case "IB":
                            ibCount++;
                            break;
                    }

                    OnDataGridUpdate();

                    CurrentEntity.TextId = String.Empty;
                    CurrentEntity.Name = String.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - {ex.Message}");
            }
        }

        public int IARectangleWidth
        {
            get { return iaRectangleWidth; }
            set
            {
                iaRectangleWidth = value;
                OnPropertyChanged("IARectangleWidth");
            }
        }

        public string IAPercentage
        {
            get { return iaPercentage; }
            set
            {
                iaPercentage = value;
                OnPropertyChanged("IAPercentage");
            }
        }

        public int IBRectangleWidth
        {
            get { return ibRectangleWidth; }
            set
            {
                ibRectangleWidth = value;
                OnPropertyChanged("IBRectangleWidth");
            }
        }

        public string IBPercentage
        {
            get { return ibPercentage; }
            set
            {
                ibPercentage = value;
                OnPropertyChanged("IBPercentage");
            }
        }
    }
}

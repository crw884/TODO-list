using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace TODO_list 
{   
    public class ToDoTask : INotifyPropertyChanged
    {
        private string description = string.Empty;
        private bool isCompleted = false;
        private string quantity = "Не указано.";
       
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(LabelStrikethrough));
                }
            }
        }

        public string Quantity
        {
            get => quantity;
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public ToDoTask() {}
        public ToDoTask(string description, string quantity)
        {
            Description = description;
            if (!string.IsNullOrWhiteSpace(quantity))
            {
                Quantity = quantity;
            }

        }
        public TextDecorations LabelStrikethrough
        {
            get => isCompleted ? TextDecorations.Strikethrough : TextDecorations.None;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

    public class MainViewModel : INotifyPropertyChanged
    {
        string description = "";
        string quantity = "";
        string unit = "";

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public ObservableCollection<ToDoTask> Tasks { get; } = new();

        public MainViewModel()
        {
            AddCommand = new Command(() =>
            {
                //if(!string.IsNullOrWhiteSpace(Description))
                Tasks.Add(new ToDoTask(Description, Quantity + " " + Unit));
                Description = "";
                Quantity = "";
                Unit = "";
            }, () => !string.IsNullOrWhiteSpace(Description)); // 0_o o_0

            RemoveCommand = new Command<ToDoTask>((ToDoTask task) =>
            {
                if (task != null)
                {
                    Tasks.Remove(task);
                }
            });

        }
        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                    ((Command)AddCommand).ChangeCanExecute();
                }
            }
        }

        public string Quantity
        {
            get => quantity;
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Unit
        {
            get => unit;
            set
            {
                if (unit != value)
                {
                    unit = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> AvailableUnits { get; } = new List<string>() { "кг.", "шт.", "л.", "п." };
        
        

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }


}

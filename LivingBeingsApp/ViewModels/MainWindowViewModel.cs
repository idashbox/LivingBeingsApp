using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using Avalonia.Threading;
using LivingBeingsApp.Models;

namespace LivingBeingsApp.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private LivingBeing? _selectedBeing;

        public ObservableCollection<LivingBeing> Beings { get; }

        public LivingBeing? SelectedBeing
        {
            get => _selectedBeing;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedBeing, value);
                this.RaisePropertyChanged(nameof(IsPantherSelected));
                this.RaisePropertyChanged(nameof(IsDogSelected));
                ActionMessage = string.Empty;
            }
        }

        public bool IsPantherSelected => SelectedBeing is Panther;
        public bool IsDogSelected => SelectedBeing is Dog;

        public ReactiveCommand<Unit, Unit> MoveCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> SoundCommand { get; }
        public ReactiveCommand<Unit, Unit> ClimbCommand { get; }

        private string _actionMessage = " ";
        public string ActionMessage
        {
            get => _actionMessage;
            set => this.RaiseAndSetIfChanged(ref _actionMessage, value);
        }

        public MainWindowViewModel()
        {
            Beings = new ObservableCollection<LivingBeing>
            {
                new Panther(),
                new Dog(),
                new Turtle()
            };

            MoveCommand = ReactiveCommand.Create(() =>
            {
                SelectedBeing?.Move();
            });

            StopCommand = ReactiveCommand.Create(() =>
            {
                SelectedBeing?.Stop();
            });

            SoundCommand = ReactiveCommand.Create(() =>
            {
                if (SelectedBeing is Panther p)
                {
                    p.Roar();
                }
                else if (SelectedBeing is Dog d)
                {
                    d.Bark();
                }
            });

            ClimbCommand = ReactiveCommand.Create(() =>
            {
                if (SelectedBeing is Panther p)
                {
                    p.ClimbTree();
                }
            });
        }
    }
}

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
                if (!Dispatcher.UIThread.CheckAccess())
                {
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        this.RaiseAndSetIfChanged(ref _selectedBeing, value);
                        this.RaisePropertyChanged(nameof(IsPantherSelected));
                    });
                }
                else
                {
                    this.RaiseAndSetIfChanged(ref _selectedBeing, value);
                    this.RaisePropertyChanged(nameof(IsPantherSelected));
                }
            }
        }

        public bool IsPantherSelected => SelectedBeing is Panther;

        public ReactiveCommand<Unit, Unit> MoveCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> SoundCommand { get; }
        public ReactiveCommand<Unit, Unit> ClimbCommand { get; }

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
                if (SelectedBeing != null)
                {
                    // Если мы не в UI потоке, то используем InvokeAsync
                    if (Dispatcher.UIThread.CheckAccess())
                    {
                        SelectedBeing.Move();
                        this.RaisePropertyChanged(nameof(SelectedBeing)); // Уведомляем о смене объекта
                    }
                    else
                    {
                        Dispatcher.UIThread.InvokeAsync(() =>
                        {
                            SelectedBeing.Move();
                            this.RaisePropertyChanged(nameof(SelectedBeing)); // Уведомляем о смене объекта
                        });
                    }
                }
            });



            StopCommand = ReactiveCommand.Create(() =>
            {
                if (!Dispatcher.UIThread.CheckAccess())
                {
                    Dispatcher.UIThread.InvokeAsync(() => SelectedBeing?.Stop());
                }
                else
                {
                    SelectedBeing?.Stop();
                }
            });

            SoundCommand = ReactiveCommand.Create(() =>
            {
                if (!Dispatcher.UIThread.CheckAccess())
                {
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        if (SelectedBeing is Panther p) p.Roar();
                        else if (SelectedBeing is Dog d) d.Bark();
                    });
                }
                else
                {
                    if (SelectedBeing is Panther p) p.Roar();
                    else if (SelectedBeing is Dog d) d.Bark();
                }
            });

            ClimbCommand = ReactiveCommand.Create(() =>
            {
                if (!Dispatcher.UIThread.CheckAccess())
                {
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        if (SelectedBeing is Panther p) p.ClimbTree();
                    });
                }
                else
                {
                    if (SelectedBeing is Panther p) p.ClimbTree();
                }
            });
        }
    }
}

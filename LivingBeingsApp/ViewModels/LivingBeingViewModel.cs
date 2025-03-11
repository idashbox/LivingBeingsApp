using Avalonia.Threading;
using ReactiveUI;
using System.Reactive;
using LivingBeingsApp.Models;

namespace LivingBeingsApp.ViewModels
{
    public class LivingBeingViewModel : ReactiveObject
    {
        private LivingBeing? _selectedBeing;

        public LivingBeing? SelectedBeing
        {
            get => _selectedBeing;
            set
            {
                // Проверка на корректный поток
                if (value != null && !Dispatcher.UIThread.CheckAccess())
                {
                    // Если это не UI-поток, используем InvokeAsync для обновления
                    Dispatcher.UIThread.InvokeAsync(() => this.RaiseAndSetIfChanged(ref _selectedBeing, value));
                }
                else
                {
                    // Если это UI-поток, обновляем напрямую
                    this.RaiseAndSetIfChanged(ref _selectedBeing, value);
                }
            }
        }

        public ReactiveCommand<Unit, Unit> MoveCommand { get; }
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        public ReactiveCommand<Unit, Unit> SoundCommand { get; }
        public ReactiveCommand<Unit, Unit> ClimbCommand { get; }

        public LivingBeingViewModel()
        {
            MoveCommand = ReactiveCommand.Create(() =>
            {
                // Используем асинхронную обработку
                SelectedBeing?.Move();
            });

            StopCommand = ReactiveCommand.Create(() =>
            {
                SelectedBeing?.Stop();
            });

            SoundCommand = ReactiveCommand.Create(() =>
            {
                if (SelectedBeing is Panther p)
                    p.Roar();
                else if (SelectedBeing is Dog d)
                    d.Bark();
            });

            ClimbCommand = ReactiveCommand.Create(() =>
            {
                if (SelectedBeing is Panther p)
                    p.ClimbTree();
            });
        }
    }
}

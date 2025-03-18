using Avalonia.Threading;
using ReactiveUI;
using System;
using LivingBeingsApp.ViewModels;

namespace LivingBeingsApp.Models
{
    public abstract class LivingBeing : ReactiveObject
    {
        private int _speed;
        protected int MaxSpeed;

        public int Speed
        {
            get => _speed;
            protected set => SetSpeed(value);
        }

        protected LivingBeing(int maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }

        // Метод для установки скорости с проверкой потока
        protected async void SetSpeed(int value)
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                _speed = value;
                this.RaisePropertyChanged(nameof(Speed));
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _speed = value;
                    this.RaisePropertyChanged(nameof(Speed));
                });
            }
        }

        public event Action<string>? OnActionMessage;

        protected void RaiseActionMessage(string message)
        {
            var app = (App)App.Current!;
            var viewModel = app.MainWindowViewModel;

            if (viewModel != null)
            {
                viewModel.ActionMessage = message;
            }
        }

        public abstract void Move();
        public abstract void Stop();
    }
}

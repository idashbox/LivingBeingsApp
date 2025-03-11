using Avalonia.Threading;
using ReactiveUI;
using System;

namespace LivingBeingsApp.Models
{
    public abstract class LivingBeing : ReactiveObject // Наследуем от ReactiveObject
    {
        private int _speed;

        public int Speed
        {
            get => _speed;
            protected set => SetSpeed(value);
        }

        protected int MaxSpeed;

        protected LivingBeing(int maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }

        // Вспомогательный метод для безопасного обновления значения скорости на UI потоке
        protected async void SetSpeed(int value)
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                _speed = value;
                this.RaisePropertyChanged(nameof(Speed)); // Уведомляем о изменении свойства
            }
            else
            {
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    _speed = value;
                    this.RaisePropertyChanged(nameof(Speed)); // Уведомляем о изменении свойства
                });
            }
        }

        public abstract void Move();
        public abstract void Stop();
    }
}

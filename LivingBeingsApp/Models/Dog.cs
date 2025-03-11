using Avalonia.Threading;
using System;

namespace LivingBeingsApp.Models
{
    public class Dog : LivingBeing
    {
        public event Action? OnBark;

        public Dog() : base(40)
        {
        }

        public override void Move()
        {
            if (Speed < MaxSpeed) SetSpeed(Speed + 5);
        }

        public override void Stop()
        {
            if (Speed > 0) SetSpeed(Speed - 5);
        }

        public void Bark()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                OnBark?.Invoke();
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() => OnBark?.Invoke());
            }
        }
    }
}

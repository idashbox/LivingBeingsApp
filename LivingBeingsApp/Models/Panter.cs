using Avalonia.Threading;
using System;

namespace LivingBeingsApp.Models
{
    public class Panther : LivingBeing
    {
        public event Action? OnRoar;

        public Panther()  : base(80)
        {
        }

        public override void Move()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                if (Speed < MaxSpeed) SetSpeed(Speed + 10);
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (Speed < MaxSpeed) SetSpeed(Speed + 10);
                });
            }
        }

        public override void Stop()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                if (Speed > 0) SetSpeed(Speed - 10);
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (Speed > 0) SetSpeed(Speed - 10);
                });
            }
        }

        public void Roar()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                OnRoar?.Invoke();
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() => OnRoar?.Invoke());
            }
        }

        public void ClimbTree()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                Console.WriteLine("Пантера залезла на дерево!");
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Console.WriteLine("Пантера залезла на дерево!");
                });
            }
        }
    }
}

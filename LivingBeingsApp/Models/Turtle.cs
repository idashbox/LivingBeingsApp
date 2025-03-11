using Avalonia.Threading;
using System;

namespace LivingBeingsApp.Models
{
    public class Turtle : LivingBeing
    {
        public Turtle()  : base(5)
        {
        }

        public override void Move()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                if (Speed < MaxSpeed) SetSpeed(Speed + 1);
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (Speed < MaxSpeed) SetSpeed(Speed + 1);
                });
            }
        }

        public override void Stop()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                if (Speed > 0) SetSpeed(Speed - 1);
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (Speed > 0) SetSpeed(Speed - 1);
                });
            }
        }
    }
}

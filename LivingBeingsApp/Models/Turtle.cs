using System;

namespace LivingBeingsApp.Models
{
    public class Turtle : LivingBeing
    {
        public Turtle() : base(5)
        {
        }

        public override void Move()
        {
            if (Speed < MaxSpeed)
            {
                SetSpeed(Speed + 1);
            }
        }

        public override void Stop()
        {
            if (Speed > 0)
            {
                SetSpeed(Speed - 1);
            }
        }
    }
}

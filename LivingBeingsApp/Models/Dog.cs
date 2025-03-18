using System;

namespace LivingBeingsApp.Models
{
    public class Dog : LivingBeing
    {

        public Dog() : base(40) { }

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
            RaiseActionMessage("Собака залаяла!");
        }
    }
}

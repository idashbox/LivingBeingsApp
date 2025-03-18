using System;

namespace LivingBeingsApp.Models
{
    public class Panther : LivingBeing
    {

        public Panther() : base(80) { }

        public override void Move()
        {
            if (Speed < MaxSpeed) SetSpeed(Speed + 10);
        }

        public override void Stop()
        {
            if (Speed > 0) SetSpeed(Speed - 10);
        }

        public void Roar()
        {
            RaiseActionMessage("Пантера зарычала!");
        }

        public void ClimbTree()
        {
            RaiseActionMessage("Пантера залезла на дерево!");
        }
    }
}

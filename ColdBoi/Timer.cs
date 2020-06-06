using System;
using System.Collections.Generic;

namespace ColdBoi
{
    public class Timer
    {
        private readonly List<Action> actions;
        private readonly double ticksPerSecond;

        public Timer(int frequency)
        {
            this.actions = new List<Action>();
            this.ticksPerSecond = 1d / frequency;
        }

        public void AddAction(Action action)
        {
            this.actions.Add(action);
        }

        public void Update(double timeElapsed)
        {
            var timesToTick = Math.Round(timeElapsed / this.ticksPerSecond);

            for (var i = 0; i < timesToTick; i++)
            {
                Tick();
            }
        }

        private void Tick()
        {
            foreach (var action in this.actions)
            {
                action.Invoke();
            }
        }
    }
}
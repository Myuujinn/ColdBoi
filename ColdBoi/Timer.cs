using System;
using System.Collections.Generic;

namespace ColdBoi
{
    public class Timer
    {
        private readonly List<Action<int>> actions;
        private readonly int frequency;

        public Timer(int frequency)
        {
            this.actions = new List<Action<int>>();
            this.frequency = frequency;
        }

        public void AddAction(Action<int> action)
        {
            this.actions.Add(action);
        }

        public void Update()
        {
            foreach (var action in this.actions)
            {
                action.Invoke(frequency);
            }
        }
    }
}
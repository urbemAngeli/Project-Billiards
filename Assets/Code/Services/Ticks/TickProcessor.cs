using System.Collections.Generic;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Services.Ticks
{
    public class TickProcessor : MonoBehaviour, IService
    {
        
        private List<ITick> ticks = new List<ITick>();

        public void Add(ITick tick)
        {
            if (!ticks.Contains(tick))
                ticks.Add(tick);
        }

        public void Remove(ITick tick)
        {
            if (ticks.Contains(tick))
                ticks.Remove(tick);
        }

        private void Update()
        {
            for (int i = 0; i < ticks.Count; i++) 
                ticks[i].Tick();
        }
    }
}
using Code.Infrastructure.Services;
using Code.UI.Bars;
using UnityEngine;

namespace Code.UI
{
    public class UIRoot : MonoBehaviour, IService
    {
        [SerializeField]
        private ImpactForceBar _impactForceBar;

        
        public ImpactForceBar ImpactForceBar => _impactForceBar;
    }
}
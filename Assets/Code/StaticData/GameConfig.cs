using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public float MaxForce = 1.5f;
        public float MaxOffsetCue = 0.75f;
        public float MaxOffsetFinger = 0.75f;
    }
}
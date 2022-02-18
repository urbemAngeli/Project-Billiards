using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "TrajectoryDrawingData", menuName = "StaticData/TrajectoryDrawingData")]
    public class TrajectoryDrawingStaticData : ScriptableObject
    {
        public GameObject LinePrefab;
        public GameObject CircleSpotPrefab;
    }
}
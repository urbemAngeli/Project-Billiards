using UnityEngine;

namespace Code.Extensions
{
    public static class GameObjectExts
    {
        public static bool IsInLayerMask(this GameObject gameObject, LayerMask mask) => 
            1 <<mask == (1 << mask | (1 << gameObject.layer));
    }
}
using UnityEngine;

namespace Code.Tools
{
    public static class MathUtils
    {
        public static Vector3 RoundHeight(Vector3 position) => 
            new Vector3(position.x, position.y, position.z);
    }
}
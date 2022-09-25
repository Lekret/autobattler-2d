using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        public static Vector2 ToVec2(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }
    }
}
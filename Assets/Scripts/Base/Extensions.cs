using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class V3Extensions
    {
        public static Vector2 ToVector2(this Vector3 vectorToModify)
        {
            return new Vector2(vectorToModify.x, vectorToModify.y);
        }
    }
}

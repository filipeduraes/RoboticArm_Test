using UnityEngine;

namespace Miscellaneous
{
    public static class ExtensionMethods
    {
        public static bool CompareLayers(this Collider collider, int layer)
        {
            return CompareLayers(collider.gameObject.layer, layer);
        }
        
        public static bool CompareLayers(this GameObject go, int layer)
        {
            return CompareLayers(go.layer, layer);
        }

        public static bool CompareLayers(int layerA, int layerB)
        {
            return ((1 << layerA) & layerB) != 0;
        }

        public static Vector3 ClampVector3(Vector3 value, Vector3 min, Vector3 max)
        {
            Vector3 clampedVector3;
            clampedVector3.x = Mathf.Clamp(value.x, min.x, max.x);
            clampedVector3.y = Mathf.Clamp(value.y, min.y, max.y);
            clampedVector3.z = Mathf.Clamp(value.z, min.z, max.z);
            
            return clampedVector3;
        }
    }
}
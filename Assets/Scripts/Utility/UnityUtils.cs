using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class UnityUtils {
        /**
         * Utility method for finding all direct child components within a 
         * parent without returning the actual parent in the result set.
         * 
         * As stated only direct children are included in the results. This
         * means that GameObjects directly under the parent in the hierarchy 
         * are included. 
         */
        public static T[] GetComponentsInDirectChildrenExcludeParent<T>(GameObject parent) where T : Component
        {
            // Find all components of type T within this parent
            T[] componentsInChildren = parent.GetComponentsInChildren<T>();

            // Declare a set to hold the results
            HashSet<T> results = new HashSet<T>();
            foreach (T component in componentsInChildren)
            {
                // Compare instance id's to filter out the parent
                if (component.gameObject.GetInstanceID() != parent.GetInstanceID() &&
                    component.transform.parent.Equals(parent.transform))
                    results.Add(component);
            }

            return results.ToArray();
        } 
    }
}

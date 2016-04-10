using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ICities;

namespace GSteigertDistricts
{
    public class ReplaceHelper
    {
        public static void ReplaceBuildingAI<TOldAI, TNewAI>()
            where TOldAI : BuildingAI where TNewAI : BuildingAI
        {
            Utils.Log(String.Format("Replacing {0} with {1}", typeof(TOldAI), typeof(TNewAI)));

            ForEachPrefab((BuildingInfo i) =>
            {
                var oldAI = i.gameObject.GetComponent<TOldAI>();
                if (oldAI == null || !(oldAI.GetType().Equals(typeof(TOldAI)))) return;

                var newAI = i.gameObject.GetComponent<TNewAI>();
                if (newAI != null && newAI.GetType().Equals(typeof(TNewAI))) return;

                Utils.Log(String.Format(" - Swapping: {0}", i));
                newAI = i.gameObject.AddComponent<TNewAI>();
                ShallowCopyTo(oldAI, newAI);

                oldAI.DestroyPrefab();
                i.m_buildingAI = newAI;
                UnityEngine.Object.Destroy(oldAI);
                newAI.InitializePrefab();
            });
        }

        public static void ReplaceVehicleAI<TOldAI, TNewAI>()
            where TOldAI : VehicleAI where TNewAI : VehicleAI
        {
            Utils.Log(String.Format("Replacing {0} with {1}", typeof(TOldAI), typeof(TNewAI)));

            ForEachPrefab((VehicleInfo i) =>
            {
                var oldAI = i.gameObject.GetComponent<TOldAI>();
                if (oldAI == null || !(oldAI.GetType().Equals(typeof(TOldAI)))) return;

                var newAI = i.gameObject.GetComponent<TNewAI>();
                if (newAI != null && newAI.GetType().Equals(typeof(TNewAI))) return;

                Utils.Log(String.Format(" - Swapping: {0}", i));
                newAI = i.gameObject.AddComponent<TNewAI>();
                ShallowCopyTo(oldAI, newAI);

                oldAI.ReleaseAI();
                i.m_vehicleAI = newAI;
                UnityEngine.Object.Destroy(oldAI);
                newAI.InitializeAI();
            });
        }

        private static void ShallowCopyTo(object src, object dst)
        {
            var srcFields = GetFields(src);
            var dstFields = GetFields(dst);

            foreach (var srcField in srcFields)
            {
                FieldInfo dstField;
                if (!dstFields.TryGetValue(srcField.Key, out dstField)) continue;
                dstField.SetValue(dst, srcField.Value.GetValue(src));
            }
        }

        private static Dictionary<string, FieldInfo> GetFields(object obj)
        {
            return obj.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(f => f.Name, f => f);
        }

        private static void ForEachPrefab<T>(Action<T> action) where T : PrefabInfo
        {
            for (var i = 0u; i < PrefabCollection<T>.LoadedCount(); i++)
            {
                var prefab = PrefabCollection<T>.GetLoaded(i);
                if (prefab != null) action(prefab);
            }
        }
    }
}

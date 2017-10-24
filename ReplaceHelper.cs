using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ICities;

namespace DistrictServiceLimit
{
    public class ReplaceHelper
    {
        public static void ReplaceBuildingAI<TOldAI, TNewAI>()
            where TOldAI : BuildingAI where TNewAI : BuildingAI
        {
            Utils.LogGeneral(String.Format(" - Replacing {0} with {1}", typeof(TOldAI), typeof(TNewAI)));

            ForEachPrefab((BuildingInfo info) =>
            {
                var oldAI = info.gameObject.GetComponent<TOldAI>();
                if (oldAI == null || !(oldAI.GetType().Equals(typeof(TOldAI)))) return;

                var newAI = info.gameObject.GetComponent<TNewAI>();
                if (newAI != null && newAI.GetType().Equals(typeof(TNewAI))) return;

                if (info.name == "Bus Depot" || info.name == "Tram Depot" || isMetroDepot(info))    //added TheDog for MOM compatibility
                {
                    Utils.LogGeneral(String.Format(" --> Skipping: {0}", info.name));
                    return;
                }

                Utils.LogGeneral(String.Format(" --> Swapping: {0}", info.name));
                newAI = info.gameObject.AddComponent<TNewAI>();
                ShallowCopyTo(oldAI, newAI);

                oldAI.DestroyPrefab();
                info.m_buildingAI = newAI;
                UnityEngine.Object.Destroy(oldAI);
                newAI.InitializePrefab();
            });
        }

        //added TheDog for MOM compatibility
        private static bool isMetroDepot(BuildingInfo info)
        {
            return info?.m_class != null && info.m_buildingAI is DepotAI && info.m_class.m_service == ItemClass.Service.PublicTransport && info.m_class.m_subService == ItemClass.SubService.PublicTransportMetro;
        }

        public static void ReplaceVehicleAI<TOldAI, TNewAI>()
            where TOldAI : VehicleAI where TNewAI : VehicleAI
        {
            Utils.LogGeneral(String.Format("Replacing {0} with {1}", typeof(TOldAI), typeof(TNewAI)));

            ForEachPrefab((VehicleInfo info) =>
            {
                var oldAI = info.gameObject.GetComponent<TOldAI>();
                if (oldAI == null || !(oldAI.GetType().Equals(typeof(TOldAI)))) return;

                var newAI = info.gameObject.GetComponent<TNewAI>();
                if (newAI != null && newAI.GetType().Equals(typeof(TNewAI))) return;

                Utils.LogGeneral(String.Format(" - Swapping: {0}", info.name));
                newAI = info.gameObject.AddComponent<TNewAI>();
                ShallowCopyTo(oldAI, newAI);

                oldAI.ReleaseAI();
                info.m_vehicleAI = newAI;
                UnityEngine.Object.Destroy(oldAI);
                newAI.InitializeAI();
            });
        }

        public static void ReplacePersonAI<TOldAI, TNewAI>()
            where TOldAI : CitizenAI where TNewAI : CitizenAI
        {
            Utils.LogGeneral(String.Format("Replacing {0} with {1}", typeof(TOldAI), typeof(TNewAI)));

            ForEachPrefab((CitizenInfo info) =>
            {
                var oldAI = info.gameObject.GetComponent<TOldAI>();
                if (oldAI == null || !(oldAI.GetType().Equals(typeof(TOldAI)))) return;

                var newAI = info.gameObject.GetComponent<TNewAI>();
                if (newAI != null && newAI.GetType().Equals(typeof(TNewAI))) return;

                Utils.LogGeneral(String.Format(" - Swapping: {0}", info.name));
                newAI = info.gameObject.AddComponent<TNewAI>();
                ShallowCopyTo(oldAI, newAI);

                oldAI.ReleaseAI();
                info.m_citizenAI = newAI;
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

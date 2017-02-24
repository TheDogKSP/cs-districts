using System.Collections.Generic;
using System.Linq;
using System.IO;
using ICities;
using ColossalFramework;
using ColossalFramework.IO;

namespace GSteigertDistricts
{
    public class ServiceBuildingOptions
    {
        private static ServiceBuildingOptions Instance;

        public static ServiceBuildingOptions GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ServiceBuildingOptions();
            }
            return Instance;
        }

        private Dictionary<ushort, ServiceBuildingData> m_serviceBuildingInfos;

        public ServiceBuildingOptions()
        {
            m_serviceBuildingInfos = new Dictionary<ushort, ServiceBuildingData>();
        }

        public bool IsTargetCovered(ushort buildingID, byte targetDistrictID)
        {
            if (DistrictChecker.IsActive(targetDistrictID))
            {
                ServiceBuildingData data = GetData(buildingID);
                return (data == null ? false :
                    (data.AreAllDistrictsServed() || data.IsAdditionalTarget(targetDistrictID)));
            }
            return false;
        }

        public bool IsAdditionalTarget(ushort buildingID, byte targetDistrictID)
        {
            ServiceBuildingData data = GetData(buildingID);
            return (data == null ? false : data.IsAdditionalTarget(targetDistrictID));
        }

        public void SetAdditionalTarget(ushort buildingID, byte targetDistrictID, bool allowed)
        {
            ServiceBuildingData data = GetData(buildingID);
            if (data != null)
            {
                data.SetAdditionalTarget(targetDistrictID, allowed);
            }
        }

        public bool AreAllDistrictsServed(ushort buildingID)
        {
            ServiceBuildingData data = GetData(buildingID);
            return (data == null ? false : data.AreAllDistrictsServed());
        }

        public void SetAllDistrictsServed(ushort buildingID, bool value)
        {
            ServiceBuildingData data = GetData(buildingID);
            if (data != null)
            {
                data.SetAllDistrictsServed(value);
            }
        }

        internal ServiceBuildingData GetData(ushort buildingID)
        {
            // the building might not be supported anymore (eg: it's district was removed), so this check
            // must be performed before anything else
            if (!IsSupported(buildingID))
            {
                m_serviceBuildingInfos.Remove(buildingID);
                return null;
            }

            if (m_serviceBuildingInfos.ContainsKey(buildingID))
            {
                return m_serviceBuildingInfos[buildingID];
            }
            else
            {
                ServiceBuildingData result = new ServiceBuildingData();
                result.buildingID = buildingID;
                SetData(buildingID, result);
                return result;
            }
        }

        internal void SetData(ushort buildingID, ServiceBuildingData data)
        {
            m_serviceBuildingInfos.Add(buildingID, data);
        }

        internal void OnBuildingRemoved(ushort buildingID)
        {
            if (m_serviceBuildingInfos.Remove(buildingID))
            {
                Utils.LogGeneral("OnBuildingRemoved;buildingID=" + buildingID);
            }
        }

        public bool IsSupported(ushort buildingID)
        {
            Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID];
            ItemClass.Service service = building.Info.GetService();

            // ignore prisons
            if (service.Equals(ItemClass.Service.PoliceDepartment)
                && building.Info.m_class.m_level >= ItemClass.Level.Level4)
            {
                return false;
            }

            if (service.Equals(ItemClass.Service.FireDepartment)
                   || service.Equals(ItemClass.Service.Garbage)
                   || service.Equals(ItemClass.Service.HealthCare)
                   || service.Equals(ItemClass.Service.PoliceDepartment)
                   || service.Equals(ItemClass.Service.Road))
            {
                DistrictManager districtManager = Singleton<DistrictManager>.instance;
                byte districtID = districtManager.GetDistrict(building.m_position);
                return DistrictChecker.IsActive(districtID);
            }
            return false;
        }

        public void Clear()
        {
            m_serviceBuildingInfos.Clear();
        }

        public ServiceBuildingData[] ToArray()
        {
            return m_serviceBuildingInfos.Values.ToArray();
        }
    }

    public class ServiceBuildingData : IDataContainer
    {
        internal ushort buildingID;
        private List<byte> additionalTargets;
        private bool allDistrictsServed;

        public ServiceBuildingData()
        {
        }

        private List<byte> GetAdditionalTargets()
        {
            if (additionalTargets == null)
            {
                additionalTargets = new List<byte>();
            }
            return additionalTargets;
        }

        public bool AreAllDistrictsServed()
        {
            return allDistrictsServed;
        }

        public void SetAllDistrictsServed(bool value)
        {
            allDistrictsServed = value;
        }

        public bool IsAdditionalTarget(byte districtID)
        {
            return GetAdditionalTargets().Contains(districtID);
        }

        internal void SetAdditionalTarget(byte districtID, bool allowed)
        {
            if (allowed)
            {
                GetAdditionalTargets().Add(districtID);
            }
            else
            {
                GetAdditionalTargets().Remove(districtID);
            }
        }

        public void Serialize(DataSerializer serializer)
        {
            GetAdditionalTargets().RemoveAll(id => !DistrictChecker.IsActive(id));
            if (serializer.version >= 1)
            {
                serializer.WriteInt32(buildingID);
                serializer.WriteByteArray(GetAdditionalTargets().ToArray());
            }
            if (serializer.version >= 2)
            {
                serializer.WriteBool(AreAllDistrictsServed());
            }
        }

        public void Deserialize(DataSerializer serializer)
        {
            if (serializer.version >= 1)
            {
                buildingID = (ushort)serializer.ReadInt32();
                foreach (byte districtID in serializer.ReadByteArray())
                {
                    GetAdditionalTargets().Add(districtID);
                }
            }
            if (serializer.version >= 2)
            {
                SetAllDistrictsServed(serializer.ReadBool());
            }
        }

        public void AfterDeserialize(DataSerializer serializer)
        {
        }
    }

    public class BuildingWatcher : BuildingExtensionBase
    {
        public override void OnBuildingReleased(ushort buildingID)
        {
            ServiceBuildingOptions.GetInstance().OnBuildingRemoved(buildingID);
        }
    }

    public class ServiceBuildingDataLoader : SerializableDataExtensionBase
    {
        private const uint VERSION = 2;
        private const string KEY = "GSteigertDistricts.savedata";

        public override void OnLoadData()
        {
            base.OnLoadData();
#if RELEASE
            try {
#endif
            ServiceBuildingOptions.GetInstance().Clear();
            byte[] bytes = serializableDataManager.LoadData(KEY);
            if (bytes == null)
            {
                return;
            }

            ServiceBuildingData[] dataArray;
            using (var stream = new MemoryStream(bytes))
            {
                dataArray = DataSerializer.DeserializeArray<ServiceBuildingData>(stream, DataSerializer.Mode.Memory);
                foreach (ServiceBuildingData data in dataArray)
                {
                    ServiceBuildingOptions.GetInstance().SetData(data.buildingID, data);
                }
            }
#if RELEASE
            } catch (Exception ignored) { }
#endif
        }

        public override void OnSaveData()
        {
            base.OnSaveData();
#if RELEASE
            try {
#endif
            ServiceBuildingData[] dataArray = ServiceBuildingOptions.GetInstance().ToArray();
            if (dataArray == null || dataArray.Length == 0)
            {
                return;
            }

            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                DataSerializer.SerializeArray(stream,
                    DataSerializer.Mode.Memory,
                    VERSION,
                    dataArray);
                bytes = stream.ToArray();
            }

            serializableDataManager.SaveData(KEY, bytes);
#if RELEASE
            } catch (Exception ignored) { }
#endif
        }
    }
}

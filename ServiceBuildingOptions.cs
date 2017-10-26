using System.Collections.Generic;
using System.Linq;
using System.IO;
using ICities;
using ColossalFramework;
using ColossalFramework.IO;

namespace DistrictServiceLimit
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

        /// <summary>
        /// Lookup for all service buildings with selected service options
        /// </summary>
        private Dictionary<ushort, ServiceBuildingData> m_serviceBuildingInfos;


        public ServiceBuildingOptions()
        {
            m_serviceBuildingInfos = new Dictionary<ushort, ServiceBuildingData>(100);
        }

        public bool IsTargetCovered(ushort buildingID, byte targetDistrictID)
        {
            ServiceBuildingData data = GetData(buildingID);
            return (data == null ? false : (data.AreAllDistrictsServed() || data.IsAdditionalTarget(targetDistrictID)));
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
                OnBuildingRemoved(buildingID);
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
                   || service.Equals(ItemClass.Service.Road)
                   || (service.Equals(ItemClass.Service.PublicTransport) && building.Info.GetSubService().Equals(ItemClass.SubService.PublicTransportTaxi))) //allow Taxi
            {
                DistrictManager districtManager = Singleton<DistrictManager>.instance;
                byte districtID = districtManager.GetDistrict(building.m_position);

                Utils.LogGeneral($"Checking buildingID {buildingID}, District {districtID}:'{districtManager.GetDistrictName(districtID)}' active... {DistrictChecker.IsActive(districtID)}");

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


    /// <summary>
    /// Monitoring Buildings
    /// </summary>
    public class BuildingWatcher : BuildingExtensionBase
    {
        public override void OnBuildingReleased(ushort buildingID)
        {
            ServiceBuildingOptions.GetInstance().OnBuildingRemoved(buildingID);
        }
    }


    /// <summary>
    /// Data Persistence
    /// </summary>
    public class ServiceBuildingData : IDataContainer
    {
        internal ushort buildingID;
        private bool allDistrictsServed;
        private List<byte> additionalTargets;


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
            if (serializer.version >= 3)
            {
                serializer.WriteInt32(buildingID);
                serializer.WriteBool(AreAllDistrictsServed());
                serializer.WriteByteArray(GetAdditionalTargets().ToArray());
            }
        }

        public void Deserialize(DataSerializer serializer)
        {
            if (serializer.version >= 3)
            {
                buildingID = (ushort)serializer.ReadInt32();
                SetAllDistrictsServed(serializer.ReadBool());
                foreach (byte districtID in serializer.ReadByteArray())
                {
                    GetAdditionalTargets().Add(districtID);
                }
            }
        }

        public void AfterDeserialize(DataSerializer serializer)
        {
        }
    }


    /// <summary>
    /// Persistance handling
    /// </summary>
    public class ServiceBuildingDataLoader : SerializableDataExtensionBase
    {
        private const uint VERSION = 3;
        private const string KEY = "DISTRICTSERVICELIMIT";

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

            Utils.LogGeneral($"DSL data loaded (Size in bytes: {bytes.Length})");
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
            Utils.LogGeneral($"DSL data saved (Size in bytes: {bytes.Length})");
#if RELEASE
            } catch (Exception ignored) { }
#endif
        }
    }
}

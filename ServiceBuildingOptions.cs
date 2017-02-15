using System;
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

        public bool IsAdditionalTarget(ushort buildingID, byte targetDistrictID)
        {
            if (DistrictChecker.IsActive(targetDistrictID))
            {
                ServiceBuildingData data = GetData(buildingID);
                return (data == null ? false : data.IsAdditionalTarget(targetDistrictID));
            }
            return false;
        }

        public void SetAdditionalTarget(ushort buildingID, byte targetDistrictID, bool allowed)
        {
            ServiceBuildingData data = GetData(buildingID);
            if (data != null)
            {
                data.SetAdditionalTarget(targetDistrictID, allowed);
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
            additionalTargets.RemoveAll(id => !DistrictChecker.IsActive(id));
            if (serializer.version >= 1)
            {
                serializer.WriteInt32(buildingID);
                serializer.WriteByteArray(GetAdditionalTargets().ToArray());
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
        private const uint VERSION = 1;
        private const string KEY = "GSteigertDistricts.savedata";

        public override void OnLoadData()
        {
            base.OnLoadData();
            try
            {
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
            }
            catch (Exception e)
            {
                Utils.LogGeneral("Error while loading data: " + e.Message + "\n" + e.StackTrace);
            }
        }

        public override void OnSaveData()
        {
            base.OnSaveData();
            try
            {
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
            }
            catch (Exception e)
            {
                Utils.LogGeneral("Error while saving data: " + e.Message + "\n" + e.StackTrace);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICities;
using ColossalFramework;
using ColossalFramework.IO;
using ColossalFramework.Math;
using ColossalFramework.Globalization;

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
            Utils.LogGeneral("Creating service building options");
            m_serviceBuildingInfos = new Dictionary<ushort, ServiceBuildingData>();
        }

        public bool IsDestinationAllowed(ushort buildingID, byte targetDistrictID)
        {
            return GetData(buildingID).IsTargetDistrictAllowed(targetDistrictID);
        }

        public ServiceBuildingData GetData(ushort buildingID)
        {
            if (!IsSupported(buildingID))
            {
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
                result.additionalServedDistricts = new List<byte>();
                SetData(buildingID, result);
                return result;
            }
        }

        public void SetData(ushort buildingID, ServiceBuildingData data)
        {
            m_serviceBuildingInfos.Add(buildingID, data);
        }

        public void OnBuildingRemoved(ushort buildingID)
        {
            if (IsSupported(buildingID))
            {
                if (m_serviceBuildingInfos.Remove(buildingID))
                {
                    Utils.LogGeneral("Removing service building configuration");
                }
            }
        }

        // TODO: INVOKE THIS METHOD
        public void OnDistrictRemoved(byte districtID)
        {
            foreach (ServiceBuildingData data in m_serviceBuildingInfos.Values)
            {
                data.RemoveDistrict(districtID);
            }
        }

        public bool IsSupported(ushort buildingID)
        {
            ItemClass.Service service = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.GetService();
            return service.Equals(ItemClass.Service.FireDepartment)
                   || service.Equals(ItemClass.Service.Garbage)
                   || service.Equals(ItemClass.Service.HealthCare)
                   || service.Equals(ItemClass.Service.PoliceDepartment)
                   || service.Equals(ItemClass.Service.Road);
        }

        public void Clear()
        {
            Utils.LogGeneral("Clearing service building options");
            m_serviceBuildingInfos.Clear();
        }

        public ServiceBuildingData[] ToArray()
        {
            return m_serviceBuildingInfos.Values.ToArray();
        }
    }

    public class ServiceBuildingData : IDataContainer
    {
        public ushort buildingID;
        public List<byte> additionalServedDistricts;

        public ServiceBuildingData()
        {
            additionalServedDistricts = new List<byte>();
        }

        public bool IsTargetDistrictAllowed(byte districtID)
        {
            return additionalServedDistricts.Contains(districtID);
        }

        public void RemoveDistrict(byte districtID)
        {
            if (additionalServedDistricts.Remove(districtID))
            {
                Utils.LogGeneral("Removing district from building's allowed targets");
            }
        }

        public void Serialize(DataSerializer serializer)
        {
            if (serializer.version >= 1)
            {
                serializer.WriteInt32(buildingID);
                serializer.WriteByteArray(additionalServedDistricts.ToArray());
            }
        }

        public void Deserialize(DataSerializer serializer)
        {
            if (serializer.version >= 1)
            {
                buildingID = (ushort)serializer.ReadInt32();
                foreach (byte targetDistrict in serializer.ReadByteArray())
                {
                    additionalServedDistricts.Add(targetDistrict);
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
                Utils.LogGeneral("[SerializableDataExtension]");
                Utils.LogGeneral("Checking saved data now");

                ServiceBuildingOptions.GetInstance().Clear();

                byte[] bytes = serializableDataManager.LoadData(KEY);
                if (bytes == null)
                {
                    Utils.LogGeneral("No saved data found");
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

                Utils.LogGeneral("Save data successfully loaded");
                Utils.LogGeneral("[/SerializableDataExtension]\n");
            }
            catch (Exception e)
            {
                Utils.LogGeneral("Error while loading data: " + e.StackTrace);
            }
        }

        public override void OnSaveData()
        {
            base.OnSaveData();
            try
            {
                Utils.LogGeneral("[SerializableDataExtension]");
                Utils.LogGeneral("Writing save data now");

                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    DataSerializer.SerializeArray(stream,
                        DataSerializer.Mode.Memory,
                        VERSION,
                        ServiceBuildingOptions.GetInstance().ToArray());
                    bytes = stream.ToArray();
                }

                serializableDataManager.SaveData(KEY, bytes);

                Utils.LogGeneral("Save data successfully written");
                Utils.LogGeneral("[/SerializableDataExtension]\n");
            }
            catch (Exception e)
            {
                Utils.LogGeneral("Error while saving data: " + e.StackTrace);
            }
        }
    }
}

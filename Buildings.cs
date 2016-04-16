using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
    public static class BuildingHelper
    {
        /**
         * Workaround a scenario where there is another building closer to the offer's position
         * that doesn't belong to the same district, causing the offer never to be handled.
         */
        public static void searchAnotherBuildingToHandle(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer,
            Type aiType, ItemClass.Service service)
        {
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            FastList<ushort> buildings = buildingManager.GetServiceBuildings(service);
            if (buildings == null) return;

            for (int index = 0; index < buildings.m_size; index++)
            {
                ushort otherBuildingID = buildings.m_buffer[index];
                if (buildingID == otherBuildingID) continue;

                Building otherBuilding = buildingManager.m_buildings.m_buffer[(int)otherBuildingID];
                if (!aiType.IsAssignableFrom(otherBuilding.Info.m_buildingAI.GetType())) continue;

                FireStationAI otherBuildingAI = (FireStationAI)otherBuilding.Info.m_buildingAI;
                if (DistrictChecker.IsBuildingTransferAllowed(otherBuildingID, ref otherBuilding, material, offer, true))
                {
                    otherBuildingAI.StartTransfer(otherBuildingID, ref otherBuilding, material, offer);
                    return;
                }
            }
        }

        /**
         * Workaround a scenario where there is another building closer to the offer's position
         * that doesn't belong to the same district, causing the offer never to be handled.
         */
        public static void searchAnotherBuildingToReceive(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer,
            Type aiType, ItemClass.Service service)
        {
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            FastList<ushort> buildings = buildingManager.GetServiceBuildings(service);
            if (buildings == null) return;

            for (int index = 0; index < buildings.m_size; ++index)
            {
                ushort otherBuildingID = buildings.m_buffer[index];
                if (buildingID == otherBuildingID) continue;

                Building otherBuilding = buildingManager.m_buildings.m_buffer[(int)otherBuildingID];
                if (!aiType.IsAssignableFrom(otherBuilding.Info.m_buildingAI.GetType())) continue;

                offer.Building = otherBuildingID;
                offer.Position = otherBuilding.m_position;

                if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer, true))
                {
                    data.Info.m_buildingAI.StartTransfer(buildingID, ref data, material, offer);
                    return;
                }
            }
        }
    }

    public class MedicalCenterAIMod : MedicalCenterAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
        }
    }

    public class HospitalAIMod : HospitalAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
        }
    }

    public class CemeteryAIMod : CemeteryAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
            else if (material == TransferManager.TransferReason.DeadMove)
            {
                BuildingHelper.searchAnotherBuildingToReceive(buildingID, ref data, material, offer,
                    typeof(CemeteryAI), ItemClass.Service.HealthCare);
            }
        }
    }

    public class PoliceStationAIMod : PoliceStationAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
        }
    }

    public class FireStationAIMod : FireStationAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
            else
            {
                BuildingHelper.searchAnotherBuildingToHandle(buildingID, ref data, material, offer,
                    typeof(FireStationAI), ItemClass.Service.FireDepartment);
            }
        }
    }

    public class LandfillSiteAIMod : LandfillSiteAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
            else if (material == TransferManager.TransferReason.GarbageMove)
            {
                BuildingHelper.searchAnotherBuildingToReceive(buildingID, ref data, material, offer,
                    typeof(LandfillSiteAI), ItemClass.Service.Garbage);
            }
        }
    }

    public class SnowDumpAIMod : SnowDumpAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
            else if (material == TransferManager.TransferReason.SnowMove)
            {
                BuildingHelper.searchAnotherBuildingToReceive(buildingID, ref data, material, offer,
                    typeof(SnowDumpAI), ItemClass.Service.Road);
            }
        }
    }

    public class MaintenanceDepotAIMod : MaintenanceDepotAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
        }
    }

    public class DepotAIMod : DepotAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
        }
    }

    public class TaxiStandAIMod : TaxiStandAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
        }
    }
}

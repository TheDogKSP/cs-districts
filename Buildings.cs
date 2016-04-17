using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
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
                BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer, ItemClass.Service.HealthCare);
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
                BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer, ItemClass.Service.FireDepartment);
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
                BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer, ItemClass.Service.Garbage);
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
                BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer, ItemClass.Service.Road);
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

    public static class BuildingHelper
    {
        /**
         * Workaround a scenario where there is another building closer to the offer's position
         * that doesn't belong to the same district, causing the offer never to be handled.
         */
        public static void delegateToAnotherBuilding(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer,
            ItemClass.Service service)
        {
            Type aiType = data.Info.m_buildingAI.GetType().BaseType;
            bool delegateMode = (material == TransferManager.TransferReason.Fire);
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            FastList<ushort> buildings = buildingManager.GetServiceBuildings(service);
            if (buildings == null) return;

            for (int index = 0; index < buildings.m_size; index++)
            {
                ushort otherBuildingID = buildings.m_buffer[index];
                if (buildingID == otherBuildingID) continue;

                Building otherBuilding = buildingManager.m_buildings.m_buffer[otherBuildingID];
                if ((otherBuilding.m_flags & Building.Flags.Active) == Building.Flags.None) continue;
                if ((otherBuilding.m_problems & Notification.Problem.Emptying) != Notification.Problem.None) continue;
                if ((otherBuilding.m_problems & Notification.Problem.EmptyingFinished) != Notification.Problem.None) continue;

                BuildingAI otherBuildingAI = (BuildingAI)otherBuilding.Info.m_buildingAI;
                if (!aiType.IsAssignableFrom(otherBuildingAI.GetType())) continue;
                if (otherBuildingAI.IsFull(otherBuildingID, ref otherBuilding)) continue;

                if (delegateMode)
                {
                    if (DistrictChecker.IsBuildingTransferAllowed(otherBuildingID, ref otherBuilding, material, offer, true))
                    {
                        // let other building handle the offer
                        otherBuildingAI.StartTransfer(otherBuildingID, ref otherBuilding, material, offer);
                        return;
                    }
                }
                else
                {
                    offer.Building = otherBuildingID;
                    offer.Position = otherBuilding.m_position;

                    if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer, true))
                    {
                        // change the target to other building
                        data.Info.m_buildingAI.StartTransfer(buildingID, ref data, material, offer);
                        return;
                    }
                }
            }
        }
    }
}

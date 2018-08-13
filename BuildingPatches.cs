using Harmony;

namespace DistrictServiceLimit
{
    [HarmonyPatch(typeof(HelicopterDepotAI), "StartTransfer")]
    class HelicopterDepotAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(MedicalCenterAI), "StartTransfer")]
    class MedicalCenterAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(HospitalAI), "StartTransfer")]
    class HospitalAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(CemeteryAI), "StartTransfer")]
    class CemeteryAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(PoliceStationAI), "StartTransfer")]
    class PoliceStationAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(FireStationAI), "StartTransfer")]
    class FirestationAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(LandfillSiteAI), "StartTransfer")]
    class LandfillSiteAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(SnowDumpAI), "StartTransfer")]
    class SnowDumpAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(MaintenanceDepotAI), "StartTransfer")]
    class MaintenanceDepotAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(DepotAI), "StartTransfer")]
    class DepotAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }

    [HarmonyPatch(typeof(TaxiStandAI), "StartTransfer")]
    class TaxiStandAIStartTransferPatch
    {
        static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                return true;
            }

            BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer);
            return false;
        }
    }
}

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

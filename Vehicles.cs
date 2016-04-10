using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
    /**
     * Ambulances have capacity for one person only, so this AI mod isn't really necessary for now.
     * The only reason I'm using it is because other mods or custom ambulance vehicles may change this capacity setting.
     */
    public class AmbulanceAIMod : AmbulanceAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }

    public class HearseAIMod : HearseAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }

    public class PoliceCarAIMod : PoliceCarAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }

    public class FireTruckAIMod : FireTruckAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }

    public class GarbageTruckAIMod : GarbageTruckAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }

    public class SnowTruckAIMod : SnowTruckAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }

    public class MaintenanceTruckAIMod : MaintenanceTruckAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }

    public class TaxiAIMod : TaxiAI
    {
        public override void StartTransfer(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }
}

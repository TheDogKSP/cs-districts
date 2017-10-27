using System;
using UnityEngine;
using ColossalFramework;

namespace DistrictServiceLimit
{
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

    public class HelicopterAIMod : HelicopterAI
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

    public class AmbulanceCopterAIMod : AmbulanceCopterAI
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

    public class FireCopterAIMod : FireCopterAI
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

    public class PoliceCopterAIMod : PoliceCopterAI
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

using Harmony;
using UnityEngine;

namespace DistrictServiceLimit
{
    class VehiclePatches
    {
        [HarmonyPatch(typeof(AmbulanceAI), "StartTransfer")]
        class AmbulanceAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(HearseAI), "StartTransfer")]
        class HearseAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(PoliceCarAI), "StartTransfer")]
        class PoliceCarAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    Debug.Log("Vehicle transfer allowed, use regular method.");
                    return true;
                }

                Debug.Log("Vehicle transfer not allowed, don't do anything.");
                return false;
            }
        }

        [HarmonyPatch(typeof(FireTruckAI), "StartTransfer")]
        class FireTruckAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(GarbageTruckAI), "StartTransfer")]
        class GarbageTruckAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(SnowTruckAI), "StartTransfer")]
        class SnowTruckAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(MaintenanceTruckAI), "StartTransfer")]
        class MaintenanceTruckAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(TaxiAI), "StartTransfer")]
        class TaxiAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(HelicopterAI), "StartTransfer")]
        class HelicopterAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(AmbulanceCopterAI), "StartTransfer")]
        class AmbulanceCopterAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(FireCopterAI), "StartTransfer")]
        class FireCopterAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(PoliceCopterAI), "StartTransfer")]
        class PoliceCopterAIStartTransferPatch
        {
            static bool Prefix(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
            {
                if (DistrictChecker.IsVehicleTransferAllowed(vehicleID, ref data, material, offer))
                {
                    return true;
                }

                return false;
            }
        }
    }
}

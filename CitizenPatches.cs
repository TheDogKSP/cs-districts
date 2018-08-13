using Harmony;

namespace DistrictServiceLimit
{
    [HarmonyPatch(typeof(ResidentAI), "StartTransfer")]
    class ResidentAIStartTransferPatch
    {
        static bool Prefix(uint citizenID, ref Citizen data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsCitizenTransferAllowed(citizenID, ref data, material, offer))
            {
                return true;
            }

            return false;
        }
    }
}

using Harmony;

namespace DistrictServiceLimit
{
    [HarmonyPatch(typeof(ResidentAI), "StartTransfer")]
    class ResidentAIStartTransferPatch
    {
        static bool Prefix(uint citizenID, ref Citizen data, TransferManager.TransferReason reason, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsCitizenTransferAllowed(citizenID, ref data, reason, offer))
            {
                return true;
            }

            return false;
        }
    }
}

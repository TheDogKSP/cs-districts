using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
    public class ResidentAIMod : ResidentAI
    {
        public override void StartTransfer(uint citizenID, ref Citizen data,
            TransferManager.TransferReason reason, TransferManager.TransferOffer offer)
        {
            if (DistrictChecker.IsCitizenTransferAllowed(citizenID, ref data, reason, offer))
            {
                base.StartTransfer(citizenID, ref data, reason, offer);
            }
        }
    }
}

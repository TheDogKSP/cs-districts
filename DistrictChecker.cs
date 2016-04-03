using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
	public static class DistrictChecker
	{
		public static bool IsTransferAllowed(Vector3 source, Vector3 destination,
		                                     TransferManager.TransferReason material)
		{
			DistrictManager districtManager = Singleton<DistrictManager>.instance;
			byte srcDistrictIdx = districtManager.GetDistrict(source);
			byte dstDistrictIdx = districtManager.GetDistrict(destination);

			switch(material) {
			case TransferManager.TransferReason.LeaveCity0:
			case TransferManager.TransferReason.LeaveCity1:
			case TransferManager.TransferReason.LeaveCity2:
				return true;
			case TransferManager.TransferReason.DeadMove:
			case TransferManager.TransferReason.GarbageMove:
			case TransferManager.TransferReason.CriminalMove:
			case TransferManager.TransferReason.SnowMove:
				return (dstDistrictIdx == 0 || srcDistrictIdx == dstDistrictIdx);
			default:
				return (srcDistrictIdx == 0 || srcDistrictIdx == dstDistrictIdx);
			}
		}
	}
}

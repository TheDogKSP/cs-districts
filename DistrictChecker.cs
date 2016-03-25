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

	public class MedicalCenterAIMod : MedicalCenterAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Health] Clinic #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data.m_position)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class HospitalAIMod : HospitalAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Health] Hospital #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data.m_position)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class CemeteryAIMod : CemeteryAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Death] Cemetery #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data.m_position)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class PoliceStationAIMod : PoliceStationAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Crime] Station #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data.m_position)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class FireStationAIMod : FireStationAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Fire] Station #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data.m_position)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif
			
			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class LandfillSiteAIMod : LandfillSiteAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Garbage] Landfill #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data.m_position)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class SnowDumpAIMod : SnowDumpAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Snow] Dump site #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data.m_position)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}
}


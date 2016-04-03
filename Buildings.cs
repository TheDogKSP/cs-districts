using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
	public class MedicalCenterAIMod : MedicalCenterAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Health] Clinic #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
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
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
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
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
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
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
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
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
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
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
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
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class MaintenanceDepotAIMod : MaintenanceDepotAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Road] Depot #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}

	public class TaxiStandAIMod : TaxiStandAI
	{
		public override void StartTransfer(ushort buildingID, ref Building data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Road] Depot #{0} queried", buildingID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			if (DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material)) {
				base.StartTransfer(buildingID, ref data, material, offer);
			}
		}
	}
}

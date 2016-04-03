using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
	public class AmbulanceAIMod : AmbulanceAI
	{
		public override void StartTransfer(ushort vehicleID, ref Vehicle data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Heatlh] Ambulance #{0} queried", vehicleID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];

			if (DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material)) {
				base.StartTransfer(vehicleID, ref data, material, offer);
			}
		}
	}

	public class HearseAIMod : HearseAI
	{
		public override void StartTransfer(ushort vehicleID, ref Vehicle data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Death] Hearse #{0} queried", vehicleID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];

			if (DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material)) {
				base.StartTransfer(vehicleID, ref data, material, offer);
			}
		}
	}

	public class PoliceCarAIMod : PoliceCarAI
	{
		public override void StartTransfer(ushort vehicleID, ref Vehicle data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Crime] Police car #{0} queried", vehicleID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];

			if (DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material)) {
				base.StartTransfer(vehicleID, ref data, material, offer);
			}
		}
	}

	public class FireTruckAIMod : FireTruckAI
	{
		public override void StartTransfer(ushort vehicleID, ref Vehicle data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Fire] Fire truck #{0} queried", vehicleID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];

			if (DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material)) {
				base.StartTransfer(vehicleID, ref data, material, offer);
			}
		}
	}

	public class GarbageTruckAIMod : GarbageTruckAI
	{
		public override void StartTransfer(ushort vehicleID, ref Vehicle data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Garbage] Garbage truck #{0} queried", vehicleID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];

			if (DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material)) {
				base.StartTransfer(vehicleID, ref data, material, offer);
			}
		}
	}

	public class SnowTruckAIMod : SnowTruckAI
	{
		public override void StartTransfer(ushort vehicleID, ref Vehicle data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Snow] Snow truck #{0} queried", vehicleID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];

			if (DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material)) {
				base.StartTransfer(vehicleID, ref data, material, offer);
			}
		}
	}

	public class MaintenanceTruckAIMod : MaintenanceTruckAI
	{
		public override void StartTransfer(ushort vehicleID, ref Vehicle data,
		                                   TransferManager.TransferReason material,
		                                   TransferManager.TransferOffer offer)
		{
			#if DEBUG
			Utils.Log(String.Format("[Road] Maintenance truck #{0} queried", vehicleID));
			Utils.Log(String.Format(" - Data: {0}", Utils.ToString(data)));
			Utils.Log(String.Format(" - Offer: {0}\n", Utils.ToString(offer)));
			#endif

			BuildingManager buildingManager = Singleton<BuildingManager>.instance;
			Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];

			if (DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material)) {
				base.StartTransfer(vehicleID, ref data, material, offer);
			}
		}
	}
}

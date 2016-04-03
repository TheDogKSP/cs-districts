using ICities;
using ColossalFramework;

namespace GSteigertDistricts
{
	public class GSteigertMod : IUserMod
	{
		public string Name { get { return "Districts"; } }
		public string Description { get { return "Restricts services within the current district"; } }
	}

	public class CustomLoadingExtension : LoadingExtensionBase
	{
		public override void OnLevelLoaded(LoadMode mode)
		{
			if (mode != LoadMode.NewGame && mode != LoadMode.LoadGame) return;

			ReplaceHelper.ReplaceBuildingAI<MedicalCenterAI, MedicalCenterAIMod>();
			ReplaceHelper.ReplaceBuildingAI<HospitalAI, HospitalAIMod>();
			ReplaceHelper.ReplaceVehicleAI<AmbulanceAI, AmbulanceAIMod>();

			ReplaceHelper.ReplaceBuildingAI<CemeteryAI, CemeteryAIMod>();
			ReplaceHelper.ReplaceVehicleAI<HearseAI, HearseAIMod>();

			ReplaceHelper.ReplaceBuildingAI<PoliceStationAI, PoliceStationAIMod>();
			ReplaceHelper.ReplaceVehicleAI<PoliceCarAI, PoliceCarAIMod>();

			ReplaceHelper.ReplaceBuildingAI<FireStationAI, FireStationAIMod>();
			ReplaceHelper.ReplaceVehicleAI<FireTruckAI, FireTruckAIMod>();

			ReplaceHelper.ReplaceBuildingAI<LandfillSiteAI, LandfillSiteAIMod>();
			ReplaceHelper.ReplaceVehicleAI<GarbageTruckAI, GarbageTruckAIMod>();

			ReplaceHelper.ReplaceBuildingAI<SnowDumpAI, SnowDumpAIMod>();
			ReplaceHelper.ReplaceVehicleAI<SnowTruckAI, SnowTruckAIMod>();

			ReplaceHelper.ReplaceBuildingAI<MaintenanceDepotAI, MaintenanceDepotAIMod>();
			ReplaceHelper.ReplaceVehicleAI<MaintenanceTruckAI, MaintenanceTruckAIMod>();

			ReplaceHelper.ReplaceBuildingAI<TaxiStandAI, TaxiStandAIMod>();
		}
	}
}

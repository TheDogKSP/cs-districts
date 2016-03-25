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
			ReplaceHelper.ReplaceBuildingAI<CemeteryAI, CemeteryAIMod>();
			ReplaceHelper.ReplaceBuildingAI<PoliceStationAI, PoliceStationAIMod>();
			ReplaceHelper.ReplaceBuildingAI<FireStationAI, FireStationAIMod>();
			ReplaceHelper.ReplaceBuildingAI<LandfillSiteAI, LandfillSiteAIMod>();
			ReplaceHelper.ReplaceBuildingAI<SnowDumpAI, SnowDumpAIMod>();
		}
	}
}

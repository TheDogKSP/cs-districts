using System;
using ICities;
using ColossalFramework;

namespace GSteigertDistricts
{
    public class GSteigertMod : IUserMod
    {
        public string Name => "District Service Limit";
        public string Description => "Allows micro-managing the services' coverage area";
    }

    public class CustomLoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.NewGame || mode == LoadMode.LoadGame)
            {
                ActivateMod();
            }
        }

        private void ActivateMod()
        {
            Utils.LogVerbose("[Loading mod]");

            DateTime then = DateTime.Now;

            // replace health buildings and vehicles
            ReplaceHelper.ReplaceBuildingAI<MedicalCenterAI, MedicalCenterAIMod>();
            ReplaceHelper.ReplaceBuildingAI<HospitalAI, HospitalAIMod>();
            ReplaceHelper.ReplaceVehicleAI<AmbulanceAI, AmbulanceAIMod>();
            ReplaceHelper.ReplaceVehicleAI<AmbulanceCopterAI, AmbulanceCopterAIMod>();

            // replace fire buildings and vehicles
            ReplaceHelper.ReplaceBuildingAI<FireStationAI, FireStationAIMod>();
            ReplaceHelper.ReplaceVehicleAI<FireTruckAI, FireTruckAIMod>();
            ReplaceHelper.ReplaceVehicleAI<FireCopterAI, FireCopterAIMod>();

            // replace cemetery buildings and vehicles
            ReplaceHelper.ReplaceBuildingAI<CemeteryAI, CemeteryAIMod>();
            ReplaceHelper.ReplaceVehicleAI<HearseAI, HearseAIMod>();

            // replace police buildings and vehicles
            ReplaceHelper.ReplaceBuildingAI<PoliceStationAI, PoliceStationAIMod>();
            ReplaceHelper.ReplaceVehicleAI<PoliceCarAI, PoliceCarAIMod>();
            ReplaceHelper.ReplaceVehicleAI<PoliceCopterAI, PoliceCopterAIMod>();

            // replace garbage buildings and vehicles
            ReplaceHelper.ReplaceBuildingAI<LandfillSiteAI, LandfillSiteAIMod>();
            ReplaceHelper.ReplaceVehicleAI<GarbageTruckAI, GarbageTruckAIMod>();

            // replace road maintenance buildings and vehicles
            ReplaceHelper.ReplaceBuildingAI<MaintenanceDepotAI, MaintenanceDepotAIMod>();
            ReplaceHelper.ReplaceVehicleAI<MaintenanceTruckAI, MaintenanceTruckAIMod>();
            ReplaceHelper.ReplaceBuildingAI<SnowDumpAI, SnowDumpAIMod>();
            ReplaceHelper.ReplaceVehicleAI<SnowTruckAI, SnowTruckAIMod>();

            // replace taxi buildings and vehicles
            ReplaceHelper.ReplaceBuildingAI<DepotAI, DepotAIMod>();
            ReplaceHelper.ReplaceBuildingAI<TaxiStandAI, TaxiStandAIMod>();
            ReplaceHelper.ReplaceVehicleAI<TaxiAI, TaxiAIMod>();

            // replace police, fire and ambulance helicopters
            ReplaceHelper.ReplaceBuildingAI<HelicopterDepotAI, HelicopterDepotAIMod>();
            ReplaceHelper.ReplaceVehicleAI<HelicopterAI, HelicopterAIMod>();

            // replace the residents
            ReplaceHelper.ReplacePersonAI<ResidentAI, ResidentAIMod>();

            long duration = (DateTime.Now - then).Milliseconds;

            Utils.LogVerbose("[/Loading mod]\n");
            Utils.LogVerbose("District Service Limit mod loaded in " + duration + "ms");
        }
    }
}

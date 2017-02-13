using System;
using ICities;
using ColossalFramework;

namespace GSteigertDistricts
{
    public class GSteigertMod : IUserMod
    {
        public string Name => "District Service Limit";
        public string Description => "Allows setting rules for service dispatching and citizen access for a given district";

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group1 = helper.AddGroup("Service Dispatching");
            group1.AddSpace(10);
            group1.AddCheckbox("Service buildings will only dispatch vehicles to the current district"
                + "\n(e.g.: garbage trucks, police cars, hearses, ambulances, etc)",
                Settings.RestrictServiceDispatching, RestrictServiceDispatchingClicked);
            group1.AddSpace(20);
            group1.AddCheckbox("Materials will only be transfered to other buildings in the current district"
                + "\n(e.g.: garbage, snow, criminals, deceased people, etc)",
                Settings.RestrictMaterialTransfer, RestrictMaterialTransferClicked);
            group1.AddSpace(10);

            UIHelperBase group2 = helper.AddGroup("Direct citizen access");
            group2.AddSpace(5);
            group2.AddCheckbox("Citizens will only attend educational buildings in the current district",
                Settings.RestrictCitizenEducationalAccess, RestrictCitizenEducationalAccessClicked);
            group2.AddCheckbox("Citizens will only attend hospitals and clinics in the current district",
                Settings.RestrictCitizenHealthAccess, RestrictCitizenHealthAccessClicked);
            group2.AddCheckbox("Citizens will only visit parks in the current district",
                Settings.RestrictCitizenParkAccess, RestrictCitizenParkAccessClicked);
            group2.AddCheckbox("Citizens will only do shopping in the current district",
                Settings.RestrictCitizenShoppingAccess, RestrictCitizenShoppingAccessClicked);
            group2.AddCheckbox("Citizens will only work in the current district",
                Settings.RestrictCitizenWorkAccess, RestrictCitizenWorkAccessClicked);
            group2.AddSpace(5);
        }

        private void RestrictServiceDispatchingClicked(bool isChecked)
        {
            Settings.RestrictServiceDispatching = isChecked;
        }

        private void RestrictMaterialTransferClicked(bool isChecked)
        {
            Settings.RestrictMaterialTransfer = isChecked;
        }

        private void RestrictCitizenEducationalAccessClicked(bool isChecked)
        {
            Settings.RestrictCitizenEducationalAccess = isChecked;
        }

        private void RestrictCitizenHealthAccessClicked(bool isChecked)
        {
            Settings.RestrictCitizenHealthAccess = isChecked;
        }

        private void RestrictCitizenParkAccessClicked(bool isChecked)
        {
            Settings.RestrictCitizenParkAccess = isChecked;
        }

        private void RestrictCitizenShoppingAccessClicked(bool isChecked)
        {
            Settings.RestrictCitizenShoppingAccess = isChecked;
        }

        private void RestrictCitizenWorkAccessClicked(bool isChecked)
        {
            Settings.RestrictCitizenWorkAccess = isChecked;
        }
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

        public override void OnLevelUnloading()
        {
            ServiceBuildingOptions.GetInstance().Clear();
            DistrictSelectionPanel.Uninstall();
        }

        private void ActivateMod()
        {
            Utils.LogGeneral("[Loading mod]");

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

            DistrictSelectionPanel.Install();

            long duration = (DateTime.Now - then).Milliseconds;

            Utils.LogGeneral("District Service Limit mod loaded in " + duration + "ms");
            Utils.LogGeneral("[/Loading mod]\n");
        }
    }
}

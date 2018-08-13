using System;
using System.Reflection;
using Harmony;
using ICities;

namespace DistrictServiceLimit
{
    public class GSteigertMod : IUserMod
    {
        public string Name => "District Service Limit";
        public string Description => "Allows setting rules for service dispatching and citizen access for a given district";

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group1 = helper.AddGroup("Restrict Service dispatching");
            group1.AddSpace(10);
            group1.AddCheckbox("Service buildings will only dispatch vehicles to the current district"
                + "\n(e.g.: garbage trucks, police cars, hearses, ambulances, etc)"
                + "\n(does not affect public transport, except Taxis!)",
                Settings.RestrictServiceDispatching, RestrictServiceDispatchingClicked);
            group1.AddSpace(20);
            group1.AddCheckbox("Materials will only be transfered to other buildings in the current district"
                + "\n(e.g.: garbage, snow, criminals, deceased people, etc)",
                Settings.RestrictMaterialTransfer, RestrictMaterialTransferClicked);
            group1.AddSpace(10);

            UIHelperBase group2 = helper.AddGroup("Restrict Citizen access\n(This is an evil restriction of personal freedom!)");
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

            UIHelperBase group3 = helper.AddGroup("Other settings");
            group3.AddSpace(5);
            group3.AddCheckbox("Display the additional coverage panel on the left side",
                Settings.DisplayBuildingOptionsOnLeftSide, DisplayBuildingOptionsOnLeftSideClicked);
            group3.AddSpace(5);
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

        private void DisplayBuildingOptionsOnLeftSideClicked(bool isChecked)
        {
            Settings.DisplayBuildingOptionsOnLeftSide = isChecked;
        }
    }

    public class CustomLoadingExtension : LoadingExtensionBase
    {
        private string harmonyId = "gsteigert.dsl3";
        private HarmonyInstance harmonyInstance;

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
            harmonyInstance.UnpatchAll(harmonyId);
            harmonyInstance = null;
        }

        private void ActivateMod()
        {
            Utils.LogGeneral("[Loading mod]");

            DateTime then = DateTime.Now;

            harmonyInstance = HarmonyInstance.Create(harmonyId);
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());

            Utils.LogGeneral("District Service Limit installing panel...");
            DistrictSelectionPanel.Install();

            long duration = (DateTime.Now - then).Milliseconds;

            Utils.LogGeneral("District Service Limit mod loaded in " + duration + "ms");
            Utils.LogGeneral("[/Loading mod]\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ColossalFramework;

namespace GSteigertDistricts
{
    public class HelicopterDepotAIMod : HelicopterDepotAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class MedicalCenterAIMod : MedicalCenterAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class HospitalAIMod : HospitalAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class CemeteryAIMod : CemeteryAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class PoliceStationAIMod : PoliceStationAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class FireStationAIMod : FireStationAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class LandfillSiteAIMod : LandfillSiteAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class SnowDumpAIMod : SnowDumpAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class MaintenanceDepotAIMod : MaintenanceDepotAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class DepotAIMod : DepotAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public class TaxiStandAIMod : TaxiStandAI
    {
        private bool triggered;

        public override void StartTransfer(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (triggered)
            {
                return;
            }
            else if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer))
            {
                base.StartTransfer(buildingID, ref data, material, offer);
                triggered = true;
            }
            else
            {
                if (BuildingHelper.delegateToAnotherBuilding(buildingID, ref data, material, offer))
                {
                    triggered = true;
                }
            }
        }

        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            triggered = false;
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
        }
    }

    public static class BuildingHelper
    {
        /**
         * Workaround a scenario where there is another building closer to the offer's position
         * that doesn't belong to the same district, causing the offer never to be handled.
         */
        internal static bool delegateToAnotherBuilding(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
#if DEBUG
            Utils.LogBuilding(" - Searching another building to delegate");
#endif

            Type aiType = data.Info.m_buildingAI.GetType().BaseType;
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            ItemClass.Service service = data.Info.GetService();
            FastList<ushort> buildings = buildingManager.GetServiceBuildings(service);

            bool delegateMode = (material == TransferManager.TransferReason.Fire
                || material == TransferManager.TransferReason.Crime
                || material == TransferManager.TransferReason.Dead
                || material == TransferManager.TransferReason.Fire
                || material == TransferManager.TransferReason.Fire2
                || material == TransferManager.TransferReason.Garbage
                || material == TransferManager.TransferReason.RoadMaintenance
                || material == TransferManager.TransferReason.Sick
                || material == TransferManager.TransferReason.Sick2
                || material == TransferManager.TransferReason.Taxi);

            if (buildings == null)
            {
                return false;
            }

            for (int index = 0; index < buildings.m_size; index++)
            {
                ushort otherBuildingID = buildings.m_buffer[index];
                if (buildingID == otherBuildingID) continue;

                Building otherBuilding = buildingManager.m_buildings.m_buffer[otherBuildingID];
                if ((otherBuilding.m_flags & Building.Flags.Active) == Building.Flags.None) continue;
                if ((otherBuilding.m_problems & Notification.Problem.Emptying) != Notification.Problem.None) continue;
                if ((otherBuilding.m_problems & Notification.Problem.EmptyingFinished) != Notification.Problem.None) continue;

                BuildingAI otherBuildingAI = (BuildingAI)otherBuilding.Info.m_buildingAI;
                if (!aiType.IsAssignableFrom(otherBuildingAI.GetType())) continue;
                if (otherBuildingAI.IsFull(otherBuildingID, ref otherBuilding)) continue;
                if (!hasSpareVehicles(otherBuildingID, ref otherBuilding, otherBuildingAI)) continue;

                if (delegateMode)
                {
                    if (DistrictChecker.IsBuildingTransferAllowed(otherBuildingID, ref otherBuilding, material, offer, true))
                    {
                        // let other building handle the offer
                        otherBuildingAI.StartTransfer(otherBuildingID, ref otherBuilding, material, offer);
                        return true;
                    }
                }
                else
                {
                    offer.Building = otherBuildingID;
                    offer.Position = otherBuilding.m_position;

                    if (DistrictChecker.IsBuildingTransferAllowed(buildingID, ref data, material, offer, true))
                    {
                        // change the target to other building
                        data.Info.m_buildingAI.StartTransfer(buildingID, ref data, material, offer);
                        return true;
                    }
                }
            }

            return false;
        }

        internal static bool hasSpareVehicles(ushort buildingID, ref Building data, BuildingAI buildingAI)
        {
            string stats = buildingAI.GetLocalizedStats(buildingID, ref data);
            if (stats == null)
            {
                return false;
            }

            stats = stats.Substring(stats.LastIndexOf(':') + 2);
            Match match = new Regex(@"(\d+)\D+(\d+)").Match(stats);
            if (match.Success)
            {
                int vehiclesInUse = int.Parse(match.Groups[1].Value);
                int vehiclesAvailable = int.Parse(match.Groups[2].Value);
                BuildingManager buildingManager = Singleton<BuildingManager>.instance;
#if DEBUG
                string buildingName = buildingManager.GetBuildingName(buildingID, InstanceID.Empty);
                Utils.LogBuilding(String.Format("   - {0} - Capacity check: {1}/{2}", buildingName, vehiclesInUse, vehiclesAvailable));
#endif
                return (vehiclesInUse < vehiclesAvailable);
            }
            else
            {
                return false;
            }
        }
    }
}

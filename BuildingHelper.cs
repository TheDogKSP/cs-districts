using ColossalFramework;
using System;
using System.Text.RegularExpressions;

namespace DistrictServiceLimit
{
    /// <summary>
    /// Help functions
    /// </summary>
    public static class BuildingHelper
    {
        /**
         * Workaround a scenario where there is another building closer to the offer's position
         * that doesn't belong to the same district, causing the offer never to be handled.
         */
        internal static bool delegateToAnotherBuilding(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            Utils.LogBuilding(" - Searching another building to delegate");

            Type aiType = data.Info.m_buildingAI.GetType().BaseType;
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            ItemClass.Service service = data.Info.GetService();
            FastList<ushort> buildings = buildingManager.GetServiceBuildings(service);

            bool delegateMode = (material == TransferManager.TransferReason.Fire
                || material == TransferManager.TransferReason.Crime
                || material == TransferManager.TransferReason.Dead
                || material == TransferManager.TransferReason.Fire2
                || material == TransferManager.TransferReason.Garbage
                || material == TransferManager.TransferReason.RoadMaintenance
                || material == TransferManager.TransferReason.Snow
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
                if (delegateMode && !hasSpareVehicles(otherBuildingID, ref otherBuilding, otherBuildingAI)) continue;   //target delegation does not need vehicle from target!

                if (delegateMode)
                {
                    if (DistrictChecker.IsBuildingTransferAllowed(otherBuildingID, ref otherBuilding, material, offer, true))
                    {
                        // let other building handle the offer
                        Utils.LogBuilding(" - delegation success, starting transfer (origin delegated)");
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
                        Utils.LogBuilding(" - delegation success, starting transfer (target changed)");
                        data.Info.m_buildingAI.StartTransfer(buildingID, ref data, material, offer);
                        return true;
                    }
                }
            }

            Utils.LogBuilding(" - delegation finally failed, request NOT handled!");

            return false;
        }

        internal static bool hasSpareVehicles(ushort buildingID, ref Building data, BuildingAI buildingAI)
        {
            string stats = buildingAI.GetLocalizedStats(buildingID, ref data);
            if ((stats == null) || (stats == ""))
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

                string buildingName = buildingManager.GetBuildingName(buildingID, InstanceID.Empty);
                Utils.LogBuilding(String.Format("   - {0} - Capacity check: {1}/{2}, result: {3}", buildingName, vehiclesInUse, vehiclesAvailable, (vehiclesInUse < vehiclesAvailable)));

                return (vehiclesInUse < vehiclesAvailable);
            }
            else
            {
                return false;
            }
        }
    }
}

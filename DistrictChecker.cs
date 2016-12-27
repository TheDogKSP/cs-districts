using System;
using UnityEngine;
using ColossalFramework;

namespace GSteigertDistricts
{
    public static class DistrictChecker
    {
        private static DistrictManager districtManager = Singleton<DistrictManager>.instance;
        private static BuildingManager buildingManager = Singleton<BuildingManager>.instance;
        private static CitizenManager citizenManager = Singleton<CitizenManager>.instance;

        private static bool IsTransferAllowed(Vector3 source, Vector3 destination,
            TransferManager.TransferReason material, bool onFoot)
        {
            byte srcDistrict = districtManager.GetDistrict(source);
            byte dstDistrict = districtManager.GetDistrict(destination);

            if (onFoot)
            {
                switch (material)
                {
                    // resident walking to use some service
                    case TransferManager.TransferReason.Sick:
                    case TransferManager.TransferReason.Student1:
                    case TransferManager.TransferReason.Student2:
                    case TransferManager.TransferReason.Student3:
                        return (dstDistrict == 0 || srcDistrict == dstDistrict);

                    default:
                        return true;
                }
            }
            else
            {
                switch (material)
                {
                    // vehicle fetching something
                    case TransferManager.TransferReason.Garbage:
                    case TransferManager.TransferReason.Crime:
                    case TransferManager.TransferReason.Sick:
                    case TransferManager.TransferReason.Sick2:
                    case TransferManager.TransferReason.Dead:
                    case TransferManager.TransferReason.Fire:
                    case TransferManager.TransferReason.Fire2:
                    case TransferManager.TransferReason.Taxi:
                    case TransferManager.TransferReason.Snow:
                    case TransferManager.TransferReason.RoadMaintenance:
                    case TransferManager.TransferReason.CriminalMove:
                        return (srcDistrict == 0 || srcDistrict == dstDistrict);

                    // vehicle freeing building capacity
                    case TransferManager.TransferReason.DeadMove:
                    case TransferManager.TransferReason.GarbageMove:
                    case TransferManager.TransferReason.SnowMove:
                        return (dstDistrict == 0 || srcDistrict == dstDistrict);

                    default:
                        return true;
                }
            }
        }

        public static bool IsBuildingTransferAllowed(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer,
            bool summarizedLog = false)
        {
            bool allowed = DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material, false);

#if DEBUG
            if (summarizedLog)
            {
                Utils.LogVerbose(String.Format("   - Building #{0} queried (allowed: {1})", buildingID, allowed));
            }
            else
            {
                string srcBuilding = buildingManager.GetBuildingName(buildingID, InstanceID.Empty);
                string dstBuilding = buildingManager.GetBuildingName(offer.Building, InstanceID.Empty);
                string dstCitizen = citizenManager.GetCitizenName(offer.Citizen);
                string srcDistrict = FindDistrictName(data.m_position);
                string dstDistrict = FindDistrictName(offer.Position);

                Utils.LogVerbose("------------------------------------------------------------");
                Utils.LogVerbose(String.Format("Building #{0} queried (allowed: {1})", buildingID, allowed));
                Utils.LogVerbose(String.Format(" - Offer: {0}", Utils.ToString(offer)));
                Utils.LogVerbose(String.Format(" - Origin: {0}", srcBuilding));
                Utils.LogVerbose(String.Format(" - Destination: building='{0}', citizen='{1}'", dstBuilding, dstCitizen));
                Utils.LogVerbose(String.Format(" - District: {0} -> {1}", srcDistrict, dstDistrict));
            }
#endif

            return allowed;
        }

        public static bool IsVehicleTransferAllowed(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            bool allowed = DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material, false);

#if DEBUG
            string srcBuilding = buildingManager.GetBuildingName(data.m_sourceBuilding, InstanceID.Empty);
            string dstBuilding = buildingManager.GetBuildingName(offer.Building, InstanceID.Empty);
            string dstCitizen = citizenManager.GetCitizenName(offer.Citizen);
            string srcDistrict = FindDistrictName(building.m_position);
            string dstDistrict = FindDistrictName(offer.Position);

            Utils.LogVerbose("------------------------------------------------------------");
            Utils.LogVerbose(String.Format("Vehicle #{0} queried (allowed: {1})", vehicleID, allowed));
            Utils.LogVerbose(String.Format(" - Offer: {0}", Utils.ToString(offer)));
            Utils.LogVerbose(String.Format(" - Origin: {0}", srcBuilding));
            Utils.LogVerbose(String.Format(" - Destination: building='{0}', citizen='{1}'", dstBuilding, dstCitizen));
            Utils.LogVerbose(String.Format(" - District: {0} -> {1}", srcDistrict, dstDistrict));
#endif

            return allowed;
        }

        public static bool IsPersonTransferAllowed(uint citizenID, ref Citizen data,
            TransferManager.TransferReason reason, TransferManager.TransferOffer offer)
        {
            Building building = buildingManager.m_buildings.m_buffer[(int)data.m_homeBuilding];
            bool allowed = DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, reason, true);
            return allowed;
        }

        private static string FindDistrictName(Vector3 position)
        {
            byte districtId = districtManager.GetDistrict(position);
            string districtName = districtManager.GetDistrictName(districtId);
            return (districtName == null ? "<undefined>" : districtName) + ":" + districtId;
        }
    }
}

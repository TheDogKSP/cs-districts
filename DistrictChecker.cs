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
            TransferManager.TransferReason material)
        {
            byte srcDistrictIdx = districtManager.GetDistrict(source);
            byte dstDistrictIdx = districtManager.GetDistrict(destination);

            switch (material)
            {
                case TransferManager.TransferReason.LeaveCity0:
                case TransferManager.TransferReason.LeaveCity1:
                case TransferManager.TransferReason.LeaveCity2:
                    return true;
                case TransferManager.TransferReason.DeadMove:
                case TransferManager.TransferReason.GarbageMove:
                case TransferManager.TransferReason.CriminalMove:
                case TransferManager.TransferReason.SnowMove:
                    return (dstDistrictIdx == 0 || srcDistrictIdx == dstDistrictIdx);
                default:
                    return (srcDistrictIdx == 0 || srcDistrictIdx == dstDistrictIdx);
            }
        }

        public static bool IsBuildingTransferAllowed(ushort buildingID, ref Building data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            bool allowed = DistrictChecker.IsTransferAllowed(data.m_position, offer.Position, material);

#if DEBUG
            string srcBuilding = buildingManager.GetBuildingName(buildingID, InstanceID.Empty);
            string dstBuilding = buildingManager.GetBuildingName(offer.Building, InstanceID.Empty);
            string srcDistrict = districtManager.GetDistrictName(districtManager.GetDistrict(data.m_position));
            string dstDistrict = districtManager.GetDistrictName(districtManager.GetDistrict(offer.Position));

            Utils.Log("------------------------------------------------------------");
            Utils.Log(String.Format("Building #{0} queried (allowed: {1})", buildingID, allowed));
            Utils.Log(String.Format(" - Offer: {0}", Utils.ToString(offer)));
            Utils.Log(String.Format(" - Origin: '{0}'", srcBuilding));
            Utils.Log(String.Format(" - Destination: '{0}'", dstBuilding));
            Utils.Log(String.Format(" - District: '{0}' -> '{1}'", srcDistrict, dstDistrict));
#endif

            return allowed;
        }

        public static bool IsVehicleTransferAllowed(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            Building building = buildingManager.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            bool allowed = DistrictChecker.IsTransferAllowed(building.m_position, offer.Position, material);

#if DEBUG
            string srcBuilding = buildingManager.GetBuildingName(data.m_sourceBuilding, InstanceID.Empty);
            string dstBuilding = buildingManager.GetBuildingName(offer.Building, InstanceID.Empty);
            string srcDistrict = districtManager.GetDistrictName(districtManager.GetDistrict(building.m_position));
            string dstDistrict = districtManager.GetDistrictName(districtManager.GetDistrict(offer.Position));
            string dstCitizen = citizenManager.GetCitizenName(offer.Citizen);

            Utils.Log("------------------------------------------------------------");
            Utils.Log(String.Format("Vehicle #{0} queried (allowed: {1})", vehicleID, allowed));
            Utils.Log(String.Format(" - Offer: {0}", Utils.ToString(offer)));
            Utils.Log(String.Format(" - Origin: '{0}'", srcBuilding));
            Utils.Log(String.Format(" - Destination: building='{0}', citizen='{1}'", dstBuilding, dstCitizen));
            Utils.Log(String.Format(" - District: '{0}' -> '{1}'", srcDistrict, dstDistrict));
#endif

            return allowed;
        }
    }
}

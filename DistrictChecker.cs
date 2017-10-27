using System;
using UnityEngine;
using ColossalFramework;

namespace DistrictServiceLimit
{
    internal static class DistrictChecker
    {

        /// <summary>
        /// Eventual handler of all service requests
        /// </summary>
        private static bool IsTransferAllowed(Vector3 source, Vector3 destination, ushort buildingID, ushort destBuildingID, TransferManager.TransferReason reason, bool isResidentTransfer)
        {
            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            byte srcDistrict = districtManager.GetDistrict(source);
            byte dstDistrict = districtManager.GetDistrict(destination);

            if (srcDistrict != 0 && !IsActive(srcDistrict))
            {
                Utils.LogGeneral($"IsTransferAllowed: srcDistrict {srcDistrict} not active, setting to zero!");
                srcDistrict = 0;
            }

            if (dstDistrict != 0 && !IsActive(dstDistrict))
            {
                Utils.LogGeneral($"IsTransferAllowed: dstDistrict {dstDistrict} not active, setting to zero!");
                dstDistrict = 0;
            }

            // check whether this transfer represents a resident going out to do something
            if (isResidentTransfer)
            {
                switch (reason)
                {
                    // resident going to a hospital or clinic
                    case TransferManager.TransferReason.Sick:
                    case TransferManager.TransferReason.Sick2:
                        return Settings.RestrictCitizenHealthAccess ?
                            (dstDistrict == 0 || srcDistrict == dstDistrict) : true;

                    // resident going to an educational building
                    case TransferManager.TransferReason.Student1:
                    case TransferManager.TransferReason.Student2:
                    case TransferManager.TransferReason.Student3:
                        return Settings.RestrictCitizenEducationalAccess ?
                            (dstDistrict == 0 || srcDistrict == dstDistrict) : true;

                    // resident going to a park
                    case TransferManager.TransferReason.Entertainment:
                    case TransferManager.TransferReason.EntertainmentB:
                    case TransferManager.TransferReason.EntertainmentC:
                    case TransferManager.TransferReason.EntertainmentD:
                        return Settings.RestrictCitizenParkAccess ?
                            (dstDistrict == 0 || srcDistrict == dstDistrict) : true;

                    // resident going to a shop
                    case TransferManager.TransferReason.Shopping:
                    case TransferManager.TransferReason.ShoppingB:
                    case TransferManager.TransferReason.ShoppingC:
                    case TransferManager.TransferReason.ShoppingD:
                    case TransferManager.TransferReason.ShoppingE:
                    case TransferManager.TransferReason.ShoppingF:
                    case TransferManager.TransferReason.ShoppingG:
                    case TransferManager.TransferReason.ShoppingH:
                        return Settings.RestrictCitizenShoppingAccess ?
                            (dstDistrict == 0 || srcDistrict == dstDistrict) : true;

                    // resident going to work
                    case TransferManager.TransferReason.Worker0:
                    case TransferManager.TransferReason.Worker1:
                    case TransferManager.TransferReason.Worker2:
                    case TransferManager.TransferReason.Worker3:
                        return Settings.RestrictCitizenWorkAccess ?
                            (dstDistrict == 0 || srcDistrict == dstDistrict) : true;

                    default:
                        return true;
                }
            }
            else
            {
                ServiceBuildingOptions opts = ServiceBuildingOptions.GetInstance();
                switch (reason)
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
                        return !Settings.RestrictServiceDispatching ? true :
                            (srcDistrict == 0
                                || srcDistrict == dstDistrict
                                || opts.IsTargetCovered(buildingID, dstDistrict));

                    // vehicle freeing building capacity
                    case TransferManager.TransferReason.DeadMove:
                    case TransferManager.TransferReason.GarbageMove:
                    case TransferManager.TransferReason.SnowMove:
                        return !Settings.RestrictMaterialTransfer ? true :
                            (dstDistrict == 0
                                || srcDistrict == dstDistrict
                                || opts.IsTargetCovered(destBuildingID, srcDistrict));  //inverted logic: destination must cover source of move!

                    // ignore prisons
                    case TransferManager.TransferReason.CriminalMove:
                        return true;

                    default:
                        return true;
                }
            }
        }


        /// <summary>
        /// Building Service requests wrapper
        /// </summary>
        public static bool IsBuildingTransferAllowed(ushort buildingID, ref Building data,
            TransferManager.TransferReason reason, TransferManager.TransferOffer offer,
            bool summarizedLog = false)
        {
            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            CitizenManager citizenManager = Singleton<CitizenManager>.instance;
            Building srcBuilding = buildingManager.m_buildings.m_buffer[(int)buildingID];
            Building dstBuilding = buildingManager.m_buildings.m_buffer[(int)offer.Building];

            //fix for services based on resident instead of building:
            if (offer.Building == 0 && offer.Citizen != 0)
                dstBuilding = buildingManager.m_buildings.m_buffer[(int)citizenManager.m_citizens.m_buffer[offer.Citizen].GetBuildingByLocation()];

            string srcBuildingName = buildingManager.GetBuildingName(buildingID, InstanceID.Empty);
            string dstBuildingName = buildingManager.GetBuildingName(offer.Building, InstanceID.Empty);
            string dstCitizenName = citizenManager.GetCitizenName(offer.Citizen);
            string srcDistrictName = FindDistrictName(srcBuilding.m_position);
            string dstDistrictName = FindDistrictName(dstBuilding.m_position);

            bool allowed = DistrictChecker.IsTransferAllowed(data.m_position, dstBuilding.m_position, buildingID, offer.Building, reason, false);

#if DEBUG
            if (summarizedLog)
            {
                Utils.LogBuilding(String.Format("   - Building #{0} queried (allowed: {1})", buildingID, allowed));
            }
            else
            {
                Utils.LogBuilding("------------------------------------------------------------"
                    + String.Format("\nBuilding #{0} queried (allowed: {1})", buildingID, allowed)
                    + String.Format("\n - Transfer: reason={0}, offer={1}", reason, Utils.ToString(offer))
                    + String.Format("\n - Origin: {0}", srcBuildingName)
                    + String.Format("\n - Destination: building='{0}', citizen='{1}'", dstBuildingName, dstCitizenName)
                    + String.Format("\n - District: {0} -> {1}", srcDistrictName, dstDistrictName));
            }
#endif
            return allowed;
        }


        /// <summary>
        /// Vehicle Service requests wrapper
        /// </summary>
        public static bool IsVehicleTransferAllowed(ushort vehicleID, ref Vehicle data,
            TransferManager.TransferReason reason, TransferManager.TransferOffer offer)
        {
            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            CitizenManager citizenManager = Singleton<CitizenManager>.instance;
            ushort buildingID = data.m_sourceBuilding;
            Building srcBuilding = buildingManager.m_buildings.m_buffer[(int)buildingID];
            Building dstBuilding = buildingManager.m_buildings.m_buffer[(int)offer.Building];

            //fix for services based on resident instead of building:
            if (offer.Building == 0 && offer.Citizen != 0)
                dstBuilding = buildingManager.m_buildings.m_buffer[(int)citizenManager.m_citizens.m_buffer[offer.Citizen].GetBuildingByLocation()];

            string srcBuildingName = buildingManager.GetBuildingName(buildingID, InstanceID.Empty);
            string dstBuildingName = buildingManager.GetBuildingName(offer.Building, InstanceID.Empty);
            string dstCitizenName = citizenManager.GetCitizenName(offer.Citizen);
            string srcDistrictName = FindDistrictName(srcBuilding.m_position);
            string dstDistrictName = FindDistrictName(dstBuilding.m_position);

            bool allowed = DistrictChecker.IsTransferAllowed(srcBuilding.m_position, dstBuilding.m_position, buildingID, offer.Building, reason, false);

#if DEBUG
            Utils.LogVehicle("------------------------------------------------------------"
                + String.Format("\nVehicle #{0} queried (allowed: {1})", vehicleID, allowed)
                + String.Format("\n - Transfer: reason={0}, offer={1}", reason, Utils.ToString(offer))
                + String.Format("\n - Origin: {0}", srcBuildingName)
                + String.Format("\n - Destination: building='{0}', citizen='{1}'", dstBuildingName, dstCitizenName)
                + String.Format("\n - District: {0} -> {1}", srcDistrictName, dstDistrictName));
#endif
            return allowed;
        }


        /// <summary>
        /// Citizen activity requests wrapper
        /// </summary>
        public static bool IsCitizenTransferAllowed(uint citizenID, ref Citizen data,
            TransferManager.TransferReason reason, TransferManager.TransferOffer offer)
        {
            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            BuildingManager buildingManager = Singleton<BuildingManager>.instance;
            CitizenManager citizenManager = Singleton<CitizenManager>.instance;
            ushort buildingID = data.m_homeBuilding;
            Building srcBuilding = buildingManager.m_buildings.m_buffer[(int)buildingID];
            Building dstBuilding = buildingManager.m_buildings.m_buffer[(int)offer.Building];

            string srcBuildingName = buildingManager.GetBuildingName(buildingID, InstanceID.Empty);
            string dstBuildingName = buildingManager.GetBuildingName(offer.Building, InstanceID.Empty);
            string dstCitizenName = citizenManager.GetCitizenName(offer.Citizen);
            string srcDistrictName = FindDistrictName(srcBuilding.m_position);
            string dstDistrictName = FindDistrictName(dstBuilding.m_position);

            bool allowed = DistrictChecker.IsTransferAllowed(srcBuilding.m_position, dstBuilding.m_position, buildingID, offer.Building, reason, true);

#if DEBUG
            Utils.LogCitizen("------------------------------------------------------------"
                + String.Format("\nCitizen #{0} queried (allowed: {1})", citizenID, allowed)
                + String.Format("\n - Transfer: reason={0}, offer={1}", reason, Utils.ToString(offer))
                + String.Format("\n - Origin: {0}", srcBuildingName)
                + String.Format("\n - Destination: building='{0}', citizen='{1}'", dstBuildingName, dstCitizenName)
                + String.Format("\n - District: {0} -> {1}", srcDistrictName, dstDistrictName));
#endif
            return allowed;
        }


        /// <summary>
        /// get name of district by world position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private static string FindDistrictName(Vector3 position)
        {
            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            byte districtId = districtManager.GetDistrict(position);
            string districtName = districtManager.GetDistrictName(districtId);
            return (districtName == null || districtName == "") ? ("<undefined>:"+districtId) : (districtName + ":"+districtId);
        }


        /// <summary>
        /// check if district is a legal and active district
        /// </summary>
        /// <param name="districtID"></param>
        /// <returns></returns>
        public static bool IsActive(byte districtID)
        {
            if (districtID == 0)
            {
                return false;
            }

            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            if (districtManager.m_districts.m_buffer[districtID].m_flags == District.Flags.None)
            {
                return false;
            }

            return true;
        }

    }
}

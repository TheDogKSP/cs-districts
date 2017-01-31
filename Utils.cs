using System;
using System.IO;
using System.ComponentModel;
using System.Threading;
using ColossalFramework.IO;
using ColossalFramework.Plugins;

namespace GSteigertDistricts
{
    internal static class Utils
    {
        private static object logLock = new object();
        private static string generalLogPath;
        private static string buildingLogPath;
        private static string vehicleLogPath;
        private static string citizenLogPath;

        static Utils()
        {
            generalLogPath = Path.Combine(DataLocation.localApplicationData, "dsl-general.log");
            buildingLogPath = Path.Combine(DataLocation.localApplicationData, "dsl-building.log");
            vehicleLogPath = Path.Combine(DataLocation.localApplicationData, "dsl-vehicle.log");
            citizenLogPath = Path.Combine(DataLocation.localApplicationData, "dsl-citizen.log");

            File.Delete(generalLogPath);
            File.Delete(buildingLogPath);
            File.Delete(vehicleLogPath);
            File.Delete(citizenLogPath);

            LogInfo("Writing verbose logs in: " + DataLocation.localApplicationData);
        }

        public static String ToString(object obj)
        {
            string result = "";
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);
                result += name + "[" + value + "] ";
            }
            return result;
        }

        public static void LogGeneral(String message)
        {
            try
            {
                Monitor.Enter(logLock);
                using (StreamWriter w = File.AppendText(generalLogPath))
                {
                    w.WriteLine(message);
                }
            }
            finally
            {
                Monitor.Exit(logLock);
            }
        }

        public static void LogBuilding(String message)
        {
            try
            {
                Monitor.Enter(logLock);
                using (StreamWriter w = File.AppendText(buildingLogPath))
                {
                    w.WriteLine(message);
                }
            }
            finally
            {
                Monitor.Exit(logLock);
            }
        }

        public static void LogVehicle(String message)
        {
            try
            {
                Monitor.Enter(logLock);
                using (StreamWriter w = File.AppendText(vehicleLogPath))
                {
                    w.WriteLine(message);
                }
            }
            finally
            {
                Monitor.Exit(logLock);
            }
        }

        public static void LogCitizen(String message)
        {
            try
            {
                Monitor.Enter(logLock);
                using (StreamWriter w = File.AppendText(citizenLogPath))
                {
                    w.WriteLine(message);
                }
            }
            finally
            {
                Monitor.Exit(logLock);
            }
        }

        public static void LogInfo(string message)
        {
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message,
                "[District Service Limit] " + message);
        }
    }
}

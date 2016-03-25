using System;
using System.IO;
using System.ComponentModel;
using System.Threading;
using ColossalFramework.IO;

namespace GSteigertDistricts
{
	public static class Utils
	{		
#if DEBUG
		private static object logLock = new object();
#endif

		static Utils()
		{
			File.Delete(GetLogFilePath());
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

		private static string GetLogFilePath()
		{
			return Path.Combine(DataLocation.localApplicationData, "gsteigert-districts.log");
		}

		public static void Log(String message) {
#if DEBUG
			try	{
				Monitor.Enter(logLock);
				using (StreamWriter w = File.AppendText(GetLogFilePath())) {
					w.WriteLine(message);
				}
			} finally {
				Monitor.Exit(logLock);
			}
#endif
		}
	}
}

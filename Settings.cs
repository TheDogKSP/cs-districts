using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSteigertDistricts
{
    internal static class Settings
    {
        private static bool? restrictServiceDispatching;
        private static bool? restrictMaterialTransfer;
        private static bool? restrictCitizenEducationalAccess;
        private static bool? restrictCitizenHealthAccess;
        private static bool? restrictCitizenParkAccess;
        private static bool? restrictCitizenShoppingAccess;
        private static bool? restrictCitizenWorkAccess;

        public static bool RestrictServiceDispatching
        {
            get
            {
                if (!restrictServiceDispatching.HasValue)
                {
                    restrictServiceDispatching = (PlayerPrefs.GetInt("DSL_RSD", 1) == 1);
                }
                return restrictServiceDispatching.Value;
            }
            set
            {
                PlayerPrefs.SetInt("DSL_RSD", value ? 1 : 0);
                restrictServiceDispatching = value;
            }
        }

        public static bool RestrictMaterialTransfer
        {
            get
            {
                if (!restrictMaterialTransfer.HasValue)
                {
                    restrictMaterialTransfer = (PlayerPrefs.GetInt("DSL_RMT", 1) == 1);
                }
                return restrictMaterialTransfer.Value;
            }
            set
            {
                PlayerPrefs.SetInt("DSL_RMT", value ? 1 : 0);
                restrictMaterialTransfer = value;
            }
        }

        public static bool RestrictCitizenEducationalAccess
        {
            get
            {
                if (!restrictCitizenEducationalAccess.HasValue)
                {
                    restrictCitizenEducationalAccess = (PlayerPrefs.GetInt("DSL_RCEA", 0) == 1);
                }
                return restrictCitizenEducationalAccess.Value;
            }
            set
            {
                PlayerPrefs.SetInt("DSL_RCEA", value ? 1 : 0);
                restrictCitizenEducationalAccess = value;
            }
        }

        public static bool RestrictCitizenHealthAccess
        {
            get
            {
                if (!restrictCitizenHealthAccess.HasValue)
                {
                    restrictCitizenHealthAccess = (PlayerPrefs.GetInt("DSL_RCHA", 0) == 1);
                }
                return restrictCitizenHealthAccess.Value;
            }
            set
            {
                PlayerPrefs.SetInt("DSL_RCHA", value ? 1 : 0);
                restrictCitizenHealthAccess = value;
            }
        }

        public static bool RestrictCitizenParkAccess
        {
            get
            {
                if (!restrictCitizenParkAccess.HasValue)
                {
                    restrictCitizenParkAccess = (PlayerPrefs.GetInt("DSL_RCPA", 0) == 1);
                }
                return restrictCitizenParkAccess.Value;
            }
            set
            {
                PlayerPrefs.SetInt("DSL_RCPA", value ? 1 : 0);
                restrictCitizenParkAccess = value;
            }
        }

        public static bool RestrictCitizenShoppingAccess
        {
            get
            {
                if (!restrictCitizenShoppingAccess.HasValue)
                {
                    restrictCitizenShoppingAccess = (PlayerPrefs.GetInt("DSL_RCSA", 0) == 1);
                }
                return restrictCitizenShoppingAccess.Value;
            }
            set
            {
                PlayerPrefs.SetInt("DSL_RCSA", value ? 1 : 0);
                restrictCitizenShoppingAccess = value;
            }
        }

        public static bool RestrictCitizenWorkAccess
        {
            get
            {
                if (!restrictCitizenWorkAccess.HasValue)
                {
                    restrictCitizenWorkAccess = (PlayerPrefs.GetInt("DSL_RCWA", 0) == 1);
                }
                return restrictCitizenWorkAccess.Value;
            }
            set
            {
                PlayerPrefs.SetInt("DSL_RCWA", value ? 1 : 0);
                restrictCitizenWorkAccess = value;
            }
        }
    }
}

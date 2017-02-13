using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.IO;
using ICities;
using ColossalFramework;
using ColossalFramework.IO;
using ColossalFramework.UI;
using ColossalFramework.Math;
using ColossalFramework.Globalization;
using UnityEngine;

namespace GSteigertDistricts
{
    class DistrictSelectionPanel : UIPanel
    {
        private CityServiceWorldInfoPanel basePanel;
        private Dictionary<byte, string> districts;
        private FieldInfo instanceIdFieldInfo;

        private UILabel title;
        private UIScrollablePanel scroller;

        private ushort lastBuildingID;
        private bool displayRequested;
        private int updateCount;

        public DistrictSelectionPanel()
        {
            districts = new Dictionary<byte, string>();
        }

        public override void Start()
        {
            base.Start();

            this.autoLayout = true;
            this.autoLayoutStart = LayoutStart.TopLeft;
            this.autoLayoutDirection = LayoutDirection.Vertical;
            this.autoLayoutPadding = new RectOffset(10, 10, 10, 10);
            this.autoSize = false;
            this.width = 220;
            this.height = 285;
            this.backgroundSprite = "GenericPanel";

            Hide();

            title = AddUIComponent<UILabel>();
            title.width = 220;
            title.height = 30;
            title.textAlignment = UIHorizontalAlignment.Right;
            title.text = "Coverage";

            scroller = AddUIComponent<UIScrollablePanel>();
            scroller.width = 220;
            scroller.height = this.height - title.height;

            for (int i = 0; i < 50; i++)
            {
                UILabel title = scroller.AddUIComponent<UILabel>();
                title.text = "Some district";
            }
        }

        /**
         * It's a bit tricky to get the building selection right:
         * 1) When selecting another building without deselecting the previous one, the OnVisibilityChanged
         * is not invoked, but just the OnPositionChanged.
         * 2) Even when the OnVisibilityChanged is called, when retrieving the current building within the
         * event handler, the previous building (or zero, if none was selected) will be retrieved instead. 
         * -----
         * In the next frame the building will have the correct value, so I'm using MonoBehavior's Update()
         * method here to wait the next tick and then retrieve the building with confidence.
         */
        public override void Update()
        {
            base.Update();
            if (displayRequested)
            {
                updateCount++;
                if (updateCount > 0)
                {
                    updateCount = 0;
                    displayRequested = false;

                    // avoid touching the UI when the position changes, but not the selection (eg: moving the map)
                    ushort selectedBuildingID = RetrieveBuildingID();
                    if (selectedBuildingID != lastBuildingID)
                    {
                        lastBuildingID = selectedBuildingID;
                        Show(selectedBuildingID);
                    }
                }
            }
        }

        private ushort RetrieveBuildingID()
        {
            if (instanceIdFieldInfo == null)
            {
                instanceIdFieldInfo = basePanel.GetType().GetField("m_InstanceID", BindingFlags.NonPublic | BindingFlags.Instance);
            }
            return ((InstanceID)instanceIdFieldInfo.GetValue(basePanel)).Building;
        }

        private void RefreshData()
        {
            districts.Clear();

            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            for (int index = 0; index < 128; ++index)
            {
                District district = districtManager.m_districts.m_buffer[index];
                if (district.m_flags != District.Flags.None)
                {
                    districts.Add((byte)index, districtManager.GetDistrictName(index));
                }
            }
        }

        private void Show(ushort selectedBuildingID)
        {
            if (!ServiceBuildingOptions.GetInstance().IsSupported(selectedBuildingID))
            {
                Hide();
                return;
            }

            RefreshData();

            if (districts.Count == 0)
            {
                Hide();
                return;
            }

            Show();
        }

        private void RefreshVisibility(bool show)
        {
            if (!show)
            {
                Hide();
                displayRequested = false;
                lastBuildingID = 0;
            }
            else if (!Singleton<UnlockManager>.instance.Unlocked(UnlockManager.Feature.Districts))
            {
                Hide();
                displayRequested = false;
            }
            else
            {
                displayRequested = true;
            }
        }

        private void OnVisibilityChanged(UIComponent component, bool visible)
        {
            //Utils.LogGeneral("OnVisibilityChanged:visible=" + visible);
            RefreshVisibility(visible);
        }

        private void OnPositionChanged(UIComponent component, Vector2 position)
        {
            bool visible = basePanel.component.isVisible;
            //Utils.LogGeneral("OnPositionChanged:visible=" + visible);
            RefreshVisibility(visible);
        }

        // Static members

        private static GameObject Root;
        private static DistrictSelectionPanel Panel;

        public static void Install()
        {
            Utils.LogGeneral("Installing DistrictSelectionPanel");
            Root = new GameObject("DistrictSelectionPanelGO");
            Panel = Root.AddComponent<DistrictSelectionPanel>();

            UIPanel servicePanel = UIView.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
            Panel.transform.parent = servicePanel.transform;
            Panel.position = new Vector3(servicePanel.width + 10, servicePanel.height);
            Panel.basePanel = servicePanel.gameObject.transform.GetComponentInChildren<CityServiceWorldInfoPanel>();
            servicePanel.eventVisibilityChanged += Panel.OnVisibilityChanged;
            servicePanel.eventPositionChanged += Panel.OnPositionChanged;
        }

        public static void Uninstall()
        {
            Utils.LogGeneral("Uninstalling DistrictSelectionPanel");
            if (Root != null)
            {
                UIPanel servicePanel = UIView.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
                servicePanel.eventVisibilityChanged -= Panel.OnVisibilityChanged;
                servicePanel.eventPositionChanged -= Panel.OnPositionChanged;
                GameObject.Destroy(Root);
                Panel = null;
                Root = null;
            }
        }
    }
}

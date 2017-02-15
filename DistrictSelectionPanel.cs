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
        private FieldInfo instanceIdFieldInfo;
        private UILabel title;
        private UIFastList fastList;
        internal ushort lastBuildingID;
        private bool displayRequested;
        private int updateCount;

        public override void Start()
        {
            base.Start();

            this.autoLayout = true;
            this.autoLayoutStart = LayoutStart.TopLeft;
            this.autoLayoutDirection = LayoutDirection.Vertical;
            this.autoSize = false;
            this.canFocus = true;
            this.isInteractive = true;
            this.width = 220;
            this.height = 285;
            this.backgroundSprite = "MenuPanel2";

            Hide();

            title = AddUIComponent<UILabel>();
            title.width = 220;
            title.height = 30;
            title.textScale = 0.9f;
            title.textAlignment = UIHorizontalAlignment.Center;
            title.text = "Additional coverage";
            title.padding = new RectOffset(10, 10, 10, 10);

            fastList = UIFastList.Create<DistrictRow>(this);
            fastList.size = new Vector2(this.width, this.height - title.height);
            fastList.rowHeight = 28;
            fastList.canSelect = false;
            fastList.rowsData = new FastList<object>();
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

        private void RefreshData(ushort selectedBuildingID)
        {
            fastList.Clear();

            DistrictManager districtManager = Singleton<DistrictManager>.instance;
            Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[selectedBuildingID];
            byte selectedBuildingDistrictID = districtManager.GetDistrict(building.m_position);

            for (int index = 1; index < 128; ++index)
            {
                if (index != selectedBuildingDistrictID && DistrictChecker.IsActive((byte)index))
                {
                    string name = districtManager.GetDistrictName(index);
                    fastList.rowsData.Add(new object[] { (byte)index, name });
                }
            }

            fastList.DisplayAt(0);
        }

        private void Show(ushort selectedBuildingID)
        {
            if (!ServiceBuildingOptions.GetInstance().IsSupported(selectedBuildingID))
            {
                Hide();
                return;
            }

            RefreshData(selectedBuildingID);

            if (fastList.rowsData.m_size == 0)
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
            RefreshVisibility(visible);
        }

        private void OnPositionChanged(UIComponent component, Vector2 position)
        {
            bool visible = basePanel.component.isVisible;
            RefreshVisibility(visible);
        }

        // Static members

        private static GameObject Root;
        internal static DistrictSelectionPanel Panel;

        public static void Install()
        {
            Root = new GameObject("DistrictSelectionPanelGO");
            Panel = Root.AddComponent<DistrictSelectionPanel>();

            UIPanel servicePanel = UIView.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
            Panel.transform.parent = servicePanel.transform;
            Panel.position = new Vector3(servicePanel.width + 5, servicePanel.height);
            Panel.basePanel = servicePanel.gameObject.transform.GetComponentInChildren<CityServiceWorldInfoPanel>();
            servicePanel.eventVisibilityChanged += Panel.OnVisibilityChanged;
            servicePanel.eventPositionChanged += Panel.OnPositionChanged;
        }

        public static void Uninstall()
        {
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

    class DistrictRow : UIPanel, IUIFastListRow
    {
        private UIPanel background;
        private UICheckBox checkbox;
        private byte districtID;
        private string districtName;
        private ushort buildingID;
        private bool ignoreEvents;

        public DistrictRow()
        {
            background = AddUIComponent<UIPanel>();
            background.atlas = UIUtils.GetAtlas("Ingame");
            background.width = 210;
            background.height = 28;
            background.relativePosition = Vector2.zero;
            background.zOrder = 0;

            checkbox = UIUtils.CreateCheckBox(this);
            checkbox.eventCheckChanged += OnCheckChanged;
        }

        private void OnCheckChanged(UIComponent component, bool value)
        {
            if (!ignoreEvents)
            {
                ServiceBuildingOptions.GetInstance().SetAdditionalTarget(buildingID, districtID, value);
            }
        }

        public void Display(object data, bool isRowOdd)
        {
            ignoreEvents = true;

            if (isRowOdd)
            {
                background.backgroundSprite = null;
            }
            else
            {
                background.backgroundSprite = "UnlockingItemBackground";
                background.color = new Color32(0, 0, 0, 50);
            }

            object[] info = (object[])data;
            districtID = (byte)info[0];
            districtName = (string)info[1];
            buildingID = DistrictSelectionPanel.Panel.lastBuildingID;

            checkbox.isChecked = ServiceBuildingOptions.GetInstance().IsAdditionalTarget(buildingID, districtID);
            checkbox.text = districtName;

            ignoreEvents = false;
        }

        public void Select(bool isRowOdd)
        {
            throw new NotImplementedException();
        }

        public void Deselect(bool isRowOdd)
        {
            throw new NotImplementedException();
        }
    }
}

using BepInEx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeChangerGUI.Source
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    { // You had stuff up here you didn't need. - ciperuoy
        public bool uiopen = true; // the gui didnt even open anyway, so i set it to true - ciperuoy

        public void Update()
        {
            if (Keyboard.current.rightShiftKey.wasPressedThisFrame)
            {
                uiopen = !uiopen;
            }
        }
        public void OnGUI()
        {
            if (NetworkSystem.Instance.InRoom && NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                if (uiopen)
                {
                    GUI.Box(new Rect(30f, 50f, 100f, 160f), "TimeChanger"); // 30 50 170 160

                    if (GUI.Button(new Rect(30f, 70f, 100f, 20f), "Set Daytime")) // 35f 250f 100f 20f
                    {
                        BetterDayNightManager.instance.SetTimeOfDay(3);
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/WeatherDayNight/AudioCrickets").SetActive(false);
                    }

                    if (GUI.Button(new Rect(30f, 90f, 100f, 20f), "Remove Rain"))
                    {
                        for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
                        {
                            BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.None;
                        }
                        GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/WeatherDayNight/rain").SetActive(false); // disables the rain/snow
                    }

                    if (GUI.Button(new Rect(30f, 110f, 100f, 20f), "Rain"))
                    {
                        for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
                        {
                            BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
                        }
                    }

                    if (GUI.Button(new Rect(30f, 130f, 100f, 20f), "Set Evening"))
                    {
                        BetterDayNightManager.instance.SetTimeOfDay(7);
                    }

                    if (GUI.Button(new Rect(30f, 150f, 100f, 20f), "Set Night"))
                    {
                        BetterDayNightManager.instance.SetTimeOfDay(0);
                    }
                }
            }
        }
    }
}

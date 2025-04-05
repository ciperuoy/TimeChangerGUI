using System;
using System.Linq;
using BepInEx;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilla;
using Utilla.Attributes;

namespace TimeChangerGUI.Source
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {

        private bool keyp;
        private bool uiopen;

        void Start()
        {
            Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {

        }
        private void OnGUI()
        {
            if (NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                if (uiopen)
                {
                    GUI.Box(new Rect(30f, 50f, 100f, 160f), "TimeChanger"); // 30 50 170 160

                    if (GUI.Button(new Rect(30f, 70f, 100f, 20f), "Set Daytime")) //35f 250f 100f 20f
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

            if (Keyboard.current.rightShiftKey.isPressed)
            {
                if (!keyp) uiopen = !uiopen;
                keyp = true;
            }
            else
            {
                keyp = false;
            }
        }
    }
}

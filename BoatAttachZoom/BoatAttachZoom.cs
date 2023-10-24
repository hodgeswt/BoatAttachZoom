using BepInEx;
using HarmonyLib;
using Jotunn.Entities;
using Jotunn.Managers;
using System.Reflection;
using UnityEngine;

namespace BoatAttachZoom
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    internal class BoatAttachZoom : BaseUnityPlugin
    {
        public const string PluginGUID = "com.willhodges.BoatAttachZoom";
        public const string PluginName = "BoatAttachZoom";
        public const string PluginVersion = "0.0.1";

        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        public static float MaxDistance { get; set; }
        public static bool ModifiedZoom { get; set; }

        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginGUID);
        }

        [HarmonyPatch(typeof(GameCamera), nameof(GameCamera.UpdateCamera))]
        public static class GameCameraUpdateCamera
        {
            public static void Prefix(GameCamera __instance, float dt)
            {
                Player player = Player.m_localPlayer;
                if (!IsNull(player) && player.IsAttachedToShip())
                {
                    MaxDistance = __instance.m_maxDistance;
                    __instance.m_maxDistance = __instance.m_maxDistanceBoat;
                    ModifiedZoom = true;
                }
            }

            public static void Postfix(GameCamera __instance, float dt)
            {
                if (ModifiedZoom)
                {
                    __instance.m_maxDistance = MaxDistance;
                    ModifiedZoom = false;
                }
            }
        }

        public static bool IsNull(object obj)
        {
            return (Object)obj == null;
        }
    }
}

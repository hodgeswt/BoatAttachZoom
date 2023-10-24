using BepInEx;
using HarmonyLib;
using Jotunn.Entities;
using Jotunn.Managers;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static BoatAttachZoom.Enumerations;

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


        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginGUID);
        }

        [HarmonyPatch(typeof(InventoryGui), "OnSelectedRecipe")]
        public static class InventoryGui_OnSelectedRecipe_Patch
        {
            public static void Postfix(InventoryGui __instance)
            {
                if (crafting)
                {
                }
            }
        }
    }
}

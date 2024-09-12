using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System;
using UnityEngine; 

namespace mystikal.dinkum.DurabilityManager
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        public static float _ToolDurabilityMultiplier;
        public static float _PowerToolDurabilityMultiplier;
        public static float _WeaponDurabilityMultiplier;
        public static int _FirstAidKitUses;

        private void Awake()
        {
            // Set global plugin logger
            Plugin.Log = base.Logger;

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            // Config
            Config.Bind("!Developer",                                       // The section under which the option is shown
                    "NexusID",                                              // The key of the configuration option in the configuration file
                    350,                                                    // The default value
                    "Nexus Mod ID");                                        // Description of the option to show in the config file

            _ToolDurabilityMultiplier = Mathf.Clamp(
                                                Config.Bind<float>(
                                                    "General",             
                                                    "ToolDurabilityMultiplier", 
                                                    1.0f,                    
                                                    "The multiplier amount for Tools (not Power Tools). Minimum value is 0.01, Max is 1000.0")
                                                .Value,
                                                0.01f,
                                                1000.0f);
            _PowerToolDurabilityMultiplier = Mathf.Clamp(
                                                Config.Bind<float>(
                                                    "General",
                                                    "PowerToolDurabilityMultiplier",
                                                    1.0f,
                                                    "The multiplier amount for Power Tools. Minimum value is 0.01, Max is 1000.0")
                                                .Value, 
                                                0.01f, 
                                                1000.0f);
            _WeaponDurabilityMultiplier = Mathf.Clamp(
                                                Config.Bind<float>(
                                                    "General",
                                                    "WeaponDurabilityMultiplier",
                                                    1.0f,
                                                    "The multiplier amount for Weapons. Minimum value is 0.01, Max is 1000.0")
                                                .Value,
                                                0.01f,
                                                1000.0f);
            _FirstAidKitUses = Mathf.Clamp(
                                        Config.Bind<int>(
                                            "General",
                                            "FirstAidKitUses",
                                            3,
                                            "Change the amount of time the First Aid Kit can be used. This is the AMOUNT value, not a MULTIPLIER. Minimum value is 1, Max is 10000")
                                        .Value,
                                        1,
                                        10000);
        }

        private void Start()
        {

            foreach (InventoryItem current in Inventory.Instance.allItems)
            {
                if (!current)
                    continue; // Let's just prevent NPE if it's null. It shouldn't happen, but better safe than sorry

                if (!current.hasFuel)   // If the current Item doesn't use fuel, let's just keep going
                    continue;

                bool flag = false;
                // Let's handle some items individually first
                switch (current.getItemId())
                {
                    case 1096:  // Bottle of clouds
                    case 1444:  // Cup of Sunshine
                    case 1530:  // Hand Trolley
                        // This items count as tools, but we don't want to edit them. We'll just skip them
                        flag = true;
                        break;
                    case 119: // ItemId 119 is First Aid Kit. For this one we replace the max fuel with the amount set by the user
                        current.fuelMax = _FirstAidKitUses;
                        flag = true;
                        break;
                    case 5:     // Shovel of Dirt
                    case 704:   // Shovel of Sand
                    case 705:   // Shovel of Mud
                    case 706:   // Shovel of Red Sand
                        // This items do not count as tools, but we need to have the same durability of a shovel
                        current.fuelMax = Convert.ToInt32(current.fuelMax * _ToolDurabilityMultiplier);
                        flag = true;
                        break;
                }

                
                if ((flag) ||               // If we handled the item in the switch-case...
                    (!current.isATool) ||   // ... or If this isn't a tool ...
                    (current.fuelMax == 0)) // ... or If it's an "empty" tool ....
                    continue;               // ... let's just continue to the next item

                // Power Tools
                if (current.isPowerTool)
                    current.fuelMax = Convert.ToInt32(current.fuelMax * _PowerToolDurabilityMultiplier);
                // Weapons
                else if (current.staminaTypeUse == InventoryItem.staminaType.Hunting)
                    current.fuelMax = Convert.ToInt32(current.fuelMax * _WeaponDurabilityMultiplier);
                // Simple tools, not power tools
                else 
                    current.fuelMax = Convert.ToInt32(current.fuelMax * _ToolDurabilityMultiplier);
            }
        }
    }
}

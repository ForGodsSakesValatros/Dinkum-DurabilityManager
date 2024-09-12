# Dinkum-DurabilityManager

Nexus Mod Link: <https://www.nexusmods.com/dinkum/mods/350>  
A simple BepInEx mod for Dinkum that allow to change the durability of items and First Aid Kits

## Installation

1. Install [BepInEx 6.0.0-pre.1](https://discord.com/channels/892654052989628436/1060375232642306088/1060375232642306088) using the tool and run the game once
2. Download and Install "DurabilityManager" using Vortex or by downloading "mystikal.dinkum.DurabilityManager.dll" from Nexus Files or GitHub and pasting it into "BepInEx\plugins\" folder
3. Run the game again, it will create the file "BepInEx\config\mystikal.dinkum.DurabilityManager.cfg"
4. Open this file with a text editor and change the values according to the next chapter. **If you don't change those values the game will work the same as without the mod**

## Settings

Inside the config file at BepInEx\config\mystikal.dinkum.DurabilityManager.cfg you will find NexusID (leave this value as is) and 4 values
* ToolDurabilityMultiplier
* PowerToolDurabilityMultiplier
* WeaponDurabilityMultiplier
* FirstAidKitUses

The first 3 values are multiplier for Tools, Power Tools and Weapons durability. The items base durability will be multiplied with the respective value. You can set those variables to any values from 0.01 to 1000.0
If you want to double the tools durability, for example, set ToolDurabilityMultiplier to 2
You can use decimal values too. Just set those values to something like 2.75

FirstAidKitUses will change how many times a First Aid Kit can be used. This is an absolute value, not a multiplier. For example, just set it to 10 if you want to use it 10 times before breaking 

## Disclaimer

Every item that have a "usage meter" (power tools, tools, weapons...) basically have 3 values that matters. A "maxFuel", a "fuelOnUse" and the current "fuel" . We can't really work on fuelOnUse, due to the fact that it is an integer and most of the time is 1, so we can't really play with it. We can, however, edit the "maxFuel" value. For example, if we just double this value, the item can be used twice as much. 

Due to this fact, once you'll install the mod and set, for example, the maxFuel for PowerTools to 2 times the original value (PowerToolDurabilityMultiplier = 2.0), every Power Tool you had in your inventory with full charge will suddenly appear with only half charge. This is because the max fuel will double while the current fuel of the item will stay the same. Once you'll recharge/repair the item, it will go back to full charge and work as intended

In short, don't worry if you suddenly have every item damaged/used. It's just because the "usage meter" of the item displays a percentage based on the maxFuel value

## Credits

Thanks to .M.I.K.E. for the idea
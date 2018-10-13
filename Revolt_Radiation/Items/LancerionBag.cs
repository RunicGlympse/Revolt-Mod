using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
	public class LancerionBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = 11;
			item.expert = true;
			bossBagNPC = mod.NPCType("Lancerion");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(5)==1)
            {
                player.QuickSpawnItem(mod.ItemType("InfiniSpear"));
            }	        
            if (Main.rand.Next(7)==1)
            {
                player.QuickSpawnItem(mod.ItemType("LancerionMask"));
            }
            if (Main.rand.Next(2)==1)
            {
                player.QuickSpawnItem(ItemID.DaybloomSeeds, 1);
            }
            else if (Main.rand.Next(2)==1)
            {
                player.QuickSpawnItem(ItemID.MoonglowSeeds, 1);
            }
            else if (Main.rand.Next(2)==1)
            {
                player.QuickSpawnItem(ItemID.WaterleafSeeds, 1);
            }
            if (Main.rand.Next(2)==1)
            {
                player.QuickSpawnItem(ItemID.BlinkrootSeeds, 1);
            }
            else if (Main.rand.Next(2)==1)
            {
                player.QuickSpawnItem(ItemID.DeathweedSeeds, 1);
            }
            else if (Main.rand.Next(2)==1)
            {
                player.QuickSpawnItem(ItemID.FireblossomSeeds, 1);
            }
        }
	}
}
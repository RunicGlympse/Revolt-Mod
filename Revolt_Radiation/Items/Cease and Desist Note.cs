using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
    public class Cease_and_Desist_Note : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 52;
            item.maxStack = 13;
            item.rare = 0;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Execution Notice");
            Tooltip.SetDefault("'We were going to use that eyeball for the Triplets'\nPlaceholder to summon Lancerion");
        }


        public override bool CanUseItem(Player player)
        {
            return NPC.downedBoss1 && !NPC.AnyNPCs(mod.NPCType("Lancerion"));
        }

        public override bool UseItem(Player player)
        {
            Main.PlaySound(29, (int)player.position.X, (int)player.position.Y, 105);
            Main.NewText("<Lancerion> I suggest you drop that weapon of yours.", 255, 0, 0);
            if (Main.netMode != 1)
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Lancerion"));
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.RedBanner, 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
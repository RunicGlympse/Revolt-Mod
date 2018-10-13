using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
    public class LunarPet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Tome");
            Tooltip.SetDefault("Exactly the same thing, but now it's your friend!");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.MagicLantern);
            item.shoot = mod.ProjectileType("LunarPetProjectile");
            item.buffType = mod.BuffType("LunarPetBuff");
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
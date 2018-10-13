
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
    public class Green_Flare : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[item.type] = true;
            DisplayName.SetDefault("Green Flare");
        }

        public override void SetDefaults()
        {
            item.shootSpeed = 6f;
            item.shoot = mod.ProjectileType("Green_Flare_Projectile");
            item.damage = 1;
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.consumable = true;
            item.ammo = AmmoID.Flare;
            item.knockBack = 1.5f;
            item.value = 7;
            item.ranged = true;
        }
    }
}
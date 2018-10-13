
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
    public class Cyber_Shard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Digital Residue");
            Tooltip.SetDefault("No use yet. Will gain one when Bright and Shadow Elementals are added");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 32;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 5);
            item.maxStack = 99;
            item.alpha = 50;
        }
    }
}
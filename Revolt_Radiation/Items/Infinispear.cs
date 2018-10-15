using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
	public class InfiniSpear : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("InfiniSpear");
            Tooltip.SetDefault("Lo and behold, a solution to the Throwing Boycott!\nShoots apparations of Lancerion's weapon");
        }

        public override void SetDefaults()
		{
			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
			item.shootSpeed = 10f;
			item.damage = 15;
			item.knockBack = 5f;
			item.useStyle = 1;
			item.useAnimation = 35;
			item.useTime = 35;
			item.width = 18;
			item.height = 54;
			item.maxStack = 1;
			item.rare = 5;

			item.consumable = false;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.thrown = true;

			item.UseSound = SoundID.Item20;
			item.value = Item.sellPrice(gold: 1);
            item.shoot = mod.ProjectileType("LancerionSpearFriendly");
		}
	}
}

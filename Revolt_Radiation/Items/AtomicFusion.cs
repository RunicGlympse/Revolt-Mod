using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
	public class AtomicFusion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Atomic Fusion");
            Tooltip.SetDefault("Shoot atoms at each other and see what blows up");
        }

        public override void SetDefaults()
		{
			item.shootSpeed = 5f;
			item.damage = 60;
			item.knockBack = 9f;
			item.useStyle = 5;
			item.useAnimation = 20;
			item.useTime = 20;
			item.width = 28;
			item.height = 32;
			item.maxStack = 1;
			item.rare = 8;

			item.noMelee = true;
			item.autoReuse = true;
			item.magic = true;

			item.UseSound = SoundID.Item66;
			item.value = Item.sellPrice(gold: 10);
            item.shoot = mod.ProjectileType("AtomicBall");
		}
	}
}

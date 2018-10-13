using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Items
{
    public class Lunatic_Cannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunatic Cannon");
            Tooltip.SetDefault("It was dormant before it was cool");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.width = 80;
            item.height = 50;
            item.value = 42000;
            item.rare = 9;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item92;
            item.noMelee = true;
            item.useStyle = 5;
            item.useTime = 5;
            item.useAnimation = 5;
            item.damage = 40;
            item.knockBack = 10;
            item.reuseDelay = 0;
            item.shoot = mod.ProjectileType("LunarFlare");
            item.shootSpeed = 32f;
            item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddIngredient(ItemID.SoulofLight, 75);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.UseSound = SoundID.Item41;
                item.useStyle = 5;
                item.useTime = 10;
                item.useAnimation = 50;
                item.damage = 20;
                item.knockBack = 4;
                item.reuseDelay = 20;
                item.shoot = mod.ProjectileType("BookTome");
                item.shootSpeed = 10f;
                item.autoReuse = true;
            }
            else
            {
                item.UseSound = SoundID.Item92;
                item.useStyle = 5;
                item.useTime = 5;
                item.useAnimation = 5;
                item.damage = 40;
                item.knockBack = 10;
                item.reuseDelay = 0;
                item.shoot = mod.ProjectileType("LunarFlare");
                item.shootSpeed = 32f;
                item.autoReuse = false;
            }
            return base.CanUseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 15);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                type = mod.ProjectileType("BookTome");
                {
                    float numberProjectiles = 2;
                    float rotation = MathHelper.ToRadians(10);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.            
                        Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BookTome"), damage, knockBack, player.whoAmI);
                    }
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SolarBall"), 50, 1f, Main.myPlayer);
                    return false;
                }
            }
            else
            {
                type = mod.ProjectileType("LunarFlare");
            }
            return true;
        }
    }
}
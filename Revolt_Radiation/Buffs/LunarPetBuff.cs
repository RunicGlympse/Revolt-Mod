using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Revolt_Radiation.Buffs
{
    public class LunarPetBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lunar Tome");
            Description.SetDefault("It does nothing except being cool to look at");
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;

            player.GetModPlayer<MyPlayer>(mod).LightPet = true;

            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("LunarPetProjectile")] <= 0;

            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("LunarPetProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
            }
            if ((player.controlDown && player.releaseDown))
            {
                if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        if (Main.projectile[j].active && Main.projectile[j].type == mod.ProjectileType("LunarPetProjectile") && Main.projectile[j].owner == player.whoAmI)
                        {
                            Projectile lightpet = Main.projectile[j];
                            Vector2 vectorToMouse = Main.MouseWorld - lightpet.Center;
                            lightpet.velocity += 5f * Vector2.Normalize(vectorToMouse);
                        }
                    }
                }
            }
        }
    }
}

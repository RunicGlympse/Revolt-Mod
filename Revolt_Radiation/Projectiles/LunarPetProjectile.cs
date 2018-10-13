using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Projectiles
{
    public class LunarPetProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.LightPet[projectile.type] = true;
            Main.projPet[projectile.type] = true;
            Main.projFrames[projectile.type] = 16;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SuspiciousTentacle);
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.alpha = 0;
        }

        const int fadeInTicks = 10;
        const int fullBrightTicks = 200;
        const int range = 50;
        int rangeHypoteneus = (int)Math.Sqrt(range * range + range * range);

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.lightOrb = false;
            return true;
        }

        public override bool CloneNewInstances { get { return true; } }
        int frame = 5;
        int frameCounter = 16;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            if (player.dead)
            {
                modPlayer.LightPet = false;
            }
            if (modPlayer.LightPet)
            {
                projectile.timeLeft = 2;
            }

            if (++frameCounter >= 5)
            {
                frameCounter = 0;
                if (++frame >= 16)
                {
                    frame = 0;
                }
                Vector2 vectorToPlayer = player.Center - projectile.Center;
                Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.9f) / 255f, ((255 - projectile.alpha) * 0.1f) / 255f, ((255 - projectile.alpha) * 0.3f) / 255f);
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Projectiles.Hostile
{
    public class LancerionStomp2 : ModProjectile
    {
        public int timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lancerion's Stomp");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 28;
            projectile.aiStyle = 1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.timeLeft = 1000;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();

            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }


            timer++;
            if (timer == 10)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height / 2, 6, 0f, -5f, 100, default(Color), 1.5f)];
                Main.PlaySound(SoundID.Item68, projectile.Center);
                timer = 0;
            }
        }
        
        public override void Kill(int timeleft)
        {
            
        }
    }
}

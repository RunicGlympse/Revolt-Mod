using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Projectiles.Hostile
{
	public class Rocket_0_Hostile : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snail Rocket");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 25;
            projectile.aiStyle = 16;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.timeLeft = 300;
            projectile.penetrate = 2;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            if (timeLeft <= 3)
            {
                projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                projectile.width = 128;
                projectile.height = 128;
                projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            }
        }
    }
}

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
            projectile.timeLeft = 900;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Projectiles.Hostile
{
	public class Digital_Residue : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Digital Residue");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 44;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.timeLeft = 20;
        }

		public override void PostAI()
		{
			if (Main.rand.Next(2) == 0)
			{
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 140);
				dust.scale = 0.5f;
			}
		}
	}
}

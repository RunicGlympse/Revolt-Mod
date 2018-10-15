using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Revolt_Radiation.Projectiles
{
	public class LancerionSpearFriendly : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("InfiniSpear");
		}

		public override void SetDefaults()
		{
			projectile.width = 9;
			projectile.height = 27;
			projectile.aiStyle = 1;
			projectile.penetrate = 2;
			projectile.scale = 1.3f;
            projectile.alpha = 125;
			projectile.ownerHitCheck = true;
			projectile.melee = true;
			projectile.tileCollide = true;
			projectile.friendly = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;

            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X * 0.66f;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y * 0.66f;
                }
                Main.PlaySound(SoundID.Item10, projectile.position);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(25, (int)projectile.position.X, (int)projectile.position.Y); // Play a death sound
            Vector2 usePos = projectile.position;                                     
            Vector2 rotVector = (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
            usePos += rotVector * 16f;

            for (int i = 0; i < 20; i++)
            {
                // Create a new dust
                Dust dust = Dust.NewDustDirect(usePos, projectile.width, projectile.height, 12);
                dust.position = (dust.position + projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }
    }
}

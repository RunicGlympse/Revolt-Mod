using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Revolt_Radiation.Projectiles
{
    public class Green_Flare_Projectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Flare");
            Main.projFrames[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 36000;
            projectile.aiStyle = 1;
        }

        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 50;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            float num1 = 4f;
            float num2 = projectile.ai[0];
            float num3 = projectile.ai[1];
            if (num2 == 0f && num3 == 0f)
            {
                num2 = 1f;
            }
            float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
            num4 = num1 / num4;
            num2 *= num4;
            num3 *= num4;
            if (projectile.alpha < 70)
            {
                Vector2 position = new Vector2(projectile.position.X, projectile.position.Y - 2f);
                float x = -projectile.velocity.X;
                float y = -projectile.velocity.Y;
                int num7 = Dust.NewDust(position, projectile.width, projectile.height / 2, 163, x, y, 100, default(Color), 0.75f);
                Main.dust[num7].noGravity = true;
                Dust dust1 = Main.dust[num7];
                dust1.position.X = dust1.position.X - num2 * 1f;
                Dust dust2 = Main.dust[num7];
                dust2.position.Y = dust2.position.Y - num3 * 1f;
                Dust dust3 = Main.dust[num7];
                dust3.velocity.X = dust3.velocity.X - num2;
                Dust dust4 = Main.dust[num7];
                dust4.velocity.Y = dust4.velocity.Y - num3;
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.ai[0] = projectile.velocity.X;
                projectile.ai[1] = projectile.velocity.Y;
                if (projectile.localAI[1] >= 30f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + 0.09f;
                    projectile.localAI[1] = 30f;
                }
            }
            else
            {
                if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.localAI[0] = 0f;
                    projectile.localAI[1] = 30f;
                }
                projectile.damage = 0;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.ai[1], (double)projectile.ai[0]) + 1.57f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.localAI[0] == 0f)
            {
                if (projectile.wet)
                {
                    projectile.position += oldVelocity / 2f;
                }
                else
                {
                    projectile.position += oldVelocity;
                }
                projectile.velocity *= 0f;
                projectile.localAI[0] = 1f;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(24, 600, false);
            }
            else
            {
                target.AddBuff(24, 300, false);
            }
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(24, 600, false);
            }
            else
            {
                target.AddBuff(24, 300, false);
            }
        }
    }
}
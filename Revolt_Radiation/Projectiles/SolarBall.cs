using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Revolt_Radiation.Projectiles
{
    public class SolarBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Ball");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = 1;
            projectile.alpha = 0;
            projectile.ignoreWater = true;
            projectile.hostile = false;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item34, projectile.position);
            }

            Lighting.AddLight(projectile.Center, 1.1f, 0.9f, 0.4f);

            if (projectile.localAI[0] == 12f)
            {
                projectile.localAI[0] = 0f;
                int num2;
                for (int num12 = 0; num12 < 12; num12 = num2 + 1)
                {
                    Vector2 value2 = Vector2.UnitX * (0f - (float)projectile.width) / 2f;
                    value2 += -Vector2.UnitY.RotatedBy((double)((float)num12 * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    value2 = value2.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
                    int num13 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
                    Main.dust[num13].scale = 1.1f;
                    Main.dust[num13].noGravity = true;
                    Main.dust[num13].position = projectile.Center + value2;
                    Main.dust[num13].velocity = projectile.velocity * 0.1f;
                    Main.dust[num13].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num13].position) * 1.25f;
                    num2 = num12;
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                int num2;
                for (int num14 = 0; num14 < 1; num14 = num2 + 1)
                {
                    Vector2 value3 = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num15 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
                    Dust dust3 = Main.dust[num15];
                    dust3.velocity *= 0.1f;
                    Main.dust[num15].position = projectile.Center + value3 * (float)projectile.width / 2f;
                    Main.dust[num15].fadeIn = 0.9f;
                    num2 = num14;
                }
            }
            if (Main.rand.Next(32) == 0)
            {
                int num2;
                for (int num16 = 0; num16 < 1; num16 = num2 + 1)
                {
                    Vector2 value4 = -Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num17 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 155, default(Color), 0.8f);
                    Dust dust3 = Main.dust[num17];
                    dust3.velocity *= 0.3f;
                    Main.dust[num17].position = projectile.Center + value4 * (float)projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num17].fadeIn = 1.4f;
                    }
                    num2 = num16;
                }
            }
            if (Main.rand.Next(2) == 0)
            {
                int num2;
                for (int num18 = 0; num18 < 2; num18 = num2 + 1)
                {
                    Vector2 value5 = -Vector2.UnitX.RotatedByRandom(0.78539818525314331).RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
                    int num19 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1.2f);
                    Dust dust3 = Main.dust[num19];
                    dust3.velocity *= 0.3f;
                    Main.dust[num19].noGravity = true;
                    Main.dust[num19].position = projectile.Center + value5 * (float)projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num19].fadeIn = 1.4f;
                    }
                    num2 = num18;
                }
            }
        }
    

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1, false);
        }

        public void OnHitPvp(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1, false);
        }

        public override void Kill(int timeleft)
        {
            Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0, 50, default(Color), 5f)];
            Main.PlaySound(SoundID.Item14, projectile.Center);
        }
        public void OnTileCollide()
        {
            projectile.timeLeft = 1;
        }
    }
}
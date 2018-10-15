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
    public class AtomicBall : ModProjectile
    {
        public int combineCount = 0;
        public int combineCap = 5;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Atomic Ball");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = 0;
            projectile.alpha = 0;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.damage = 5;
            projectile.timeLeft = 200;
            projectile.light = 1f;
            projectile.tileCollide = false;
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

            projectile.velocity -= 1f;

            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item34, projectile.position);
            }

            Lighting.AddLight(projectile.Center, 1.1f, 0.9f, 0.4f);

            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].Hitbox.Intersects(projectile.Hitbox))
                {
                    if (combineCount < combineCap)
                    {
                        projectile.width *= 2;
                        projectile.height *= 2;
                        projectile.scale += 2f;
                        projectile.damage += 10;
                        combineCount += 1;
                        projectile.timeLeft += 25;
                    }
                }
            }
            if (projectile.timeLeft == 1)
            {
                for (int i = 0; i < 1000; i++)
                {
                    NPC target = Main.npc[i];


                    float distanceX = target.position.X - projectile.Center.X;
                    float distanceY = target.position.Y - projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(distanceX * distanceX + distanceY * distanceY));
                    float growthVar = 0f;
                    float rotation = MathHelper.ToRadians(25);
                    

                    if (!target.friendly && target.active && target.type != 488 && !target.dontTakeDamage && Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1))
                    {
                        if (combineCount == 1)
                        {


                            if (!target.friendly && target.active && target.type != 488 && !target.dontTakeDamage && Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1))
                            {
                                distance = 3f / distance;
                                distanceX *= distance * 3;
                                distanceY *= distance * 3;

                                Vector2 perturbedSpeed = new Vector2(distanceX, distanceY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (growthVar - 1))) * .2f;
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FriendlyLightning"), (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f);
                            }
                        }

                        if (combineCount == 2)
                        {
                            growthVar = 2f;

                            distance = 3f / distance;
                            distanceX *= distance * 3;
                            distanceY *= distance * 3;

                            for (int k = 0; k < growthVar; k++)
                            {
                                Vector2 perturbedSpeed = new Vector2(distanceX, distanceY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (growthVar - 1))) * .2f;
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FriendlyLightning"), (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f);
                            }

                        }

                        if (combineCount == 3)
                        {
                            growthVar = 3f;

                            distance = 3f / distance;
                            distanceX *= distance * 3;
                            distanceY *= distance * 3;

                            for (int k = 0; k < growthVar; k++)
                            {
                                Vector2 perturbedSpeed = new Vector2(distanceX, distanceY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (growthVar - 1))) * .2f;
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FriendlyLightning"), (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f);
                            }

                        }

                        if (combineCount == 4)
                        {
                            growthVar = 4f;

                            distance = 3f / distance;
                            distanceX *= distance * 3;
                            distanceY *= distance * 3;

                            for (int k = 0; k < growthVar; k++)
                            {
                                Vector2 perturbedSpeed = new Vector2(distanceX, distanceY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (growthVar - 1))) * .2f;
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FriendlyLightning"), (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f);
                            }

                        }

                        if (combineCount == 5)
                        {
                            growthVar = 5f;
                            distance = 3f / distance;
                            distanceX *= distance * 3;
                            distanceY *= distance * 3;

                            for (int k = 0; k < growthVar; k++)
                            {
                                Vector2 perturbedSpeed = new Vector2(distanceX, distanceY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (growthVar - 1))) * .2f;
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FriendlyLightning"), (int)(projectile.damage * 1f), 0, projectile.owner, 0f, 0f);
                            }

                        }
                    }
                }
            }
        }
    }
}
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
    public class LunarFlare : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Flare");
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.LunarFlare);
            aiType = ProjectileID.LunarFlare;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 10, false);
        }

        public void OnHitPvp(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 10, false);
        }

        public override void Kill(int timeleft)
        {
            if (Main.rand.NextBool(5))
            {
                Main.PlaySound(SoundID.Item122, projectile.position);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SeekerVortex"), 20, 3, Main.myPlayer);
            }
            else
            {
                Main.PlaySound(SoundID.Item34, projectile.position);
            }
        }
        public void OnTileCollide()
        {
        }
    }
}
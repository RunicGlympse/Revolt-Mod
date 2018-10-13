using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Revolt_Radiation.Projectiles
{
    public class BookTome : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BookTome");
            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BookStaffShot);
            aiType = ProjectileID.BookStaffShot;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Dazed, 3, false);
        }

        public void OnHitPvp(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Dazed, 3, false);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item32);
        }
    }
}
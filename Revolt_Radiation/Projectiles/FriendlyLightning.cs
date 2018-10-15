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
    public class FriendlyLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 88;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            aiType = ProjectileID.CultistBossLightningOrbArc;
        }
    }
}
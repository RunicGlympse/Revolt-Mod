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
    public class SeekerVortex : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seeker Vortex");
            Main.projFrames[projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 180; // 3 seconds
            projectile.hide = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = 5;
        }

        public override void AI()
        {
            if (projectile.owner == Main.myPlayer)
            {
                if (projectile.timeLeft == 160)
                {
                    projectile.ai[0] = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ProjectileID.MoonlordTurret, 34, 5, Main.myPlayer, projectile.ai[1]);
                }
                if (projectile.timeLeft == 1)
                {
                    Projectile child = Main.projectile[(int)projectile.ai[0]];
                    if (child.active && child.type == ProjectileID.MoonlordTurret)
                    {
                        child.Kill();
                    }
                }
            }
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
    }
}
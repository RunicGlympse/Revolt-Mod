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

namespace Revolt_Radiation.NPCs
{
    public class Rocket_Snail : ModNPC
    {
        int deadTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket Snail");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 42;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 67;
            animationType = NPCID.Snail;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.Underground.Chance * 0.05f;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];

            if (player.Center.X - npc.Center.X < 100 && player.Center.X - npc.Center.X > -100 && player.Center.Y - npc.Center.Y < 100 && player.Center.Y - npc.Center.Y > -100)
            {
                float perturbedSpeedX = player.Center.X - npc.Center.X / 10;
                float perturbedSpeedY = player.Center.Y - npc.Center.Y / 10;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeedX, perturbedSpeedY, mod.ProjectileType("Rocket_0_Hostile"), 20, 2, Main.myPlayer);
                npc.netUpdate = true;
                npc.life = 0;
            }
        }
    }
}

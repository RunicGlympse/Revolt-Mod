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
	public class Cyber_Elemental : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cyber Elemental");
            Main.npcFrameCount[npc.type] = 15;
        }

		public override void SetDefaults()
		{
            npc.width = 28;
            npc.height = 42;
            npc.damage = 10;
			npc.defense = 5;
			npc.lifeMax = 100;
			npc.HitSound = SoundID.NPCHit53;
			npc.DeathSound = SoundID.NPCDeath44;
			npc.value = 60f;
			npc.knockBackResist = 1f;
            npc.aiStyle = 3;
            aiType = NPCID.Zombie;
            animationType = NPCID.ChaosElemental;
        }

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.1f;
		}

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(-5,6), Main.rand.Next(-5,6), mod.ProjectileType("Digital_Residue"), 20, 0);
        }
	}
}

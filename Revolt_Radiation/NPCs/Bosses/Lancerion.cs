using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.World.Generation;
using Terraria.Localization;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.Utilities;

namespace Revolt_Radiation.NPCs.Bosses
{
    enum LancerionState : byte
    {
        Following = 0,
        Stomping = 1,
        Teleporting = 2,
        Dashing = 3
    }

    [AutoloadBossHead]
    public class Lancerion : ModNPC
    {
        // Determines the AI that is to be fired. Kind of like a state manager.
        // Using enums to determine AI states is very neat, since networking the 'LancerionState' will only need to send one byte. NEATO! :D
        internal LancerionState state;

        bool teleporting = false;
        Vector2 teleportationPosition = Vector2.Zero;

        public int dustPortal = 0;
        public int timer = 0;
        public double frametick = 0.0;
        public int frame = 0;
        public int frametick2 = 0;
        public double despawnTimer = 0;
        public int regenTimer = 0;
        public bool deadMeat = false;
        public bool collision = false;
        public bool attack1 = false;
        public bool attack2 = false;
        public bool jump = false;
        public bool throwingUp = false;
        public int wait = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Executioner Knight, Lancerion"); //spawn me like the eye of cthulhu! =D
            Main.npcFrameCount[npc.type] = 13;
        }

        public override void SetDefaults()
        {
            npc.width = 50;
            npc.height = 50;
            npc.damage = 25;
            npc.defense = 25;
            npc.lifeMax = 2000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 26;
            npc.boss = true;
            music = MusicID.OldOnesArmy;
            musicPriority = MusicPriority.BossLow;
            npc.netAlways = true;
            bossBag = mod.ItemType("LancerionBag");
            state = LancerionState.Following;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 188;
            Revolt_Radiation_World.downedLancerion = true;
        }

        public override void NPCLoot()
        {
            if (Main.netMode != 1)
            {
                if (Main.expertMode)
                {
                    npc.DropBossBags();
                }
                else
                {
                    if (Main.rand.Next(5) == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfiniSpear"));
                    }
                    if (Main.rand.Next(2) == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.DaybloomSeeds, 1);
                    }
                    else if (Main.rand.Next(2) == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MoonglowSeeds, 1);
                    }
                    else if (Main.rand.Next(2) == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.WaterleafSeeds, 1);
                    }
                    if (Main.rand.Next(2) == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BlinkrootSeeds, 1);
                    }
                    else if (Main.rand.Next(2) == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.DeathweedSeeds, 1);
                    }
                    else if (Main.rand.Next(2) == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FireblossomSeeds, 1);
                    }
                }
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (numPlayers > 1)
            {
                npc.lifeMax = (int)(2500 * numPlayers * 0.20f);
            }
        }

        public override void PostAI()
        {
            npc.netUpdate = true;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!npc.HasValidTarget)
            {
                npc.velocity *= 0f;
                npc.alpha += 1;
                despawnTimer++;
                npc.spriteDirection = npc.direction;
                Main.PlaySound(SoundID.Item24, (int)npc.position.X, (int)npc.position.Y);
                Dust.NewDustDirect(npc.position, npc.width, npc.height / 2, 58, 0f, -5f, 100, default(Color), 1.5f);

                if (despawnTimer == 255)
                {
                    npc.active = false;
                    Main.PlaySound(SoundID.Item6, (int)npc.position.X, (int)npc.position.Y);
                    Dust.NewDustDirect(npc.position, npc.width, npc.height, 58, 0f, 0f, 0, default(Color), 1.5f);
                }
            }

            npc.rotation = npc.velocity.X * 0.03F;
            npc.spriteDirection = npc.direction;

            
            if (npc.life >= npc.lifeMax)
                npc.life = npc.lifeMax;

            timer++;

            if (state == LancerionState.Following)
            {
                npc.width = 50;



                if (timer == 200)
                {
                    state = LancerionState.Dashing;
                }

                if (timer >= 600)
                {
                    if (Main.netMode != 1)
                    {
                        // First time entering this AI. Determines the attack type that is used.
                        if (timer == 600)
                        {
                            if (Main.rand.Next(2) == 1)
                            {
                                attack1 = true;
                            }
                            else
                            {
                                attack2 = true;
                            }
                        }


                        npc.velocity.X = 0;
                        npc.velocity.Y = 0;

                        if (attack1 == true)
                        {
                            Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height / 2, 12, 0f, -5f, 100, default(Color), 1.5f)];
                            dust.position = npc.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(npc.width * 1.5f, npc.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                            if (timer < 700)
                            {
                                if ((timer - 600) % 20 == 0)
                                {
                                    Main.PlaySound(SoundID.Item82, npc.Center);
                                }
                            }
                            // No longer need for an extra timer. Just use the one we already have.
                            else if ((timer - 700) % 20 == 0)
                            {
                                throwingUp = true;
                                float perturbedSpeedX = player.Center.X - npc.Center.X;
                                float perturbedSpeedY = player.Center.Y - npc.Center.Y;
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeedX, perturbedSpeedY, mod.ProjectileType("LancerionSpear"), (int)(npc.damage * 0.50f), 2, Main.myPlayer);

                                npc.netUpdate = true;

                                Main.PlaySound(SoundID.Item19, npc.Center);
                            }
                        }
                        else if (attack2 == true)
                        {
                            Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height / 2, 58, 0f, -5f, 100, default(Color), 1.5f)];
                            dust.position = npc.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(npc.width * 1.5f, npc.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                            // No longer need for an extra timer. Just use the one we already have.
                            if ((timer - 600) % 30 == 0)
                            {
                                float floatingY = 1;
                                float acceleration = 1.3f;
                                float SpeedY = floatingY * acceleration;
                                Projectile.NewProjectile(player.Center.X, player.Center.Y - 125, 0, SpeedY, mod.ProjectileType("LancerionSpear"), (int)(npc.damage * 0.50f), 2, Main.myPlayer);

                                npc.netUpdate = true;

                                Main.PlaySound(SoundID.Item19, npc.Center);
                            }
                        }

                        // If the timer reaches a certain point, start one of the other two AI states.
                        if (timer >= 900)
                        {

                            if (Main.rand.Next(2) == 1)
                            {
                                state = LancerionState.Teleporting;
                            }
                            else
                            {
                                state = LancerionState.Stomping;
                            }

                            timer = 0;
                            attack1 = attack2 = false;
                        }

                    }
                }
            }
            else if (state == LancerionState.Dashing)
            {
                if (timer < 250)
                {
                    npc.velocity *= 0;
                }

                else if (timer == 250)
                {
                    Main.PlaySound(SoundID.Item19, npc.Center);
                    if (npc.direction == 1)
                    {
                        npc.velocity.X = 15;
                    }
                    if (npc.direction == -1)
                    {
                        npc.velocity.X = -15;
                    }
                    if (player.Center.Y - npc.Center.Y <= 0)
                    {
                        npc.velocity.Y = 1;
                    }
                    if (player.Center.Y - npc.Center.Y >= 0)
                    {
                        npc.velocity.Y = -1;
                    }
                }

                else if (timer == 400)
                {
                    state = LancerionState.Following;
                }
            }

            else if (state == LancerionState.Teleporting) //make this work
            {
                npc.velocity *= 0;
                if (++dustPortal >= 0)
                {
                    Vector2 usePos = npc.position;
                    Vector2 rotVector = (npc.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
                    usePos += rotVector * 16f;
                    Dust.NewDustDirect(npc.position, npc.width, npc.height / 2, 58, 0f, -5f, 100, default(Color), 1.5f);
                }
                if (++regenTimer >= 5 && npc.life != 2000)
                {
                    npc.life += 1;
                    regenTimer = 0;
                }


                // We've already gained a new teleportation position, so we can begin processing it.
                if (teleporting)
                {
                    if (teleportationPosition != Vector2.Zero && timer++ >= 100)
                    {
                        Vector2 usePos = npc.position;
                        Vector2 rotVector = (npc.rotation - MathHelper.ToRadians(90f)).ToRotationVector2();
                        usePos += rotVector * 16f;
                        npc.position = teleportationPosition;

                        // Teleport to dust.
                        Main.PlaySound(SoundID.Item6, (int)npc.position.X, (int)npc.position.Y);
                        for (int i = 0; i < 50; ++i)
                        {
                            Dust.NewDustDirect(teleportationPosition, npc.width, npc.height, 58, 0f, 0f, 0, default(Color), 1.5f);
                        }
                        npc.netUpdate = true;

                        // Reset NPC values and teleportation variables.
                        timer = -50;
                        teleporting = false;
                        state = LancerionState.Following;
                        if (timer == 0)
                        {
                            teleportationPosition = Vector2.Zero;
                        }
                    }
                    else if (teleportationPosition == Vector2.Zero) // Teleportation did not succeed; reset everything to default.
                    {
                        timer = 0;
                        teleporting = false;
                        state = LancerionState.Following;
                    }
                }
                else // We do not yet have a teleportation position for this teleport sequence.
                {
                    bool tryTeleport = true;
                    int teleportTries = 0;

                    // We give the teleportation 50 tries to get a new, valid position. If this does not get a valid teleportation position before the tries are up, it won't teleport at all.
                    while (tryTeleport && teleportTries <= 50)
                    {
                        // This position was done in pixel coordinates, not tile coordinate :s
                        int randX = (int)(player.position.X / 16) + Main.rand.Next(16) - 8;
                        int randY = (int)(player.position.Y / 16) + Main.rand.Next(16) - 8;
                        int minTilePosX = randX;
                        int maxTilePosX = randX + ((npc.width * 2) / 16);
                        int minTilePosY = randY;
                        int maxTilePosY = randY + ((npc.height * 2) / 16);

                        for (int x = minTilePosX; x < maxTilePosX; x++)
                        {
                            for (int y = minTilePosY; y < maxTilePosY; y++)
                            {
                                if (WorldGen.InWorld(x, y))
                                {
                                    if (!Collision.SolidTiles(randX, randX + 2, randY, randY + 2))
                                    {
                                        Vector2 vector2;
                                        vector2.X = (x * 16);
                                        vector2.Y = (y * 16);

                                        teleportationPosition = new Vector2(randX * 16, randY * 16);
                                        tryTeleport = false;
                                    }
                                }
                            }
                        }

                        teleportTries++;
                    }

                    teleporting = true;
                }
            }
            else if (state == LancerionState.Stomping)
            {
                int stompTry = 0;
                stompTry++;

                npc.velocity.X = 0;
                if (npc.velocity.Y == 0)
                {
                    npc.ai[0] = 3;
                }

                if (npc.ai[0] > 0)
                {
                    npc.velocity.Y -= 5f;
                    npc.ai[0]--;
                }

                if (npc.collideY && npc.velocity.Y >= 0)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (Main.player[k].active)
                        {
                            MyPlayer modPlayer = Main.player[k].GetModPlayer<MyPlayer>(mod);
                            modPlayer.cameraShakeDur = 20;
                            modPlayer.cameraShakeMagnitude = 5f;
                        }
                    }
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 3f, -5f, mod.ProjectileType("LancerionStomp"), (int)(npc.damage * 0.50f), 2f, Main.myPlayer);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -3f, -5f, mod.ProjectileType("LancerionStomp"), (int)(npc.damage * 0.50f), 2f, Main.myPlayer);
                    }
                    Main.PlaySound(SoundID.Item66, npc.Center);
                    timer = 0;
                    state = LancerionState.Following;
                }
                else if (stompTry >= 1000)

                {
                    stompTry = 0;
                    npc.width = 50;
                    timer = 0;
                    state = LancerionState.Following;
                }
            }

            else
            {
                timer = 0;
                state = LancerionState.Following;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * frame;
            frametick++;

            if (npc.velocity.Y >= 0.5f)
            {
                frametick = 0;
                frame = 1;
            }
            else
            {
                npc.spriteDirection = npc.direction;
                if (attack1 == true)
                {
                    frametick = 0;
                    frametick2++;

                    if (frametick2 < 10)
                    {
                        frame = 9;
                    }
                    else if (frametick2 < 20)
                    {
                        frame = 10;
                    }
                    else if (frametick2 < 30)
                    {
                        frame = 11;
                    }
                    else
                    {
                        frametick2 = 0;
                    }
                }

                else if (npc.velocity.X == 0f)
                {
                    frametick = 0.0;
                    frame = 0;
                }
                else
                {
                    frametick += (double)(Math.Abs(npc.velocity.X) * 1.5f);
                    if (frametick > 10.0)
                    {
                        frame += 1;
                        frametick = 0.0;
                    }
                    if (frame >= 9)
                    {
                        frame = 2;
                    }
                }

            }
            if (Main.netMode != 1)
            {
                if (state == LancerionState.Teleporting)
                {
                    frame = 12;
                }
                if (attack2 == true)
                {
                    frame = 12;
                }
                if (despawnTimer > 0)
                {
                    frame = 12;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (teleporting)
            {
                if (Main.netMode != 1)
                {
                    Texture2D texture = mod.GetTexture("NPCs/Bosses/AttackIndicator");
                    Vector2 drawPos = teleportationPosition + new Vector2(texture.Width / 2, texture.Height / 2) - Main.screenPosition;
                    spriteBatch.Draw(texture, drawPos, Color.White);
                }
            }
            if (state == LancerionState.Dashing)
            {
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width, npc.height);
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Texture2D texture = mod.GetTexture("NPCs/Bosses/LancerionDash");
                    Vector2 afterPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                    spriteBatch.Draw
                    (
                    texture,
                    afterPos,
                    npc.frame,
                    Color.White,
                    npc.rotation,
                    texture.Size(),
                    1f,
                    SpriteEffects.None,
                    200f
                    );
                }
                return true;
            }
            return true;
        }


        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.rand.Next(2) == 1)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 94);
            }
        }

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            damage *= 5;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage *= 2;
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > 100)
                damage /= 2;

            return true;
        }
    }
}
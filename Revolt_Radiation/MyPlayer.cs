using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Revolt_Radiation
{
    public class MyPlayer : ModPlayer
    {
        public bool LightPet = false;
        public bool LunarPetProjectile = false;
        public bool LunarPetBuff = false;

        public int cameraShakeDur = 0;
        public float cameraShakeMagnitude = 0f;
        public Vector2 shakeO;
        public bool shakeReset = false;

        public override void ResetEffects()
        {
            LunarPetBuff = false;
            LightPet = false;

            if (cameraShakeDur > 0)
            {
                cameraShakeDur--;
                shakeReset = false;
            }
            else
            {
                cameraShakeMagnitude = 0;
                if (shakeReset == true)
                    shakeO = player.position;
                else
                {
                    player.position = shakeO;
                    shakeReset = true;
                }
            }
        }

        public override void PreUpdate()
        {
            Random random = new Random();
            if (cameraShakeDur > 0)
            {
                cameraShakeMagnitude += 1f / 5f;
                player.position.X = shakeO.X - cameraShakeMagnitude + (float)random.NextDouble() * cameraShakeMagnitude * 2;
                player.position.Y = shakeO.Y - cameraShakeMagnitude + (float)random.NextDouble() * cameraShakeMagnitude * 2;
            }
        }
    }
}
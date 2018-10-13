using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Revolt_Radiation
{
    public class MyPlayer : ModPlayer
    {
        public bool LightPet = false;
        public bool LunarPetProjectile = false;
        public bool LunarPetBuff = false;

        public override void ResetEffects()
        {
            LunarPetBuff = false;
            LightPet = false;
        }
    }
}
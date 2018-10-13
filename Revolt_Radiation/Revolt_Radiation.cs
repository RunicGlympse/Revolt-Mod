using Terraria.ModLoader;

namespace Revolt_Radiation
{
	class Revolt_Radiation : Mod
	{
		public Revolt_Radiation()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
	}
}

using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Buffs.Minions
{
	public class Funguy : ModBuff
	{
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = Assets.Buffs.Minions + "Funguy";

			return mod.Properties.Autoload;
		}
        public override void SetDefaults()
		{
			DisplayName.SetDefault("Funguy");
			Description.SetDefault("The fungi is quite a fun-guy!");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MinionPlayer modPlayer = player.GetModPlayer<MinionPlayer>();
			modPlayer.funguy = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.Funguy>()] > 0;

			if (!modPlayer.funguy)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}

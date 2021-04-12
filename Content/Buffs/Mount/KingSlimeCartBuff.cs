using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Buffs.Mount
{
	public class KingSlimeCartBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("King Slime Minecart");
			Description.SetDefault("'Sticky Rails!'");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.mount.SetMount(ModContent.MountType<Content.Mounts.KingSlimeCart>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}

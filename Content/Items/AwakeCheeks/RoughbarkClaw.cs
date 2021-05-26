using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items.AwakeCheeks
{
	public class RoughbarkClaw : ModItem
	{
		public override void SetDefaults()
		{
			item.width = item.height = 12;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 40, 0);

			item.pick = 45;
			item.damage = 4;
			item.knockBack = 2f;

			item.useTime = 7;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.SwingThrow;
			
			item.melee = true;
			item.useTurn = true;
			item.autoReuse = true;
			
			item.UseSound = SoundID.Item1;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				Dust dust = Dust.NewDustDirect(hitbox.Location.ToVector2(), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.Wood>());
				dust.velocity += player.velocity * 0.8f;
			}
		}
	}
}

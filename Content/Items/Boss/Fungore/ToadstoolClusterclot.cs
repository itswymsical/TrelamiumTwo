using Terraria;
using Terraria.ID;

using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Boss.Fungore
{
	public class ToadstoolClusterclot : ModItem
	{
        public override string Texture => Assets.Items.Fungore + "ToadstoolClusterclot";
		public override void SetStaticDefaults() => Tooltip.SetDefault("Fires a cluster of fungi clots that stick to enemies");
        public override void SetDefaults()
		{
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 5);

			item.crit = 2;
			item.damage = 10;
			item.knockBack = 5f;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.noUseGraphic =
				item.ranged =
				item.noMelee = true;

			item.autoReuse = true;

			item.shootSpeed = 8f;
			item.shoot = ModContent.ProjectileType<Projectiles.Ranged.ToadstoolClusterclot>();
			
			item.UseSound = SoundID.Item1;
		}
	}
}

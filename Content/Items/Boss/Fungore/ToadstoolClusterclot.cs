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
			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(silver: 5);

			Item.crit = 2;
			Item.damage = 10;
			Item.knockBack = 5f;

			Item.useTime = Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.noUseGraphic =
				Item.DamageType =
				// item.noMelee = true;

			Item.autoReuse = true;

			Item.shootSpeed = 8f;
			Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.ToadstoolClusterclot>();
			
			Item.UseSound = SoundID.Item1;
		}
	}
}

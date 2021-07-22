using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Boss.Fungore
{
	public class FunguyStaff : ModItem
	{
		public override string Texture => Assets.Items.Fungore + "FunguyStaff";
        public override void SetStaticDefaults() => DisplayName.SetDefault("Funguy Staff");

		public override void SetDefaults()
		{
			item.summon = true;
			item.noMelee = true;
			item.autoReuse = false;

			item.damage = 7;
			item.knockBack = 4f;
			item.mana = 10;

			item.width = 42;
			item.height = 48;

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.shoot = ModContent.ProjectileType<Projectiles.Summon.Funguy>();

			item.buffType = ModContent.BuffType<Buffs.Minions.Funguy>();

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 5);

			item.UseSound = SoundID.Item44;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;

			return true;
		}
	}
}

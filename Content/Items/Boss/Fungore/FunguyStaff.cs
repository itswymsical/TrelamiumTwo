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
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			// item.autoReuse = false;

			Item.damage = 7;
			Item.knockBack = 4f;
			Item.mana = 10;

			Item.width = 42;
			Item.height = 48;

			Item.useTime = Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.shoot = ModContent.ProjectileType<Projectiles.Summon.Funguy>();

			Item.buffType = ModContent.BuffType<Buffs.Minions.Funguy>();

			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(silver: 5);

			Item.UseSound = SoundID.Item44;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;

			return true;
		}
	}
}

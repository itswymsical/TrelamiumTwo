using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Projectiles.Melee;

namespace TrelamiumTwo.Content.Items.Weapons.Magic
{
    public class LightningRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Rod");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 62;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ModContent.ProjectileType<LightningProjectile>();
            item.useTime = item.useAnimation = 7;
            item.shootSpeed = 9.4f;
            item.knockBack = 2.4f;
            item.damage = 14;
            item.rare = ItemRarityID.Green;
            item.magic = true;
            item.mana = 7;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.value = Item.sellPrice(gold: 1, silver: 25);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 offset = Vector2.Normalize(new Vector2(speedX, speedY)) * 75f;
            if (Collision.CanHit(position, 0, 0, position + offset, 0, 0))
            {
                position += offset;
            }

            int rand = Main.rand.Next(2);
            for (int i = 0; i < 3 + rand; i++)
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10f)), type, damage, knockBack, item.owner);
            }

            return false;
        }
    }
}
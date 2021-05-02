using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.ForestGuardian
{
    public class AlluvialCollider : TrelamiumItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alluvial Collider");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.magic = true;
            item.noMelee = true;
            item.useTurn = false;
            item.autoReuse = true;

            item.useAnimation = 22;
            item.useTime = 22;

            item.damage = 10;
            item.crit = 2;
            item.mana = 8;
            item.knockBack = 2.5f;

            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item9;

            item.shoot = ModContent.ProjectileType<Projectiles.Magic.AlluvialBoulderProjectile>();
            item.shootSpeed = 9f;

            item.value = Item.buyPrice(silver: 50);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int index = 0; index < 2; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 0.5 + (Main.rand.Next(150) * -player.direction) + 
                    (Main.mouseX + (double)Main.screenPosition.X - player.position.X)), (float)(player.position.Y + player.height * 0.5 - 600.0));

                vector2_1.X = (float)((vector2_1.X + (double)player.Center.X) / 2.0) + Main.rand.Next(-150, 150);
                vector2_1.Y -= (100 * index);

                float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if (num13 < 0.0) num13 *= -1f;
                if (num13 < 20.0) num13 = 20f;

                float num14 = (float)Math.Sqrt(num12 * (double)num12 + num13 * (double)num13);
                float num15 = item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;

                float SpeedX = num16 + Main.rand.Next(-40, 41) * 0.02f;
                float SpeedY = num17 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, 
                    ModContent.ProjectileType<Projectiles.Magic.AlluvialBoulderProjectile>(), damage, knockBack, Main.myPlayer, 0.0f, Main.rand.Next(5));
            }
            return false;
        }
    }
}
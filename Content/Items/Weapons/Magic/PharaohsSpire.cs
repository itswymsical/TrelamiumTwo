using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Weapons.Magic
{
    public class PharaohsSpire : ModItem
    {
        public override string Texture => Assets.Items.Magic + "PharaohsSpire";
        public override void SetStaticDefaults() => DisplayName.SetDefault("Pharaoh's Spire");  

        public override void SetDefaults()
        {
            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.useTurn = false;
            item.autoReuse = true;

            item.useAnimation =
                item.useTime = 35;

            item.damage = 7;
            item.crit = 2;
            item.mana = 5;
            item.knockBack = 2.5f;

            item.rare = ItemRarityID.White;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item9;

            item.shoot = ModContent.ProjectileType<Projectiles.Magic.SandBall>();
            item.shootSpeed = 7.8f;

            item.value = Item.buyPrice(silver: 80);

            item.width = 52;
            item.height = 72;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-8, 0);
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 80f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int i = 0; i < 5; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed *= scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage / 2, knockBack, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AntlionMandible, 8);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;
using Terraria.Audio;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
    public class WoodDaibo : ModProjectile
    {
        public override string Texture => Assets.Projectiles.Melee + "WoodDaibo";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 64;
            Projectile.penetrate = -1;
            Projectile.friendly = Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Melee;

            Projectile.hostile = Projectile.tileCollide = false;
        }
        private Player player;
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (!owner.active || owner.dead)
            {
                Projectile.Kill();
            }
            player = Main.player[Projectile.owner];
            Projectile.soundDelay--;
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.DD2_SkyDragonsFurySwing, Projectile.position);
                Projectile.soundDelay = 48;
            }

            if (Main.myPlayer == Projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    Projectile.Kill();
                }
            }

            Projectile.Center = player.Center;

            Projectile.spriteDirection = player.direction;
            Projectile.rotation += 0.2f * player.direction;

            if (Projectile.rotation > MathHelper.TwoPi)
            {
                Projectile.rotation -= MathHelper.TwoPi;
            }
            else if (Projectile.rotation < 0)
            {
                Projectile.rotation += MathHelper.TwoPi;
            }

            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = Projectile.rotation;
            foreach (Projectile hProjectile in Main.projectile)
            {
                if (hProjectile.Hitbox.Intersects(Projectile.Hitbox) && hProjectile.type != Projectile.type && hProjectile.hostile && hProjectile.damage < 10)
                {
                    hProjectile.Kill();
                }
            }

            SetOwnerAnimation(owner);
        }
        private void SetOwnerAnimation(Player owner)
        {
            owner.itemTime = owner.itemAnimation = 10;

            owner.heldProj = Projectile.whoAmI;

            float currentAnimationFraction = Projectile.rotation;

            if (currentAnimationFraction < 0.4f)
                owner.bodyFrame.Y = owner.bodyFrame.Height * 3;

            else if (currentAnimationFraction < 0.75f)
                owner.bodyFrame.Y = owner.bodyFrame.Height * 2;

            else
                owner.bodyFrame.Y = owner.bodyFrame.Height;
        }
    }
}
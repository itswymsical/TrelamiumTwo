﻿using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Projectiles.Melee
{
    public class WoodDaibo : ModProjectile
    {
        public override string Texture => Assets.Projectiles.Melee + "WoodDaibo";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = projectile.height = 64;
            projectile.penetrate = -1;
            projectile.friendly =
                projectile.ignoreWater =
                projectile.melee = true;

            projectile.hostile =
                projectile.tileCollide = false;
        }
        private Player player;
        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (!owner.active || owner.dead)
            {
                projectile.Kill();
            }
            player = Main.player[projectile.owner];
            projectile.soundDelay--;
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(SoundID.DD2_SkyDragonsFurySwing, projectile.position);
                projectile.soundDelay = 48;
            }

            if (Main.myPlayer == projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    projectile.Kill();
                }
            }

            projectile.Center = player.Center;

            projectile.spriteDirection = player.direction;
            projectile.rotation += 0.2f * player.direction;

            if (projectile.rotation > MathHelper.TwoPi)
            {
                projectile.rotation -= MathHelper.TwoPi;
            }
            else if (projectile.rotation < 0)
            {
                projectile.rotation += MathHelper.TwoPi;
            }

            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = projectile.rotation;
            foreach (Projectile hProjectile in Main.projectile)
            {
                if (hProjectile.Hitbox.Intersects(projectile.Hitbox) && hProjectile.type != projectile.type && hProjectile.hostile && hProjectile.damage < 10)
                {
                    hProjectile.Kill();
                }
            }

            SetOwnerAnimation(owner);
        }
        private void SetOwnerAnimation(Player owner)
        {
            owner.itemTime = owner.itemAnimation = 10;

            owner.heldProj = projectile.whoAmI;

            float currentAnimationFraction = projectile.rotation;

            if (currentAnimationFraction < 0.4f)
                owner.bodyFrame.Y = owner.bodyFrame.Height * 3;

            else if (currentAnimationFraction < 0.75f)
                owner.bodyFrame.Y = owner.bodyFrame.Height * 2;

            else
                owner.bodyFrame.Y = owner.bodyFrame.Height;
        }
    }
}
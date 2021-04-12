#region Using directives

using System.Linq;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TrelamiumTwo.Common.Players;

#endregion

namespace TrelamiumTwo.Content.Items.Tools
{
	public sealed class TravelersLantern : ModItem
	{
		private int currentActiveEmber = 0;
		
		private readonly int MaxEmberAmount = 1;
		private readonly float MaxActiveDistance = 120;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Traveler's Lantern");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 12;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(0, 0, 40, 0);

			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.holdStyle = ItemHoldStyleID.HoldingOut;

			item.noMelee = true;
			
			item.UseSound = SoundID.Item1; // TODO: Eldrazi - Implement correct sound.
		}

		public override bool AltFunctionUse(Player player)
			=> true;

		public override bool CanUseItem(Player player)
		{
			if (Main.myPlayer == player.whoAmI)
			{
				int emberType = ModContent.ProjectileType<Projectiles.LanternLight>();

				if (player.altFunctionUse == 2)
				{
					foreach (var proj in Main.projectile.Where(x => x.active && x.owner == player.whoAmI && x.type == emberType))
					{
						proj.ai[0] = 0f;
						proj.netUpdate = true;
						proj.localAI[0] = proj.localAI[1] = 0f;
					}

					return (false);
				}

				Vector2 targetPosition = Main.MouseWorld;

				if (!Collision.CanHitLine(player.Center, 1, 1, targetPosition, 1, 1))
				{
					return (false);
				}
				
				float distanceToTargetPosition = (targetPosition - player.Center).Length();
				if (distanceToTargetPosition > MaxActiveDistance)
				{
					targetPosition += Vector2.Normalize(player.Center - targetPosition) * (distanceToTargetPosition - MaxActiveDistance);
				}
				
				Projectile currentEmber = Main.projectile
					.Where(x => x.active && x.owner == player.whoAmI && x.type == emberType)
					.Skip(currentActiveEmber)
					.FirstOrDefault();

				currentEmber.ai[0] = 1f;
				currentEmber.netUpdate = true;
				currentEmber.localAI[0] = targetPosition.X;
				currentEmber.localAI[1] = targetPosition.Y;

				currentActiveEmber = (currentActiveEmber + 1) % MaxEmberAmount;
			}

			return (false);
		}

		public override void HoldStyle(Player player)
		{
			if (Main.myPlayer == player.whoAmI)
			{
				int emberType = ModContent.ProjectileType<Projectiles.LanternLight>();
				int emberCount = player.ownedProjectileCounts[emberType];
				for (int i = 0; i < MaxEmberAmount - emberCount; ++i)
				{
					Vector2 projectilePos = player.position + new Vector2(Main.rand.Next(player.width), Main.rand.Next(player.height));

					Projectile.NewProjectile(projectilePos, Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi), emberType, 0, 0, player.whoAmI);
				}
			}

			player.itemLocation.Y -= 22;
			if (player.direction == 1)
			{
				player.itemLocation.X -= 48;
			}
			player.itemLocation.X -= 6 * player.direction;

			Lighting.AddLight(player.itemLocation, new Vector3(0.5f, 0.2f, 0f) * 0.5f);

			player.GetModPlayer<TrelamiumPlayer>().HeldItemOverlayOperationModifier += TravelersLanternGlow;
		}

		private void TravelersLanternGlow(Player player, TrelamiumPlayer mPlayer)
		{
			Texture2D glowmask = ModContent.GetTexture(Texture + "_Glow");
			Rectangle frame = glowmask.Frame();
			SpriteEffects effects = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 origin = new Vector2(0, 0);
			
			Vector2 drawPosition = player.itemLocation - Main.screenPosition + new Vector2(0, player.gfxOffY);
			drawPosition.X += 33 * player.direction;

			drawPosition.X = (int)drawPosition.X;
			drawPosition.Y = (int)drawPosition.Y;

			float opacity = 0.4f + System.Math.Abs((float)System.Math.Sin(Main.time / 30));
			if (opacity > 1f)
			{
				opacity = 1f;
			}
			DrawData glowmaskData = new DrawData(glowmask, drawPosition, frame, Color.White * opacity, 0, origin, item.scale, effects, 0);
			Main.playerDrawData.Add(glowmaskData);
		}
	}
}

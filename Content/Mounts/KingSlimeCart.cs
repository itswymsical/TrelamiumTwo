using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Utilities;

namespace TrelamiumTwo.Content.Mounts
{
	public sealed class KingSlimeCart : ModMountData
	{
		public override void SetDefaults()
		{
			mountData.Minecart = true;
			mountData.MinecartDirectional = true;
			mountData.MinecartDust = DelegateMethods.Minecart.Sparks;

			mountData.fallDamage = 1f;
			mountData.heightBoost = 10;
			mountData.flightTimeMax = 0;
			mountData.textureWidth = 56;
			mountData.textureHeight = 46;
			mountData.jumpHeight = 15;
			mountData.jumpSpeed = 5.15f;

			mountData.runSpeed = 13f;
			mountData.dashSpeed = 13f;
			mountData.acceleration = 0.04f;
			mountData.blockExtraJumps = true;

			mountData.spawnDust = 12;
			mountData.buff = ModContent.BuffType<Buffs.Mount.KingSlimeCartBuff>();

			mountData.totalFrames = 1;
			int[] array = new int[mountData.totalFrames];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = 13;
			}
			mountData.playerYOffsets = array;

			mountData.xOffset = 0;
			mountData.bodyFrame = 3;
			mountData.yOffset = 5;
			mountData.playerHeadOffset = 14;
			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;
			mountData.runningFrameCount = 3;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 0;
			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;
			mountData.inAirFrameCount = 0;
			mountData.inAirFrameDelay = 0;
			mountData.inAirFrameStart = 0;
			mountData.idleFrameCount = 0;
			mountData.idleFrameDelay = 0;
			mountData.idleFrameStart = 0;
			mountData.idleFrameLoop = false;

			if (Main.netMode != NetmodeID.Server)
			{
				mountData.backTexture = null;
				mountData.backTextureExtra = null;
				mountData.frontTextureExtra = null;
				mountData.textureWidth = mountData.frontTexture.Width;
				mountData.textureHeight = mountData.frontTexture.Height;
			}
		}
	}
}

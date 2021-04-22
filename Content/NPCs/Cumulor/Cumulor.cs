using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.Cumulor
{
	public sealed class Cumulor : ModNPC
	{
		#region AIState
		private enum AIState
		{
			Normal = 0,
			Dash = 1,
			Scream = 2,
			Relocate = 3
		}
		private AIState State
		{
			get => (AIState)npc.ai[0];
			set => npc.ai[0] = (int)value;
		}
		public float StateTimer
		{
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}
		private float DashTimer
		{
			get => npc.ai[2];
			set => npc.ai[2] = value;
		}
		private float AttackTimer
		{
			get => npc.ai[3];
			set => npc.ai[3] = value;
		}
		private bool aetherRain;
		private bool lightningShock;
		private bool gasSpew;
		#endregion
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cumulor");
			//Main.npcFrameCount[npc.type] = 6;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 184;
			npc.height = 180;

			npc.damage = 30;
			npc.defense = 12;
			npc.lifeMax = 4200;

			npc.noGravity = true;
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noTileCollide = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit18;
			npc.DeathSound = SoundID.NPCDeath13;
			npc.value = Item.buyPrice(0, 0, 2, 50);
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/FogFulcrum");
		}

		public override void AI()
		{
			Player player = Main.player[npc.target];
			npc.spriteDirection = npc.direction;
			StateTimer++;
			Main.NewText($"Timer Value: {StateTimer}", default, false);

			if (State == AIState.Normal)
			{
				NormalStateMovement();
				if (StateTimer >= 600 && StateTimer <= 1200)
					State = AIState.Dash;
			}
			if (StateTimer > 1200)
				StateTimer = (int)AIState.Normal;

			if (State == AIState.Dash)
            {
				DashTimer++;
				if (DashTimer >= 90)
                {
					npc.TargetClosest(true);
					npc.netUpdate = true;
					Vector2 PlayerPosition = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
					PlayerPosition.Normalize();
					npc.velocity = PlayerPosition * 8f;
					DashTimer = 0;
				}
			}
		}
		private void AttackTypes()
        {
			Player player = Main.player[npc.target];
			bool[] attackTypes = new bool[]
			{
				aetherRain,
				lightningShock,
				gasSpew
			};

		}
		private void NormalStateMovement()
		{
			npc.TargetClosest(true);
			float num = 7.5f;
			float Multiplier = 0.045f; //Velocity Multiplier

			Vector2 Center = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
			float PosX = Main.player[npc.target].Center.X + (Main.player[npc.target].width / 2);
			float PosY = Main.player[npc.target].Center.Y + (Main.player[npc.target].height / 2);

			PosX = (int)(PosX / 8f) * 8; PosY = (int)(PosY / 8f) * 8;
			Center.X = (int)(Center.X / 8f) * 8; Center.Y = (int)(Center.Y / 8f) * 8;
			PosX -= Center.X; PosY -= Center.Y;

			float Center2 = (float)Math.Sqrt(PosX * PosX + PosY * PosY);
			if (Center2 == 0f)
			{
				PosX = npc.velocity.X; PosY = npc.velocity.Y;
			}
			else
			{
				Center2 = num / Center2;
				PosX *= Center2;
				PosY *= Center2;
			}

			if (npc.velocity.X < PosX)
				npc.velocity.X = npc.velocity.X + Multiplier;

			else if (npc.velocity.X > PosX)
				npc.velocity.X = npc.velocity.X - Multiplier;

			if (npc.velocity.Y < PosY)
				npc.velocity.Y = npc.velocity.Y + Multiplier;

			else if (npc.velocity.Y > PosY)
				npc.velocity.Y = npc.velocity.Y - Multiplier;
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, 16, hitDirection, -1f, 0, default(Color), 1f);
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, 825, 1f);
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, 16, hitDirection, -1f, 0, default(Color), 1f);
				}
				for (int k2 = 0; k2 < 3; k2++)
				{
					Gore.NewGore(npc.position, npc.velocity, 826, Main.rand.NextFloat(1.3f, 1.75f));
				}
			}
		}
	}
}
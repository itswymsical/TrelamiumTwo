using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Utilities.Extensions;
using TrelamiumTwo.Content.Items.ForestGuardian;

using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.ForestGuardian
{
	[AutoloadBossHead]
	public class ForestGuardian : ModNPC
	{
		public enum AIState
        {
			Idle,
			Phase2
        }
		private AIState State
		{
			get => (AIState)npc.ai[0];
			set => npc.ai[0] = (int)value;
		}
		public float AITimer
		{
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forest Guardian");
			NPCID.Sets.TrailCacheLength[npc.type] = 10;
			NPCID.Sets.TrailingMode[npc.type] = 0;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 200;
			npc.height = 184;

			npc.damage = 0;
			npc.defense = 13;
			npc.lifeMax = 1800;

			npc.noTileCollide = true;
			npc.noGravity = true;
			npc.boss = true;
			npc.lavaImmune = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit18;
			npc.DeathSound = SoundID.NPCDeath13;
			npc.value = Item.sellPrice(0, 0, 3, 0);
			bossBag = ItemType<ForestGuardianBag>();
		}
		bool handsSpawned;
		private bool slamming;
		private Player target; 
		public override void NPCLoot()
		{
			int choice = Main.rand.Next(4);
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				if (choice == 0)
				{
					Item.NewItem(npc.getRect(), ItemType<AlluvialCollider>());
				}
				else if (choice == 1)
				{
					Item.NewItem(npc.getRect(), ItemType<Earthbound>());
				}
				else if (choice == 2)
				{
					Item.NewItem(npc.getRect(), ItemType<PrimordialEarth>());
				}
				else if (choice == 3)
				{
					Item.NewItem(npc.getRect(), ItemType<TheAncient>());
				}
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem(npc.getRect(), ItemType<ForestGuardianMask>());
				}
			}
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax / Main.expertLife * 1.45f * bossLifeScale);
			npc.damage = (int)(npc.damage * .5f);
		}
		public override void AI()
		{
			target = Main.player[npc.target];
			if (target.dead || !target.active)
			{
				npc.TargetClosest(false);
				npc.velocity = new Vector2(0f, -10f);
				npc.timeLeft--;
				if (npc.timeLeft <= 0)
				{
					npc.active = false;
				}
			}

			if (!handsSpawned)
			{
				var Left = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<GuardianArmLeft>());
				Main.npc[Left].ai[0] = npc.whoAmI;
				var Right = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<GuardianArmRight>());
				Main.npc[Right].ai[0] = npc.whoAmI;
				handsSpawned = true;
			}

			if (NPC.AnyNPCs(NPCType<GuardianArmLeft>()) || NPC.AnyNPCs(NPCType<GuardianArmRight>()))
			{
				npc.dontTakeDamage = true;
				Movement();
			}
			else
			{
				npc.dontTakeDamage = false;
				State = AIState.Phase2;		
			}
			if (State == AIState.Phase2)
            {
                AITimer++;
				if (AITimer < 240)
                {
					Movement();
                }
				if (AITimer >= 240 && AITimer <= 380)
                {
					Slam();		
                }
				if (AITimer > 380)
                {
					AITimer = 0;
                }
			}
		}
		private void Move(Vector2 position, float speed)
		{
			Vector2 direction = npc.DirectionTo(position);

			Vector2 velocity = direction * speed;

			npc.velocity = Vector2.SmoothStep(npc.velocity, velocity, 0.2f);
		}
		private void Movement()
		{
			npc.noTileCollide = true;
			var position = new Vector2(target.Center.X, target.Center.Y - 320);
			float speed = Vector2.Distance(npc.Center, position);
			speed = MathHelper.Clamp(speed, -8f, 8f);
			Move(position, speed);
			npc.rotation = Utils.AngleLerp(npc.rotation, npc.velocity.X * 0.01f, 0.2f);
		}
		private void Slam()
		{
			npc.noTileCollide = false;

			if (!slamming)
			{
				npc.damage = 0;
				npc.ai[2]++;
				npc.velocity.X = 0;
				if (npc.position.Y > npc.Center.Y)
				{
					npc.velocity.Y -= 0.1f;
				}
				else
				{
					npc.velocity.Y *= 0.8f;
				}
				slamming = true;
				npc.ai[2] = 0;
			}
			else
			{
				npc.damage = 60;
				npc.ai[2] += 0.08f;

				npc.velocity.Y += npc.ai[2];
			}
			if (npc.collideY && npc.position.Y == npc.oldPosition.Y)
			{
				CheckPlatform(target);
				Collision.HitTiles(npc.position, npc.velocity, npc.width, npc.height);
				npc.ai[2] = 0;
				slamming = !slamming;

				Main.player[npc.target].GetModPlayer<Common.Players.TrelamiumPlayer>().shakeEffects = Math.Abs(npc.velocity.Y * 3f);
			}
		}
		private void CheckPlatform(Player player) // Spirit Mod :kek:
		{
			bool onplatform = true;
			for (int i = (int)npc.position.X; i < npc.position.X + npc.width; i += npc.height / 2)
			{
				Tile tile = Framing.GetTileSafely(new Point((int)npc.position.X / 16, (int)(npc.position.Y + npc.height + 8) / 16));
				if (!TileID.Sets.Platforms[tile.type])
					onplatform = false;
			}
			if (onplatform && (npc.Center.Y < player.position.Y - 20))
				npc.noTileCollide = true;
			else
				npc.noTileCollide = false;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			npc.DrawNPCTrailCentered(spriteBatch, lightColor, 0.8f, 0.25f, 1);

			return npc.DrawNPCCentered(spriteBatch, lightColor);
		}
	}
}

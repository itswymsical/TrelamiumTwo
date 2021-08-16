using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.Forest
{
	public class WoodLog : ModNPC
	{
		public override string Texture => Assets.NPCs.Forest + "WoodLog";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wood Log");

			Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 30;
			NPC.damage = 6;
			NPC.defense = 2;

			NPC.width = 48;
			NPC.height = 28;

			NPC.knockBackResist = 0.2f;

			NPC.aiStyle = -1;
			aiType = -1;

			NPC.value = Item.sellPrice(copper: 10);
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
		}

		private int frame;

		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;

			if (NPC.ai[2] == 0f)
			{
				frame = 0;
			}
			else
			{
				NPC.frameCounter++;

				if (NPC.frameCounter > 5f)
				{
					frame++;	

					NPC.frameCounter = 0f;
				}

				if (frame > 4)
					frame = 1;
			}

			NPC.frame.Y = frame * frameHeight;
		}

		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit) => Wake();

		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit) => Wake();

		public override void AI()
		{
			if (NPC.ai[2] == 1f)
				NPC.GenericFighterAI();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
				for (int i = 1; i <= 3; i++)
					Gore.NewGore(NPC.Center, NPC.velocity, Mod.GetGoreSlot("Gores/WoodLog/WoodLogGore" + i));

			for (int i = 0; i < 5; i++)
				Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Wood>(), hitDirection, -1f);		
		}

		public override void NPCLoot()
		{
			Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Materials.Leaf>(), Main.rand.Next(4));
			Item.NewItem(NPC.getRect(), ItemID.Wood, Main.rand.Next(4, 9));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDay.Chance * 0.2f;

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => NPC.DrawNPCCentered(spriteBatch, drawColor);

		private void Wake()
		{
			if (NPC.ai[2] == 1f)
				return;

			frame = 2;

			NPC.velocity.Y = -6f;

			NPC.ai[2] = 1f;
		}
	}
}
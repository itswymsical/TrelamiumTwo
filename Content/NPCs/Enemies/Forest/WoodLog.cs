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

			Main.npcFrameCount[npc.type] = 5;
		}

		public override void SetDefaults()
		{
			npc.lifeMax = 30;
			npc.damage = 6;
			npc.defense = 2;

			npc.width = 48;
			npc.height = 28;

			npc.knockBackResist = 0.2f;

			npc.aiStyle = -1;
			aiType = -1;

			npc.value = Item.sellPrice(copper: 10);
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
		}

		private int frame;

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;

			if (npc.ai[2] == 0f)
			{
				frame = 0;
			}
			else
			{
				npc.frameCounter++;

				if (npc.frameCounter > 5f)
				{
					frame++;	

					npc.frameCounter = 0f;
				}

				if (frame > 4)
					frame = 1;
			}

			npc.frame.Y = frame * frameHeight;
		}

		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit) => Wake();

		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit) => Wake();

		public override void AI()
		{
			if (npc.ai[2] == 1f)
				npc.GenericFighterAI();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
				for (int i = 1; i <= 3; i++)
					Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/WoodLog/WoodLogGore" + i));

			for (int i = 0; i < 5; i++)
				Dust.NewDust(npc.Center, npc.width, npc.height, ModContent.DustType<Wood>(), hitDirection, -1f);		
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.Leaf>(), Main.rand.Next(4));
			Item.NewItem(npc.getRect(), ItemID.Wood, Main.rand.Next(4, 9));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDay.Chance * 0.2f;

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => npc.DrawNPCCentered(spriteBatch, drawColor);

		private void Wake()
		{
			if (npc.ai[2] == 1f)
				return;

			frame = 2;

			npc.velocity.Y = -6f;

			npc.ai[2] = 1f;
		}
	}
}
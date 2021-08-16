using TrelamiumTwo.Content.Dusts;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.Desert
{
	public class SandcrawlerQueen : ModNPC
	{
		public override string Texture => Assets.NPCs.Desert + "SandcrawlerQueen";
		public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = 4;

		public override void SetDefaults()
		{
			NPC.lifeMax = 180;
			NPC.damage = 14;
			NPC.defense = 8;

			NPC.width = 108;
			NPC.height = 82;

			NPC.knockBackResist = 0.2f;

			NPC.aiStyle = -1;
			aiType = -1;

			NPC.value = Item.sellPrice(copper: 22);
			NPC.HitSound = SoundID.NPCHit31;
			NPC.DeathSound = SoundID.NPCDeath1;
		}

		private int frame;
		public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = -NPC.direction;
			NPC.frameCounter++;

			if (NPC.frameCounter > 4f)
			{
				frame++;

				NPC.frameCounter = 0f;
			}

			if (frame >= 3)
				frame = 0;

			NPC.frame.Y = frame * frameHeight;
		}
		public override void AI() => NPC.GenericFighterAI();
		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
				for (int i = 1; i <= 3; i++)
					Gore.NewGore(NPC.Center, NPC.velocity, Mod.GetGoreSlot("Gores/Sandcrawler/SandcrawlerGore" + i));

			for (int i = 0; i < 5; i++)
				Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Wood>(), hitDirection, -1f);		
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(2) == 0)
				Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Materials.SandcrawlerShell>(), Main.rand.Next(1, 2));

			if (Main.rand.Next(2) == 0)
				Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Materials.HardenedCarapace>(), Main.rand.Next(1, 2));

				Item.NewItem(NPC.getRect(), ModContent.ItemType<Items.Materials.AntlionChitin>(), Main.rand.Next(2, 4));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.DesertCave.Chance * 0.15f;
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => NPC.DrawNPCCentered(spriteBatch, drawColor);

	}
}
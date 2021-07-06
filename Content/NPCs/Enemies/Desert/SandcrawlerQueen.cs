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
		public override void SetStaticDefaults() => Main.npcFrameCount[npc.type] = 4;

		public override void SetDefaults()
		{
			npc.lifeMax = 180;
			npc.damage = 14;
			npc.defense = 8;

			npc.width = 108;
			npc.height = 82;

			npc.knockBackResist = 0.2f;

			npc.aiStyle = -1;
			aiType = -1;

			npc.value = Item.sellPrice(copper: 22);
			npc.HitSound = SoundID.NPCHit31;
			npc.DeathSound = SoundID.NPCDeath1;
		}

		private int frame;
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = -npc.direction;
			npc.frameCounter++;

			if (npc.frameCounter > 4f)
			{
				frame++;

				npc.frameCounter = 0f;
			}

			if (frame >= 3)
				frame = 0;

			npc.frame.Y = frame * frameHeight;
		}
		public override void AI() => npc.GenericFighterAI();
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
				for (int i = 1; i <= 3; i++)
					Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/Sandcrawler/SandcrawlerGore" + i));

			for (int i = 0; i < 5; i++)
				Dust.NewDust(npc.Center, npc.width, npc.height, ModContent.DustType<Wood>(), hitDirection, -1f);		
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(2) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.SandcrawlerShell>(), Main.rand.Next(1, 2));

			if (Main.rand.Next(2) == 0)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.HardenedCarapace>(), Main.rand.Next(1, 2));

				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.Materials.AntlionChitin>(), Main.rand.Next(2, 4));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.DesertCave.Chance * 0.15f;
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor) => npc.DrawNPCCentered(spriteBatch, drawColor);

	}
}
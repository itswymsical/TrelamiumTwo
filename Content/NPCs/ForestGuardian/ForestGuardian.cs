using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.ForestGuardian
{
	public class ForestGuardian : ModNPC
	{
		private enum AIState
		{
			Idle = 0,
			Smash = 1
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forest Guardian");
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 200;
			npc.height = 184;

			npc.damage = 0;
			npc.defense = 8;
			npc.lifeMax = 1500;

			npc.noGravity = true;
			npc.boss = true;
			npc.lavaImmune = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.NPCHit18;
			npc.DeathSound = SoundID.NPCDeath13;
			npc.value = Item.buyPrice(0, 0, 1, 25);
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Forest");
		}
		bool spawn;
		public override void AI()
		{
			if (!spawn)
			{
				NPC.NewNPC((int)npc.Center.X - 120, (int)npc.Center.Y - 40, NPCType<ForestGuardian_Left>());
				NPC.NewNPC((int)npc.Center.X + 120, (int)npc.Center.Y - 40, NPCType<ForestGuardian_Right>());
				spawn = true;
			}
		}
	}
	public class ForestGuardian_Left : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guardian's Fist L");
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 54;
			npc.height = 84;

			npc.damage = 30;
			npc.defense = 8;
			npc.lifeMax = 350;

			npc.noGravity = true;
			npc.lavaImmune = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
        public override void AI()
        {
			Player target = Main.player[npc.target];
			
        }
    }
	public class ForestGuardian_Right : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guardian's Fist");
			NPCID.Sets.TrailCacheLength[npc.type] = 5;
			NPCID.Sets.TrailingMode[npc.type] = 0;
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 54;
			npc.height = 84;

			npc.damage = 30;
			npc.defense = 8;
			npc.lifeMax = 350;

			npc.noGravity = true;
			npc.lavaImmune = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
		private int DashTimer;
		public override void AI()
		{
			Player target = Main.player[npc.target];
			npc.TargetClosest(true);
			npc.spriteDirection = npc.direction;
			DashTimer++;
			if (DashTimer >= 90)
			{
				npc.TargetClosest(true);
				npc.netUpdate = true;
				Vector2 PlayerPosition = new Vector2(target.Center.X - npc.Center.X, target.Center.Y - npc.Center.Y);
				PlayerPosition.Normalize();
				npc.velocity = PlayerPosition * 5f;
				DashTimer = 0;
			}
		}
	}
}
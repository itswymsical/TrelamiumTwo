using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TrelamiumTwo.Content.NPCs.ForestGuardian
{
	public class ForestGuardian : ModNPC
	{
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
			DisplayName.SetDefault("Guardian's Fist");
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 54;
			npc.height = 84;

			npc.damage = 30;
			npc.defense = 8;
			npc.lifeMax = 250;

			npc.noGravity = true;
			npc.lavaImmune = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
	}
	public class ForestGuardian_Right : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Guardian's Fist");
		}
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 54;
			npc.height = 84;

			npc.damage = 30;
			npc.defense = 8;
			npc.lifeMax = 250;

			npc.noGravity = true;
			npc.lavaImmune = true;

			npc.knockBackResist = 0f;
			npc.HitSound = SoundID.DD2_CrystalCartImpact;
			npc.DeathSound = SoundID.Item14;
		}
	}
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.NPCs.Overworld
{
    public class MushroomSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Slime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            npc.aiStyle = 1;
            npc.knockBackResist = 0.75f;
            npc.damage = 18;
            npc.lifeMax = 45;
            npc.defense = 2;
            npc.width = 32;
            npc.height = 26;
            animationType = NPCID.BlueSlime;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.buyPrice(0, 0, 0, 20);
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.5f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDaySlime.Chance * 0.215f;
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom, Main.rand.Next(1, 3));
            }
        }
    }
}
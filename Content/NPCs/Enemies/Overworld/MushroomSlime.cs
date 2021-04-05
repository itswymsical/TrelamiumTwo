using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Trelamium2.Content.NPCs.Enemies.Overworld
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
            npc.width = 32;
            npc.height = 26;

            npc.damage = 18;
            npc.lifeMax = 45;
            npc.defense = 2;
            npc.aiStyle = 1;

            npc.knockBackResist = 0.75f;

            animationType = NPCID.BlueSlime;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(copper: 20);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.5f);
        } // Arent enemy stats automatically scaled?

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDaySlime.Chance * 0.215f;

        public override void NPCLoot()
        {
            if (Main.rand.NextBool(2))
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom, Main.rand.Next(1, 3));           
        }
    }
}
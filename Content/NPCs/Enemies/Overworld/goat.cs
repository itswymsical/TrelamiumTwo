using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.Overworld
{
    public class Goat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goat");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 78;
            npc.height = 56;

            npc.damage = 18;
            npc.lifeMax = 45;
            npc.defense = 2;
            npc.aiStyle = 7;

            npc.knockBackResist = 0.75f;

            animationType = NPCID.Zombie;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;

            npc.value = Item.buyPrice(copper: 20);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDay.Chance * 0.235f;

    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.DruidsGarden
{
    public class BlossomSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blossom Slime");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 32;

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

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDaySlime.Chance * 0.215f;
        public override void AI() 
            => npc.direction = -npc.spriteDirection;
    }
}
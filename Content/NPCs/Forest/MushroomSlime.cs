using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.NPCs.Enemies.Forest
{
    public class MushroomSlime : ModNPC
    {
        public override string Texture => Assets.NPCs.Forest + "MushroomSlime";
        public override void SetStaticDefaults()
        {
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

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDaySlime.Chance * 0.215f;
        public override void AI() => npc.direction = npc.spriteDirection;
        public override void NPCLoot()
        {
            if (Main.rand.NextBool(2))
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom, Main.rand.Next(1, 3));           
        }
    }
}
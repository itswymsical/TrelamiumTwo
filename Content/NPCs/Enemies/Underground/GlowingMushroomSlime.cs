using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.Underground
{
    public class GlowingMushroomSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Mushroom Slime");
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

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.UndergroundMushroom.Chance * 0.35f;

        public override void NPCLoot()
        {
            if (Main.rand.NextBool(2))
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GlowingMushroom, Main.rand.Next(1, 3));           
        }
    }
}
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
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 26;

            NPC.damage = 12;
            NPC.lifeMax = 20;
            NPC.defense = 5;
            NPC.aiStyle = 1;
            aiType = NPCID.YellowSlime;
            NPC.knockBackResist = 0.75f;

            animationType = NPCID.BlueSlime;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.value = Item.buyPrice(copper: 20);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.OverworldDaySlime.Chance * 0.225f;
        public override void AI() => NPC.direction = NPC.spriteDirection;
        public override void NPCLoot() => Item.NewItem(NPC.getRect(), ItemID.Mushroom, Main.rand.Next(3));
        
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
                for (int i = 1; i <= 35; i++)
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.Mushroom>(), hitDirection, 1f);

        }
    }
}
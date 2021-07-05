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

            npc.damage = 12;
            npc.lifeMax = 20;
            npc.defense = 5;
            npc.aiStyle = 1;
            aiType = NPCID.YellowSlime;
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
            if (Main.rand.NextBool(3))
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom, Main.rand.Next(1, 3));
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
                for (int i = 1; i <= 30; i++)
                    Dust.NewDust(npc.Center, npc.width, npc.height, ModContent.DustType<Dusts.Mushroom>(), hitDirection, -1f);

        }
    }
}
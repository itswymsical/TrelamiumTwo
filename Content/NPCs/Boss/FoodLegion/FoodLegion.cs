using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;

using Terraria.ModLoader;
using TrelamiumTwo.Content.Items.Boss.Fungore;
using TrelamiumTwo.Content.Items.Consumable.Food;
using TrelamiumTwo.Content.Projectiles.Melee;
using TrelamiumTwo.Core;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Content.NPCs.Boss.FoodLegion
{
    public class FoodLegion : ModNPC
    {
        private enum States
        {
            Walking,
            Punching,
            Jumping,
            SuperJumping
        }

        private States State
        {
            get => (States)npc.ai[0];
            set => npc.ai[0] = (int)value;
        }

        private float AttackCooldown
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }

        private Player player;

        private int frameY;
        private int frameX;
        private int punchDirection;

        public override string Texture => Assets.NPCs.FoodLegion + "FoodLegion";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Food Legion");

            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.boss = true;
            npc.lavaImmune = true;

            npc.width = npc.height = 94;

            npc.lifeMax = 5000;
            npc.defense = 16;
            npc.damage = 22;

            npc.knockBackResist = 0f;

            npc.aiStyle = -1;
            aiType = -1;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            //bossBag = ModContent.ItemType<FungoreBag>();
            npc.value = Item.buyPrice(gold: 1);
        }
        public override void AI()
        {
            base.AI();
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = State == States.Punching ? punchDirection : npc.direction;

            npc.frame.Width = 94;
            npc.frame.Height = 96;
            npc.frameCounter++;

            int frameRate = 4;

            if (npc.frameCounter > frameRate)
            {
                frameY++;

                npc.frameCounter = 0;
            }

            frameX = State == States.Walking ? 0 : State == States.Punching ? 1 : State == States.Jumping ? 2 : 3;

            if (State == States.Walking && frameY > 7)
            {
                frameY = 0;
            }
            npc.frame.Y = frameY * frameHeight;
            npc.frame.X = frameX * npc.frame.Width;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * .5f * bossLifeScale);
            float damage = npc.damage;
            npc.damage += (int)(damage * .15f);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.LesserHealingPotion;
            int foodChoice = Main.rand.Next(5);

            if (foodChoice == 0)
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Apple>(), Main.rand.Next(4));
            if (foodChoice == 1)
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Banana>(), Main.rand.Next(4));
            if (foodChoice == 2)
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Onion>(), Main.rand.Next(3));
            if (foodChoice == 3)
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Elderberry>(), Main.rand.Next(3));
            if (foodChoice == 4)
                Item.NewItem(npc.Hitbox, ModContent.ItemType<Carrot>(), Main.rand.Next(3));

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
        }
    }
}
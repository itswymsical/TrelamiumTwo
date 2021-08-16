using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.NPCs.Enemies.Underground
{
    public class IllustriousSlime : ModNPC
    {
        public override string Texture => Assets.NPCs.Underground + "IllustriousSlime";
        public override void SetStaticDefaults() => Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        
        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 40;

            NPC.damage = 17;
            NPC.lifeMax = 68;
            NPC.defense = 9;
            NPC.aiStyle = 1;

            NPC.alpha = 100;
            aiType = NPCID.BlueSlime;
            NPC.knockBackResist = 0.45f;

            animationType = NPCID.BlueSlime;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.value = Item.buyPrice(silver: 2, copper: 80);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return base.PreDraw(spriteBatch, drawColor);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int alpha = (int)NPC.ai[1] / 4;
            SpriteEffects spriteEffects = SpriteEffects.None;

            if (NPC.spriteDirection == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
            
            Color color = Lighting.GetColor((int)(NPC.position.X + NPC.width * 0.5) / 16, (int)((NPC.position.Y + NPC.height * 0.5) / 16.0));
            Color whiteAlpha = new Color(255, 0, 0, alpha / 2);

            Texture2D text = Main.npcTexture[NPC.type];
            //Texture2D textGlow = ModContent.GetTexture(Texture + "_Glow");

            Vector2 drawOrigin = NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);
            Vector2 rot = NPC.rotation.ToRotationVector2();

            Rectangle rectangle = new Rectangle(0, 0, text.Width, text.Height);
            Vector2 origin2 = rectangle.Size() / 2f;

            int i = 1;
            int num = 1;
            float value3 = 0.5f;
            float num2 = 0f;
            int num3 = num;

            while ((i > 0 && num3 < 0) || (i < 0 && num3 > 0))
            {
                NPC.GetAlpha(color);
                if (!(NPC.oldPos[num3] == Vector2.Zero))
                {
                    float num161 = num3 / num;
                    if (num161 < 0.5f)
                    {
                        color = Color.Lerp(color, whiteAlpha, Utils.InverseLerp(0f, 0.5f, num161, false));
                        goto IL_65CC;
                    }
                    color = Color.Lerp(whiteAlpha, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), Utils.InverseLerp(0.5f, 1f, num161, false));
                    goto IL_65CC;
                }
                else
                {
                    goto IL_65CC;
                }
            IL_65B4:
                num3 += i;
                continue;
            IL_65CC:

                float i2 = 0 - num3;

                if (i < 0)
                    i2 = num - num3;

                color *= i2 / NPCID.Sets.TrailCacheLength[NPC.type] * 1.5f;

                Vector2 value4 = NPC.oldPos[num3];
                float num163 = NPC.rotation;
                SpriteEffects effects = spriteEffects;

                Main.spriteBatch.Draw(text, value4 + NPC.Size / 2f - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), 
                    new Rectangle?(rectangle), color, num163 + NPC.rotation * num2 * (num3 - 1) * -spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, MathHelper.Lerp(NPC.scale, value3, num3 / 15f), effects, 0f);
                goto IL_65B4;
            }

            for (int i3 = 0; i3 < 4; i3++)
            {
                spriteBatch.Draw(text, drawOrigin + rot.RotatedBy(1.57079637f * i3, default) * 4f, new Rectangle?(rectangle), whiteAlpha, NPC.rotation, origin2, NPC.scale, spriteEffects, 0f);
            }
            
            spriteBatch.Draw(text, drawOrigin, new Rectangle?(rectangle), NPC.GetAlpha(color), NPC.rotation, origin2, NPC.scale, spriteEffects, 0f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnCondition.Underground.Chance * 0.26f;
        public override void NPCLoot() => Item.NewItem(NPC.getRect(), ModContent.ItemType<IllustriousShard>(), Main.rand.Next(3));  
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
                for (int i = 1; i <= 60; i++)
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.t_Slime, hitDirection, hitDirection, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
        }
    }

    public class IllustriousShard : ModItem
    {
        public override string Texture => Assets.PlaceholderTexture;
        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.Expert;
            Item.value = Item.sellPrice(silver: 9);
        }
    }
}
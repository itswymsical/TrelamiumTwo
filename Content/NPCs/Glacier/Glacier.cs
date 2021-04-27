#region Using Directives
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
#endregion

namespace TrelamiumTwo.Content.NPCs.Glacier
{
    public sealed class Glacier : ModNPC
    {
        public override string HeadTexture => TrelamiumTwo.PLACEHOLDER_TEXTURE;
        #region AIState
        private enum AIState
        {
            Phase1 = 0,
            Phase2 = 1,
            Rage = 2
        }
        private AIState State
        {
            get => (AIState)npc.ai[0];
            set => npc.ai[0] = (int)value;
        }
        public float TeleportTimer
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        private float AttackTimer
        {
            get => npc.ai[2];
            set => npc.ai[2] = value;
        }
        private bool frostboundArk;
        private bool iceChains;
        private bool permafrost;
        #endregion
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glacier");
            //Main.npcFrameCount[npc.type] = 6;
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 132;
            npc.height = 172;

            npc.damage = 30;
            npc.defense = 16;
            npc.lifeMax = 12000;

            npc.noGravity = true;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noTileCollide = true;

            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit18;
            npc.DeathSound = SoundID.NPCDeath13;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/glacier");
        }
        int First = 0;
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Spawn <= 450 || npc.ai[3] == 2)
            {
                return false;
            }
            return true;
        }
        int Spawn = 0;
        int rotTimer = 0;
        public override void AI()
        {
            rotTimer++;
            npc.spriteDirection = npc.direction;
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            int num414 = (int)npc.Center.X;
            int num415 = (int)npc.Center.Y;
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 150 && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        npc.timeLeft = 150;
                        npc.netUpdate = true;
                    }
                    return;
                }
            }
            Spawn += 1;
            if (Spawn <= 275)
            {
                npc.dontTakeDamage = true;
                if (Spawn <= 5)
                {
                    npc.alpha = 200;
                }
                if (Spawn >= 50)
                {
                    npc.alpha -= 2;
                    npc.velocity = new Vector2(0f, -0.5f);
                }
                if (Spawn == 250)
                {
                    for (int num623 = 0; num623 < 100; num623++)
                    {
                        int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("HollowBurn"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num624].noGravity = true;
                        Main.dust[num624].velocity *= 4f;
                    }
                }
                if (Spawn == 275)
                {
                    npc.alpha = 0;
                    npc.ai[3] = 0;
                }
            }
            else
            {
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                }
                float maxSpeed = 20f;
                if ((float)(player.position.X - npc.Center.X) >= 550f || (float)(player.position.Y - npc.Center.Y) >= 550f)
                {
                    maxSpeed = 35f;
                }
                else
                {
                    maxSpeed = 20f;
                }
                if (npc.ai[0] == 1)
                {
                    int amount = 175 - Main.rand.Next(150);
                    int amount2 = 35 - Main.rand.Next(10);
                    int amount3 = 35 - Main.rand.Next(10);
                    float positionX = player.position.X - amount;
                    float positionY = player.position.Y - amount;
                    if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        positionX = player.position.X + amount + amount3;
                        positionY = player.position.Y - amount + amount2;
                    }
                    else if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        positionX = player.position.X + amount + amount2;
                        positionY = player.position.Y + amount + amount3;
                    }
                    else if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        positionX = player.position.X - amount + amount3;
                        positionY = player.position.Y + amount + amount2;
                    }
                    npc.ai[0] = 2;
                    if (npc.position.X == positionX)
                    {
                        npc.velocity = new Vector2(0f, 0f);
                    }
                    if (npc.position.Y == positionY)
                    {
                        npc.velocity = new Vector2(0f, 0f);
                    }
                    Vector2 toTarget = new Vector2(positionX - npc.Center.X, positionY - npc.Center.Y);
                    toTarget = new Vector2(positionX - npc.Center.X, positionY - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * maxSpeed;
                    npc.netUpdate = true;
                }
                if (npc.ai[0] >= 2)
                {
                    npc.velocity.X *= 0.999f;
                    npc.velocity.Y *= 0.999f;
                    npc.ai[0] += 1;
                }
                if (npc.ai[0] >= 40)
                {
                    npc.velocity.X *= 0.7f;
                    npc.velocity.Y *= 0.7f;
                }
                if (npc.ai[0] == 90)
                {
                    npc.ai[0] = 0;
                }
                if (npc.ai[3] == 0)
                {
                    int num001 = 65;
                    int num003 = 135;
                    int num004 = 190;
                    int num005 = 60;

                    int num006 = num005 *= 2;
                    if ((double)npc.life < (double)npc.lifeMax * 0.90)
                    {
                        num001 = 60;
                        num003 = 125;
                        num004 = 175;
                        num005 = 56;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.65)
                    {
                        if (First == 0 && npc.ai[3] == 0)
                        {
                            npc.ai[3] = 1;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[0] = 1;
                            First = 1;
                        }
                        num001 = 45;
                        num003 = 110;
                        num004 = 150;
                        num005 = 48;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.35)
                    {
                        num001 = 35;
                        num003 = 75;
                        num004 = 125;
                        num005 = 38;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.14)
                    {
                        num001 = 15;
                        num003 = 45;
                        num004 = 65;
                        num005 = 25;
                    }
                    if (npc.ai[2] == 0)
                    {
                        npc.ai[0] = 1;
                        npc.ai[2] = 1;
                    }
                    if (npc.ai[2] >= 1 && npc.ai[2] <= 4)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] >= num001)
                        {
                            npc.ai[2] += 1;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                            Vector2 vector23 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num147 = 12f;
                            float num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                            float num149 = Math.Abs(num148) * 0.1f;
                            float num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y - num149;
                            float num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                            npc.netUpdate = true;
                            num151 = num147 / num151;
                            num148 *= num151;
                            num150 *= num151;
                            int num25;
                            for (int num154 = 0; num154 < 5; num154 = num25 + 1)
                            {
                                num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                                num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y;
                                num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                                num151 = 12f / num151;
                                num148 += (float)Main.rand.Next(-100, 101);
                                num150 += (float)Main.rand.Next(-100, 101);
                                num148 *= num151;
                                num150 *= num151;
                                Projectile.NewProjectile(vector23.X, vector23.Y, num148, num150, ProjectileID.FrostBlastHostile, 42, 0f, Main.myPlayer, 0f, 0f);
                                num25 = num154;
                            }
                        }
                    }
                    if (npc.ai[2] == 5)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 25)
                        {
                            npc.ai[0] = 1;
                        }
                        float Speed = 2f;
                        if (npc.ai[1] == num003)
                        {
                            if ((double)npc.life < (double)npc.lifeMax * 0.65 && Main.netMode != 1)
                            {
                                Projectile.NewProjectile((float)num414, (float)num415, 0f, 0f, ProjectileID.CultistBossIceMist, 25, 0f, Main.myPlayer, 0f, 0f);
                            }
                            Projectile.NewProjectile((float)num414, (float)num415, Speed, -Speed, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -Speed, -Speed, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, Speed, Speed, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -Speed, Speed, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, 0f, -Speed, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, 0f, Speed, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, Speed, 0f, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -Speed, 0f, ProjectileID.IcewaterSpit, 35, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (npc.ai[1] >= num004)
                        {
                            if (npc.life < npc.lifeMax * 0.90)
                            {
                                npc.ai[2] = 6;
                            }
                            else
                            {
                                npc.ai[2] = 0;
                            }
                            npc.netUpdate = true;
                            npc.ai[1] = 0;
                        }
                    }
                    if (npc.ai[2] >= 6 && npc.ai[2] <= 8)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 10)
                        {
                            npc.ai[0] = 1;
                            npc.netUpdate = true;
                        }
                        if (npc.ai[1] >= 75)
                        {
                            npc.ai[2] += 1;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                            Vector2 vector23 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num147 = 11f;
                            float num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                            float num149 = Math.Abs(num148) * 0.1f;
                            float num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y - num149;
                            float num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                            npc.netUpdate = true;
                            num151 = num147 / num151;
                            num148 *= num151;
                            num150 *= num151;
                            int num25;
                            for (int num154 = 0; num154 < 2; num154 = num25 + 1)
                            {
                                num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                                num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y;
                                num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                                num151 = 12f / num151;
                                num148 += (float)Main.rand.Next(-60, 61);
                                num150 += (float)Main.rand.Next(-60, 61);
                                num148 *= num151;
                                num150 *= num151;
                                Projectile.NewProjectile(vector23.X, vector23.Y, num148, num150, ProjectileID.FrostBlastHostile, 32, 0f, Main.myPlayer, 0f, 0f);
                                num25 = num154;
                            }
                        }
                    }
                    if (npc.ai[2] == 9)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == num005)
                        {
                            float Speed = 0.5f;
                            npc.netUpdate = true;
                            Projectile.NewProjectile((float)num414, (float)num415, 0f, -Speed, ProjectileID.CultistBossIceMist, 32, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (npc.ai[1] == num006)
                        {
                            npc.ai[2] = 11;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                        }
                    }
                    if (npc.ai[2] == 11)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 25 && Main.netMode != 1)
                        {
                            npc.ai[0] = 1;
                            npc.netUpdate = true;
                        }
                        if (npc.ai[1] == 135 && Main.netMode != 1)
                        {
                            Projectile.NewProjectile((float)num414, (float)num415, 3f, -3f, ProjectileID.FrostBlastHostile, 36, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -3f, -3f, ProjectileID.CultistBossIceMist, 36, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, 6f, -6f, ProjectileID.CultistBossIceMist, 36, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -6f, -6f, ProjectileID.FrostBlastHostile, 36, 0f, Main.myPlayer, 0f, 0f);
                            npc.netUpdate = true;
                        }
                        if (npc.ai[1] == 195)
                        {
                            if ((double)npc.life < (double)npc.lifeMax * 0.35)
                            {
                                npc.ai[2] = 12;
                            }
                            else
                            {
                                npc.ai[2] = 0;
                            }
                            npc.netUpdate = true;
                            npc.ai[1] = 0;
                        }
                    }
                    if (npc.ai[2] == 12)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 5)
                        {
                            npc.ai[0] = 1;
                            npc.netUpdate = true;
                        }
                        if (npc.ai[1] == num005)
                        {
                            if (Main.expertMode)
                            {
                                npc.netUpdate = true;
                                NPC.NewNPC((int)(npc.position.X -= 160), (int)(npc.position.Y -= 160), NPCID.IceElemental);
                                NPC.NewNPC((int)(npc.position.X += 160), (int)(npc.position.Y += 160), NPCID.IceElemental);
                                Main.PlaySound(SoundID.Item, (int)npc.position.X, (int)npc.position.Y, 20);
                            }
                        }
                        if (npc.ai[1] == num006)
                        {
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                            npc.ai[2] = 0;
                            npc.ai[0] = 0;
                            npc.ai[3] = 0;
                        }
                    }
                    npc.dontTakeDamage = false;
                }
                if (npc.ai[3] == 1)
                {
                    npc.dontTakeDamage = true;
                    npc.ai[1] += 1;
                    if (npc.ai[1] == 50 && Main.netMode != 1 || npc.ai[1] == 100 && Main.netMode != 1 || npc.ai[1] == 150 && Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                        float Speed = 12f;
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                        int damage = 32;
                        int type = ProjectileID.CultistBossIceMist;
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
                    if (npc.ai[1] >= 200)
                    {
                        npc.ai[1] = 0;
                        float Speed = 0.5f;
                        npc.netUpdate = true;
                        Projectile.NewProjectile((float)num414, (float)num415, 0f, -Speed, ModContent.ProjectileType<Projectiles.Hostile.FrostboundMagic>(), 32, 0f, Main.myPlayer, 0f, 0f);
                        npc.ai[2] = 0;
                        npc.ai[0] = 0;
                        npc.ai[3] = 2;
                        Projectile.NewProjectile((float)num414, (float)num415, -3f, -3f, ModContent.ProjectileType<Projectiles.Hostile.FrostboundMagic>(), 34, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                if (npc.ai[3] == 2)
                {
                    npc.ai[1] += 1;
                    if (npc.ai[1] <= 450)
                    {
                        npc.alpha = 200;
                        npc.ai[2] += 1;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        npc.alpha -= 4;
                    }
                    if (npc.ai[2] >= 45 && npc.ai[1] <= 450)
                    {
                        npc.ai[0] = 1;
                        npc.netUpdate = true;
                        npc.ai[2] = 0;
                        Projectile.NewProjectile((float)num414, (float)num415, 0f, 0f, ProjectileID.CultistBossIceMist, 34, 0f, Main.myPlayer, 0f, 0f);
                    }
                    if (npc.ai[1] >= 500 && Main.netMode != 1)
                    {
                        npc.ai[2] = 0;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                        npc.alpha = 0;
                        Projectile.NewProjectile((float)num414, (float)num415, 3f, -3f, ModContent.ProjectileType<Projectiles.Hostile.FrostboundMagic>(), 36, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int num281 = 25;
            for (int num282 = 0; num282 < num281; num282++)
            {
                int num283 = Dust.NewDust(npc.Center, 0, 0, DustID.Ice, 0f, 0f, 0, default, 1f);
                Dust dust = Main.dust[num283];
                dust.velocity *= 1.6f;
                Dust dust25 = Main.dust[num283];
                dust25.velocity.Y = dust25.velocity.Y - 1f;
                Main.dust[num283].position = Vector2.Lerp(Main.dust[num283].position, npc.Center, 1f);
            }
            if (npc.life <= 0)
            {
                for (int k = 0; k < 25; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Ice, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
        #region Draw Code
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            int alpha = (int)npc.ai[1] / 4;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            Texture2D texture2D3 = Main.npcTexture[npc.type];

            Rectangle rectangle = new Rectangle(0, 0, texture2D3.Width, texture2D3.Height);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color29 = npc.GetAlpha(color25);
            Main.spriteBatch.Draw(texture2D3, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY),
                new Rectangle?(rectangle), color29, npc.rotation, origin2, npc.scale, spriteEffects, 0f);
            Color color = new Color(13, 87, 189, alpha / 2);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                float scale = npc.scale * (npc.oldPos.Length - k) / npc.oldPos.Length * 1f;
                SpriteBatch spriteBatch1 = Main.spriteBatch;
                Texture2D texture = Main.npcTexture[npc.type];
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + origin2;
                Vector2 rotation = npc.rotation.ToRotationVector2();
                double rotAngle = 1.57079637f * k;
                Vector2 center = default;
                spriteBatch1.Draw(texture, drawPos + rotation.RotatedBy(rotAngle, center) * 4f,
                                    new Rectangle?(rectangle), color, npc.rotation, origin2, scale, spriteEffects, 0f);
            }
            return false;
        }     
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int alpha = (int)npc.ai[1] / 4;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            Texture2D texture2D3 = Main.npcTexture[npc.type];
            int num154 = Main.npcTexture[npc.type].Height;
            int y3 = num154;
            Rectangle rectangle = new Rectangle(0, 0, texture2D3.Width, texture2D3.Height);
            Vector2 origin2 = rectangle.Size() / 2f;
            int num155 = 0;
            int num156 = 1;
            int num157 = 1;
            float value3 = 0.5f;
            float num158 = 0f;
            int num159 = num157;
            while ((num156 > 0 && num159 < num155) || (num156 < 0 && num159 > num155))
            {
                Color color26 = color25;
                color26 = npc.GetAlpha(color26);
                if (!(npc.oldPos[num159] == Vector2.Zero))
                {
                    float num161 = num159 / num157;
                    if (num161 < 0.5f)
                    {
                        color26 = Color.Lerp(color26, new Color(13, 87, 189, alpha / 2), Utils.InverseLerp(0f, 0.5f, num161, false));
                        goto IL_65CC;
                    }
                    color26 = Color.Lerp(new Color(13, 87, 189, alpha / 2), Color.Cyan, Utils.InverseLerp(0.5f, 1f, num161, false));
                    goto IL_65CC;
                }
                else
                {
                    goto IL_65CC;
                }
            IL_65B4:
                num159 += num156;
                continue;
            IL_65CC:
                float num162 = num155 - num159;
                if (num156 < 0)
                {
                    num162 = num157 - num159;
                }

                color26 *= num162 / NPCID.Sets.TrailCacheLength[npc.type] * 1.5f;
                Vector2 oldPos = npc.oldPos[num159];
                float rotation = npc.rotation;
                rotation = rotTimer;
                SpriteEffects effects = spriteEffects;
                Main.spriteBatch.Draw(texture2D3, oldPos + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), new Rectangle?(rectangle),
                    color26, rotation + npc.rotation * num158 * (num159 - 1) * -spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(),
                    origin2, MathHelper.Lerp(npc.scale, value3, num159 / 15f), effects, 0f);
                goto IL_65B4;
            }

            Color color = new Color(13, 87, 189, alpha / 2);
            for (int k = 0; k < 4; k++)
            {
                SpriteBatch spriteBatch1 = Main.spriteBatch;
                Texture2D texture = Main.npcTexture[npc.type];
                Vector2 drawPos = npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY);
                Vector2 rotation = npc.rotation.ToRotationVector2();
                double rotAngle = 1.57079637f * k;
                Vector2 center = default;
                spriteBatch1.Draw(texture, drawPos + rotation.RotatedBy(rotAngle, center) * 4f,
                    new Rectangle?(rectangle), color, npc.rotation, origin2, npc.scale, spriteEffects, 0f);
            }
            Color color29 = npc.GetAlpha(color25);
            Main.spriteBatch.Draw(texture2D3, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY),
                new Rectangle?(rectangle), color29, npc.rotation, origin2, npc.scale, spriteEffects, 0f);
        }
        #endregion
    }
}
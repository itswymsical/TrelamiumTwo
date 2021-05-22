using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Hooks;

namespace TrelamiumTwo.Common.Tiles
{
    public class TrelamiumGlobalTile : GlobalTile
    {
        public override bool Drop(int i, int j, int type)
        {
            var ModPlayer = Main.LocalPlayer.GetModPlayer<Players.TrelamiumPlayer>();
            Player player = Main.LocalPlayer;
            int amount = Main.rand.Next(1, 3);

            if (ModPlayer.microlith && player.HeldItem.IsShovel()
            || ModPlayer.microlith && player.HeldItem.IsPickaxe())
            {
                if (Main.rand.Next(8) == 0)
                {
                    if (type == TileID.Copper)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.CopperOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(255, 146, 97), "Copper Harvest +" + amount);
                    }
                    if (Main.tileSpelunker[type] == true)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, type, amount);
                        CombatText.NewText(player.Hitbox, new Color(229, 220, 163), $"{type} Harvest +" + amount);
                    }
                    if (type == TileID.Tin)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.TinOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(229, 220, 163), "Tin Harvest +" + amount);
                    }
                    if (type == TileID.Iron)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.IronOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(105, 105, 105), "Iron Harvest +" + amount);
                    }
                    if (type == TileID.Lead)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.LeadOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(104, 141, 151), "Lead Harvest +" + amount);
                    }
                    if (type == TileID.Silver)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.SilverOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(162, 174, 175), "Silver Harvest +" + amount);
                    }
                    if (type == TileID.Tungsten)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.TungstenOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(76, 119, 73), "Tungsten Harvest +" + amount);
                    }
                    if (type == TileID.Gold)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.GoldOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(234, 208, 138), "Gold Harvest +" + amount);
                    }
                    if (type == TileID.Platinum)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.PlatinumOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(182, 195, 218), "Platinum Harvest +" + amount);
                    }
                } //Pre-Hardmode Tier
                if (Main.rand.Next(10) == 0)
                {
                    if (type == TileID.Amethyst)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.Amethyst, amount);
                        CombatText.NewText(player.Hitbox, new Color(166, 0, 237), "Amethyst Harvest +" + amount);
                    }
                    if (type == TileID.Topaz)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.Topaz, amount);
                        CombatText.NewText(player.Hitbox, new Color(255, 199, 0), "Topaz Harvest +" + amount);
                    }
                    if (type == TileID.Sapphire)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.Sapphire, amount);
                        CombatText.NewText(player.Hitbox, new Color(16, 148, 235), "Sapphire Harvest +" + amount);
                    }
                    if (type == TileID.Emerald)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.Emerald, amount);
                        CombatText.NewText(player.Hitbox, new Color(128, 247, 203), "Emerald Harvest +" + amount);
                    }
                    if (type == TileID.Ruby)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.Ruby, amount);
                        CombatText.NewText(player.Hitbox, new Color(243, 115, 114), "Ruby Harvest +" + amount);
                    }
                    if (type == TileID.Diamond)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.Diamond, amount);
                        CombatText.NewText(player.Hitbox, new Color(224, 231, 239), "Diamond Harvest +" + amount);
                    }
                    if (type == TileID.Meteorite)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.Meteorite, amount);
                        CombatText.NewText(player.Hitbox, new Color(228, 162, 172), "Meteorite Harvest +" + amount);
                    }
                    if (type == TileID.Demonite)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.DemoniteOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(164, 151, 220), "Demonite Harvest +" + amount);
                    }
                    if (type == TileID.Crimtane)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.CrimtaneOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(217, 56, 61), "Crimtane Harvest +" + amount);
                    }
                } //Gem & Misc Tier
                if (Main.rand.Next(11) == 0)
                {
                    if (type == TileID.Cobalt)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.CobaltOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(27, 141, 235), "Cobalt Harvest +" + amount);
                    }
                    if (type == TileID.Palladium)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.PalladiumOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(239, 90, 49), "Palladium Harvest +" + amount);
                    }
                    if (type == TileID.Mythril)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.MythrilOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(22, 119, 125), "Mythril Harvest +" + amount);
                    }
                    if (type == TileID.Orichalcum)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.OrichalcumOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(249, 42, 243), "Orichalcum Harvest +" + amount);
                    }
                    if (type == TileID.Adamantite)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.AdamantiteOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(210, 25, 28), "Adamantite Harvest +" + amount);
                    }
                    if (type == TileID.Titanium)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.TitaniumOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(86, 84, 94), "Titanium Harvest +" + amount);
                    }
                    if (type == TileID.Chlorophyte)
                    {
                        Item.NewItem(i * 16, j * 16, 16, 48, ItemID.ChlorophyteOre, amount);
                        CombatText.NewText(player.Hitbox, new Color(161, 236, 0), "Chlorophyte Harvest +" + amount);
                    }
                }
            }
            return true;
        }
    }
}
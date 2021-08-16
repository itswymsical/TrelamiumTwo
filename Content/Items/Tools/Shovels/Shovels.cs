using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Items;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Tools.Shovels
{
    public class WoodShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "WoodShovel";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Breaks a cluster of blocks");
        public override void SetDefaults()
        {
            DiggingPower(28);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 2;
            Item.useTime = Item.useAnimation = 17;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(copper: 33);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 20).AddTile(TileID.WorkBenches).Register();
        }
    }
    public class CopperShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "CopperShovel";
        public override void SetDefaults()
        {
            DiggingPower(32);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 2;
            Item.useTime = Item.useAnimation = 19;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(copper: 33);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.CopperBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
    public class TinShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "TinShovel";
        public override void SetDefaults()
        {
            DiggingPower(34);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 2;
            Item.useTime = Item.useAnimation = 19;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(copper: 46);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.TinBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
    public class IronShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "IronShovel";
        public override void SetDefaults()
        {
            DiggingPower(36);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 2;
            Item.useTime = Item.useAnimation = 19;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(copper: 80);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.IronBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
    public class LeadShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "LeadShovel";
        public override void SetDefaults()
        {
            DiggingPower(38);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 3;
            Item.useTime = Item.useAnimation = 19;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(silver: 1);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.LeadBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
    public class SilverShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "SilverShovel";
        public override void SetDefaults()
        {
            DiggingPower(42);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 3;
            Item.useTime = Item.useAnimation = 19;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(silver: 4);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.SilverBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
    public class TungstenShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "TungstenShovel";
        public override void SetDefaults()
        {
            DiggingPower(48);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 4;
            Item.useTime = Item.useAnimation = 19;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(silver: 4);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.TungstenBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
    public class GoldShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "GoldShovel";
        public override void SetDefaults()
        {
            DiggingPower(50);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 5;
            Item.useTime = Item.useAnimation = 21;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(silver: 5);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.GoldBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
    public class PlatinumShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "PlatinumShovel";
        public override void SetDefaults()
        {
            DiggingPower(58);

            Item.DamageType = DamageClass.Melee;
            Item.damage = 5;
            Item.useTime = Item.useAnimation = 21;
            Item.width = Item.height = 32;

            Item.autoReuse = Item.useTurn = true;

            Item.value = Item.sellPrice(silver: 7);

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wood, 8).AddIngredient(ItemID.PlatinumBar, 10).AddTile(TileID.Anvils).Register();
        }
    }
}
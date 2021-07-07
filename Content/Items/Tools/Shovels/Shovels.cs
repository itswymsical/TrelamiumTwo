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

            item.melee = true;
            item.damage = 2;
            item.useTime = item.useAnimation = 17;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(copper: 33);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class CopperShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "CopperShovel";
        public override void SetDefaults()
        {
            DiggingPower(32);

            item.melee = true;
            item.damage = 2;
            item.useTime = item.useAnimation = 19;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(copper: 33);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.CopperBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class TinShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "TinShovel";
        public override void SetDefaults()
        {
            DiggingPower(34);

            item.melee = true;
            item.damage = 2;
            item.useTime = item.useAnimation = 19;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(copper: 46);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.TinBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class IronShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "IronShovel";
        public override void SetDefaults()
        {
            DiggingPower(36);

            item.melee = true;
            item.damage = 2;
            item.useTime = item.useAnimation = 19;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(copper: 80);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class LeadShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "LeadShovel";
        public override void SetDefaults()
        {
            DiggingPower(38);

            item.melee = true;
            item.damage = 3;
            item.useTime = item.useAnimation = 19;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(silver: 1);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class SilverShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "SilverShovel";
        public override void SetDefaults()
        {
            DiggingPower(42);

            item.melee = true;
            item.damage = 3;
            item.useTime = item.useAnimation = 19;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(silver: 4);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.SilverBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class TungstenShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "TungstenShovel";
        public override void SetDefaults()
        {
            DiggingPower(48);

            item.melee = true;
            item.damage = 4;
            item.useTime = item.useAnimation = 19;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(silver: 4);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.TungstenBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class GoldShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "GoldShovel";
        public override void SetDefaults()
        {
            DiggingPower(50);

            item.melee = true;
            item.damage = 5;
            item.useTime = item.useAnimation = 21;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(silver: 5);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
    public class PlatinumShovel : ShovelItem
    {
        public override string Texture => Assets.Items.Shovels + "PlatinumShovel";
        public override void SetDefaults()
        {
            DiggingPower(58);

            item.melee = true;
            item.damage = 5;
            item.useTime = item.useAnimation = 21;
            item.width = item.height = 32;

            item.autoReuse = item.useTurn = true;

            item.value = Item.sellPrice(silver: 7);

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item18;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 8);
            recipe.AddIngredient(ItemID.PlatinumBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
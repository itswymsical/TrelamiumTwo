#region Using Directives
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
#endregion

namespace TrelamiumTwo.Content.Tiles.DruidsGarden
{
    public class AncientAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;

            disableSmartCursor = true;
            minPick = int.MaxValue;

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(new Color(0, 255, 125), name); name.SetDefault("Forest Altar");
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
        }

        public override void NumDust(int i, int j, bool fail, ref int num) 
            => num = fail ? 1 : 3;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            g = 3f;
            b = 0.5f;
        }
    }
}
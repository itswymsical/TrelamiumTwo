using Terraria.UI;
using System.Collections.Generic;

namespace TrelamiumTwo.Core.Abstraction
{
    public abstract class SmartUIState : UIState
    {
        public abstract int InsertionIndex(List<GameInterfaceLayer> layers);

        public virtual bool Visible { get; set; } = false;

        public virtual InterfaceScaleType Scale { get; set; } = InterfaceScaleType.UI;
    }
}

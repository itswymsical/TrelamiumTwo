using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TrelamiumTwo.Common.Configuration
{
	public class TrelamiumClientConfig : ModConfig
	{
		internal static TrelamiumClientConfig Instance => ModContent.GetInstance<TrelamiumClientConfig>();

		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("Visual Settings [i:75]")]

		[DefaultValue(true)]
		[Label("Discord Invite")]
		[Tooltip("Enables/Disables the discord invite button on the main menu.")]
		public bool DiscordInvite;

		[Header("Developer Settings [i:3611]")]

		[DefaultValue(false)]
		[Label("Debug Mode")]
		[Tooltip("Enables/Disables debug mode.")]
		public bool Debug;
	}
}

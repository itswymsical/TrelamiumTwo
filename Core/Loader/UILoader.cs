using TrelamiumTwo.Core.Abstracts;
using TrelamiumTwo.Core.Abstracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.UI;

namespace TrelamiumTwo.Core.Loaders
{
	internal sealed class UILoader : ILoadable
	{
		public float Priority => 1f;

		public bool LoadOnDedServer => false;

		public static List<SmartUIState> UIStates = new List<SmartUIState>();
		public static List<UserInterface> UserInterfaces = new List<UserInterface>();

		public void Load()
		{
			foreach (Type t in TrelamiumTwo.Instance.Code.GetTypes())
			{
				if (t.IsSubclassOf(typeof(SmartUIState)))
				{
					var userInterface = new UserInterface();
					var state = (SmartUIState)Activator.CreateInstance(t, null);

					userInterface.SetState(state);

					UIStates.Add(state);
					UserInterfaces.Add(userInterface);
				}
			}
		}

		public void Unload()
		{
			UIStates?.Clear();
			UserInterfaces?.Clear();
		}

		public static void AddLayer(List<GameInterfaceLayer> layers, UserInterface userInterface, UIState state, int index, bool visible, InterfaceScaleType scale)
		{
			string name = state == null ? "Unknown" : state.ToString();

			layers.Insert(index, new LegacyGameInterfaceLayer(TrelamiumTwo.AbbreviationPrefix + name,
				delegate
				{
					if (visible)
					{
						userInterface.Update(Main._drawInterfaceGameTime);
						state.Draw(Main.spriteBatch);
					}
					return true;
				},
				scale
				));
		}

		public static T GetUIState<T>() where T : SmartUIState => UIStates.FirstOrDefault(n => n is T) as T;
		public static void ReloadState<T>() where T : SmartUIState
		{
			var index = UIStates.IndexOf(GetUIState<T>());
			UIStates[index] = (T)Activator.CreateInstance(typeof(T), null);

			UserInterfaces[index] = new UserInterface();
			UserInterfaces[index].SetState(UIStates[index]);
		}
	}
}
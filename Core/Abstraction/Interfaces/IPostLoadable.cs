namespace TrelamiumTwo.Core.Abstraction.Interfaces
{
    public interface IPostLoadable
	{
		/// <summary>
		/// Stages loading of objects specific to the deriving class.
		/// </summary>
		void Load();
	}
}

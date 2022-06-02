namespace Game.Structures
{
	public interface IUpgradable
	{
		public void Upgrade();

		public bool IsMaxUpgraded();

		public bool CanBeUpgraded();
	}
}

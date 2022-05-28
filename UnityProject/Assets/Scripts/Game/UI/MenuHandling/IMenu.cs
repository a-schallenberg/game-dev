using System;

namespace Game.UI.MenuHandling {
	/// <summary>
	///     The interface has to be implemented by all classes representing a menu.
	///     So every menu in the game needs a C# script implementing this interface and its methods.
	///     <example>
	///         Here is a prefab for the implementation of the methods:
	///         <code>
	/// 	[Obsolete(IMenu.EnableObsoleteMessage, true)]
	///  public void Enable() {
	/// 		// Your code here
	///  }
	/// 
	/// 	[Obsolete(IMenu.DisableObsoleteMessage, true)]
	///  public void Disable() {
	/// 		// Your code here
	///  }
	///  </code>
	///     </example>
	/// </summary>
	public interface IMenu {
		protected const string EnableObsoleteMessage  = "Do not enable the menu here. Use 'MenuHandler.EnableMenu(this) instead.";
		protected const string DisableObsoleteMessage = "Do not disable the menu here. Use 'MenuHandler.DisableMenu() instead.";

		[Obsolete(EnableObsoleteMessage, false)]
		public void Enable();

		[Obsolete(DisableObsoleteMessage, false)]
		public void Disable();
	}
}
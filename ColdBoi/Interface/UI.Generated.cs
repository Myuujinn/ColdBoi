using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ColdBoi.Interface
{
	partial class UI: VerticalStackPanel
	{
		private void BuildUI()
		{
			menuItemOpen = new MenuItem();
			menuItemOpen.Text = "Open ROM...";
			menuItemOpen.Id = "menuItemOpen";

			var menuSeparator1 = new MenuSeparator();

			menuItemQuit = new MenuItem();
			menuItemQuit.Text = "Quit";
			menuItemQuit.Id = "menuItemQuit";

			menuItemFile = new MenuItem();
			menuItemFile.Text = "File";
			menuItemFile.Id = "menuItemFile";
			menuItemFile.Items.Add(menuItemOpen);
			menuItemFile.Items.Add(menuSeparator1);
			menuItemFile.Items.Add(menuItemQuit);

			_mainMenu = new HorizontalMenu();
			_mainMenu.VerticalAlignment = VerticalAlignment.Stretch;
			_mainMenu.AcceptsKeyboardFocus = true;
			_mainMenu.Id = "_mainMenu";
			_mainMenu.Items.Add(menuItemFile);

			
			Proportions.Add(new Proportion
			{
				Type = ProportionType.Auto,
			});
			Proportions.Add(new Proportion
			{
				Type = ProportionType.Auto,
			});
			Proportions.Add(new Proportion
			{
				Type = ProportionType.Fill,
			});
			AcceptsKeyboardFocus = false;
			Widgets.Add(_mainMenu);
		}

		
		public MenuItem menuItemOpen;
		public MenuItem menuItemQuit;
		public MenuItem menuItemFile;
		public HorizontalMenu _mainMenu;

		public int MenuHeight => this._mainMenu.ActualBounds.Height - 4; // off by 4 error in myra?
	}
}
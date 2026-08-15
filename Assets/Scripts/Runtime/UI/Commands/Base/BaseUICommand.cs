namespace Runtime.UI.Commands
{
	public abstract class BaseUICommand
	{
		public abstract void Execute(BaseUICommandArgs args);
		public abstract void Undo();
	}
}
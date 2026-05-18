namespace TrainingApp.SessionScripts
{
	public interface IClientDataProvider
	{
		public T LoadData<T>();
		
		public void SaveData<T>(T data);
	}
}
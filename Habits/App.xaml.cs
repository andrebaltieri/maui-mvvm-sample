namespace Habits;

public partial class App : Application
{
	public App()
	{
		//DependencyService.Register<IMessageService, MessageService>();
		//DependencyService.Register<GoalsViewModel>();
        InitializeComponent();

		MainPage = new AppShell();
	}
}

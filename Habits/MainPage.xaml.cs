using System.Collections.ObjectModel;

namespace Habits;

public record Habit(Guid Id, string Title, int Goal, int Current);

public interface IMessageService
{
    Task<string> ShowAsync();
}

public class MessageService : IMessageService
{
    public async Task<string> ShowAsync()
    {
        return await App.Current.MainPage.DisplayPromptAsync("What habit do you want to create or track?", "Describe a goal, a lifestyle, something you want to study or master or anything else you want to track."); ;
    }
}

public class GoalsViewModel
{
    private readonly IMessageService _messageService;
    public ObservableCollection<Habit> Habits { get; set; } = new();

    public Command AddCommand { get; set; }

    public GoalsViewModel(IMessageService messageService)
    {
        _messageService = messageService;
        AddCommand = new Command(OnAddTapped);
    }

    private async void OnAddTapped(object obj)
    {
        var result = await _messageService.ShowAsync();
        var habit = new Habit(Guid.NewGuid(), result, 10, 0);
        Habits.Add(habit);
    }
}

public partial class MainPage : ContentPage
{
    public List<Habit> HabitList { get; set; } = new();

    public MainPage(GoalsViewModel goalsViewModel)
    {
        InitializeComponent();
        BindingContext = goalsViewModel;
    }

    private async void OnCreateGoal(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("What habit do you want to create or track?", "Describe a goal, a lifestyle, something you want to study or master or anything else you want to track.");
        HabitList.Add(new Habit(Guid.NewGuid(), result, 10, 0));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}


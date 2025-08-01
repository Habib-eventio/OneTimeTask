namespace CamcoTasks.Infrastructure.ViewModel.Shared;

public interface IComboBoxItem<T>
{
    T Id { get; }

    string Text { get; }
}
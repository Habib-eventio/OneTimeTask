namespace CamcoTasks.Infrastructure.ViewModel.Shared;

public class ComboBoxModel<T> : IComboBoxItem<T>
{
    public T Id { get; set; }

    public string Text { get; set; }
}
namespace Assets.Scripts.Signals
{
    public class SelectUISignal
    {
        public bool IsSelected {  get; }
        public SelectUISignal(bool isSelected)
        {
            IsSelected = isSelected;
        }
    }
}

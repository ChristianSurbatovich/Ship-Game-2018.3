namespace ShipGame.UI
{
    public interface IChatBox : IUIObject
    {
        void AddChatMessage(string feed);

        void Cancel();
    }
}


namespace ShipGame.Network
{
    public interface INetActionHandler
    {

        void HandleAction(byte[] action);
    }
}


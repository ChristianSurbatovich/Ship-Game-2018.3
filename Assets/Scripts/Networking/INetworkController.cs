// interface for gameobjects to be manipulated by a network game event
namespace ShipGame.Network
{
    public interface INetworkController
    {

        void Action(GameMessage message);
        void SetName(string n);
        void SetID(short id);
        short GetID();
    }
}


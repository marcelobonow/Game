using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
    private JoinRoomDelegate JoinRoomCallback; 

    [SerializeField]
    private Text roomNameText;

    private MatchInfoSnapshot match;

    public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
    {
        match = _match;
        JoinRoomCallback = _joinRoomCallback;
        roomNameText.text = match.name + " (" + match.currentSize.ToString() + "/" + match.maxSize.ToString() + ")";
    }
    public void JoinRoom()
    {
        JoinRoomCallback.Invoke(match);
    }
}

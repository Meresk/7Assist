namespace LiveKit.API.Interfaces
{
    public interface ILiveKitService
    {
        string CreateLiveKitJWT(string roomName, string participantName);
        void VerifyWebhookEvent(string authHeader, string body);
    }
}

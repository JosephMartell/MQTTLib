namespace MQTTLib.Protocol {
    public enum ConnectReturnCode : byte {
        Accepted = 0,
        UnsupportedProtocol = 1,
        IdentifierRejected = 2,
        ServerUnavailable = 3,
        AuthenticationFailed = 4,
        NotAuthorized = 5
    }
}

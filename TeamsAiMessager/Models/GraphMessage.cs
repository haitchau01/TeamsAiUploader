public class GraphMessage
{
    public string body { get; set; }
    public string createdDateTime { get; set; }
    public string fromUserId { get; set; }
    public List<Attachment> attachments { get; set; }

    public class Attachment
    {
        public string contentType { get; set; }
        public string contentUrl { get; set; }
        public string name { get; set; }
    }
}
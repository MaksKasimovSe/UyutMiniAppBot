namespace UyutMiniApp.Service.Helpers
{
    public class EnvironmentHelper
    {
        public static string WebRootPath { get; set; }
        public static string AttachmentPath => Path.Combine(WebRootPath, "images");
        public static string ReceiptsPath => Path.Combine(WebRootPath, "receipts");
        public static string FilePath => "images";
    }
}

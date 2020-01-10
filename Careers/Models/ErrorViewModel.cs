namespace Careers.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int Code { get; set; }
        public string Message { get; set; }
        public string ReturnArea { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }
    }
}

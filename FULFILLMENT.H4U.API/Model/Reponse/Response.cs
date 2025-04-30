namespace FULFILLMENT.H4U.API.Model.Reponse
{
    public class Response
    {
        public bool status { get; set; }
        public string? message { get; set; }
        public string? inner_exception { get; set; }
        public string? response_time { get; set; }
        public object? output_data { get; set; }
    }
}

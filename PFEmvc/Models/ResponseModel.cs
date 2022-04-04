using PFEmvc.Models.Enums;

namespace PFEmvc.Models
{
    public class ResponseModel
    {
        public ResponseModel(ResponseCode responseCode,string responseMessage, object dataSet)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            DataSet = dataSet;

        }
        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object DataSet { get; set; }
    }
}

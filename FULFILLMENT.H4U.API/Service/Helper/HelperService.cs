using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Reponse;
using System.Globalization;

namespace FULFILLMENT.H4U.API.Service.Helper
{
    public class HelperService
    {
        readonly DataContext _dbContext = new();

        public string GetUserId(int userId)
        {
            string userName = "";

            try
            {
                var getData = _dbContext.SysUsers.Select(x => new { x.UserId, x.UserName }).Where(x => x.UserId == Convert.ToInt32(userId)).FirstOrDefault();

                if (getData != null)
                {
                    userName = getData.UserName;
                }

            }
            catch (Exception)
            {
                userName = "";
            }

            return userName;
        }

        public DateTime GetDateTimeNow()
        {
            DateTime dt = DateTime.Now;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            return dt;
        }

        public string GetYear()
        {
            DateTime date1 = DateTime.Now;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            Console.WriteLine(date1.Year);

            string result = "";

            result = Convert.ToString(date1.Year);

            return result;
        }

        public string GetMonth()
        {
            string result = "";

            result = DateTime.Now.ToString("MM");

            return result;
        }

        public string GetDay()
        {
            string result = "";

            result = DateTime.Now.ToString("dd");

            return result;
        }

        public async Task<string> GenerateDocumentNo(string typeDocument, int vendorId)
        {
            string getYear = GetYear();
            string getMonth = GetMonth();
            string getDay = GetDay();

            string separate = "-";

            string result = "";
            string running = "";

            int checkRunning = 0;

            if (!string.IsNullOrEmpty(typeDocument))
            {
                checkRunning = await Task.Run(() => _dbContext.SysDocuments.Where(x =>
                    x.Tpye == typeDocument &&
                    x.VendorId == vendorId &&
                    x.DocumentYear == getYear &&
                    x.DocumentMonth == getMonth &&
                    x.DocumentDay == getDay).Count());

                checkRunning++;

                if (Convert.ToString(checkRunning).Length == 1)
                {
                    running = "00" + checkRunning;
                }
                if (Convert.ToString(checkRunning).Length == 2)
                {
                    running = "0" + checkRunning;
                }


                if (typeDocument == Constants.DOCUMENT_TYPE_INBOUND)
                {
                    result = Constants.DOCUMENT_TYPE_INBOUND + separate + vendorId + separate + getYear + getMonth + getDay + separate + running;

                }
                else if (typeDocument == Constants.DOCUMENT_TYPE_PURCHASE_ORDER)
                {
                    result = Constants.DOCUMENT_TYPE_PURCHASE_ORDER + separate + vendorId + separate + getYear + getMonth + getDay + separate + running;
                }
                else if (typeDocument == Constants.DOCUMENT_TYPE_GOOD_RECEIVE)
                {
                    result = Constants.DOCUMENT_TYPE_GOOD_RECEIVE + separate + vendorId + separate + getYear + getMonth + getDay + separate + running;
                }
                else if (typeDocument == Constants.DOCUMENT_TYPE_LOT)
                {
                    result = Constants.DOCUMENT_TYPE_LOT + separate + vendorId + separate + getYear + getMonth + getDay + separate + running;
                }
                else if (typeDocument == Constants.DOCUMENT_TYPE_PICK_LIST)
                {
                    result = Constants.DOCUMENT_TYPE_PICK_LIST + separate + vendorId + separate + getYear + getMonth + getDay + separate + running;
                }
                else if (typeDocument == Constants.DOCUMENT_TYPE_PACKING)
                {
                    result = Constants.DOCUMENT_TYPE_PACKING + separate + vendorId + separate + getYear + getMonth + getDay + separate + running;
                }
            }

            return result;
        }

        public async Task<string> GenerateLotNo(string typeDocument, int lot, int productId, int vendorId)
        {
            string getYear = GetYear();
            string getMonth = GetMonth();
            string getDay = GetDay();

            string separate = "-";

            string result = "";
            string running = "";

            if (!string.IsNullOrEmpty(typeDocument))
            {

                if (Convert.ToString(lot).Length == 1)
                {
                    running = "0" + lot;
                }
                if (Convert.ToString(lot).Length == 2)
                {
                    running = Convert.ToString(lot);
                }


                if (typeDocument == Constants.DOCUMENT_TYPE_LOT)
                {
                    result = "(" + running + ")" + Constants.DOCUMENT_TYPE_LOT + separate + productId + separate + vendorId + separate + getYear + getMonth + getDay;

                }
            }

            return result;
        }

        public async Task<string> GenerateTrackingNo()
        {
            string getYear = GetYear();
            string getMonth = GetMonth();
            string getDay = GetDay();

            string separate = "-";

            string result = "";
            string running = "";


            result = "H4U" + getYear + getMonth + getDay + Guid.NewGuid().ToString().Substring(0, 10).ToUpper().Replace("-", "");


            return result;
        }


        public async Task<string> convertMonthNoToMonthName(int monthNo)
        {
            string result = string.Empty;

            switch (monthNo)
            {
                case 1:
                    result = "มกราคม";
                    break;
                case 2:
                    result = "กุมภาพันธ์";
                    break;
                case 3:
                    result = "มีนาคม";
                    break;
                case 4:
                    result = "เมษายน";
                    break;
                case 5:
                    result = "พฤษภาคม";
                    break;
                case 6:
                    result = "มิถุนายน";
                    break;
                case 7:
                    result = "กรกฏาคม";
                    break;
                case 8:
                    result = "สิงหาคม";
                    break;
                case 9:
                    result = "กันยายน";
                    break;
                case 10:
                    result = "ตุลาคม";
                    break;
                case 11:
                    result = "พฤศจิกายน";
                    break;
                case 12:
                    result = "ธันวาคม";
                    break;
            }

            return result;

        }


    }
}

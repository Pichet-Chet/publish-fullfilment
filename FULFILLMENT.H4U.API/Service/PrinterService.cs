using FULFILLMENT.H4U.API.Model;
using FULFILLMENT.H4U.API.Model.Constants;
using FULFILLMENT.H4U.API.Model.Custom;
using FULFILLMENT.H4U.API.Model.Reponse;
using FULFILLMENT.H4U.API.Service.Helper;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;

namespace FULFILLMENT.H4U.API.Service
{
    public class PrinterService
    {
        HelperService helper = new HelperService();

        readonly DataContext _dbContext = new();

        public async Task<Response> GET_ALL(FilterModel param)
        {
            Response resp = new Response();

            try
            {
                List<string> printers = new List<string>();

                // Getting the list of printers is easy with Standard .Net Framework.
                System.Drawing.Printing.PrinterSettings.StringCollection printersx = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
                foreach (string printer in printersx)
                {
                    // Extra checks can be placed here. For example: 
                    // if(printer.Contains("CX-7000") == false)
                    //		continue;
                    printers.Add(printer);
                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.message = ex.Message;
                resp.inner_exception = ex.InnerException == null ? "" : ex.InnerException.ToString();
            }

            return resp;
        }


       
    }
}
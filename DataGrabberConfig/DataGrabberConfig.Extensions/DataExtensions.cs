using DataGrabberConfig.Services.Common;
using DataGrabberConfig.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGrabberConfig.Utility.Extensions
{
    public static class DataExtensions
    {

        // Format to DbResponse
        public static IDbResponse FormatToDbResponse<T>(this T obj) where T : IDbResponse, new()
        {
            IDbResponse response = new DbResponse();

            try
            {
                if (obj != null)
                {
                    response = new DbResponse()
                    {
                        IsSuccess = obj.IsSuccess,
                        Message = obj.Message
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //LogWriter log = new LogWriter("Exception in FormatToDbResponse in DataHelper; Message:" + ex.Message);
            }
            finally
            {

            }
            return response;
        }


        // For empty list and empty string, will assign "NA" as Text
        public static void GetSetList<T>(this List<T> obj) where T : Dropdown, new()
        {
            try
            {
                if (obj.Count == 0 || string.IsNullOrEmpty(obj.FirstOrDefault()?.Text))
                {
                    T text = new T()
                    {
                        Text = "NA"
                    };

                    obj.Clear();
                    obj.Add(text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //LogWriter log = new LogWriter("Exception in GetSetList in DataHelper; Message:" + ex.Message);
            }
            finally
            {

            }
        }


        // Set Default Yes or No option for dropdown
        public static void SetDefaultDropdownValue<T>(this List<T> obj, params string[] additionalOptions) where T : Dropdown, new()
        {
            try
            {
                obj.Clear();

                obj.Add(new T() { Value = "yes", Text = "Yes" });
                obj.Add(new T() { Value = "no", Text = "No" });

                foreach (string option in additionalOptions)
                {
                    obj.Add(new T() { Value = option.ToLower(), Text = option });
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //LogWriter log = new LogWriter("Exception in SetDefaultDropdownValue in DataHelper; Message:" + ex.Message);
            }
            finally
            {

            }
        }



    }
}

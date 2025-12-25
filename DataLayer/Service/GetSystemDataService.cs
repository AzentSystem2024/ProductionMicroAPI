using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using System.Globalization;

namespace MicroApi.DataLayer.Service
{
    public class GetSystemDataService:IGetSystemDataService
    {
        public GetSystemDataResponse GetSystemInfo()
        {
            GetSystemDataResponse res = new GetSystemDataResponse();

            try
            {
                DateTime now = DateTime.Now;

                CultureInfo culture = CultureInfo.CurrentCulture;
                RegionInfo region = new RegionInfo(culture.Name);
                NumberFormatInfo numberFormat = culture.NumberFormat;
                DateTimeFormatInfo dateFormat = culture.DateTimeFormat;

                res.Data = new GetSystemData
                {
                    SYSTEMDATETIME = now,
                    SYSTEM_NAME = Environment.MachineName,
                    CULTURE = culture.Name,

                    DATE_INFO = new SystemDateInfo
                    {
                        DATE = now.ToString(dateFormat.ShortDatePattern),
                        SHORT_DATE_FORMAT = dateFormat.ShortDatePattern,
                        LONG_DATE_FORMAT = dateFormat.LongDatePattern,
                        FIRST_DAY_OF_WEEK = dateFormat.FirstDayOfWeek.ToString()
                    },

                    TIME_INFO = new SystemTimeInfo
                    {
                        TIME = now.ToString(dateFormat.LongTimePattern),
                        SHORT_TIME_FORMAT = dateFormat.ShortTimePattern,
                        LONG_TIME_FORMAT = dateFormat.LongTimePattern
                    },

                    NUMBER_INFO = new SystemNumberInfo
                    {
                        DECIMAL_SYMBOL = numberFormat.NumberDecimalSeparator,
                        DECIMAL_SEPARATOR = numberFormat.NumberDecimalSeparator,
                        THOUSAND_SEPARATOR = numberFormat.NumberGroupSeparator,
                        DECIMAL_DIGITS = numberFormat.NumberDecimalDigits,
                        DIGIT_GROUPING = string.Join(",", numberFormat.NumberGroupSizes),
                        LIST_SEPARATOR = culture.TextInfo.ListSeparator,
                        NEGATIVE_SIGN_SYMBOL = numberFormat.NegativeSign,
                        NEGATIVE_NO_FORMAT = numberFormat.NumberNegativePattern
                    },

                    CURRENCY_INFO = new SystemCurrencyInfo
                    {
                        CURRENCY_CODE = region.ISOCurrencySymbol,
                        CURRENCY_SYMBOL = region.CurrencySymbol,
                        CURRENCY_DECIMAL_DIGITS = numberFormat.CurrencyDecimalDigits,
                        POSITIVE_CURRENCY_FORMAT = numberFormat.CurrencyPositivePattern,
                        NEGATIVE_CURRENCY_FORMAT = numberFormat.CurrencyNegativePattern
                    }
                };

                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }


    }
}

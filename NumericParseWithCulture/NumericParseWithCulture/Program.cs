using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericParseWithCulture
{
    class Program
    {
        const long ticks_local = 635827489367497911;
        const long ticks_UTC = 635827489367497911;	

        static void Main(string[] args)
        {

            {
                DateTime dt_local_1 = new DateTime(ticks_local,DateTimeKind.Local);
                DateTime dt_local_1_1 = dt_local_1.ToLocalTime();
                string CultureTime1 = dt_local_1_1.ToString("G"/*, CultureInfo*/);

                DateTime dt_UTC = new DateTime(ticks_local, DateTimeKind.Utc);
                string CultureTime2 = dt_UTC.ToString("G",CultureInfo.InvariantCulture);
                string CultureTime2_1 = dt_UTC.ToString("G", CultureInfo.InvariantCulture);

            }

            {
                DateTime dt_local = DateTime.Now;
                string str_Local = dt_local.ToString("G", CultureInfo.InvariantCulture);
                DateTime dt_UTC = DateTime.UtcNow;
                string str_UTC = dt_UTC.ToString("G", CultureInfo.InvariantCulture);
                string str_UTC_he = dt_UTC.ToString("G", new CultureInfo("he"));

                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("he");
                {
                    string CultureTime = dt_UTC.ToString("G", CultureInfo.InvariantCulture);
                    const char directionalCharacter = '\x200E';
                    int blankIndex = CultureTime.IndexOf(" ");
                    string date = CultureTime.Substring(0, blankIndex);
                    string time = CultureTime.Substring(blankIndex + 1);

                    date = string.Concat(date, directionalCharacter);
                    date = string.Concat(date, " ");
                    date = string.Concat(date, directionalCharacter);
                    CultureTime = date + time;
                }

                DateTime dt_UTC_2;
                DateTime.TryParseExact(str_UTC, "G", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out dt_UTC_2);
                long preConvert = dt_UTC_2.Ticks;
                long postConvert = (long)(Int64)preConvert;

                DateTime.TryParse(str_UTC, out dt_UTC_2);
                DateTime dt_UTC_3 = DateTime.SpecifyKind(dt_UTC_2, DateTimeKind.Utc);

                long ticks_local = dt_local.Ticks;
                long ticks_UTC = dt_UTC.Ticks;




                DateTime dt_local_2 = new DateTime(ticks_UTC);
                DateTime dt_local_3 = new DateTime(ticks_UTC, DateTimeKind.Utc);

            }

            {
                double d = 12312432434.12312312;
                DateTime dt = DateTime.UtcNow;

                string LocalTime_CurrentCulture = dt.ToLocalTime().ToString();
                long ticks_CurrentCulture = dt.Ticks;
                long ticks_localTime_CurrentCulture = dt.ToLocalTime().Ticks;

                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                string LocalTime_InvariantCulture = dt.ToLocalTime().ToString();

                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("de");
                string LocalTime_DeCulture = dt.ToLocalTime().ToString();




                var backupCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;


                string d_str_1 = d.ToString();
                string d_str_1_0 = d.ToString(CultureInfo.InvariantCulture);

                string dt_str_1 = dt.ToString(new CultureInfo("ar"));
                string dt_str_1_2 = dt.ToString(CultureInfo.InvariantCulture);

                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

                string d_str_2 = d.ToString();
                string dt_str_2 = dt.ToString();
                string dt_str_2_2 = dt.ToString();

                var str1 = Convert.ChangeType(d, typeof(string), CultureInfo.InvariantCulture);
                var str2 = Convert.ChangeType(d, typeof(string), new CultureInfo("ar"));
                var str3 = Convert.ChangeType(d, typeof(string), new CultureInfo("ru"));
                var str4 = Convert.ChangeType(d, typeof(string), new CultureInfo("es"));
                var str5 = Convert.ChangeType(d, typeof(string), new CultureInfo("de"));
                var str6 = Convert.ChangeType(d, typeof(string), new CultureInfo("zh-cn"));
            }

            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ru");

                DateTime dt1 = DateTime.UtcNow;
                string dt1_str = dt1.ToString();
                string dt1_str_Invariant = dt1.ToString(CultureInfo.InvariantCulture);
                string dt1_str_pt = dt1.ToString(new CultureInfo("pt"));

                DateTime dt1_str_dt = DateTime.Parse(dt1_str, CultureInfo.InvariantCulture);
                DateTime dt1_str_Invariant_dt = DateTime.Parse(dt1_str_Invariant, CultureInfo.InvariantCulture);

            }

        }



    }

    public enum myEnum
    {
        Name1 = 1,
        Name2 = 1,
    }
}

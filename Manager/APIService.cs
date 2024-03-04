using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Hunarmis.Models;

namespace Hunarmis.Manager
{
    public class APIServices
    {
        public static Hunar_DBEntities db = new Hunar_DBEntities();

        #region AUTORIZATION
        //CARE INDIA //https://careindia.surveycto.com/index.html
        public static string GetUserName()
        {
            return "sunkumar@careindia.org";
        }
        public static string GETPassword()
        {
            return "Sun@Kumar";
        }
        //https://carebtsp.surveycto.com/
        public static string GetBTSPUserName()
        {
            //return "";
            return "";
        }
        public static string GETBTSPPassword()
        {
            // return "";
            return "";
        }
        private static long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
        public static string GetMAXDate(string para)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand comd = new SqlCommand("sp_GetMAXDate", connection);
            comd.CommandType = CommandType.StoredProcedure;
            comd.Parameters.AddWithValue("@para", para);
            string date = comd.ExecuteScalar().ToString();
            return date;
        }
        

        #endregion



        #region Bihar, Jharkhand, UP 
        //public static string GET_IPEL_AWC_Monitoring()
        //{
        //    try
        //    {
        //        WebRequest req;
        //        string maxdate = GetMAXDate("1");
        //        if (maxdate != "")
        //        {
        //            long timestamp = ConvertToTimestamp(Convert.ToDateTime(maxdate));
        //            req = WebRequest.Create(@"https://careindia.surveycto.com/api/v1/forms/data/wide/json/IPEL_AWC_MONITORING_V1?date=" + timestamp);//?date=" + timestamp//?date=" + timestamp
        //        }
        //        else
        //        {
        //            req = WebRequest.Create(@"https://careindia.surveycto.com/api/v1/forms/data/wide/json/IPEL_AWC_MONITORING_V1");
        //        }
        //        req.Method = "GET";
        //        req.Credentials = new System.Net.NetworkCredential(GetUserName(), GETPassword());
        //        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

        //        var dataStream = resp.GetResponseStream();
        //        StreamReader reader = new StreamReader(dataStream);
        //        string JsonData = reader.ReadToEnd();

        //        List<OMS_AWC_Monitoring> model = (List<OMS_AWC_Monitoring>)JsonConvert.DeserializeObject(JsonData, typeof(List<OMS_AWC_Monitoring>));
        //        if (model != null)
        //        {
        //            foreach (var m in model)
        //            {
        //                OMS_AWC_Monitoring tbl = new OMS_AWC_Monitoring();
        //                if (!db.OMS_AWC_Monitoring.Where(x => x.KEY == m.KEY).Any())
        //                {
        //                    tbl.ID = Guid.NewGuid();

        //                    tbl.SubmissionDate = m.SubmissionDate;
        //                    tbl.starttime = m.starttime;
        //                    tbl.endtime = m.endtime;
        //                    tbl.text_audit = m.text_audit;
        //                    tbl.audio_audit = m.audio_audit;
        //                    tbl.Total_duration = m.Total_duration;
        //                    tbl.Q101 = m.Q101;
        //                    tbl.Q102 = m.Q102;
        //                    tbl.Q103 = m.Q103;
        //                    tbl.Q104 = m.Q104;
        //                    tbl.Q105 = m.Q105;
        //                    tbl.Q106 = m.Q106;
        //                    tbl.Q106_SP = m.Q106_SP;
        //                    tbl.Q107 = m.Q107;
        //                    tbl.Q107_SP = m.Q107_SP;
        //                    tbl.Q109 = m.Q109;
        //                    tbl.Q110 = m.Q110;
        //                    tbl.Q110A = m.Q110A;
        //                    tbl.Q110A_SP = m.Q110A_SP;
        //                    tbl.B1 = m.B1;
        //                    tbl.B2 = m.B2;
        //                    tbl.B3 = m.B3;
        //                    tbl.B3_A = m.B3_A;
        //                    tbl.B3_B = m.B3_B;
        //                    tbl.B3_C = m.B3_C;
        //                    tbl.B3_D = m.B3_D;
        //                    tbl.B3_88 = m.B3_88;
        //                    tbl.B3_sp = m.B3_sp;
        //                    tbl.B4 = m.B4;
        //                    tbl.B4_sp = m.B4_sp;
        //                    tbl.F1 = m.F1;
        //                    tbl.F2 = m.F2;
        //                    tbl.F3 = m.F3;
        //                    tbl.F4 = m.F4;
        //                    tbl.F5 = m.F5;
        //                    tbl.F5_A = m.F5_A;
        //                    tbl.F5_B = m.F5_B;
        //                    tbl.F5_C = m.F5_C;
        //                    tbl.F5_D = m.F5_D;
        //                    tbl.F5_E = m.F5_E;
        //                    tbl.F5_F = m.F5_F;
        //                    tbl.F5_99 = m.F5_99;
        //                    tbl.F5_88 = m.F5_88;
        //                    tbl.F5_sp = m.F5_sp;
        //                    tbl.F6 = m.F6;
        //                    tbl.F7 = m.F7;
        //                    tbl.F8 = m.F8;
        //                    tbl.F9 = m.F9;
        //                    tbl.F10 = m.F10;
        //                    tbl.O1 = m.O1;
        //                    tbl.O2 = m.O2;
        //                    tbl.O2_A = m.O2_A;
        //                    tbl.O2_B = m.O2_B;
        //                    tbl.O2_C = m.O2_C;
        //                    tbl.O2_D = m.O2_D;
        //                    tbl.O2_E = m.O2_E;
        //                    tbl.O2_F = m.O2_F;
        //                    tbl.O2_88 = m.O2_88;
        //                    tbl.O2A = m.O2A;
        //                    tbl.O2A_A = m.O2A_A;
        //                    tbl.O2A_B = m.O2A_B;
        //                    tbl.O2A_C = m.O2A_C;
        //                    tbl.O2A_D = m.O2A_D;
        //                    tbl.O2A_E = m.O2A_E;
        //                    tbl.O2A_F = m.O2A_F;
        //                    tbl.O2A_G = m.O2A_G;
        //                    tbl.O2A_H = m.O2A_H;
        //                    tbl.O2A_I = m.O2A_I;
        //                    tbl.O2A_J = m.O2A_J;
        //                    tbl.O2A_K = m.O2A_K;
        //                    tbl.O2A_L = m.O2A_L;
        //                    tbl.O2A_M = m.O2A_M;
        //                    tbl.O2A_N = m.O2A_N;
        //                    tbl.O2A_O = m.O2A_O;
        //                    tbl.O2A_P = m.O2A_P;
        //                    tbl.O2A_Q = m.O2A_Q;
        //                    tbl.O2A_R = m.O2A_R;
        //                    tbl.O2A_S = m.O2A_S;
        //                    tbl.O2A_T = m.O2A_T;
        //                    tbl.O2A_U = m.O2A_U;
        //                    tbl.O2A_V = m.O2A_V;
        //                    tbl.O2A_W = m.O2A_W;
        //                    tbl.O2A_X = m.O2A_X;
        //                    tbl.O2A_Y = m.O2A_Y;
        //                    tbl.O2A_Z = m.O2A_Z;
        //                    tbl.O2A_AA = m.O2A_AA;
        //                    tbl.O2A_88 = m.O2A_88;
        //                    tbl.O2A_sp = m.O2A_sp;
        //                    tbl.O3 = m.O3;
        //                    tbl.O4 = m.O4;
        //                    tbl.O5 = m.O5;
        //                    tbl.O6 = m.O6;
        //                    tbl.O7 = m.O7;
        //                    tbl.O8 = m.O8;
        //                    tbl.O8A = m.O8A;
        //                    tbl.O8B = m.O8B;
        //                    tbl.O8C = m.O8C;
        //                    tbl.O8D = m.O8D;
        //                    tbl.O9 = m.O9;
        //                    tbl.O9_A = m.O9_A;
        //                    tbl.O9_B = m.O9_B;
        //                    tbl.O9_C = m.O9_C;
        //                    tbl.O9_88 = m.O9_88;
        //                    tbl.O9_sp = m.O9_sp;
        //                    tbl.O10 = m.O10;
        //                    tbl.O11 = m.O11;
        //                    tbl.O11_A = m.O11_A;
        //                    tbl.O11_B = m.O11_B;
        //                    tbl.O11_C = m.O11_C;
        //                    tbl.O11_D = m.O11_D;
        //                    tbl.O11_99 = m.O11_99;
        //                    tbl.O11A_A = m.O11A_A;
        //                    tbl.O11A_B = m.O11A_B;
        //                    tbl.O11A_C = m.O11A_C;
        //                    tbl.O11A_D = m.O11A_D;
        //                    tbl.O11B = m.O11B;
        //                    tbl.O11B_A = m.O11B_A;
        //                    tbl.O11B_B = m.O11B_B;
        //                    tbl.O11B_C = m.O11B_C;
        //                    tbl.O11B_D = m.O11B_D;
        //                    tbl.O11B_99 = m.O11B_99;
        //                    tbl.O12 = m.O12;
        //                    tbl.O12A = m.O12A;
        //                    tbl.O12B = m.O12B;
        //                    tbl.O12C = m.O12C;
        //                    tbl.O12D = m.O12D;
        //                    tbl.O12E = m.O12E;
        //                    tbl.O12F = m.O12F;
        //                    tbl.O12G = m.O12G;
        //                    tbl.O12_88 = m.O12_88;
        //                    tbl.O12_88_sp = m.O12_88_sp;
        //                    tbl.O14 = m.O14;
        //                    tbl.O15 = m.O15;
        //                    tbl.O16 = m.O16;
        //                    tbl.O17 = m.O17;
        //                    tbl.O18 = m.O18;
        //                    tbl.AO19 = m.AO19;
        //                    tbl.O19 = m.O19;
        //                    tbl.O20 = m.O20;
        //                    tbl.O21 = m.O21;
        //                    tbl.O22 = m.O22;
        //                    tbl.O23 = m.O23;
        //                    tbl.O24 = m.O24;
        //                    tbl.O25 = m.O25;
        //                    tbl.O26 = m.O26;
        //                    tbl.O27 = m.O27;
        //                    tbl.O28 = m.O28;
        //                    tbl.O29 = m.O29;
        //                    tbl.O30 = m.O30;
        //                    tbl.O31 = m.O31;
        //                    tbl.O32A_M = m.O32A_M;
        //                    tbl.O32A_F = m.O32A_F;
        //                    tbl.O32B_M = m.O32B_M;
        //                    tbl.O32B_F = m.O32B_F;
        //                    tbl.O33 = m.O33;
        //                    tbl.KEY = m.KEY;
        //                    tbl.CreatedOn = m.CreatedOn;

        //                    tbl.IsActive = true;
        //                    db.OMS_AWC_Monitoring.Add(tbl);
        //                    db.SaveChanges();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string er = ex.Message;
        //        return "Error Outcome Monitoring AWC Monitoring";
        //    }
        //    return "Success Outcome Monitoring AWC Monitoring";
        //}
       
        #endregion
    }
}
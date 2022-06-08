using System;
using DataLib;
using Ope = DataLib.OperationSql;
using GSql = DataLib.GlobalSql;
using Val = BusLib.Validation.BOValidation;
 
namespace BusLib.Configuration
{    
    public class BOConfiguration
    {        
        public enum EnumServer
        {
            ACC = 0 
        }

        public enum EnumSqlTran
        {
            Start = 0,
            Continue = 1,
            Stop = 2,
            None = 4
        }
      
        public static bool ReadUserSetting()
        {
            if (Ope.ReadUserSetting() == true)
            {                
                Ope.GStrEndUserPass = Ope.GStrEndUserPass.Replace("amp", "&");

                Ope.GStrEndUserPass =  Ope.ENCODE_DECODE(Ope.GStrEndUserPass, "D");
                 
                return true;
            }
            return false;
        }
   

        public static string EndUserId
        {
            get
            {
                return Ope.GStrEndUserId;
            }
        }

        public static string EndUserPass
        {
            get
            {
                return Ope.GStrEndUserPass;
            }
        }
        public static string GetEncPassWord(string StrPassWord, string EncType)
        {
            return Ope.ENCODE_DECODE(StrPassWord, EncType);
        }

    }
}


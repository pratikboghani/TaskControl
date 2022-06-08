using System;
using System.Collections.Generic;
using System.Text;

namespace BusLib.Table
{
    public class clsLogin
    {
        private string _UserId;
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId
        {            
            get { return _UserId; }
            set { _UserId = value; }
        }

        private string _Password;
        /// <summary>
        /// User Password
        /// </summary>        
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _Cat;
        /// <summary>
        /// User Category
        /// </summary>
        public string Cat
        {
            get { return _Cat; }
            set { _Cat = value; }
        }	
    }
}
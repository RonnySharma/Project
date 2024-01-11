using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_utility
{
    public static class SD
    {
        //covertype storedProcedure 
        public const string proc_getcovertypes = "getCoverTypes";
        public const string proc_getcovertype = "getCoverType";
        public const string proc_CreateCoverType = "covertytre";
        public const string proc_updateCoverType = "updateCoverType";
        public const string proc_DeleteCoverTypes = "DeleteCoverTypes";
        //rolles
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
        public const string Role_Company = "company";
        public const string Role_Individual = "Individual";
        //see
        public const string Ss_cartSessionCount = "CartCountSession";
        //orderhe
        public const string orderStatusPending = "Pending";
        public const string orderStatusApproved = "Approved";
        public const string orderStatusInProgress = "InProgress";
        public const string orderStatusshipped = "shipped";
        public const string orderStatusRefunded = "Refunded";
        //pay
        public const string PeymentStatusPending = "Pending";
        public const string PeymentStatusApproved = "Approved";
        public const string PeymentStatusDelayPayment = "DelayPayment";
        public const string PeymentStatusRejected = "Rejected";

        public static double GetPriceBasedOnQuantity(double Quantity, double Price, double Price50, double Price100)
        {
            if (Quantity < 50)
            {
                return Price;
            }
            else if (Quantity < 100)
            {
                return Price50;
            }
            else
            {
                return Price100;
            }
        }
        public static string ConvertToRawHtml(string source)
        {
            char[]array=new char[source.Length];
            int arrayIndex = 0;
            bool inside=false;
            for(int i=0;i<source.Length;i++)
            {
                char last = source[i];
                if(last=='<')
                {
                    inside=true;
                    continue;
                }
                if(last=='>')
                {
                    inside = false;
                    continue;
                }
                if(!inside)
                {
                    array[arrayIndex] = last;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);

        }
    }
}

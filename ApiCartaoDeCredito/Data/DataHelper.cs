using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCartaoDeCredito.Data
{
    public class DataHelper
    {
        /// <summary>
        /// Validates email
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generates a new valid credit card object
        /// </summary>
        /// <param name="ccFormat">Credit card number format</param>
        /// <param name="expYears">Amount of years before expiring</param>
        /// <param name="cvvLength">Length of CVV security code (3 or 4)</param>
        /// <returns>CreditCard object without CreditCardId (Identity DB column)</returns>
        public static CreditCard GenerateCreditCard(int personId, string ccFormat = "4260 55XX XXXX XXXX", int expYears = 5, int cvvLength = 3)
        {
            Random rnd = new();
            ccFormat = ccFormat.Trim().Replace(" ", "");
            string cc = "";

            for (int j = 0; j < (ccFormat.Length - 1); j++)
            {
                if (ccFormat[j].ToString().ToUpper() == "X")
                    cc += (rnd.Next(0, 10));
                else
                    cc += (ccFormat[j]);
            }

            //Luhn algorithm - Checksum
            int sum = 0, position = 0, digit;
            for (int j = cc.Length; j > 0; j--)
            {
                digit = int.Parse(cc.Substring(j - 1, 1));
                if (position % 2 == 0)
                    digit *= 2;
                if (digit > 9)
                    digit -= 9;
                sum += digit;
                position++;
            }
            int checkDigit = (sum * 9) % 10;
            string ccNumber = cc + checkDigit.ToString();

            //Security code
            string cvv;
            if (cvvLength == 3)
                cvv = rnd.Next(100, 999).ToString();
            else
                cvv = rnd.Next(1000, 9999).ToString();

            //Expiry date
            DateTime now = DateTime.Now;
            string expMonth = now.Month.ToString();
            if (expMonth.Length == 1)
                expMonth = "0" + expMonth;
            string expYear = (now.Year + expYears).ToString();
            string expDate = expMonth + "/" + expYear.Substring(2, 2); //format: MM/YY

            return new CreditCard
            {
                PersonId = personId,
                IsActive = false, //Credit card must be activated later by the user
                DateOfCreation = now,
                Number = ccNumber,
                CVV = cvv,
                ExpiryDate = expDate
            };
        }

    }
}

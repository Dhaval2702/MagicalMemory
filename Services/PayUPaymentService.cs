using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;
using WebApi.Models.Payment;

namespace WebApi.Services
{
    public interface IPayUPaymentService
    {
        PayUPaymentResponse getUserPaymentDetails(int accountId, string paymentYear);

        bool UpdateChildPaymentDetaisForSucess(int accountId, Guid childId, string paymentYear);
    }
    public class PayUPaymentService : IPayUPaymentService
    {
        private readonly PayUPaymentSetting _payUPaymentSetting;
        private readonly IAccountService _accountService;
        private readonly IChildService _childService;
        public PayUPaymentService(IAccountService accountService, IChildService childService, IOptions<PayUPaymentSetting> payUPaymentSetting)
        {
            _accountService = accountService;
            _childService = childService;
            _payUPaymentSetting = payUPaymentSetting.Value;
        }


        public PayUPaymentResponse getUserPaymentDetails(int accountId, string paymentYear)
        {

            var accountdetails = _accountService.GetById(accountId);
            string hash1 = string.Empty;
            string[] hashVarsSeq;
            string hash_string = string.Empty;

            PayUPaymentResponse payUPaymentResponse = new PayUPaymentResponse();
            payUPaymentResponse.Amount = _payUPaymentSetting.Amount;
            payUPaymentResponse.FirstName = accountdetails.FatherFullName;
            payUPaymentResponse.Email = accountdetails.Email;
            payUPaymentResponse.PhoneNumber = accountdetails.MobileNumber;
            payUPaymentResponse.ProductInfo = _payUPaymentSetting.ProductInfo;
            payUPaymentResponse.SuccessURL = _payUPaymentSetting.SuccessUrl + "/" + paymentYear;
            payUPaymentResponse.FailureUrl = _payUPaymentSetting.FailureUrl;

            if (string.IsNullOrEmpty(payUPaymentResponse.TransectionId)) // generating txnid
            {
                Random rnd = new Random();
                string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                payUPaymentResponse.TransectionId = strHash.ToString().Substring(0, 20);

            }

            if (string.IsNullOrEmpty(payUPaymentResponse.HashString)) // generating hash value
            {
                if (
                    string.IsNullOrEmpty(_payUPaymentSetting.MerchantKey) ||
                    string.IsNullOrEmpty(payUPaymentResponse.TransectionId) ||
                    string.IsNullOrEmpty(payUPaymentResponse.Amount) ||
                    string.IsNullOrEmpty(payUPaymentResponse.FirstName) ||
                    string.IsNullOrEmpty(payUPaymentResponse.Email) ||
                    string.IsNullOrEmpty(payUPaymentResponse.PhoneNumber) ||
                    string.IsNullOrEmpty(payUPaymentResponse.ProductInfo) ||
                    string.IsNullOrEmpty(payUPaymentResponse.SuccessURL) ||
                    string.IsNullOrEmpty(payUPaymentResponse.FailureUrl)
                    )
                {
                    //Some Validation Message with the details that we want here.
                }

                else
                {
                    hashVarsSeq = _payUPaymentSetting.hashSequence.Split('|'); // spliting hash sequence from config
                    hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + _payUPaymentSetting.MerchantKey;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "txnid")
                        {
                            hash_string = hash_string + payUPaymentResponse.TransectionId;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "amount")
                        {
                            hash_string = hash_string + Convert.ToDecimal(payUPaymentResponse.Amount).ToString("g29");
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "productinfo")
                        {
                            hash_string = hash_string + payUPaymentResponse.ProductInfo;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "firstname")
                        {
                            hash_string = hash_string + payUPaymentResponse.FirstName;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "email")
                        {
                            hash_string = hash_string + payUPaymentResponse.Email;
                            hash_string = hash_string + '|';
                        }

                        else
                        {

                            hash_string = hash_string + (hash_var != null ? hash_var : "");// isset if else
                            hash_string = hash_string + '|';
                        }
                    }

                    hash_string += _payUPaymentSetting.Salt;// appending SALT

                    hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                    payUPaymentResponse.PayUTransectionURl = _payUPaymentSetting.PayuBaseUrl + "/_payment";// setting URL
                    payUPaymentResponse.HashString = hash1;

                }
            }
            return payUPaymentResponse;
        }



        public bool UpdateChildPaymentDetaisForSucess(int accountId, Guid childId, string paymentYear)
        {
            return _childService.UpdateChildPaymentHistory(accountId, childId, paymentYear);

        }

        /// <summary>
        /// Generate HASH for encrypt all parameter passing while transaction
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += string.Format("{0:x2}", x);
            }
            return hex;

        }


    }
}

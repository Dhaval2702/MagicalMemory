using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public static class ValidationMessages
    {
        public static class Messages
        {
            public const string RegistrationSuccessful = "Hey Mom, We registered ourselves successfully.They are sending confirmation link to our email id.Lets confirm our email quickly.";
            public const string VerifyEmail = "Thank you Mom.Verification successful, you can now login";
            public const string ForgotPassword = "Please check your email for password reset instructions";
            public const string ResetPassword = "Password reset successful, Mom !! you can now login";
            public const string LoginFailed = "Oh no Mom, something went wrong. I think ID or Password or both which you are entering are wrong.";
            public const string EmailConfirmationFailed = "Oh no, email confirmation failed.";
            public const string NewChildrenAddConfirmation = "Hey Mom,You have added me successfully.";
        }
    }

}

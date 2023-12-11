using KPFS.Web.Dtos;
using KPFS.Web.Resources;
using RazorEngineCore;

namespace KPFS.Web.Helpers
{
    public static class EmailContentHelper
    {
        public static async Task<EmailMessageContentDto> GetUserLoginOptEmailContent(string otp)
        {
            IRazorEngineCompiledTemplate compiledTemplate = await RazorEngineCompiledTemplate.LoadFromFileAsync(GetCompiledTemplateFilePath(nameof(EmailContentResource.LoginOtpEmailBody)));
            string result = compiledTemplate.Run(new
            {
                LoginOtp = otp
            });

            return new EmailMessageContentDto()
            {
                Subject = EmailContentResource.LoginOtpEmailSubject,
                Body = result
            };
        }

        public static async Task<EmailMessageContentDto> GetUserEmailConfirmationEmailContent(string confirmEmailLink)
        {
            IRazorEngineCompiledTemplate compiledTemplate = await RazorEngineCompiledTemplate.LoadFromFileAsync(GetCompiledTemplateFilePath(nameof(EmailContentResource.UserEmailConfirmationEmailBody)));
            string result = compiledTemplate.Run(new
            {
                ConfirmEmailLink = confirmEmailLink
            });

            return new EmailMessageContentDto()
            {
                Subject = EmailContentResource.UserEmailConfirmationEmailSubject,
                Body = result
            };
        }

        private static string GetCompiledTemplateFilePath(string resourceKey) => $"CompiledEmailTemplates\\{resourceKey}.dll";
    }
}

using KPFS.Api.Resources;
using KPFS.Web.Dtos;
using RazorEngineCore;

namespace KPFS.Web.Helpers
{
    public static class EmailContentHelper
    {
        public static async Task<EmailMessageContentDto> GetUserLoginOptEmailContentAsync(string otp)
        {
            return await GetEmailContentInternalAsync(nameof(EmailContentResource.LoginOtpEmailBody), new
            {
                LoginOtp = otp
            });
        }

        public static async Task<EmailMessageContentDto> GetUserEmailConfirmationEmailContentAsync(string confirmEmailLink)
        {
            return await GetEmailContentInternalAsync(nameof(EmailContentResource.UserEmailConfirmationEmailBody), new
            {
                ConfirmEmailLink = confirmEmailLink
            });
        }

        private static async Task<EmailMessageContentDto> GetEmailContentInternalAsync(string resourceKey, object model)
        {
            IRazorEngineCompiledTemplate compiledTemplate = await RazorEngineCompiledTemplate.LoadFromFileAsync(GetCompiledTemplateFilePath(resourceKey));
            string result = compiledTemplate.Run(model);

            return new EmailMessageContentDto()
            {
                Subject = EmailContentResource.UserEmailConfirmationEmailSubject,
                Body = result
            };
        }

        private static string GetCompiledTemplateFilePath(string resourceKey) => $"CompiledEmailTemplates\\{resourceKey}.dll";
    }
}

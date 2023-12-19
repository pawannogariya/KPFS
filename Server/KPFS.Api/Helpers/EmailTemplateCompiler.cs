using KPFS.Api.Resources;
using RazorEngineCore;

namespace KPFS.Web.Helpers
{
    public static class EmailTemplateCompiler
    {
        private static string[] EmailBodyTemplateName = new string[]
        {
            nameof(EmailContentResource.LoginOtpEmailBody),
            nameof(EmailContentResource.UserEmailConfirmationEmailBody)
        };

        public static void CompileEmailTemplates()
        {
            IRazorEngine razorEngine = new RazorEngine();

            foreach(var templateName in EmailBodyTemplateName) 
            {
                var templateString = EmailContentResource.ResourceManager.GetString(templateName);

                IRazorEngineCompiledTemplate template = razorEngine.Compile(templateString);
                template.SaveToFile($"CompiledEmailTemplates\\{templateName}.dll");
            }
        }
    }
}

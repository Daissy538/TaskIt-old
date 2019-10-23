using System.Diagnostics;
using System.IO;

namespace TaskItApi.Helper
{
    public class ResourcesHelper: IResourcesHelper
    {
        private readonly string baseDirectory;
        private string resourceDirectory;
        private string emailDirectory;
        private string emailInviteTemplate;

        public ResourcesHelper()
        {
            baseDirectory = Directory.GetCurrentDirectory();
        }

        public string GetInviteEmailTemplatePath()
        {
            if (string.IsNullOrEmpty(emailDirectory))
            {
                SetEmailDirectory();
            }

            if (string.IsNullOrEmpty(emailInviteTemplate))
            {
                emailInviteTemplate = Path.Combine(emailDirectory, "GroupInvitation.html");
            }

            return emailInviteTemplate;
        }

        private void SetEmailDirectory()
        {      
            if(string.IsNullOrEmpty(this.resourceDirectory))
            {
                SetResourceDirectory();
            }

            this.emailDirectory = Path.Combine(resourceDirectory, "EmailTemplates");
        }

        private void SetResourceDirectory()
        {
            this.resourceDirectory = Path.Combine(baseDirectory, "Resources");
        }
    }
}


using System;
using System.Collections.Generic;
using Aspose.Finance.UI.Models;

namespace Aspose.Finance.UI.Models
{
    public class FAQPageModel
    {
        public List<FAQItem> List { get; set; }

        public ViewModel Parent;
        private FlexibleResources Resources => Parent.Resources;

        public FAQPageModel(ViewModel parent)
        {
            Parent = parent;
            var extension = Parent.Extension?.ToUpper();
            var extension2 = Parent.Extension2?.ToUpper();
			if (string.IsNullOrEmpty(extension) || Parent.IsCanonical)
			{
				extension = Parent.Product.ToUpper();
			}
			else if ("acroform".Equals(extension, StringComparison.InvariantCultureIgnoreCase))
			{
				extension = "Xfa PDF";
				extension2 = "Normal PDF (AcroForm)";
			}
			else if (!string.IsNullOrEmpty(extension2) &&
			         (extension2.Equals("pdfa1a", StringComparison.InvariantCultureIgnoreCase) ||
			          extension2.Equals("pdfa1b", StringComparison.InvariantCultureIgnoreCase) ||
			          extension2.Equals("pdfa2a", StringComparison.InvariantCultureIgnoreCase) ||
			          extension2.Equals("pdfa3a", StringComparison.InvariantCultureIgnoreCase)))
			{
				extension2 = Parent.DesktopAppNameByExtension(extension2);
			}
            //if (string.IsNullOrEmpty(extension2))
            //    extension2 = extension;

            List = new List<FAQItem>();
            var faqQuestion = Parent.Product + "FAQQuestion" + Parent.AppName + "Feature";
            var faqAnswer = Parent.Product + "FAQAnswer" + Parent.AppName + "Feature";
            if (!Parent.IsCanonical && !string.IsNullOrEmpty(extension2))
            {
	            faqQuestion = Parent.Product + "FAQQuestion" + Parent.AppName + "Feature2";
	            faqAnswer = Parent.Product + "FAQAnswer" + Parent.AppName + "Feature2";
            }
            var i = 1;
            while (Resources.ContainsKey(faqQuestion + i))
            {
	            var faqQuestionRes = Resources[faqQuestion + i];
	            var faqAnswerRes = Resources[faqAnswer + i];
                List.Add(new FAQItem() {
	                Question = string.Format(faqQuestionRes, extension, extension2),
					AcceptedAnswer = string.Format(faqAnswerRes, extension, extension2)
                });
                i++;
            }
        }
    }

    public class FAQItem
    {
	    public string Question { get; set; }
	    public string AcceptedAnswer { get; set; }
    }
}

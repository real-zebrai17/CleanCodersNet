using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCoders
{
    public class CodecastDetailsUseCase
    {
        public PresentableCodeCastDetails RequestCodecastDetails(User loggerInUser, string permalink)
        {
            var codecast = Context.CodecastGateway.FindCodecastByPermalink(permalink);
            return new PresentableCodeCastDetails
            {
                title = codecast.Title,
                publicationDate = codecast.PublicationDate.ToString("M/d/yyyy", CultureInfo.InvariantCulture)
            };
        }
    }
}

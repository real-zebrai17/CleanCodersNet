using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCoders.Specs.TestDoubles
{
    public class InMemoryCodecastGateway : GatewayUtilities<Codecast>,
        ICodecastGateway
    {
        public List<Codecast> FindAllCodecastsSortedChronologically()
        {
            return Entities.OrderBy(c => c.PublicationDate)
                             .ToList();
        }

        public Codecast FindCodecastByPermalink(string permalink)
        {
            return Entities.SingleOrDefault(c => c.Permalink == permalink);
        }

        public Codecast FindCodecastByTitle(string codeCastTitle)
        {
            return Entities.SingleOrDefault(c => c.Title == codeCastTitle);
        }
    }
}
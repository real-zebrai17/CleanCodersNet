using System.Collections.Generic;

namespace CleanCoders
{
    public interface ICodecastGateway
    {
        Codecast FindCodecastByTitle(string codeCastTitle);
        List<Codecast> FindAllCodecastsSortedChronologically();
        void Delete(Codecast cc);
        Codecast Save(Codecast codecast);
    }
}
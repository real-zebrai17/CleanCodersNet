﻿using CleanCoders.Entities;
using System.Collections.Generic;

namespace CleanCoders.Gateways
{
    public interface ICodecastGateway
    {
        Codecast FindCodecastByTitle(string codeCastTitle);
        Codecast FindCodecastByPermalink(string permalink);
        List<Codecast> FindAllCodecastsSortedChronologically();
        void Delete(Codecast cc);
        Codecast Save(Codecast codecast);

    }
}
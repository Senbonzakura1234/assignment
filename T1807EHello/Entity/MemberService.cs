﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace T1807EHello.Entity
{
    internal interface IMemberService
    {
        string Login(string email, string password);

        Member Register(Member member);

        Member GetInformation(string token);

        StorageFile CreateTokenFile(string token);

        string ReadTokenFile(string fileName);

    }
}

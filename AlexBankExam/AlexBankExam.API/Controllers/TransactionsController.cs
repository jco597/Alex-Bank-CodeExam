﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AlexBankExam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetAccounts()
        {
            return new string[] { "value1", "value2" };
        }
    }
}

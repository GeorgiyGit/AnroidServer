﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Help_elements
{
	public class JwtOptions
	{
		public string Issuer { get; set; }
		public int Lifetime { get; set; }
		public string Key { get; set; }
	}
}

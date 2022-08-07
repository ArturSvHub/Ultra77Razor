using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;

using UpakUtilitiesLibrary.Utility.Extentions;

namespace UpakUtilitiesLibrary.Services
{
	public class CartService
	{
		public int CountOfProducts { get; set; }
	}
}

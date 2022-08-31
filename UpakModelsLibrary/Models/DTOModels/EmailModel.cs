using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpakModelsLibrary.Models.DTOModels
{
	public class EmailModel
	{
		public string? From { get; set; }
		public string? FromTitle { get; set; }
		public string? FromPhone { get; set; }
		public string? To { get; set; }
		public string? ToTitle { get; set; }
		public string? Subject { get; set; }
		public string? EmailTextBody { get; set; }
	}
}

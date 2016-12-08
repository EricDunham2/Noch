using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NochWeb.Models
{
	public class ChannelModel
	{
		public int ChannelID { get; set; }
		public int DomainID { get; set; }
		public string Name { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}
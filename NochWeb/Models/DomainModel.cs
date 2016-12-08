using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NochWeb.Models
{
	public class DomainModel
	{
		public int DomainID { get; set; }
		public string Name { get; set; }
		public DateTime UpdatedOn { get; set; }
		public virtual ICollection<ChannelModel> Channels { get; set; }
	}
}
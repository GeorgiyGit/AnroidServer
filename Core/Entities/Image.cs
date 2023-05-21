using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public class Image:Monitoring
	{
		public int Id { get; set; }
		public string Path { get; set; }
		public string FullName { get; set; }
		public string Name { get; set; }

		public User? User { get; set; }

		public Category? Category { get; set; }
		public int? CategoryId { get; set; }
	}
}

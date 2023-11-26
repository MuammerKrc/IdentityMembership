using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityStructureModel.ViewModels
{
	public class RoleAssignViewModel
	{
		public string UserId { get; set; }
		public string UserName { get; set; }
		public List<RoleExistViewModel> RoleExistViewModels { get; set; } = new List<RoleExistViewModel>();

	}
	public class RoleExistViewModel
	{
		public long RoleId { get; set; }
		public string RoleName { get; set; }
		public bool Exist { get; set; }
	}
}

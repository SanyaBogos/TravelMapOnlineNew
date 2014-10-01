using System.ComponentModel.DataAnnotations;

namespace TravelMap.Models
{
	[MetadataType(typeof(UserProfileMetaData))]
	public partial class UserProfile
	{
	}

	public class UserProfileMetaData
	{
		[DisplayFormat(NullDisplayText = "-Phone is not defined-")]
		public string Phone { get; set; }
	}
}
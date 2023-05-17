using System.Text.RegularExpressions;

namespace Common.Helpers;
public partial class RegexHelper
{
	public static readonly Regex IsInteger = GetIsIntegerRegex();

	[GeneratedRegex("^[0-9]*$")]
	private static partial Regex GetIsIntegerRegex();
}
namespace CollectionManager.Models {
	public class Ordinal {
		/// <summary>
		/// 序数
		/// 数値で表せない場合は<see cref="Alternative"/>を参照すること。
		/// </summary>
		public double Number {
			get;
			set;
		} = double.NaN;

		/// <summary>
		/// 数値で表せない場合の置換文字列
		/// </summary>
		public string Alternative {
			get;
			set;
		}
	}
}

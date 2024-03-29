using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SandBeige.MediaBox.Controls.Converters {
	/// <summary>
	/// Bool値を反転させるコンバーター
	/// </summary>
	public class BoolReverseConverter : IValueConverter {

		/// <summary>
		/// コンバート
		/// </summary>
		/// <param name="value">変換前値(<see cref="bool"/>)</param>
		/// <param name="targetType">未使用</param>
		/// <param name="parameter">未使用</param>
		/// <param name="culture">未使用</param>
		/// <returns>変換後値(<see cref="bool"/>)</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is bool b) {
				return !b;
			}
			throw new InvalidOperationException();
		}

		/// <summary>
		/// コンバートバック
		/// </summary>
		/// <param name="value">変換前値(<see cref="Visibility"/></param>
		/// <param name="targetType">未使用</param>
		/// <param name="parameter">未使用</param>
		/// <param name="culture">未使用</param>
		/// <returns>変換後値(<see cref="bool"/>)</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is bool b) {
				return !b;
			}
			throw new InvalidOperationException();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace CollectionManager.Composition.Enums {
	/// <summary>
	/// 表示列
	/// </summary>
	public enum AvailableColumns {
		/// <summary>
		/// タイトル
		/// </summary>
		Title,
		/// <summary>
		/// タイトル読み
		/// </summary>
		TitleYomi,
		/// <summary>
		/// 最小値
		/// </summary>
		Min,
		/// <summary>
		/// 最大値
		/// </summary>
		Max,
		/// <summary>
		/// 作者
		/// </summary>
		Authors,
		/// <summary>
		/// メモ
		/// </summary>
		Note,
		/// <summary>
		/// 次回リリース日
		/// </summary>
		NextReleaseDate,
		/// <summary>
		/// 済
		/// </summary>
		Completed
	}

	/// <summary>
	/// AvailableColumns→stringコンバーター
	/// </summary>
	public class AvailableColumnsToStringConverter : IValueConverter {
		/// <summary>
		/// 変換
		/// </summary>
		/// <param name="value"><see cref="AvailableColumns"/></param>
		/// <param name="targetType">未使用</param>
		/// <param name="parameter">未使用</param>
		/// <param name="culture">未使用</param>
		/// <returns><see cref="AvailableColumns"/>をstringに変換したもの</returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if (!(value is AvailableColumns ac)) {
				return DependencyProperty.UnsetValue;
			}

			var dict = new Dictionary<AvailableColumns, string> {
				{ AvailableColumns.Title, "タイトル" },
				{ AvailableColumns.TitleYomi, "タイトル読み" },
				{ AvailableColumns.Min, "最小値" },
				{ AvailableColumns.Max, "最大値" },
				{ AvailableColumns.Authors, "作者" },
				{ AvailableColumns.Note, "メモ" },
				{ AvailableColumns.NextReleaseDate, "次回リリース日" },
				{ AvailableColumns.Completed, "済" }
			};

			if (dict.TryGetValue(ac, out var val)) {
				return val;
			}

			return DependencyProperty.UnsetValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
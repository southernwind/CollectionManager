using System;
using System.Collections.Generic;

namespace CollectionManager.DataBase.Tables {
	public class ItemSet {

		public int ItemSetId {
			get;
			set;
		}

		public string DirectoryPath {
			get;
			set;
		}

		public string TitleYomi {
			get;
			set;
		}

		public ICollection<ItemSetAuthor> Authors {
			get;
			set;
		}

		public string Note {
			get;
			set;
		}

		public double? Min {
			get;
			set;
		}

		public double? Max {
			get;
			set;
		}

		public string OrdinalRegex {
			get;
			set;
		}

		public bool Completed {
			get;
			set;
		}

		public DateTime? NextReleaseDate {
			get;
			set;
		}
	}
}

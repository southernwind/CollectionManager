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

		public ICollection<ItemSetAuthor> Authors {
			get;
			set;
		}

		public string Note {
			get;
			set;
		}

		public virtual ICollection<Item> Items {
			get;
			set;
		}
	}
}

namespace CollectionManager.DataBase.Tables {
	public class ItemSetAuthor {
		public int ItemSetId {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public string NameYomi {
			get;
			set;
		}

		public virtual ItemSet ItemSet {
			get;
			set;
		}
	}
}

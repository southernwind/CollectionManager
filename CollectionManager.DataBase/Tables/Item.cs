namespace CollectionManager.DataBase.Tables {
	public class Item {
		public int ItemSetId {
			get;
			set;
		}

		public string FilePath {
			get;
			set;
		}

		public double OrdinalNumber {
			get;
			set;
		}

		public string OrdinalAlternative {
			get;
			set;
		}

		public virtual ItemSet ItemSet {
			get;
			set;
		}
	}
}

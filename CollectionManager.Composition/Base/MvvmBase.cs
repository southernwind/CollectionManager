using System;
using System.Reactive.Disposables;

using Prism.Mvvm;

namespace CollectionManager.Composition.Base {
	public abstract class ModelBaseCore : BindableBase, IDisposable {
		private CompositeDisposable _compositeDisposable;
		protected CompositeDisposable CompositeDisposable {
			get {
				return this._compositeDisposable ??= new CompositeDisposable();
			}
		}

		private protected ModelBaseCore() {
		}

		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="disposing">マネージドリソースの破棄を行うかどうか</param>
		protected virtual void Dispose(bool disposing) {
			this._compositeDisposable?.Dispose();
		}
	}
}

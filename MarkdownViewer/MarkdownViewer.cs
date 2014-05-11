using System;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects.Text;
using Xwt;
using Xwt.Drawing;
using MonoDevelop.Core;

namespace MarkdownViewer
{
	public class MarkdownViewer:AbstractBaseViewContent, IAttachableViewContent, IViewContent
	{
		ScrollView scrollView;
		MarkdownView markdownControl;
		Document doc;

		public MarkdownViewer (Document doc)
		{
			this.doc = doc;
		}

		public override Gtk.Widget Control {
			get {
				if (scrollView == null) {
					markdownControl = new MarkdownView ();
					try {
						((Gtk.TextView)Toolkit.CurrentEngine.GetNativeWidget (markdownControl)).CursorVisible = false;
					} catch {
						//Just in case if Xwt does some internal refactoring
					}
					markdownControl.Font = markdownControl.Font.WithSize (11);
					markdownControl.Margin = 30;
					var frame = new FrameBox (markdownControl);
					frame.MinWidth = 800;
					frame.WidthRequest = 800;
					frame.HorizontalPlacement = WidgetPlacement.Center;
					frame.VerticalPlacement = WidgetPlacement.Center;
					frame.BorderColor = Colors.LightGray;
					frame.BorderWidth = new WidgetSpacing (1, 1, 1, 1);
					frame.BackgroundColor = Colors.White;
					frame.Margin = 10;
					scrollView = new ScrollView (frame);
				}
				return (Gtk.Widget)Toolkit.CurrentEngine.GetNativeWidget (scrollView);
			}
		}

		void IAttachableViewContent.Selected ()
		{
			markdownControl.Markdown = doc.GetContent<ITextFile> ().Text;
		}

		void IAttachableViewContent.Deselected ()
		{
		}

		void IAttachableViewContent.BeforeSave ()
		{
		}

		void IAttachableViewContent.BaseContentChanged ()
		{
		}

		event EventHandler IViewContent.BeforeSave { add { } remove { }
		}

		event EventHandler IViewContent.ContentChanged { add { } remove { }
		}

		event EventHandler IViewContent.ContentNameChanged { add { } remove { }
		}

		event EventHandler IViewContent.DirtyChanged { add { } remove { }
		}

		void IViewContent.Load (string fileName)
		{
		}

		void IViewContent.LoadNew (System.IO.Stream content, string mimeType)
		{
		}

		void IViewContent.Save (string fileName)
		{
		}

		void IViewContent.Save ()
		{
		}

		void IViewContent.DiscardChanges ()
		{
		}

		MonoDevelop.Projects.Project IViewContent.Project {
			get {
				return null;
			}
			set {
			}
		}

		string IViewContent.PathRelativeToProject {
			get {
				return "";
			}
		}

		string IViewContent.ContentName {
			get {
				return TabPageLabel;
			}
			set { }
		}

		string IViewContent.UntitledName {
			get {
				return "";
			}
			set { }
		}

		string IViewContent.StockIconId {
			get {
				return null;
			}
		}

		bool IViewContent.IsUntitled {
			get {
				return false;
			}
		}

		bool IViewContent.IsViewOnly {
			get {
				return true;
			}
		}

		bool IViewContent.IsFile {
			get {
				return false;
			}
		}

		bool IViewContent.IsDirty {
			get {
				return false;
			}
			set { }
		}

		bool IViewContent.IsReadOnly {
			get {
				return true;
			}
		}

		public override string TabPageLabel {
			get {
				return GettextCatalog.GetString ("Markdown view");
			}
		}
	}
}


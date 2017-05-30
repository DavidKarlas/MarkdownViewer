using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects.Text;
using Xwt;
using Xwt.Drawing;

namespace MarkdownViewer
{
    public class MarkdownViewer: AbstractXwtViewContent
	{
		ScrollView scrollView;

        MarkdownView markdownControl;

        readonly Document doc;

        public MarkdownViewer (Document doc)
        {
            this.doc = doc;
            ContentName = "Markdown";
        }

        public override Widget Widget {
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
				return scrollView;
			}
		}

        public override string StockIconId {
			get {
				return null;
			}
		}

        public override bool IsViewOnly {
			get {
				return true;
			}
		}

        public override bool IsFile {
			get {
				return false;
			}
		}

        public override bool IsReadOnly {
			get {
				return true;
			}
		}
     
		public override string TabPageLabel {
			get {
				return GettextCatalog.GetString (ContentName);
			}
		}

        protected override void OnSelected ()
        {
        	markdownControl.Markdown = doc.GetContent<ITextFile> ().Text;
        }
	}
}


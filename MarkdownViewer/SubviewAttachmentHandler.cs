using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace MarkdownViewer
{
	class SubviewAttachmentHandler : CommandHandler
	{
		protected override void Run ()
		{
			IdeApp.Workbench.ActiveDocumentChanged += HandleDocumentChanged;
		}

		static void HandleDocumentChanged (object sender, EventArgs e)
		{
			var document = IdeApp.Workbench.ActiveDocument;
			if (document == null || !document.IsFile || document.Window.FindView<MarkdownViewer> () >= 0)
				return;
			var extension = document.FileName.Extension.ToLower ();
			if (extension == ".md" || extension == ".markdown") {
				document.Window.AttachViewContent (new MarkdownViewer (document));
			}
		}
	}
}
